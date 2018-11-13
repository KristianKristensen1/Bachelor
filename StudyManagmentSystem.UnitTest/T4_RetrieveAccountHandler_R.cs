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
    public class T4_RetrieveAccountHandler_R
    {
        public IRetrieveAccountHandler uut;
        public IQueryable participants;
        public Mock<DbSet<Researcher>> mockSet;
        public Mock<BachelorBackEnd.bachelordbContext> mockContext;

        [SetUp]
        public void Setup()
        {
            // Participant Setup
            participants = new List<Researcher>
            {
                new Researcher
                {
                    Email = "test@testo.com",
                    Password = "123456",
                    FirstName = "James",
                    LastName = "Bond",
                    Isverified = true
                }
            }.AsQueryable();

            // Required to do this. If not the "mock" does not recognize "part" in uut.RegisterParticipantDB
            mockSet = new Mock<DbSet<Researcher>>();
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Provider).Returns(participants.Provider);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Expression).Returns(participants.Expression);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.ElementType).Returns(participants.ElementType);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.GetEnumerator())
                .Returns((IEnumerator<Researcher>)participants.GetEnumerator());

            mockContext = new Mock<BachelorBackEnd.bachelordbContext>();
            mockContext.Setup(c => c.Researcher).Returns(mockSet.Object);
        }


        [Test]
        public void Check_VerifyResearcherDB_WithValidEmail_ResultIsCorrect()
        {
            //Act
            uut = new RetrieveAccountHandler(mockContext.Object);
            var handler = uut.VerifyResearcherDB("test@testo.com");


            //Assert
            Assert.IsTrue(handler.LoginStatus.IsSuccess);

        }

        [Test]
        public void Check_VerifyResearcherDB_WithInvalidEmail_ErrorMessage()
        {
            //Act
            uut = new RetrieveAccountHandler(mockContext.Object);
            var handler = uut.VerifyResearcherDB("different@testo.com");

            //Assert
            Assert.AreEqual(handler.LoginStatus.ErrorMessage, "No researcher with this email exists");

        }

        [Test]
        public void Check_VerifyResearcherDB_WithInvalidEmail_FalseBool()
        {
            //Act
            uut = new RetrieveAccountHandler(mockContext.Object);
            var handler = uut.VerifyResearcherDB("different@testo.com");

            //Assert
            Assert.IsFalse(handler.LoginStatus.IsSuccess);

        }

    }
}
