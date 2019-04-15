using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;


namespace TrainingProject
{
    class MailHelper
    {
        private const int Timeout = 180000;
        private string host = ConfigurationManager.AppSettings["SmtpServer"];
        private int port = int.Parse(ConfigurationManager.AppSettings["port"]);
        private string username = ConfigurationManager.AppSettings["AuthUsername"];
        private string password = ConfigurationManager.AppSettings["AuthPassword"];
        private bool sslRequired = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);

        public string Recipient { get; set; }
        public string RecipientCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string AttachmentFile { get; set; }


        public void SendMail()
        {
            try
            {
                using (var message = new MailMessage(username, Recipient, Subject, Body) { IsBodyHtml = true })
                {
                    if (RecipientCC != null)
                    {
                        message.Bcc.Add(RecipientCC);
                    }
                    using (var smtp = new SmtpClient(host, port)
                    {
                        DeliveryMethod = SmtpDeliveryMethod.Network
                    })
                    {
                        if (!String.IsNullOrEmpty(AttachmentFile))
                        {
                            if (File.Exists(AttachmentFile))
                            {
                                using (var attachment = new Attachment(AttachmentFile))
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
                    }
                }
            }

            catch (Exception ex)
            {
                //obsługa błędu
            }
        }

    }
}
