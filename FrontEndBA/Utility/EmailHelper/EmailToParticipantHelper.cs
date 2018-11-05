using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.CreateStudy;
using FrontEndBA.Models.ResearcherModel.EmailModels;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;

namespace FrontEndBA.Utility.EmailHelper
{
    public class EmailToParticipantHelper
    {
        // https://www.youtube.com/watch?v=C4O8vqg295o


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
                message.From.Add(new MailboxAddress("Tandlaehoejskolen", "donotreplyTandlægeHøjskolen@gmail.com"));
                message.To.Add(new MailboxAddress("Participant for TandlaegeHoejskolen", participant.Email));
                message.Subject = sModel.mails.Subject;
                var builder = new BodyBuilder();
                builder.TextBody = sModel.mails.Body;

                using (var client = new SmtpClient())
                {

                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("noreplytandlaegehaejskolen@gmail.com", "Q2E4t6u8");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }

       
            

            
        }
    }
}
