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
    public class ManageStudyHandler_T3_R
    {
        public IManageStudyHandler uut;

        //public Researcher researcher;
        //public IQueryable researchers;
        //public Mock<DbSet<Researcher>> mockResearcherSet;

        public Study study;
        public IQueryable studies;
        public Mock<DbSet<Study>> mockStudySet;

        //public Inclusioncriteria inclusioncriteria;
        //public IQueryable inclusionCriteriaCollection;
        //public Mock<DbSet<Inclusioncriteria>> mockCriteriaSet;

        public Mock<bachelordbContext> mockContext;

        [SetUp]
        public void SetUp()
        {
            //researchers = new List<Researcher>
            //{
            //    //A researcher with an id to connect the studies
            //    new Researcher
            //    {
            //        IdResearcher = 1,
            //        Email = "Coco@Jambo.dk",
            //        Password = "123456",
            //        Isverified = true
            //    },
            //    new Researcher
            //    {
            //        IdResearcher = 2,
            //        Email = "AllThat@SheWants.dk",
            //        Password = "123456",
            //        Isverified = true
            //    }
            //}.AsQueryable();

            //A list of studies
            studies = new List<Study>
            {
                new Study
                {
                    Name = "New Study for people!",
                    IdStudy = 5,
                    Description = "Ladies and gentlemen, this is study no. 5",
                    Isdraft = false,
                    IdResearcher = 1,
                },
                new Study
                {
                    Name = "Not for everyone!",
                    IdStudy = 6,
                    Description = "Not as great as study no. 5",
                    Isdraft = true,
                    IdResearcher = 1,
                },
                new Study
                {
                    Name = "Terrible Study!",
                    IdStudy = 7,
                    Description = "Stay away!",
                    Isdraft = false,
                    IdResearcher = 3,
                }
            }.AsQueryable();

            // Required to do this. If not the "mock" does not recognize "part" in uut.RegisterParticipantDB
            // Men forstår det godt nok ikke, Jacob? :O :D xD :P
            // Skal Studies og InclusionCriteria også have gjort sådan? Og I så fald, kan den samme context anvendes, eller skal de have én hver?
            mockContext = new Mock<bachelordbContext>();

            //mockResearcherSet = new Mock<DbSet<Researcher>>();
            //mockResearcherSet.As<IQueryable<Researcher>>().Setup(m => m.Provider).Returns(researchers.Provider);
            //mockResearcherSet.As<IQueryable<Researcher>>().Setup(m => m.Expression).Returns(researchers.Expression);
            //mockResearcherSet.As<IQueryable<Researcher>>().Setup(m => m.ElementType).Returns(researchers.ElementType);
            //mockResearcherSet.As<IQueryable<Researcher>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Researcher>)researchers.GetEnumerator());           
            //mockContext.Setup(c => c.Researcher).Returns(mockResearcherSet.Object);

            mockStudySet = new Mock<DbSet<Study>>();
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.Provider).Returns(studies.Provider);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.Expression).Returns(studies.Expression);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.ElementType).Returns(studies.ElementType);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Study>)studies.GetEnumerator());
            mockContext.Setup(c => c.Study).Returns(mockStudySet.Object);

            //mockCriteriaSet = new Mock<DbSet<Inclusioncriteria>>();
            //mockCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.Provider).Returns(inclusionCriteriaCollection.Provider);
            //mockCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.Expression).Returns(inclusionCriteriaCollection.Expression);
            //mockCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.ElementType).Returns(inclusionCriteriaCollection.ElementType);
            //mockCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Inclusioncriteria>)inclusionCriteriaCollection.GetEnumerator());
            //mockContext.Setup(c => c.Inclusioncriteria).Returns(mockCriteriaSet.Object);
        }

        [Test]
        public void GetAllStudiesDB_StudiesToShow()
        {
            //Setup
            uut = new ManageStudyHandler(mockContext.Object);

            //Act -
            var listOfStudies = uut.GetAllStudiesDB();

            // Assert -
            Assert.AreEqual(listOfStudies.Count, 3);
        }

        [Test]
        public void GetAllStudiesDB_NoStudiesToShow()
        {
            mockContext = new Mock<bachelordbContext>();
            //Setup
            uut = new ManageStudyHandler(mockContext.Object); //Listen må ikke være null, så hvad gør jeg lige her? Hmm.

            //Act -
            var listOfStudies = uut.GetMyResearcherStudiesDB(1);

            // Assert - That the researcher with ID = 1 has two studies
            Assert.AreEqual(listOfStudies, null);
        }

        [Test]
        public void GetMyResearcherStudiesDB_StudiesToShow()
        {
            //Setup
            uut = new ManageStudyHandler(mockContext.Object);

            //Act -
            var listOfStudies = uut.GetMyResearcherStudiesDB(1);

            // Assert -
            Assert.AreEqual(listOfStudies.Count, 2);
        }

        [Test]
        public void GetMyResearcherStudiesDB_NoStudiesToShow()
        {
            //Setup
            uut = new ManageStudyHandler(mockContext.Object);

            //Act -
            var listOfStudies = uut.GetMyResearcherStudiesDB(2);

            // Assert -
            Assert.AreEqual(listOfStudies.Count, 0);
        }
    }
}