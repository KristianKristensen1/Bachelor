using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models;

namespace FrontEndBA.Utility
{
    /// <summary>
    /// Creates a ViewStudyModel from a Study id. A ViewStudyModel is a model containing a Study and the corresponding inclusioncriteria, and is used to show the datails of a Study. 
    /// </summary>
    public class ViewStudyModelHelper
    {

        public ViewStudyModel createViewStudyModel(int id)
        {
            ViewStudyModel viewStudyModel = new ViewStudyModel();
            Study study = GetStudy(id);
            viewStudyModel.study = study;
            viewStudyModel.inclusioncriteria = GetInclusioncriteria(id);
            viewStudyModel.researcher =  GetResearcherEmail(study.IdResearcher);

            return viewStudyModel;
        }
        public Study GetStudy(int id)
        {
            ManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
            return msh.getStudyDB(id);
        }

        public Inclusioncriteria GetInclusioncriteria(int id)
        {
            ManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
            return msh.getInclusioncriteriaDB(id);
        }

        public Researcher GetResearcherEmail(int id)
        {
            ManageStudyHandler msh = new ManageStudyHandler(new bachelordbContext());
            return msh.getResearcherDB(id);
            
        }
    }
}
