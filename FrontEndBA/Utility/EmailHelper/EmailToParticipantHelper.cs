using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;

namespace FrontEndBA.Utility.EmailHelper
{
    public class EmailToParticipantHelper
    {
        // https://www.youtube.com/watch?v=C4O8vqg295o

        public void SendMessge()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Tandlaehoejskolen", "donotreplyTandlægeHøjskolen@gmail.com"));



            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Send(message);
                client.Disconnect(true);
            }
            

            
        }
    }
}
