using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;

namespace FrontEndBA.DALAccess
{
    public class DALResearcher
    {
        private BachelorBackEnd.IResearcherDAO ResearcherDAO;
        public DALResearcher()
        {
            
            //ResearcherDAO = new ResearcherDAOImp();
        }

        public void SaveRegisterDto(BachelorBackEnd.Participants participantobj)
        {
            //ResearcherDAO.AddParticipant(participantobj);
        }
    }
}
