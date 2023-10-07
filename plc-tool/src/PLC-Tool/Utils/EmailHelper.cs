using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCommon.Utils
{
    /// <summary>
    /// Email
    /// </summary>
    public class EmailHelper
    {
        private SmtpClient client;
        private MailMessage message;

        /// <summary>
        /// 发件人地址
        /// </summary>
        public string SendEmailAddress { get; set; }
        /// <summary>
        /// 发件人显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 发件账号密码
        /// </summary>
        public string SendEmailPwd { get; set; }
        /// <summary>
        /// 收件人地址
        /// </summary>
        public string RecvEmailAddress { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string EmailSubject { get; set; } = "验布报告";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smtpHost">发件服务器</param>
        /// <param name="smtpPort">发件端口</param>
        public EmailHelper(string smtpHost, int smtpPort)
        {
            client = new SmtpClient(smtpHost, smtpPort);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="file">附件</param>
        /// <returns></returns>
        public bool SendEmail(string content, string file, out string errorMsg)
        {
            bool isSuccessed = false;
            errorMsg = "";

            NetworkCredential senderCredential = new NetworkCredential(SendEmailAddress, SendEmailPwd);
            client.Credentials = senderCredential;
            client.EnableSsl = false;

            MailAddress sendAddr = new MailAddress(SendEmailAddress, DisplayName);
            MailAddress recvAddr = new MailAddress(RecvEmailAddress);
            message = new MailMessage(sendAddr, recvAddr);
            message.Subject = EmailSubject;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = content;

            if (!string.IsNullOrEmpty(file))
            {
                if (!File.Exists(file))
                {
                    errorMsg = "附件文件不存在";
                    return false;
                }
                Attachment attachment = new Attachment(file);
                attachment.Name = System.IO.Path.GetFileName(file);
                message.Attachments.Add(attachment);
            }

            try
            {
                client.Send(message);
                message.Dispose();
                isSuccessed = true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                LogHelper.Default.Error("Send Email Failed", ex);
            }

            return isSuccessed;
        }
    }
}
