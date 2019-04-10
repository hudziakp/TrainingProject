using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace TrainingProject
{
    class MailHelper
    {
        private const int Timeout = 180000;
        private string host;
        private int port;
        private string username;
        private string password;
        private bool sslRequired;

        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string RecipientCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentFile { get; set; }

        public MailHelper()
        {
            host = ConfigurationManager.AppSettings["SmtpServer"];
            port = int.Parse(ConfigurationManager.AppSettings["port"]);
            username = ConfigurationManager.AppSettings["AuthUsername"];
            password = ConfigurationManager.AppSettings["AuthPassword"];
            sslRequired = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
        }

        public void SendMail()
        {
            try
            {
                Attachment attachment = null;
                var message = new MailMessage(Sender, Recipient, Subject, Body) { IsBodyHtml = true };
                if (RecipientCC != null)
                {
                    message.Bcc.Add(RecipientCC);
                }
                var smtp = new SmtpClient(host, port);

                if (!String.IsNullOrEmpty(AttachmentFile))
                {
                    if (File.Exists(AttachmentFile))
                    {
                        attachment = new Attachment(AttachmentFile);
                        message.Attachments.Add(attachment);
                    }
                }

                if (username.Length > 0 && password.Length > 0)
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(username, password);
                    smtp.EnableSsl = sslRequired;
                }

                smtp.Send(message);

                //sprzątanie
                if (attachment != null)
                {
                    attachment.Dispose();
                }
                message.Dispose();
                smtp.Dispose();
            }

            catch (Exception ex)
            {
                //obsługa błędu
            }
        }

    }
}
