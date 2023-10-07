using PLCTool.UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TsComm;

namespace PLCTool.Forms
{
    public partial class FormColourDifference : BaseForm
    {
        static readonly Dictionary<ColorSpace, string[]> ColorSpaceItems = new Dictionary<ColorSpace, string[]>
        {
            {ColorSpace.CIELAB, new []{"L*", "a*", "b*" } },
            {ColorSpace.CIEXYZ, new []{"X", "Y", "Z" } },
            {ColorSpace.CIEYxy, new []{"Y", "x", "y" } },
            {ColorSpace.CIELCH, new []{"L*", "C*", "h°" } },
            {ColorSpace.CIELUV, new []{"L*", "u*", "v*" } },
            {ColorSpace.HUNTERLAB, new []{"L", "a", "b" } },
            {ColorSpace.BETA_XY, new []{"β", "x", "y" } },
            {ColorSpace.DIN_LAB99, new []{"L99", "a99", "b99" } },
            {ColorSpace.SRGB, new []{"R", "G", "B" } },
        };

        Instrument instrument = new Instrument();

        private StandardRecord standard = null;
        private TrialRecord trial = null;
        private StandardIlluminant standardIlluminant = StandardIlluminant.D65;
        private StandardObserver standardObserver = StandardObserver.CIE1964;
        private ScMode scMode = ScMode.SCI;
        private ColorSpace colorSpace = ColorSpace.CIELAB;


        public FormColourDifference()
        {


            InitializeComponent();

            // 初始化标样和试样列表

            var labels = new[]
            {
                "ID",
                FormColourDifference_Name,
                FormColourDifference_DateTime,
                SpectralType,
                Illuminant,
                Observer,
                LAsterisk,
                AAsterisk,
                BAsterisk
            };
            foreach (var label in labels)
            {
                standardsListView.Columns.Add(label);
                samplesListView.Columns.Add(label);
            }
            for (int wavelength = Instrument.WaveLengthMin;
                wavelength <= Instrument.WaveLengthMax;
                wavelength += Instrument.WaveLengthInterval)
            {
                standardsListView.Columns.Add($"{wavelength}nm");
                samplesListView.Columns.Add($"{wavelength}nm");
            }


            // 初始化测量列表上下文菜单
            measureResultListContextMenuStrip.Items.Add(
                new ToolStripRadioButtonMenuItem(_(ScMode.SCI), scModeClicked, group: "ScMode") { Tag = ScMode.SCI, Checked = true });
            measureResultListContextMenuStrip.Items.Add(
                new ToolStripRadioButtonMenuItem(_(ScMode.SCE), group: "ScMode") { Tag = ScMode.SCE });

            foreach (var value in Enum.GetValues(typeof(StandardIlluminant)))
            {
                var item = new ToolStripRadioButtonMenuItem(
                    Enum.GetName(typeof(StandardIlluminant), value),
                    illuminantClicked, "StandardIlluminant")
                { Tag = value };
                if ((StandardIlluminant)value == standardIlluminant)
                    item.Checked = true;
                standardIlluminantToolStripMenuItem.DropDownItems.Add(item);
            }
            foreach (var value in Enum.GetValues(typeof(StandardObserver)))
            {
                var item = new ToolStripRadioButtonMenuItem(
                    _(Enum.GetName(typeof(StandardObserver), value)),
                    observerClicked, "StandardObserver")
                { Tag = value };
                if ((StandardObserver)value == standardObserver)
                {
                    item.Checked = true;
                }
                standardObserverToolStripMenuItem.DropDownItems.Add(item);
            }

            foreach (var value in Enum.GetValues(typeof(ColorSpace)))
            {
                // Not supported Munsell
                if ((ColorSpace)value == ColorSpace.MUNSELL)
                    continue;
                var item = new ToolStripRadioButtonMenuItem(
                    _(Enum.GetName(typeof(ColorSpace), value)),
                    onColorSpaceClicked, "ColorSpace")
                { Tag = value };
                if ((ColorSpace)item.Tag == colorSpace)
                {
                    item.Checked = true;
                }
                colorSpaceToolStripMenuItem.DropDownItems.Add(item);
            }

            foreach (var label in new[] { "Item", "Standard", "Sample", "Difference" })
            {
                measureResultListView.Columns.Add(_(label));
            }

            comboBoxConnectionMethods.SelectedIndex = 0;

            instrument.MeasurementResultReceived += OnMeasurementResultReceived;
        }

        private void OnMeasurementResultReceived(Record record)
        {
            if (record is StandardRecord std)
            {
                standard = std;
            } 
            else if (record is TrialRecord t)
            {
                trial = t;
            }

            BeginInvoke(new Action(updateMeasurementListView));

        }


        string _(string key)
        {
            ComponentResourceManager rm = new ComponentResourceManager(typeof(LanguageResource));
            return rm.GetString("FormColourDifference_" + key);
        }

        string _(Enum enumItem)
        {
            return _(enumItem.ToString());
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (instrument.IsOpen)
            {
                instrument.Close();
                buttonConnect.Text = Connect;
            }
            else
            {
                ErrorCode error;
                if (comboBoxConnectionMethods.SelectedIndex == 0) // By USB
                {
                    error = instrument.Open(null);
                }
                else
                {
                    var dlg = new FormColourDifferenceDlg();
                    if (dlg.ShowDialog(this) == DialogResult.Cancel)
                    {
                        return;
                    }
                    error = instrument.OpenByBleDongle(dlg.SelectedPortName, dlg.SelectedSlaveMac);
                    // or search instrument automatically
                    // error = instrument.OpenByBleDongle(null, null);
                }

                
                if (error == ErrorCode.NoError)
                {
                    buttonConnect.Text = Disconnect;
                    standardIndeicesComboBox.Items.Clear();
                    standardsListView.Items.Clear();
                    buttonUploadStandard.Enabled = false;
                }
                else
                {
                    MessageBox.Show(_(error.ToString()));
                }
            }
        }

        private void buttonGetDevInfo_Click(object sender, EventArgs e)
        {
            DeviceInfo info;
            var error = instrument.GetDeviceInfo(out info);
            if (error != ErrorCode.NoError)
            {
                MessageBox.Show(_(error.ToString()));
                return;
            }

            devInfoListView.Items[0].SubItems[1].Text = info.Model;
            devInfoListView.Items[1].SubItems[1].Text = info.Sn;
            devInfoListView.Items[2].SubItems[1].Text = info.Optical;
            devInfoListView.Items[3].SubItems[1].Text = info.SoftwareVersion;
            devInfoListView.Items[4].SubItems[1].Text = info.HardwareVersion;
            devInfoListView.Items[5].SubItems[1].Text = info.WhiteBoardNumber;
        }

        private void buttonGetStatus_Click(object sender, EventArgs e)
        {
            InstrumentStatus status;
            var error = instrument.GetInstrumentStatus(out status);
            if (error != ErrorCode.NoError)
            {
                MessageBox.Show(_(error.ToString()));
                return;
            }

            instrumentStatusListView.Items[0].SubItems[1].Text = _(status.Aperture.ToString());
            instrumentStatusListView.Items[1].SubItems[1].Text = _(status.ScMode.ToString());
            instrumentStatusListView.Items[2].SubItems[1].Text = _(status.IsTransitive ? "Transitive" : "Reflective");
            instrumentStatusListView.Items[3].SubItems[1].Text = _(status.UvMode.ToString());
            instrumentStatusListView.Items[4].SubItems[1].Text = _(status.Observer.ToString());
            instrumentStatusListView.Items[5].SubItems[1].Text = status.Illuminant.ToString();
            instrumentStatusListView.Items[6].SubItems[1].Text = _(status.ColorSpace.ToString());
            instrumentStatusListView.Items[7].SubItems[1].Text = _(status.ColorDiffFormula.ToString());
            instrumentStatusListView.Items[8].SubItems[1].Text = _(status.ColorIndex.ToString());
            instrumentStatusListView.Items[9].SubItems[1].Text = status.StandardCount.ToString();
            instrumentStatusListView.Items[10].SubItems[1].Text = status.TrialCount.ToString();
        }

        private void standardIndeicesComboBox_DropDown(object sender, EventArgs e)
        {
            if (standardIndeicesComboBox.Items.Count == 0)
            {
                uint total;
                var error = instrument.GetTotalStandards(out total);
                if (error != ErrorCode.NoError)
                {
                    MessageBox.Show(_(error.ToString()));
                    return;
                }

                if (total > 0)
                {
                    standardIndeicesComboBox.Items.Add(-1);
                    for (int i = 0; i < total; i++)
                        standardIndeicesComboBox.Items.Add(i);
                    buttonUploadStandard.Enabled = true;
                }
            }
        }
        private void buttonUploadStandard_Click(object sender, EventArgs e)
        {
            if (standardIndeicesComboBox.SelectedItem == null)
            {
                return;
            }

            int index = (int)standardIndeicesComboBox.SelectedItem;
            StandardRecord[] standards;
            ErrorCode error;
            if (index >= 0)
            {
                error = instrument.UploadStandards(out standards, (uint)index, 1);
            }
            else
            {
                error = instrument.UploadStandards(out standards, 0, uint.MaxValue);
            }

            if (error != ErrorCode.NoError)
            {
                MessageBox.Show(_(error.ToString()));
                return;
            }

            foreach (var standard in standards)
            {
                var item = new ListViewItem(new[]
                {
                    standard.Id.ToString(),
                    standard.Name,
                    standard.DateTime.ToString(CultureInfo.CurrentCulture),
                    standard.Flags.IsTransitive ? _("Transitive") : _("Reflective"),
                    standard.Flags.Illuminant.ToString(),
                    _(standard.Flags.Observer.ToString()),
                    standard.Sci.L.ToString("F2"),
                    standard.Sci.a.ToString("F2"),
                    standard.Sci.b.ToString("F2"),
                });
                if (standard.Flags.ColorDataType == ColorDataType.SpectalData)
                {
                    foreach (var t in standard.Sci.SpectralData)
                    {
                        item.SubItems.Add((t * 100.0).ToString("F2"));
                    }
                }

                item.Tag = standard.Id;
                standardsListView.Items.Add(item);
            }
        }
        private void onUploadSample(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            if (item == null)
                return;

            var tag = (Tuple<uint, int>)item.Tag;
            TrialRecord[] records;
            var error = instrument.UploadSamples(out records,
                tag.Item1, tag.Item2 == -1 ? 0 : (uint)tag.Item2, tag.Item2 == -1 ? uint.MaxValue : 1);

            if (tag.Item2 == -1)
            {
                samplesListView.Tag = tag.Item1;    // 将标样ID存到Tag中
                fillSamplesListView(records);
            }
            else
            {
                // 如果试样列表中有其他标样的试样，先清空列表
                if (samplesListView.Tag is uint && (uint)samplesListView.Tag != tag.Item1)
                {
                    samplesListView.Items.Clear();
                    samplesListView.Tag = tag.Item1;
                }
                addSamplesListView(records);
            }
        }

        private void standardsListViewContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (standardsListView.SelectedItems.Count > 0)
            {
                uint stdId = (uint)standardsListView.SelectedItems[0].Tag;
                uint sampleCount = 0;
                var error = instrument.GetSampleCount(stdId, out sampleCount);
                uploadSampleToolStripMenuItem.Enabled = sampleCount > 0;
                if (sampleCount > 0)
                {
                    uploadSampleToolStripMenuItem.DropDownItems.Clear();
                    var subItem = uploadSampleToolStripMenuItem.DropDownItems.Add(AllSamples, null, onUploadSample);
                    subItem.Tag = new Tuple<uint, int>(stdId, -1);
                    for (int i = 0; i < (int)sampleCount; i++)
                    {
                        subItem = uploadSampleToolStripMenuItem.DropDownItems.Add(
                            string.Format(SampleX, i), null, onUploadSample);
                        subItem.Tag = new Tuple<uint, int>(stdId, i);
                    }
                }
            }
            else
            {
                uploadSampleToolStripMenuItem.Enabled = false;
                deleteStandardMenuItem.Enabled = false;
                deleteAllStandardsMenuItem.Enabled = standardsListView.SelectedItems.Count > 0;
                clearStandardListMenuItem.Enabled = standardsListView.SelectedItems.Count > 0;
            }
        }

        private void fillSamplesListView(TrialRecord[] samples)
        {
            samplesListView.Items.Clear();
            addSamplesListView(samples);
        }

        private void addSamplesListView(TrialRecord[] samples)
        {
            foreach (var sample in samples)
            {
                var item = new ListViewItem(new[]
                {
                    sample.Id.ToString(),
                    sample.Name,
                    sample.DateTime.ToString(CultureInfo.CurrentCulture),
                    sample.Flags.IsTransitive ? _("Transitive") : _("Reflective"),
                    sample.Flags.Illuminant.ToString(),
                    _(sample.Flags.Observer.ToString()),
                    sample.Sci?.L.ToString("F2"),
                    sample.Sci?.a.ToString("F2"),
                    sample.Sci?.b.ToString("F2"),
                });

                if (sample.Sci == null)
                {
                    return;
                }

                foreach (var t in sample.Sci.SpectralData)
                {
                    item.SubItems.Add((t * 100.0).ToString("F2"));
                }

                item.Tag = sample.StandardId;
                samplesListView.Items.Add(item);
            }
        }

        private void standardsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            var item = standardsListView.GetItemAt(pos.X, pos.Y);
            if (item == null)
                return;

            var stdId = (uint)item.Tag;
            TrialRecord[] records;
            var error = instrument.UploadSamples(out records, stdId, 0, uint.MaxValue);
            if (error != ErrorCode.NoError)
            {
                MessageBox.Show(_(error.ToString()));
                return;
            }
            fillSamplesListView(records);
        }

        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            standardsListView.Items.Clear();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standardsListView.SelectedItems.Count == 0)
                return;

            uint stdId = (uint)standardsListView.SelectedItems[0].Tag;
            var error = instrument.DeleteStandard(stdId);
            if (error != ErrorCode.NoError)
            {
                MessageBox.Show(_(error.ToString()));
            }
            else
            {
                standardsListView.Items.Remove(standardsListView.SelectedItems[0]);
            }
        }

        private void deleteAllRecordsMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (standardsListView.Items.Count > 0)
            {
                var error = instrument.DeleteAllRecords();
                if (error != ErrorCode.NoError)
                {
                    MessageBox.Show(_(error.ToString()));
                }
                else
                {
                    samplesListView.Items.Clear();
                    standardsListView.Items.Clear();
                }
            }
            Cursor = Cursors.Default;
        }

        private void clearSamplesListViewMenuItem_Click(object sender, EventArgs e)
        {
            samplesListView.Items.Clear();
        }

        private void deleteAllSamplesFromInstrumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (samplesListView.Items.Count > 0)
            {
                var error = instrument.DeleteSample((uint)samplesListView.Tag, uint.MaxValue);
                if (error != ErrorCode.NoError)
                {
                    MessageBox.Show(_(error.ToString()));
                }
                else
                {
                    samplesListView.Items.Clear();
                }
            }

            Cursor = Cursors.Default;
        }

        private void samplesListViewContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            deleteAllSamplesMenuItem.Enabled = samplesListView.Items.Count > 0;
            clearSampleListMenuItem.Enabled = samplesListView.Items.Count > 0;
            deleteSelectedSampleMenuItem.Enabled = samplesListView.SelectedItems.Count > 0;
        }

        private void buttonCalibrateBlack_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var error = instrument.CalibrateBlack();
            MessageBox.Show(error != ErrorCode.NoError ? _(error.ToString()) : BlackCalibrationCompleted);
            Cursor = Cursors.Default;
        }

        private void buttonCalibrateWhite_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var error = instrument.CalibrateWhite();
            MessageBox.Show(error != ErrorCode.NoError ? _(error.ToString()) : WhiteCalibrationCompleted);
            Cursor = Cursors.Default;
        }

        private void scModeClicked(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            if (item?.Tag is ScMode)
            {
                scMode = (ScMode)item.Tag;
                updateMeasurementListView();
            }
        }

        private void illuminantClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item?.Tag is StandardIlluminant)
            {
                standardIlluminant = (StandardIlluminant)item.Tag;
                updateMeasurementListView();
            }
        }

        private void observerClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item?.Tag is StandardObserver)
            {
                standardObserver = (StandardObserver)item.Tag;
                updateMeasurementListView();
            }
        }


        private void onColorSpaceClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item?.Tag is ColorSpace)
            {
                colorSpace = (ColorSpace)item.Tag;
                updateMeasurementListView();
            }
        }

        private void updateMeasurementListView()
        {
            measureResultListView.Items.Clear();
            if (standard == null && trial == null)
            {
                return;
            }

            double[] stdValues = null;
            double[] stdSpectralData = null;
            double[] trialValues = null;
            double[] trialSpectralData = null;
            if (standard != null)
            {
                stdSpectralData = (scMode == ScMode.SCI ? standard.Sci : standard.Sce)?.SpectralData;
                if (stdSpectralData != null)
                {
                    stdValues = Instrument.GetColorDataFromSpectralData(colorSpace, stdSpectralData,
                        standardIlluminant, standardObserver);
                }
            }

            if (trial != null)
            {
                trialSpectralData = (scMode == ScMode.SCI ? trial.Sci : trial.Sce)?.SpectralData;
                if (trialSpectralData != null)
                {
                    trialValues = Instrument.GetColorDataFromSpectralData(colorSpace, trialSpectralData,
                        standardIlluminant, standardObserver);
                }
            }

            for (int i = 0; i < ColorSpaceItems[colorSpace].Length; i++)
            {
                var item = new ListViewItem();
                var label = ColorSpaceItems[colorSpace][i];
                item.Text = label;
                // item.SubItems.Add(label);
                var format = new[] { "x", "y", "β" }.Contains(label) ? "F4" : "F2";
                item.SubItems.Add(stdValues == null ? "--" : stdValues[i].ToString(format));
                item.SubItems.Add(trialValues == null ? "--" : trialValues[i].ToString(format));
                if (stdValues != null && trialValues != null)
                {
                    item.SubItems.Add((trialValues[i] - stdValues[i]).ToString(format));
                }

                measureResultListView.Items.Add(item);
            }

            for (int i = 0; i < Instrument.SpectralDataCount; i++)
            {
                var item = new ListViewItem();
                var label = $"{Instrument.WaveLengthMin + i * Instrument.WaveLengthInterval}nm";
                item.Text = label;
                item.SubItems.Add(stdSpectralData == null ? "--" : (stdSpectralData[i] * 100.0).ToString("F2"));
                item.SubItems.Add(trialSpectralData == null ? "--" : (trialSpectralData[i] * 100.0).ToString("F2"));
                if (stdSpectralData != null && trialSpectralData != null)
                {
                    item.SubItems.Add(((trialSpectralData[i] - stdSpectralData[i]) * 100.0).ToString("F2"));
                }

                measureResultListView.Items.Add(item);
            }
        }

        private void buttonMeasureStandard_Click(object sender, EventArgs e)
        {
            StandardRecord record;
            var error = instrument.MeasureStandard(out record);
            if (error != ErrorCode.NoError)
            {
                MessageBox.Show(_(error));
                return;
            }

            standard = record;
            updateMeasurementListView();
        }

        private void buttonMeasureSample_Click(object sender, EventArgs e)
        {
            TrialRecord record;
            var error = instrument.MeasureTrial(out record);
            if (error != ErrorCode.NoError)
            {
                MessageBox.Show(_(error));
                return;
            }

            trial = record;
            updateMeasurementListView();
        }

        private void buttonWriteStandard_Click(object sender, EventArgs e)
        {
            double[] sci = {
                0.773480713367462, 0.791757047176361, 0.805315136909485, 0.820970892906189, 0.825888991355896,
                0.834949731826782, 0.843686759471893, 0.852535903453827, 0.856413841247559, 0.86250901222229,
                0.868217408657074, 0.872072875499725, 0.875243484973907, 0.878312885761261, 0.881376445293427,
                0.884171009063721, 0.887442350387573, 0.887731373310089, 0.890885710716248, 0.892350614070892,
                0.896016597747803, 0.897695481777191, 0.900597453117371, 0.903980374336243, 0.904019892215729,
                0.908419251441956, 0.909656882286072, 0.911392688751221, 0.912112891674042, 0.911177635192871,
                0.920914828777313
            };
            double[] sce = {
                0.759858906269073, 0.776629984378815, 0.792130053043365, 0.808480799198151, 0.81301361322403,
                0.822815418243408, 0.831218242645264, 0.839630126953125, 0.845517933368683, 0.851781070232391,
                0.857541680335999, 0.861586153507233, 0.864967465400696, 0.86747795343399, 0.870756983757019,
                0.874444961547852, 0.877042651176453, 0.879364311695099, 0.881074845790863, 0.883933842182159,
                0.887082457542419, 0.889182150363922, 0.891600012779236, 0.89349240064621, 0.896647274494171,
                0.898745059967041, 0.90128755569458, 0.901129424571991, 0.903187215328217, 0.90252161026001,
                0.912837684154511
            };

            // 使用光谱的记录
            StandardRecord spectral_record = new StandardRecord
            {
                // 记录标识
                Flags  = new RecordFlags
                {
                    Illuminant = StandardIlluminant.D65,    // 对于颜色值是光谱的，可以任意填写
                    Observer = StandardObserver.CIE1964,    //  对于颜色值是光谱的，可以任意填写
                    Aperture = Aperture.Phi8,   // 如果没有匹配的或不知道，可以随意填
                    IsTransitive = false,       // 指示是否是透射记录
                    ColorDataType = ColorDataType.SpectalData,   // 指示Sci和Sce的颜色值是光谱数据
                    ScMode = ScMode.SCI_AND_SCE,        // 指示Sci和Sce都有效
                    UvMode = UvMode.CutNone,    // UV模式，不知道就真CutNone
                },
                DateTime = DateTime.Now, // 测试时间
                Name = "spec", // 名称
                // SCI模式下颜色数据
                Sci = new ColorData
                {
                    SpectralData = sci
                },
                // SCE模式下颜色数据
                Sce = new ColorData
                {
                    SpectralData = sce
                },
                // 容差
                Tolerance = new Tolerance
                {
                    DE = 1.0,
                    LowerDL = -1.0,
                    UpperDL = 1.0,
                    LowerDA = -1.0,
                    UpperDA = 1.0,
                    LowerDB = -1.0,
                    UpperDB = 1.0,
                }
            };

            // 使用XYZ值的记录
            StandardRecord xyz_record = new StandardRecord
            {
                Flags  = new RecordFlags
                {
                    Illuminant = StandardIlluminant.D65,    // 对于颜色值是XYZ的，必须指定值
                    Observer = StandardObserver.CIE1964,    // 对于颜色值是XYZ的，必须指定值
                    Aperture = Aperture.Phi8,   // 如果没有匹配的或不知道，可以随意填
                    IsTransitive = false,       // 指示是否是透射记录
                    ColorDataType = ColorDataType.CieLab,   // 指示Sci和Sce的颜色值是Lab
                    ScMode = ScMode.SCI_AND_SCE,        // 指示Sci和Sce都有效
                    UvMode = UvMode.CutNone,    // UV模式，不知道就真CutNone
                },
                DateTime = DateTime.Now,    
                Name = "Lab",
                Sci = new ColorData
                {
                    L = 62.21,
                    a = 62.56,
                    b = 53.32,
                },
                Sce = new ColorData
                {
                    L = 53.63,
                    a = 53.90,
                    b = 45.84
                },
                Tolerance = new Tolerance
                {
                    DE = 1.0,
                    LowerDL = -1.0,
                    UpperDL = 1.0,
                    LowerDA = -1.0,
                    UpperDA = 1.0,
                    LowerDB = -1.0,
                    UpperDB = 1.0,
                }
            };

            // 写入一个
            var err = instrument.DownloadStandards(new[] {spectral_record, xyz_record});

            if (err != ErrorCode.NoError)
            {
                MessageBox.Show(string.Format("写入记录失败：{0}", _(err)));
            }
            else
            {
                MessageBox.Show($"成功写入两个记录，名称分别是{spectral_record.Name}和{xyz_record.Name}");
            }
        }
        private void clearResultListMenuItem_Click(object sender, EventArgs e)
        {
            measureResultListView.Items.Clear();
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            FormColourDifferenceTest frm = new FormColourDifferenceTest();
            frm.ShowDialog();
        }

        #region 多语言
        private string AAsterisk => LanguageResource.FormColourDifference_AAsterisk;
        private string AllSamples => LanguageResource.FormColourDifference_AllSamples;
        private string BAsterisk => LanguageResource.FormColourDifference_BAsterisk;
        private string BlackCalibrationCompleted => LanguageResource.FormColourDifference_BlackCalibrationCompleted;       
        private string Connect => LanguageResource.FormColourDifference_Connect;       
        private string Disconnect => LanguageResource.FormColourDifference_Disconnect;       
        private string Illuminant => LanguageResource.FormColourDifference_Illuminant;       
        private string LAsterisk => LanguageResource.FormColourDifference_LAsterisk;       
        private string FormColourDifference_Name => LanguageResource.FormColourDifference_Name;
        private string FormColourDifference_DateTime => LanguageResource.FormColourDifference_DateTime;
        private string Observer => LanguageResource.FormColourDifference_Observer;       
        private string SampleX => LanguageResource.FormColourDifference_SampleX;       
        private string SpectralType => LanguageResource.FormColourDifference_SpectralType;       
        private string WhiteCalibrationCompleted => LanguageResource.FormColourDifference_WhiteCalibrationCompleted;
        #endregion

        
    }
}
