using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Moq;
using BachelorBackEnd;

namespace Tests
{
    public class ManageStudyHandler_T3_P
    {
        //public IManageStudyHandler uut;
        //public Participant participant;
        public Study study;
        public IQueryable studies;
        //public Inclusioncriteria inclusioncriteria;
        //public IQueryable inclusionCriteriaCollection;
        //public Mock<bachelordbContext> mockContext;

        [SetUp]
    public void SetUp()
        {
            //A participant with some information to sort through which studies are relevant.
            //new Participant
            //{
                

            //};

            //A list of studies
            studies = new List<Study>
            {
                new Study
                {
                    Name = "New Study for people!",
                    IdStudy = 5,
                    Description = "Ladies and gentlemen, this is study no. 5",
                    Isdraft = true,
                    IdResearcher = 1,
                }
            }.AsQueryable();

            ////A list of criteria with a reference studyId
            //inclusionCriteriaCollection = new List<Inclusioncriteria>
            //{
            //    new Inclusioncriteria
            //    {
            //        IdInclusionCriteria = 1,
            //        Male = 1,
            //        Female = 1,
            //        MinAge = 18,
            //        MaxAge = 36,
            //        English = 1,
            //        IdStudy = 5
            //    }
            //}.AsQueryable();
        }
    }
}
