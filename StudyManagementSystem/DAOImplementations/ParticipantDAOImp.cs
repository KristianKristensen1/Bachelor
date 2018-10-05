using System;
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
                if(db.Participants.Any( x => x.Email == participant.Email))
                {
                    //The email allready exists in database
                }
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
            using (var db = new mydbContext())
            {
                var parti = from b in db.Participants
                            where b.Firstname.StartsWith("h")
                            select b.Firstname;

                Console.WriteLine(parti);

            }
        }

        public void VerifyParticipant()
        {
            throw new NotImplementedException();
        }
        
    }
    
}
