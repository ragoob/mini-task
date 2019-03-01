using Core.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Extentions
{
    public  class SendEmailTask
    {
       private static  IConfigurationSection setting = EngineContext.Current.Resolve<IConfiguration>().GetSection("AppSettings");

        public static async Task SendAsync(string subject,string body,string[] to)
        {

            SmtpClient client = new SmtpClient(setting["Smtpserver"]);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(setting["EmailUserName"], setting["EmailPassword"]);
            client.Port = Convert.ToInt32(setting["Port"]);
            client.EnableSsl = Convert.ToBoolean(setting["EnableSsl"]);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(setting["EmailUserName"]);
            to.ForEach(email => { mailMessage.To.Add(email); });
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            await client.SendMailAsync(mailMessage);
        }

        public static Task Send(string subject, string body, string to)
        {
            return SendAsync(subject, body, new string[] { to });
        }
    }
}
