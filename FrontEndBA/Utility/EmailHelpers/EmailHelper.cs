using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.CreateStudy;
using FrontEndBA.Models.ResearcherModel.EmailModels;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit.Utils;

namespace FrontEndBA.Utility.EmailHelper
{
    public class EmailHelper
    {
        // https://www.youtube.com/watch?v=C4O8vqg295o
        public async Task RetrieveAccount(string email,string password)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Tandlægehøjskolen", "donotreplyTandlægeHøjskolen@gmail.com"));
            message.To.Add(new MailboxAddress("ResetPassword", email));

            var builder = new BodyBuilder();


            builder.HtmlBody = "<p>Your password for Tandlægehøjskolen is " + password + "</p>\r\n<p>&nbsp;</p>\r\n" +
                               System.Environment.NewLine + "" + System.Environment.NewLine;


            SendingWithSmtpClient(builder,message,"Password Reset");
     
        }

        public void SendingWithSmtpClient(BodyBuilder builder, MimeMessage message,string subject)
        {
            var logoPath = Directory.GetCurrentDirectory() + @"\wwwroot\images\AuLogo.PNG";

            var image = builder.LinkedResources.Add(logoPath);
            image.ContentId = MimeUtils.GenerateMessageId();


            builder.HtmlBody += System.Environment.NewLine + "" + System.Environment.NewLine + "" + System.Environment.NewLine +
                               string.Format(@"
                <div>Best regards Department of Dentistry and Oral Health, Vennelyst Blvd. 9, 8000 Aarhus</div><center><img src=""cid:{0}""></center>", image.ContentId);

            message.Body = builder.ToMessageBody();
            message.Subject = subject;

            using (var client = new SmtpClient())
            {
                // This should be removed or made more secure but is a work around if the server has a antivirus installed. More information here https://github.com/jstedfast/MailKit/blob/master/FAQ.md?fbclid=IwAR0Wr6J_q9qak5KM2ZvD-i5Sgg9ThvZIjEU2ndUXVMGOEXQyY482ARRWiq4#InvalidSslCertificate
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("noreplytandlaegehaejskolen@gmail.com", "Q2E4t6u8");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        public void SendMessages(SendingModel sModel, List<Participant> participants)
        {
            Thread t = new Thread(()=>SendToParticipants(sModel,participants));
            t.Start();
           
        }

        public void SendToParticipants(SendingModel sModel, List<Participant> participants)
        {
            

            foreach (var participant in participants)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Tandlægehøjskolen", "donotreplyTandlægeHøjskolen@gmail.com"));
                message.To.Add(new MailboxAddress("Participant for Tandlægehøjskolen", participant.Email));
   
                var builder = new BodyBuilder();
                


                builder.HtmlBody = sModel.Mail.MailBody + System.Environment.NewLine + "" + System.Environment.NewLine +
                                   "" + System.Environment.NewLine;


            SendingWithSmtpClient(builder,message,sModel.Mail.Subject);


            }
        }

        public void PrefillTextArea(SendingModel sModel)
        {
            if(sModel.Mail==null)
                sModel.Mail = new EmailModel();

            sModel.Mail.MailBody = "<p>Hallo!</p>\r\n<p>&nbsp;</p>" +
                                    " <p>We are contacting you because we are in a need of new participants!</p>\r\n" + Environment.NewLine +
                                    " <p>The Description is as follows:" + sModel.Study.study.Description.ToString() + "</p>\r\n" + System.Environment.NewLine +
                                    "<p>The pay will be as follows: " + sModel.Study.study.Pay + "kr" + "</p>\r\n" + System.Environment.NewLine +
                                    "<p>The duration will be as follows:" + sModel.Study.study.Duration + "</p>\r\n" + System.Environment.NewLine +
                                    "If you are interested please contact "+sModel.Study.researcher.FirstName+ " " + sModel.Study.researcher.LastName + "  at " + sModel.Study.researcher.Email;
        }
    }
}
