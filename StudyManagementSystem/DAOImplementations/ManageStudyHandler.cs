using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class ManageStudyHandler : IManageStudyHandler
    {
        public void AddParticipantDB(string email, Study study)
        {
            throw new NotImplementedException();
        }

        public void CreateStudyDB(Study study)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                DBmodel.Study.Add(study);
                DBmodel.SaveChanges();
            }
        }

        public void DeleteStudyDB(Study study)
        {
            using (bachelordbContext DBmodel = new bachelordbContext())
            {
                DBmodel.Study.First(stud => stud.IdStudy == study.IdStudy);
            }
        }

        public void GetStudyDB(string studyID)
        {
            throw new NotImplementedException();
        }

        public void RemoveParticipantDB(Participant participant, Study study)
        {
            throw new NotImplementedException();
        }

        public void SaveAsDraftDB(Study study)
        {
            throw new NotImplementedException();
        }

        public void ShowStudyDB(string studyID)
        {
            throw new NotImplementedException();
        }
    }
}
