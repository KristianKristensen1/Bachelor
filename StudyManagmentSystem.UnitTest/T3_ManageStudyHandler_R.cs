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
    public class T3_ManageStudyHandler_R
    {
        public IManageStudyHandler uut;
        public Study study;
        public IQueryable studies;
        public IQueryable inclusionCriteria;
        public Mock<DbSet<Study>> mockStudySet;
        public Mock<bachelordbContext> mockContext;

        public Mock<DbSet<Study>> mockStudySet2;


        [SetUp]
        public void SetUp()
        {
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
                    DateCreated = DateTime.Now.Date,
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

            //A list of inclusion criteria
            inclusionCriteria = new List<Inclusioncriteria>
            {
                new Inclusioncriteria
                {
                    Male = true,
                    Female = false,
                    MinAge = 10,
                    MaxAge = 60,
                    English = true,
                    IdStudy = 5,
                },

            }.AsQueryable();

            var listen = new List<Study>().AsQueryable();

            // Required to do this. If not the "mock" does not recognize "part" in uut.RegisterParticipantDB
            mockContext = new Mock<bachelordbContext>();
            mockStudySet = new Mock<DbSet<Study>>();
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.Provider).Returns(studies.Provider);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.Expression).Returns(studies.Expression);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.ElementType).Returns(studies.ElementType);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Study>)studies.GetEnumerator());
            mockContext.Setup(c => c.Study).Returns(mockStudySet.Object);
        }

        [Test]
        public void GetAllStudiesDB_StudiesToShow()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);

            //Act -
            var listOfStudies = uut.GetAllStudiesDB();

            //Assert -
            Assert.AreEqual(listOfStudies.Count, 3);
        }

        [Test]
        public void GetAllStudiesDB_NoStudiesToShow()
        {
            mockContext = new Mock<bachelordbContext>();
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object); //Listen må ikke være null, så hvad gør jeg lige her? Hmm.

            //Act -
            var listOfStudies = uut.GetAllStudiesDB();

            //Assert - That the researcher with ID = 1 has two studies
            Assert.AreEqual(listOfStudies.Count, 0);
        }

        [Test]
        public void GetMyResearcherStudiesDB_StudiesToShow()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);

            //Act -
            var listOfStudies = uut.GetMyResearcherStudiesDB(1);

            //Assert -
            Assert.AreEqual(listOfStudies.Count, 2);
        }

        [Test]
        public void GetMyResearcherStudiesDB_NoStudiesToShow()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);

            //Act -
            var listOfStudies = uut.GetMyResearcherStudiesDB(2);

            //Assert -
            Assert.AreEqual(listOfStudies.Count, 0);
        }

        [Test]
        public void CreateStudyDB()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);


            Inclusioncriteria inc = new Inclusioncriteria()
            {
                Male = true,
                Female = false,
                MinAge = 10,
                MaxAge = 60,
                English = true,
            };

            Study study = new Study()
            {
                Name = "New Study for people! This is real",
                IdStudy = 13,
                Description = "Ladies and gentlemen, this is study no. 5",
                Isdraft = false,
                IdResearcher = 1,
                Abstract = "Here is a nice abstract!",
                DirectStudyLink = "ThisIsADirectStudyLink",
                Duration = "60",
                DateCreated = DateTime.Now.Date,
                Preparation = "Please come prepared",
                EligibilityRequirements = "You must be nice to participate",
                Pay = 150,
            };

           
            //Act
            uut.CreateStudyDB(study, inc);

            //Assert
            //Assert.AreEqual(mockContext.Object.Study.Count(), 4);

            /*
            mockStudySet.Verify(m => m.Add(It.IsAny<Study>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
            */
        }
    }
}