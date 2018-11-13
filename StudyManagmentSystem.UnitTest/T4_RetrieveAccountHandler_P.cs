using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BachelorBackEnd;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using StudyManagementSystem.DAOImplementations;
using StudyManagementSystem.DAOInterfaces;

namespace Tests
{
    public class T4_RetrieveAccountHandler_P
    {
        public IRetrieveAccountHandler uut;
        public IQueryable participants;
        public Mock<DbSet<Participant>> mockSet;
        public Mock<BachelorBackEnd.bachelordbContext> mockContext;

        [SetUp]
        public void Setup()
        {
            // Participant Setup
            participants = new List<Participant>
            {
                new Participant
                {
                    Email = "test@testo.com",
                    Age = DateTime.Now,
                    English = true,
                    Gender = true,
                    IdParticipant = 0,
                    Password = "123456"
                }
            }.AsQueryable();

            // Required to do this. If not the "mock" does not recognize "part" in uut.RegisterParticipantDB
            mockSet = new Mock<DbSet<Participant>>();
            mockSet.As<IQueryable<Participant>>().Setup(m => m.Provider).Returns(participants.Provider);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.Expression).Returns(participants.Expression);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.ElementType).Returns(participants.ElementType);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.GetEnumerator())
                .Returns((IEnumerator<Participant>) participants.GetEnumerator());

            mockContext = new Mock<BachelorBackEnd.bachelordbContext>();
            mockContext.Setup(c => c.Participant).Returns(mockSet.Object);
        }


        [Test]
        public void Check_VerifyParticipantDB_WithValidEmail_ResultIsCorrect()
        {
            uut = new RetrieveAccountHandler(mockContext.Object);
            var handler= uut.VerifyParticipantDB("test@testo.com");

            Assert.IsTrue(handler.LoginStatus.IsSuccess);

        }

        [Test]
        public void Check_VerifyParticipant_WithInvalidEmail_ErrorMessage()
        {
            uut = new RetrieveAccountHandler(mockContext.Object);
            var handler = uut.VerifyParticipantDB("different@testo.com");

            Assert.AreEqual(handler.LoginStatus.ErrorMessage, "No participant with this email exists");

        }

        [Test]
        public void Check_VerifyParticipant_WithInvalidEmail_FalseBool()
        {
            //Act
            uut = new RetrieveAccountHandler(mockContext.Object);
            var handler = uut.VerifyParticipantDB("different@testo.com");

            //Assert
            Assert.IsFalse(handler.LoginStatus.IsSuccess);

        }
    }
}