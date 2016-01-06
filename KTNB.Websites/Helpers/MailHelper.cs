using System;
using System.Net.Mail;

namespace VPB_KTNB.Helpers
{
    /// <summary>
    /// Summary description for MailHelper
    /// </summary>
    public class MailHelper
    {
        private string _emailFrom = "it.survey@vpb.com.vn";
        private string _emailPass = "ttth2010";
        private string _emailFromName = "VPBank-IT Survey";
        private string _clientHost = "210.245.63.86";
        private int _clientPort = 25;

        public MailHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public MailHelper(string clientHost, int clientPort)
        {
            _clientHost = clientHost == "" ? "localhost" : clientHost;
            _clientPort = clientPort < 0 ? 25 : clientPort;
        }

        public MailHelper(string clientHost, int clientPort, string emailFrom)
        {
            _emailFrom = emailFrom == "" ? "admin@ho.vpb.com.vn" : emailFrom;
            _clientHost = clientHost == "" ? "localhost" : clientHost;
            _clientPort = clientPort < 0 ? 25 : clientPort;
        }

        public MailHelper(string clientHost, int clientPort, string emailFrom, string emailPass)
        {
            _emailFrom = emailFrom == "" ? "admin@ho.vpb.com.vn" : emailFrom;
            _clientHost = clientHost == "" ? "localhost" : clientHost;
            _clientPort = clientPort < 0 ? 25 : clientPort;
            _emailPass = emailPass;
        }

        public MailHelper(string clientHost, int clientPort, string emailFrom, string emailPass, string emailFromName)
        {
            _emailFrom = emailFrom == "" ? "admin@ho.vpb.com.vn" : emailFrom;
            _clientHost = clientHost == "" ? "localhost" : clientHost;
            _clientPort = clientPort < 0 ? 25 : clientPort;
            _emailPass = emailPass;
            _emailFromName = emailFromName;
        }

        public string SendEmail_usingPass(string sFrom, string sPass, string sTo, string sSubject, string sBody,
            string attachment)
        {
            _emailFrom = sFrom;
            _emailPass = sPass;
            return SendEmailPass(sTo, sSubject, sBody, attachment);
        }

        public string SendEmailPass(string SendTo, string Subject, string Body, string attachment)
        {
            //replace xyz@xyz.com,zzz@xyz.com & xxxxx with actual values.
            //smtp.yyyy.com should be replaced with smtp host of your email service
            //UseDefaultCredentials is set to false
            //Also EnableSsl property is skipped(If your smtp server supports, you can include it)

            try
            {
                //Create MailMessage Object
                MailMessage email_msg = new MailMessage();

                //Specifying From,Sender & Reply to address
                email_msg.From = new MailAddress(_emailFrom, _emailFromName);
                email_msg.Sender = new MailAddress(_emailFrom, _emailFromName);
                email_msg.ReplyTo = new MailAddress(_emailFrom, _emailFromName);

                //The To Email id
                email_msg.To.Add(SendTo);

                email_msg.Subject = Subject;//Subject of email
                email_msg.Body = Body;
                email_msg.IsBodyHtml = true;
                email_msg.Priority = MailPriority.Normal;

                //Create an object for SmtpClient class
                SmtpClient mail_client = new SmtpClient();

                //Providing Credentials (Username & password)
                System.Net.NetworkCredential network_cdr = new System.Net.NetworkCredential();
                network_cdr.UserName = _emailFrom;
                network_cdr.Password = _emailPass;

                mail_client.Host = _clientHost; //SMTP host    
                mail_client.UseDefaultCredentials = false;
                mail_client.Credentials = network_cdr;


                if (attachment != "")
                {
                    Attachment attach = new Attachment(attachment);
                    email_msg.Attachments.Add(attach);
                }

                //Now Send the message
                mail_client.Send(email_msg);

                return "";
            }
            catch (Exception ex)
            {
                //Some error occured
                return ex.Message;
            }
        }

        public string SendEmailNoPass(string sFrom, string sTo, string sSubject, string sBody)
        {
            _emailFrom = sFrom;
            return SendEmail_NoPass(sTo, sSubject, sBody);
        }

        public string SendEmailNoPass(string sTo, string sSubject, string sBody)
        {
            return SendEmail_NoPass(sTo, sSubject, sBody);
        }

        private string SendEmail_NoPass(string SendTo, string Subject, string Body)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

                if (!regex.IsMatch(SendTo))
                {
                    return "Invalid Email Address";
                }
                else
                {
                    System.Net.Mail.SmtpClient smtp = new SmtpClient();
                    System.Net.Mail.MailMessage msg = new MailMessage(_emailFrom, SendTo, Subject, Body);
                    msg.IsBodyHtml = true;
                    smtp.Host = _clientHost;
                    smtp.Port = _clientPort;

                    smtp.Send(msg);
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string SendEmailWithAttachment(string SendTo, string Subject, string Body, string AttachmentPath)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

                string from = _emailFrom;
                string to = SendTo;
                string subject = Subject;
                string body = Body;

                if (!regex.IsMatch(to))
                {
                    return "Invalid Email Address";
                }
                else
                {
                    try
                    {
                        MailMessage em = new MailMessage(from, to, subject, body);
                        em.IsBodyHtml = true;

                        if (AttachmentPath.Trim().Length > 0)
                        {
                            Attachment attach = new Attachment(AttachmentPath);
                            em.Attachments.Add(attach);
                        }

                        //em.Bcc.Add(from);

                        System.Net.Mail.SmtpClient smtp = new SmtpClient();

                        smtp.Host = _clientHost;
                        smtp.Port = _clientPort;
                        smtp.Send(em);
                        return "";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SendTo">Danh sach email duoc ngan cach nhau boi dau ;</param>
        /// <param name="SendBCC"></param>
        /// <param name="_emailFrom"></param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="AttachmentPath"></param>
        /// <returns></returns>
        public string SendEmailWithBCCAttachment(string SendTo, string SendBCC, string Subject, string Body, string AttachmentPath)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                string from = _emailFrom;
                string to = SendTo;
                string subject = Subject;
                string body = Body;
                string bcc = SendBCC;

                bool result = true;
                //String[] ALL_EMAILS = to.Split(clsVpbUtils.chrSepaList);

                //foreach (string emailaddress in ALL_EMAILS)
                //{
                //    result = regex.IsMatch(emailaddress);
                //    if (result == false)
                //    {
                //        return "Invalid Email Address";
                //    }
                //}

                if (result == true)
                {
                    try
                    {
                        MailMessage em = new MailMessage(from, to, subject, body);
                        Attachment attach = new Attachment(AttachmentPath);
                        em.Attachments.Add(attach);
                        em.Bcc.Add(bcc);

                        System.Net.Mail.SmtpClient smtp = new SmtpClient();
                        smtp.Host = _clientHost;
                        smtp.Port = _clientPort;

                        smtp.Send(em);

                        return "";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}