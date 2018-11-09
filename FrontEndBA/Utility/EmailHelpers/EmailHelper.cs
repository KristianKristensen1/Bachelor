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
        public void RetrieveAccount(string email,string password)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Tandlægehøjskolen", "donotreplyTandlægeHøjskolen@gmail.com"));
            message.To.Add(new MailboxAddress("ResetPassword", email));

            var builder = new BodyBuilder();

            var image = builder.LinkedResources.Add(@"C:\Users\panda\source\repos\ReBachelor\Bachelor\FrontEndBA\wwwroot\images\AuLogo.PNG");
            image.ContentId = MimeUtils.GenerateMessageId();

            builder.HtmlBody = "<p>Your password for Tandlægehøjskolen is "+password+"</p>\r\n<p>&nbsp;</p>\r\n"+ System.Environment.NewLine + "" + System.Environment.NewLine +
                               string.Format(@"
                <div>Best regards TandlægeHøjskolen, Vennelyst Blvd. 9, 8000 Aarhus</div><center><img src=""cid:{0}""></center>", image.ContentId);

            message.Body = builder.ToMessageBody();
            message.Subject = "Password Reset";

            using (var client = new SmtpClient())
            {

                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("noreplytandlaegehaejskolen@gmail.com", "Q2E4t6u8");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        public void SendMessages(SendingModel sModel, List<Participant> participants)
        {
            Thread t = new Thread(()=>SendMessge(sModel,participants));
            t.Start();
           
        }

        public void SendMessge(SendingModel sModel, List<Participant> participants)
        {
            

            foreach (var participant in participants)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Tandlægehøjskolen", "donotreplyTandlægeHøjskolen@gmail.com"));
                message.To.Add(new MailboxAddress("Participant for Tandlægehøjskolen", participant.Email));
                message.Subject = sModel.Mail.Subject;
                var builder = new BodyBuilder();
                
                var logoPath = Directory.GetCurrentDirectory() + @"\wwwroot\images\AuLogo.PNG";

                var image = builder.LinkedResources.Add(logoPath);
           
               
                image.ContentId = MimeUtils.GenerateMessageId();
                
                builder.HtmlBody = sModel.Mail.MailBody + System.Environment.NewLine+""+System.Environment.NewLine +"" + System.Environment.NewLine +
                                    string.Format(@"
                <div>Best regards TandlægeHøjskolen, Vennelyst Blvd. 9, 8000 Aarhus</div><center><img src=""cid:{0}""></center>", image.ContentId);

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {

                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("noreplytandlaegehaejskolen@gmail.com", "Q2E4t6u8");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }

       
            

            
        }

        public void PrefillTextArea(SendingModel sModel)
        {
            if(sModel.Mail==null)
                sModel.Mail = new EmailModel();

            sModel.Mail.MailBody = "<p>Hallo!</p>\r\n<p>&nbsp;</p>" +
                                    " <p>We are contacting you because we are in a need of new participants!</p>\r\n" + Environment.NewLine +
                                    " <p>The Description is as follows:" + sModel.Study.study.Description.ToString() + "</p>\r\n" + System.Environment.NewLine +
                                    "<p>The pay will be as follows: " + sModel.Study.study.Pay + "</p>\r\n" + System.Environment.NewLine +
                                    "<p>The duration will be as follows:" + sModel.Study.study.Duration + "</p>\r\n" + System.Environment.NewLine +
                                    "If you are interested please contact "+sModel.Study.researcher.Name+"  at " + sModel.Study.researcher.Email;
        }
    }
}
