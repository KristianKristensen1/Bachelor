using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using StudyManagementSystem.Models;
using Moq;
using BachelorBackEnd;

namespace Tests
{
    public class T3_ManageStudyHandler_P
    {
        public IManageStudyHandler uut;
        public Study study;
        public IQueryable studies;
        public Mock<DbSet<Study>> mockStudySet;
        public Inclusioncriteria inclusionCriteria;
        public IQueryable inclusionCriteriaCollection;
        public Mock<DbSet<Inclusioncriteria>> mockInclusionCriteriaSet;
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
                    Tag ="draftForNo5",
                    IdResearcher = 1,
                },
                new Study
                {
                    Name = "Not for everyone!",
                    IdStudy = 6,
                    Description = "Not as great as study no. 5",
                    Isdraft = false,
                    Tag ="publishedNo6",
                    IdResearcher = 1,
                },
                new Study
                {
                    Name = "Terrible Study!",
                    IdStudy = 7,
                    Description = "Stay away!",
                    Isdraft = false,
                    Tag ="publishedNo7",
                    IdResearcher = 3,
                }
            }.AsQueryable();

            //A list of criteria with a reference studyId
            inclusionCriteriaCollection = new List<Inclusioncriteria>
            {
                new Inclusioncriteria
                {
                    IdInclusionCriteria = 1,
                    Male = true,
                    Female = true,
                    MinAge = 20,
                    MaxAge = 99,
                    English = true,
                    IdStudy = 5
                },
                new Inclusioncriteria
                {
                    IdInclusionCriteria = 2,
                    Male = true,
                    Female = false,
                    MinAge = 20,
                    MaxAge = 39,
                    English = false,
                    IdStudy = 6
                },
                new Inclusioncriteria
                {
                    IdInclusionCriteria = 3,
                    Male = false,
                    Female = true,
                    MinAge = 40,
                    MaxAge = 59,
                    English = false,
                    IdStudy = 7
                }
            }.AsQueryable();

            mockContext = new Mock<bachelordbContext>();

            mockStudySet = new Mock<DbSet<Study>>();
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.Provider).Returns(studies.Provider);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.Expression).Returns(studies.Expression);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.ElementType).Returns(studies.ElementType);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Study>)studies.GetEnumerator());
            mockContext.Setup(c => c.Study).Returns(mockStudySet.Object);

            mockInclusionCriteriaSet = new Mock<DbSet<Inclusioncriteria>>();
            mockInclusionCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.Provider).Returns(inclusionCriteriaCollection.Provider);
            mockInclusionCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.Expression).Returns(inclusionCriteriaCollection.Expression);
            mockInclusionCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.ElementType).Returns(inclusionCriteriaCollection.ElementType);
            mockInclusionCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Inclusioncriteria>)inclusionCriteriaCollection.GetEnumerator());
            mockContext.Setup(c => c.Inclusioncriteria).Returns(mockInclusionCriteriaSet.Object);
        }

        [Test]
        public void GetRelevantStudiesDB_Male_23_English()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);
            Participant participant = new Participant
            {
                Gender = true, //is male
                Age = DateTime.Parse("Jan 1, 1995"), //is 23 years old
                English = true // speaks english
            };

            //Act - Get relevant studies for this participant.
            var listOfStudies = uut.GetRelevantStudiesDB(participant);

            // Assert - that there are two (2) relevant studies by the expected names.
            Assert.AreEqual(listOfStudies.Count, 2);
            Assert.That(listOfStudies.Any(study => study.Name == "New Study for people!"));
            Assert.That(listOfStudies.Any(study => study.Name == "Not for everyone!"));
            Assert.That(listOfStudies.Any(study => study.Name != "Terrible Study!"));
        }

        [Test]
        public void GetRelevantStudiesDB_Male_23_NoEnglish()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);
            Participant participant = new Participant
            {
                Gender = true, //is male
                Age = DateTime.Parse("Jan 1, 1995"), //is 23 years old
                English = false //does not speak english
            };

            //Act - Get relevant studies for this participant.
            var listOfStudies = uut.GetRelevantStudiesDB(participant);

            // Assert - that there is one (1) relevant study by the expected name.
            Assert.AreEqual(listOfStudies.Count, 1);
            Assert.That(listOfStudies.Any(study => study.Name != "New Study for people!"));
            Assert.That(listOfStudies.Any(study => study.Name == "Not for everyone!"));
            Assert.That(listOfStudies.Any(study => study.Name != "Terrible Study!"));
        }

        [Test]
        public void GetRelevantStudiesDB_Female_53_English()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);
            Participant participant = new Participant
            {
                Gender = false, //is female
                Age = DateTime.Parse("Jan 1, 1965"), //is 53 years old
                English = true //speaks english
            };

            //Act - Get relevant studies for this participant.
            var listOfStudies = uut.GetRelevantStudiesDB(participant);

            //Assert - that there are two (2) relevant study by the expected names.
            Assert.AreEqual(listOfStudies.Count, 2);
            Assert.That(listOfStudies.Any(study => study.Name == "New Study for people!"));
            Assert.That(listOfStudies.Any(study => study.Name != "Not for everyone!"));
            Assert.That(listOfStudies.Any(study => study.Name == "Terrible Study!"));
        }

        [Test]
        public void GetRelevantStudiesDB_Female_53_NoEnglish()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);
            Participant participant = new Participant
            {
                Gender = false, //is female
                Age = DateTime.Parse("Jan 1, 1965"), //is 53 years old
                English = false //does not speak english
            };

            //Act - Get relevant studies for this participant.
            var listOfStudies = uut.GetRelevantStudiesDB(participant);

            //Assert - that there is one (1) relevant studies by the expected name.
            Assert.AreEqual(listOfStudies.Count, 1);
            Assert.That(listOfStudies.Any(study => study.Name != "New Study for people!"));
            Assert.That(listOfStudies.Any(study => study.Name != "Not for everyone!"));
            Assert.That(listOfStudies.Any(study => study.Name == "Terrible Study!"));
        }

        [Test]
        public void GetRelevantStudiesDB_Male_83_English()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);
            Participant participant = new Participant
            {
                Gender = true, //is male
                Age = DateTime.Parse("Jan 1, 1935"), //is 83 years old
                English = true //speaks english
            };

            //Act - Get relevant studies for this participant.
            var listOfStudies = uut.GetRelevantStudiesDB(participant);

            //Assert - that there is one (1) relevant studies by the expected name.
            Assert.AreEqual(listOfStudies.Count, 1);
            Assert.That(listOfStudies.Any(study => study.Name == "New Study for people!"));
            Assert.That(listOfStudies.Any(study => study.Name != "Not for everyone!"));
            Assert.That(listOfStudies.Any(study => study.Name != "Terrible Study!"));
        }

        [Test]
        public void GetRelevantStudiesDB_Female_83_English()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);
            Participant participant = new Participant
            {
                Gender = false, //is female
                Age = DateTime.Parse("Jan 1, 1935"), //is 83 years old
                English = true //speaks english
            };

            //Act - Get relevant studies for this participant.
            var listOfStudies = uut.GetRelevantStudiesDB(participant);

            //Assert - that there is one (1) relevant studies by the expected name.
            Assert.AreEqual(listOfStudies.Count, 1);
            Assert.That(listOfStudies.Any(study => study.Name == "New Study for people!"));
            Assert.That(listOfStudies.Any(study => study.Name != "Not for everyone!"));
            Assert.That(listOfStudies.Any(study => study.Name != "Terrible Study!"));
        }

        [Test]
        public void GetRelevantStudiesDB_Female_183_NoEnglish()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);
            Participant participant = new Participant
            {
                Gender = false, //is female
                Age = DateTime.Parse("Jan 1, 1835"), //is 183 years old
                English = true //does not speak english
            };

            //Act - Get relevant studies for this participant.
            var listOfStudies = uut.GetRelevantStudiesDB(participant);

            //Assert - There are no relevant studies for this ol' gal.
            Assert.AreEqual(listOfStudies.Count, 0);
        }
    }
}
