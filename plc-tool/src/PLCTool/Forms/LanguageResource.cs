using System.ComponentModel;

namespace PLCTool
{
    public static class LanguageResource
    {
        #region FormAddLogColumn
        public static string FormAddLogColumn_AddLogColumnTitle { get; set; }
        public static string FormAddLogColumn_AddressIncorrectMsg { get; set; }
        public static string FormAddLogColumn_EditLogColumnTitle { get; set; }
        public static string FormAddLogColumn_ExistNameMsg { get; set; }
        public static string FormAddLogColumn_SelectDataTypeMsg { get; set; }
        #endregion

        #region FormChangePassword
        public static string FormChangePassword_ChangePswSuccessMsg { get; set; }
        public static string FormChangePassword_NewPswEmptyMsg { get; set; }
        public static string FormChangePassword_PswIncorrectMsg { get; set; }
        public static string FormChangePassword_PswNotMatchMsg { get; set; }
        #endregion

        #region FormIOStatus
        public static string FormIOStatus_ExportSuccessMsg { get; set; }
        public static string FormIOStatus_SaveSuccessMsg { get; set; }
        #endregion

        #region FormLog
        public static string FormLog_NotFoundAlarmMsg { get; set; }
        public static string FormLog_NotFoundChangeMsg { get; set; }
        #endregion

        #region FormLogin
        public static string FormLogin_PswErrMsg { get; set; }
        #endregion

        #region FormLogSetting
        public static string FormLogSetting_DeleteColumn { get; set; }
        public static string FormLogSetting_DeleteRowMsg { get; set; }
        public static string FormLogSetting_NotSelectDeleteRowMsg { get; set; }
        public static string FormLogSetting_NotSelectEditRowMsg { get; set; }
        #endregion

        #region FormMain
        public static string FormMain_ConnectedInfo { get; set; }
        public static string FormMain_LeatherLabel22 { get; set; }
        public static string FormMain_NotConnectedInfo { get; set; }
        public static string FormMain_XinyuanLabel23 { get; set; }
        public static string FormMain_FineTensionAdjustmentRange_Add { get; set; }

        public static string FormMain_ClothOutSpeed { get; set; }
        #endregion

        #region FormMsg
        public static string FormMsg_CloseMonitorInfo { get; set; }
        public static string FormMsg_CloseMonitorMsg { get; set; }
        public static string FormMsg_FormatIncorrectMsg { get; set; }
        public static string FormMsg_NotConnectedErrMsg { get; set; }
        public static string FormMsg_OpenMonitorInfo { get; set; }
        public static string FormMsg_OpenMonitorMsg { get; set; }
        public static string FormMsg_ReceiveFormat { get; set; }
        public static string FormMsg_SendFormat { get; set; }
        public static string FormMsg_StartCounterInfo { get; set; }
        public static string FormMsg_StartReadingMsg { get; set; }
        public static string FormMsg_StopCounterInfo { get; set; }
        public static string FormMsg_StopReadingMsg { get; set; }
        public static string FormMsg_StopReadingMsg2 { get; set; }
        #endregion

        #region FormWidthData
        public static string FormWidthData_FileNameTitle { get; set; }
        public static string FormWidthData_YardLengthTitle { get; set; }
        public static string FormWidthData_WidthTitle { get; set; }
        public static string FormWidthData_NotCodeLengthOffsetMsg { get; set; }
        public static string FormWidthData_NumbersCodeLengthOffsetMsg { get; set; }
        public static string FormWidthData_NotNetWeightMsg { get; set; }
        public static string FormWidthData_NumbersNetWeightMsg { get; set; }
        #endregion

        #region FormColourDifference
        public static string FormColourDifference_AAsterisk { get; set; }
        public static string FormColourDifference_AllSamples { get; set; }       
        public static string FormColourDifference_BAsterisk { get; set; }      
        public static string FormColourDifference_BlackCalibrationCompleted { get; set; }       
        public static string FormColourDifference_Connect { get; set; }       
        public static string FormColourDifference_DateTime { get; set; }      
        public static string FormColourDifference_Illuminant { get; set; }      
        public static string FormColourDifference_LAsterisk { get; set; }       
        public static string FormColourDifference_Name { get; set; }      
        public static string FormColourDifference_Observer { get; set; }        
        public static string FormColourDifference_SpectralType { get; set; }       
        public static string FormColourDifference_WhiteCalibrationCompleted { get; set; }
        public static string FormColourDifference_Disconnect { get; set; }
        public static string FormColourDifference_SampleX { get; set; }
        public static string FormColourDifference_PleaseSelectAPortName { get; set; }
        #endregion

        static LanguageResource()
        {
            LoadResource();
        }
        public static void LoadResource()
        {
            ComponentResourceManager rm = new ComponentResourceManager(typeof(LanguageResource));
            FormAddLogColumn_AddLogColumnTitle = rm.GetString("FormAddLogColumn_AddLogColumnTitle");
            FormAddLogColumn_AddressIncorrectMsg = rm.GetString("FormAddLogColumn_AddressIncorrectMsg");
            FormAddLogColumn_EditLogColumnTitle = rm.GetString("FormAddLogColumn_EditLogColumnTitle");
            FormAddLogColumn_ExistNameMsg = rm.GetString("FormAddLogColumn_ExistNameMsg");
            FormAddLogColumn_SelectDataTypeMsg = rm.GetString("FormAddLogColumn_SelectDataTypeMsg");
            FormChangePassword_ChangePswSuccessMsg = rm.GetString("FormChangePassword_ChangePswSuccessMsg");
            FormChangePassword_NewPswEmptyMsg = rm.GetString("FormChangePassword_NewPswEmptyMsg");
            FormChangePassword_PswIncorrectMsg = rm.GetString("FormChangePassword_PswIncorrectMsg");
            FormChangePassword_PswNotMatchMsg = rm.GetString("FormChangePassword_PswNotMatchMsg");
            FormIOStatus_ExportSuccessMsg = rm.GetString("FormIOStatus_ExportSuccessMsg");
            FormIOStatus_SaveSuccessMsg = rm.GetString("FormIOStatus_SaveSuccessMsg");
            FormLog_NotFoundAlarmMsg = rm.GetString("FormLog_NotFoundAlarmMsg");
            FormLog_NotFoundChangeMsg = rm.GetString("FormLog_NotFoundChangeMsg");
            FormLogin_PswErrMsg = rm.GetString("FormLogin_PswErrMsg");
            FormLogSetting_DeleteColumn = rm.GetString("FormLogSetting_DeleteColumn");
            FormLogSetting_DeleteRowMsg = rm.GetString("FormLogSetting_DeleteRowMsg");
            FormLogSetting_NotSelectDeleteRowMsg = rm.GetString("FormLogSetting_NotSelectDeleteRowMsg");
            FormLogSetting_NotSelectEditRowMsg = rm.GetString("FormLogSetting_NotSelectEditRowMsg");
            FormMain_ConnectedInfo = rm.GetString("FormMain_ConnectedInfo");
            FormMain_LeatherLabel22 = rm.GetString("FormMain_LeatherLabel22");
            FormMain_NotConnectedInfo = rm.GetString("FormMain_NotConnectedInfo");
            FormMain_XinyuanLabel23 = rm.GetString("FormMain_XinyuanLabel23");
            FormMain_FineTensionAdjustmentRange_Add = rm.GetString("FormMain_FineTensionAdjustmentRange_Add");
            FormMain_ClothOutSpeed = rm.GetString("FormMain_ClothOutSpeed");
            FormMsg_CloseMonitorInfo = rm.GetString("FormMsg_CloseMonitorInfo");
            FormMsg_CloseMonitorMsg = rm.GetString("FormMsg_CloseMonitorMsg");
            FormMsg_FormatIncorrectMsg = rm.GetString("FormMsg_FormatIncorrectMsg");
            FormMsg_NotConnectedErrMsg = rm.GetString("FormMsg_NotConnectedErrMsg");
            FormMsg_OpenMonitorInfo = rm.GetString("FormMsg_OpenMonitorInfo");
            FormMsg_OpenMonitorMsg = rm.GetString("FormMsg_OpenMonitorMsg");
            FormMsg_ReceiveFormat = rm.GetString("FormMsg_ReceiveFormat");
            FormMsg_SendFormat = rm.GetString("FormMsg_SendFormat");
            FormMsg_StartCounterInfo = rm.GetString("FormMsg_StartCounterInfo");
            FormMsg_StartReadingMsg = rm.GetString("FormMsg_StartReadingMsg");
            FormMsg_StopCounterInfo = rm.GetString("FormMsg_StopCounterInfo");
            FormMsg_StopReadingMsg = rm.GetString("FormMsg_StopReadingMsg");
            FormMsg_StopReadingMsg2 = rm.GetString("FormMsg_StopReadingMsg2"); 
            FormWidthData_FileNameTitle = rm.GetString("FormWidthData_FileNameTitle");
            FormWidthData_YardLengthTitle = rm.GetString("FormWidthData_YardLengthTitle");
            FormWidthData_WidthTitle = rm.GetString("FormWidthData_WidthTitle");
            FormWidthData_NotCodeLengthOffsetMsg = rm.GetString("FormWidthData_NotCodeLengthOffsetMsg");
            FormWidthData_NumbersCodeLengthOffsetMsg = rm.GetString("FormWidthData_NumbersCodeLengthOffsetMsg");
            FormWidthData_NotNetWeightMsg = rm.GetString("FormWidthData_NotNetWeightMsg");
            FormWidthData_NumbersNetWeightMsg = rm.GetString("FormWidthData_NumbersNetWeightMsg");
            FormColourDifference_AAsterisk = rm.GetString("FormColourDifference_AAsterisk");
            FormColourDifference_AllSamples = rm.GetString("FormColourDifference_AllSamples");
            FormColourDifference_BAsterisk = rm.GetString("FormColourDifference_BAsterisk");
            FormColourDifference_BlackCalibrationCompleted = rm.GetString("FormColourDifference_BlackCalibrationCompleted");           
            FormColourDifference_Connect = rm.GetString("FormColourDifference_Connect");           
            FormColourDifference_DateTime = rm.GetString("FormColourDifference_DateTime");           
            FormColourDifference_Disconnect = rm.GetString("FormColourDifference_Disconnect");           
            FormColourDifference_Illuminant = rm.GetString("FormColourDifference_Illuminant");           
            FormColourDifference_LAsterisk = rm.GetString("FormColourDifference_LAsterisk");          
            FormColourDifference_Name = rm.GetString("FormColourDifference_Name");          
            FormColourDifference_Observer = rm.GetString("FormColourDifference_Observer");           
            FormColourDifference_SampleX = rm.GetString("FormColourDifference_SampleX");          
            FormColourDifference_SpectralType = rm.GetString("FormColourDifference_SpectralType");           
            FormColourDifference_WhiteCalibrationCompleted = rm.GetString("FormColourDifference_WhiteCalibrationCompleted");
            FormColourDifference_PleaseSelectAPortName = rm.GetString("FormColourDifference_PleaseSelectAPortName");
        }
    }
}
