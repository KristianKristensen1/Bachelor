using BachelorBackEnd;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    public class T6_ManageStudyHandler
    {
        public IManageStudyHandler uut;
        //public IQueryable studies;
        public Mock<bachelordbContext> mockContext;
        public Mock<DbSet<Study>> mockStudySet;
        public Mock<DbSet<Inclusioncriteria>> mockInclusionCriteriaSet;
        public List<Study> studiesCollection;
        public List<Inclusioncriteria> inclusionCriteriaCollection;


        [SetUp]
        public void SetUp()
        {
            studiesCollection = new List<Study>
            {
                new Study
                {
                    Name = "New Study for people!",
                    IdStudy = 0,
                    Description = "Ladies and gentlemen, this is study no. 5",
                    Isdraft = false,
                    IdResearcher = 1,
                },
                new Study
                {
                    Name = "Not for everyone!",
                    IdStudy = 1,
                    Description = "Not as great as study no. 5",
                    Isdraft = false,
                    IdResearcher = 1,
                },
                new Study
                {
                    Name = "Terrible Study!",
                    IdStudy = 2,
                    Description = "Stay away!",
                    Isdraft = false,
                    IdResearcher = 3,
                }
            };
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
                    IdStudy = 0
                },
                new Inclusioncriteria
                {
                    IdInclusionCriteria = 2,
                    Male = true,
                    Female = false,
                    MinAge = 20,
                    MaxAge = 39,
                    English = false,
                    IdStudy = 1
                },
                new Inclusioncriteria
                {
                    IdInclusionCriteria = 3,
                    Male = false,
                    Female = true,
                    MinAge = 40,
                    MaxAge = 59,
                    English = false,
                    IdStudy = 2
                }
            };

            var QueryableStudies = studiesCollection.AsQueryable();
            var QueryableInclusioncriteria = inclusionCriteriaCollection.AsQueryable();

            mockContext = new Mock<bachelordbContext>();

            mockStudySet = new Mock<DbSet<Study>>();
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.Provider).Returns(QueryableStudies.Provider);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.Expression).Returns(QueryableStudies.Expression);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.ElementType).Returns(QueryableStudies.ElementType);
            mockStudySet.As<IQueryable<Study>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Study>)QueryableStudies.GetEnumerator());
            mockStudySet.Setup(d => d.Add(It.IsAny<Study>())).Callback<Study>((s) => studiesCollection.Add(s));
            mockStudySet.Setup(d => d.Remove(It.IsAny<Study>())).Callback<Study>((s) => studiesCollection.Remove(s));
            mockContext.Setup(s => s.Study).Returns(mockStudySet.Object);

            mockInclusionCriteriaSet = new Mock<DbSet<Inclusioncriteria>>();
            mockInclusionCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.Provider).Returns(QueryableInclusioncriteria.Provider);
            mockInclusionCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.Expression).Returns(QueryableInclusioncriteria.Expression);
            mockInclusionCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.ElementType).Returns(QueryableInclusioncriteria.ElementType);
            mockInclusionCriteriaSet.As<IQueryable<Inclusioncriteria>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Inclusioncriteria>)QueryableInclusioncriteria.GetEnumerator());
            mockInclusionCriteriaSet.Setup(d => d.Add(It.IsAny<Inclusioncriteria>())).Callback<Inclusioncriteria>((s) => inclusionCriteriaCollection.Add(s));
            mockInclusionCriteriaSet.Setup(d => d.Remove(It.IsAny<Inclusioncriteria>())).Callback<Inclusioncriteria>((s) => inclusionCriteriaCollection.Remove(s));
            mockContext.Setup(c => c.Inclusioncriteria).Returns(mockInclusionCriteriaSet.Object);
        }

        [Test]
        public void EditStudy()
        {
            //Arrange
            Study studyEdited = new Study
            {
                Name = "Edited name",
                IdStudy = 1,
                Description = "Ladies and gentlemen, this is study no. 5, edited",
                Isdraft = false,
                IdResearcher = 1,
                Abstract = "The abstract",
                Pay = 1000,
                Duration = "2 sessions of 10 min",
                DirectStudyLink = "ThisIsTheLink",
                DateCreated = DateTime.Now,
                Location = "Right here",
            };
            Inclusioncriteria inclusioncriteriaEdited = new Inclusioncriteria
            {
                Male = false,
                Female = false,
                English = false,
                MinAge = 10,
                MaxAge = 100,
            };

            //Act
            uut = new ManageStudyHandler(mockContext.Object);
            uut.EditStudyDB(studyEdited, inclusioncriteriaEdited);

            Study study = mockStudySet.Object.FirstOrDefault(s => s.IdStudy == 1);
            Inclusioncriteria inclusioncriteria = mockInclusionCriteriaSet.Object.FirstOrDefault(inc => inc.IdStudy == 1);

            //Assert
            Assert.That(study.Name == "Edited name");
            Assert.That(study.Description == "Ladies and gentlemen, this is study no. 5, edited");
            Assert.That(inclusioncriteria.Male == false);
            Assert.That(inclusioncriteria.Female == false);
            Assert.That(inclusioncriteria.English == false);
        }

        [Test]
        public void GetStudy()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);

            //Act
            Study study = uut.GetStudyDB(1);

            //Assert
            Assert.That(study == studiesCollection[1]);
        }

        [Test]
        public void CreateStudy()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);

            //New study to be created
            Study newStudy = new Study
            {
                Name = "New study",
                Description = "The newest study!",
                Isdraft = false,
                IdResearcher = 1,
                Abstract = "The abstract",
                Pay = 1000,
                Duration = "2 sessions of 10 min",
                DirectStudyLink = "ThisIsTheLink",
                DateCreated = DateTime.Now,
                Location = "Right here",
                IdStudy = 10,

            };
            //Inclusion criteria to be created
            Inclusioncriteria newInclusioncriteria = new Inclusioncriteria
            {
                Male = true,
                Female = false,
                English = false,
                MinAge = 10,
                MaxAge = 100,
            };

            //Act
            uut.CreateStudyDB(newStudy, newInclusioncriteria);
            Study study = mockStudySet.Object.FirstOrDefault(stud => stud.IdStudy == newStudy.IdStudy);
            Inclusioncriteria inclusioncriteria = mockInclusionCriteriaSet.Object.FirstOrDefault(inc => inc.IdStudy == newStudy.IdStudy);

            //Assert
            Assert.That(study == newStudy);
            Assert.That(inclusioncriteria == newInclusioncriteria);
        }

        [Test]
        public void DeleteStudy()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);

            //Act
            uut.DeleteStudyDB(1);

            //Assert
            Assert.AreEqual(mockContext.Object.Study.Count(), 2);
            Assert.That(mockContext.Object.Study.FirstOrDefault(stud => stud.IdStudy == 1) == null);
        }

        [Test]
        public void GetInclusioncriteria()
        {
            //Arrange
            uut = new ManageStudyHandler(mockContext.Object);

            //Act
            Inclusioncriteria inclusioncriteria = uut.GetInclusioncriteriaDB(1);

            //Assert
            Assert.That(inclusioncriteria == inclusionCriteriaCollection[1]);
        }
    }
}
