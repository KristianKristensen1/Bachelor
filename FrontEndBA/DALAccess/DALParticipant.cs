using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.IDALAccess;

namespace FrontEndBA.DALAccess
{
    public class DalParticipant:IDALParticipant
    {
        private BachelorBackEnd.IParticipantDAO ParticipantDAO;
        public DalParticipant()
        {
            ParticipantDAO = new ParticipantDAOImp();
        }
        public void SaveRegisterDto(BachelorBackEnd.Participants participantobj)
        {
            ParticipantDAO.AddParticipant(participantobj);
        }
    }
}
