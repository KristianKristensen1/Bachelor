using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BachelorBackEnd;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class T7_MangaParticipantHandler_P
    {
        public IManageParticipantHandler uut;
        public IQueryable studyparticipant;
        public IQueryable participants;
        public List<Participant> sparticipants;
        public List<Study> sstudies;
        public Mock<DbSet<Studyparticipant>> mockStudyParticipant;
        public Mock<DbSet<Participant>> mockParticipant;
        public Mock<BachelorBackEnd.bachelordbContext> mockContext;

        [SetUp]
        public void Setup()
        {
            //StudyParticipant Setup
            sparticipants = new List<Participant>
            {
                new Participant
                {
                    Email = "test@testo.com",
                    Age = DateTime.Now,
                    English = true,
                    Gender = true,
                    IdParticipant = 1,
                    Password = "123456"
                }
            };

            sstudies = new List<Study>
            {
                    new Study
                    {
                        Name = "New Study for people!",
                        IdStudy = 1,
                        Description = "Ladies and gentlemen, this is study no. 5",
                        Isdraft = false,
                        IdResearcher = 1,
                    }
            };
            
            studyparticipant = new List<Studyparticipant>
            {
                new Studyparticipant
                {
                    IdParticipant = 1,
                    IdStudy =1,
                    IdStudyParticipant = 1,
                    IdParticipantNavigation = sparticipants[0],
                    IdStudyNavigation = sstudies[0]

                   
                
                }
               
            }.AsQueryable();

            // Participant Setup
            participants = new List<Participant>
            {
                new Participant
                {
                    Email = "test@testo.com",
                    Age = DateTime.Now,
                    English = true,
                    Gender = true,
                    IdParticipant = 1,
                    Password = "123456"
                }
            }.AsQueryable();

            // Required to do this. If not the "mock" does not recognize "part" in uut.RegisterParticipantDB

            mockContext = new Mock<BachelorBackEnd.bachelordbContext>();

            //Participant setup db
            mockParticipant = new Mock<DbSet<Participant>>();
            mockParticipant.As<IQueryable<Participant>>().Setup(m => m.Provider).Returns(participants.Provider);
            mockParticipant.As<IQueryable<Participant>>().Setup(m => m.Expression).Returns(participants.Expression);
            mockParticipant.As<IQueryable<Participant>>().Setup(m => m.ElementType).Returns(participants.ElementType);
            mockParticipant.As<IQueryable<Participant>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Participant>)participants.GetEnumerator());
            mockContext.Setup(c => c.Participant).Returns(mockParticipant.Object);


            //StudyParticipant setup db
            mockStudyParticipant = new Mock<DbSet<Studyparticipant>>();
            mockStudyParticipant.As<IQueryable<Studyparticipant>>().Setup(m => m.Provider).Returns(studyparticipant.Provider);
            mockStudyParticipant.As<IQueryable<Studyparticipant>>().Setup(m => m.Expression).Returns(studyparticipant.Expression);
            mockStudyParticipant.As<IQueryable<Studyparticipant>>().Setup(m => m.ElementType).Returns(studyparticipant.ElementType);
            mockStudyParticipant.As<IQueryable<Studyparticipant>>().Setup(m => m.GetEnumerator())
                .Returns((IEnumerator<Studyparticipant>)studyparticipant.GetEnumerator());

           // setup mockContext
            mockContext.Setup(c => c.Studyparticipant).Returns(mockStudyParticipant.Object);

        }

        //Add Participant to Study
        [Test]
        public void AddParticipantToStudyDB_WithValidInput_SuccesIsTrue()
        {
            //mockContext.Object.Participant.Add(participants[0]);
            uut = new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.AddParticipantToStudyDB(1, 5);

            Assert.IsTrue(dbrecept.success);
        }

        [Test]
        public void AddParticipantToStudyDB_WithValidInput_SuccesIsFalse()
        {
            //mockContext.Object.Participant.Add(participants[0]);
            uut = new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.AddParticipantToStudyDB(2, 5);

            Assert.False(dbrecept.success);
        }
        [Test]
        public void AddParticipantToStudyDB_WithValidInput_ParticipantIDdoesnotexistinthesystem()
        {
            //mockContext.Object.Participant.Add(participants[0]);
            uut = new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.AddParticipantToStudyDB(2, 5);

            Assert.AreEqual(dbrecept.errormessage, "Participant with this ID does not exist in the system");
        }
        [Test]
        public void AddParticipantToStudyDB_WithValidInput_Participantallreadyenrolledinstudy()
        {
            //mockContext.Object.Participant.Add(participants[0]);
            uut = new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.AddParticipantToStudyDB(1, 1);

            Assert.AreEqual(dbrecept.errormessage, "Participant is all ready enrolled in study");
        }

        // RemoveParticipantFromStudyDB

        [Test]
        public void RemoveParticipantFromStudyDB_WithValidInput_SuccesIstrue()
        {
            uut = new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.RemoveParticipantFromStudyDB(1, 1);

            Assert.IsTrue(dbrecept.success);
        }

        [Test]
        public void RemoveParticipantFromStudyDB_WithInvalidInput_Participantisnotenrolledinstudy()
        {
            uut = new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.RemoveParticipantFromStudyDB(10, 1);

            Assert.AreEqual(dbrecept.errormessage, "Participant is not enrolled in study");
        }

        [Test]
        public void RemoveParticipantFromStudyDB_WithInvalidInput_SuccesIsFalse()
        {
            uut = new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.RemoveParticipantFromStudyDB(10, 1);

            Assert.IsFalse(dbrecept.success);
        }

        // GetParticipantsInStudyDB

        [Test]
        public void GetParticipantsInStudyDB_WithhValidInput_ResulstIsCorrect()
        {
            uut= new ManageParticipantHandler(mockContext.Object);
            var partList = uut.GetParticipantsInStudyDB(1);

            Assert.AreEqual(partList[0].Email, "test@testo.com");
        }

        [Test]
        public void GetParticipantsInStudyDB_WithInvalidInput_ResulstIsNotCorrect()
        {
            uut = new ManageParticipantHandler(mockContext.Object);
            var partList = uut.GetParticipantsInStudyDB(10);

            Assert.That(partList.Count==0);
        }


        // GetParticipantsEmailDB
        [Test]
        public void GetParticipantEmailDB_WithValidInput_SuccesIsTrue()
        {
            uut= new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.GetParticipantEmailDB(1);

            Assert.IsTrue(dbrecept.success);
        }

        [Test]
        public void GetParticipantEmailDB_WithValidInput_EmailIsCorrrect()
        {
            uut = new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.GetParticipantEmailDB(1);

            Assert.AreEqual(dbrecept.participantEmail, "test@testo.com");
        }

        [Test]
        public void GetParticipantEmailDB_WithInvalidInput_SuccesIsFalse()
        {
            uut = new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.GetParticipantEmailDB(10);

            Assert.IsFalse(dbrecept.success);
        }

        [Test]
        public void GetParticipantEmailDB_WithInvalidInput_EmailIsNotCorrect()
        {
            uut = new ManageParticipantHandler(mockContext.Object);
            var dbrecept = uut.GetParticipantEmailDB(10);

            Assert.AreNotEqual(dbrecept.participantEmail,"test@testo.com");
        }

    }
}
