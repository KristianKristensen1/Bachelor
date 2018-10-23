using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BachelorBackEnd;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests
{
    public class LoginHandler_T1_P
    {
        public ILoginHandler uut;
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
            mockSet.As<IQueryable<Participant>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Participant>)participants.GetEnumerator());

            mockContext = new Mock<BachelorBackEnd.bachelordbContext>();
            mockContext.Setup(c => c.Participant).Returns(mockSet.Object);
        }

        [Test]
        public void Always_Green_Test()
        {
            Assert.Pass();
        }

        [Test]
        public void LoginParticipantDB_Login_WithInvalidPassword()
        {
            //Setup

            //Act - trying to log in 
            uut = new LoginHandler(mockContext.Object);
            var actual = uut.LoginParticipantDB("test@testo.com", "bla");

            //Assert - Checking Loginstatus.ErrorMessage.
            Assert.AreEqual(actual.LoginStatus.ErrorMessage, "Wrong password");
        }

        [Test]
        public void LoginParticipantDB_Login_WithInvalidEmail()
        {
            //Setup           

            //Act - trying to log in 
            uut = new LoginHandler(mockContext.Object);
            var actual = uut.LoginParticipantDB("bla", "123456");

            //Assert - Checking Loginstatus.ErrorMessage.
            Assert.AreEqual(actual.LoginStatus.ErrorMessage, "No participant with this email exists");
        }

        [Test]
        public void LoginParticipantDB_Login_WithValidUser()
        {
            //Setup            

            //Act - trying to log in 
            uut = new LoginHandler(mockContext.Object);
            var actual = uut.LoginParticipantDB("test@testo.com", "123456");

            //Assert - Checking Loginstatus.IsSuccess.
            Assert.IsTrue(actual.LoginStatus.IsSuccess);
        }

        [Test]
        public void LoginParticipantDB_Login_WithInValidUser()
        {
            //Setup            

            //Act - trying to log in 
            uut = new LoginHandler(mockContext.Object);
            var actual = uut.LoginParticipantDB("bla", "bla");

            //Assert - Checking Loginstatus.IsSuccess.
            Assert.IsFalse(actual.LoginStatus.IsSuccess);
        }
    }
}