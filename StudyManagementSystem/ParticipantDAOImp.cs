﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class ParticipantDAOImp : IParticipantDAO
    {
        
        public void AddParticipant(Participants participant)
        {
            /*
            Participants participant = new Participants();
            participant.Firstname = "Jacob";
            participant.Lastname = "Pedersen";
            participant.Email = "Panda";
            participant.Password = "123454";
            participant.Pause = 1;
            */
            
            
            using (var db = new mydbContext())
            {

                db.Participants.Add(participant);
                db.SaveChanges();
            }
        }

        public void DeleteParticipant()
        {
            throw new NotImplementedException();
        }

        public void GetAllPArticipants()
        {
            throw new NotImplementedException();
        }

        public void GetParticipant()
        {
            throw new NotImplementedException();
        }

        public void VerifyParticipant()
        {
            throw new NotImplementedException();
        }
        
    }
    
}
