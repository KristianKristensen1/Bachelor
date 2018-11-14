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
    public class T5_UserHandler_P
    {
        public IUserHandler uut;
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
                .Returns((IEnumerator<Participant>)participants.GetEnumerator());

            mockContext = new Mock<BachelorBackEnd.bachelordbContext>();
            mockContext.Setup(c => c.Participant).Returns(mockSet.Object);
        }

        [Test]
        public void getParticipant_WithValidInput_EmailsCorrect()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curparticipant = uut.GetParticipantDB(0);

            Assert.AreEqual(curparticipant.Email, "test@testo.com");

        }
        [Test]
        public void getParticipant_WithValidInput_PasswordIsCorrect()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curparticipant = uut.GetParticipantDB(0);

            Assert.AreEqual(curparticipant.Password, "123456");

        }

        [Test]
        public void getParticipant_WithValidInput_ParticipantIsNotNull()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curparticipant = uut.GetParticipantDB(0);

            Assert.That(curparticipant!=null);

        }
        [Test]
        public void getParticipant_WithInvalidInput_ParticipantIsNull()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curparticipant = uut.GetParticipantDB(1);

           Assert.That(curparticipant==null);

        }
    }
}
