using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public interface IRegisterHandler
    {
        bool RegisterParticipantDB(Participant participant, out string ErrorMessage);

        bool RegisterResearcherDB(Researcher researcher, out string ErrorMessage);

        void VerifyResearcherDB(Researcher researcher);
    }
}
