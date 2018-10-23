using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BachelorBackEnd;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests
{
    public class LoginHandler_T1_R
    {
        public ILoginHandler uut;
        public Researcher researcher;
        public IQueryable researchers;
        public Mock<DbSet<Researcher>> mockSet;
        public Mock<BachelorBackEnd.bachelordbContext> mockContext;
        [SetUp]
        public void Setup()
        {


            researchers = new List<Researcher>
            {
                new Researcher
                {
                Email = "test@testo.com",
                Password = "123456"
                }
             
            }.AsQueryable();

            // Required to do this. If not the "mock" does not recognize "part" in uut.RegisterParticipantDB
            mockSet = new Mock<DbSet<Researcher>>();
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Provider).Returns(researchers.Provider);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Expression).Returns(researchers.Expression);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.ElementType).Returns(researchers.ElementType);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Researcher>)researchers.GetEnumerator());

            mockContext = new Mock<BachelorBackEnd.bachelordbContext>();
            mockContext.Setup(c => c.Researcher).Returns(mockSet.Object);
        }

        [Test]
        public void LoginResearcherDB_Login_WithInvalidPassword()
        {
            //Setup

            //Act - trying to log in 
            uut = new LoginHandler(mockContext.Object);
            var actual = uut.LoginResearcherDB("test@testo.com", "bla");


            //Assert - Checking Loginstatus.ErrorMessage.
            Assert.AreEqual(actual.LoginStatus.ErrorMessage, "Wrong password");


        }

        [Test]
        public void LoginResearcherDB_Login_WithInvalidEmail()
        {
            //Setup


            //Act - trying to log in 
            uut = new LoginHandler(mockContext.Object);
            var actual = uut.LoginResearcherDB("bla", "123456");


            //Assert - Checking Loginstatus.ErrorMessage.
            Assert.AreEqual(actual.LoginStatus.ErrorMessage, "No researcher with this email exists");


        }

        [Test]
        public void LoginResearcherDB_Login_WithValidUser()
        {
            //Setup


            //Act - trying to log in 
            uut = new LoginHandler(mockContext.Object);
            var actual = uut.LoginResearcherDB("test@testo.com", "123456");


            //Assert - Checking Loginstatus.IsSuccess.
            Assert.IsTrue(actual.LoginStatus.IsSuccess);


        }

        [Test]
        public void LoginParticipantDB_Login_WithInValidUser()
        {
            //Setup


            //Act - trying to log in 
            uut = new LoginHandler(mockContext.Object);
            var actual = uut.LoginResearcherDB("bla", "bla");


            //Assert - Checking Loginstatus.IsSuccess.
            Assert.IsFalse(actual.LoginStatus.IsSuccess);


        }

    }
}