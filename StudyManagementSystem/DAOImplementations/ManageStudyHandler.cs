using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class ManageStudyHandler : IManageStudyHandler
    {
        private bachelordbContext _context;
        public ManageStudyHandler(bachelordbContext context)
        {
            _context = context;
        }

        public void CreateStudyDB(Study study, Inclusioncriteria inclusioncriteria)
        {
            //Adds the study to the database and saves
            _context.Study.Add(study);
            _context.SaveChanges();


            //Retrieves the id from the study just saved and sets the study_id in inclusioncriteria.            
            var dbStudy = (_context.Study.FirstOrDefault(stud =>
                              stud.Name == study.Name && stud.DateCreated == study.DateCreated)) ??
                          _context.Study.Local.FirstOrDefault(stud =>
                              stud.Name == study.Name && stud.DateCreated == study.DateCreated);
            inclusioncriteria.IdStudy = dbStudy.IdStudy;

            //Sets the direct study link with the generated ID
            if (dbStudy != null)
            {
                dbStudy.DirectStudyLink = "http://localhost:61728/ViewStudy/ViewStudy?studyID=" + dbStudy.IdStudy;
                _context.Study.Update(dbStudy);
            }

            //Saves the inclusioncriteria and the study link
            _context.Inclusioncriteria.Add(inclusioncriteria);
            _context.SaveChanges();    
        }

        public void EditStudyDB(Study study, Inclusioncriteria inclusioncriteria)
        {
            Study oldStudy = _context.Study.FirstOrDefault(stud => stud.IdStudy == study.IdStudy);
            if (oldStudy != null)
            {
                oldStudy.Name = study.Name;
                oldStudy.Description = study.Description;
                oldStudy.Abstract = study.Abstract;
                oldStudy.Duration = study.Duration;
                oldStudy.EligibilityRequirements = study.EligibilityRequirements;
                oldStudy.Inclusioncriteria = study.Inclusioncriteria;
                oldStudy.Isdraft = study.Isdraft;
                oldStudy.Pay = study.Pay;
                oldStudy.Preparation = study.Preparation;
                oldStudy.Location = study.Location;
                _context.Study.Update(oldStudy);
                _context.SaveChanges();
            }

            Inclusioncriteria oldInc = _context.Inclusioncriteria.FirstOrDefault(inc => inc.IdStudy == study.IdStudy);
            if (oldInc != null)
            {
                oldInc.Male = inclusioncriteria.Male;
                oldInc.Female = inclusioncriteria.Female;
                oldInc.English = inclusioncriteria.English;
                oldInc.MinAge = inclusioncriteria.MinAge;
                oldInc.MaxAge = inclusioncriteria.MaxAge;
                _context.Inclusioncriteria.Update(oldInc);
                _context.SaveChanges();
            }
        }

        public void DeleteStudyDB(int studyID)
        {
            Study study = _context.Study.FirstOrDefault(stud => stud.IdStudy == studyID);
            _context.Study.Remove(study);
            _context.SaveChanges();
        }

        public Study GetStudyDB(int id)
        {
            Study study = _context.Study.FirstOrDefault(stud => stud.IdStudy == id);
            return study;
        }

        public Inclusioncriteria GetInclusioncriteriaDB(int id)
        {
            Inclusioncriteria incCrit = _context.Inclusioncriteria.FirstOrDefault(inc => inc.IdStudy == id);
            return incCrit;
        }

    }
}