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
        public Mock<DbSet<Study>> mockStudySet;
        public Mock<bachelordbContext> mockContext;

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
    }
}