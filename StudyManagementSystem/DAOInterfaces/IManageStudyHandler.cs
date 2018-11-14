using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BachelorBackEnd
{
    public interface IManageStudyHandler
    {
        void CreateStudyDB(Study study, Inclusioncriteria inclusioncriteria);

        void EditStudyDB(Study study, Inclusioncriteria inclusioncriteria);

        void DeleteStudyDB(int studyID);

        Study GetStudyDB(int id);

        Inclusioncriteria GetInclusioncriteriaDB(int id);

    }
}
