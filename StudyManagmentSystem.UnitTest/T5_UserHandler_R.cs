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
    public class T5_UserHandler_R
    {
        public IUserHandler uut;
        public IQueryable researchers;
        public Mock<DbSet<Researcher>> mockSet;
        public Mock<BachelorBackEnd.bachelordbContext> mockContext;

        [SetUp]
        public void Setup()
        {
            // Participant Setup
            researchers = new List<Researcher>
            {
                new Researcher
                {
                    Email = "test@testo.com",
                    Password = "123456",
                    FirstName = "James",
                    LastName = "Bond",
                    Isverified = true,
                },
                new Researcher
                {
                    Email = "test2@testo.com",
                    Password = "123456",
                    FirstName = "James2",
                    LastName = "Bond2",
                    Isverified = false,
                    IdResearcher = 1,
                   
                }
            }.AsQueryable();

           
            mockSet = new Mock<DbSet<Researcher>>();
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Provider).Returns(researchers.Provider);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Expression).Returns(researchers.Expression);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.ElementType).Returns(researchers.ElementType);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.GetEnumerator())
                .Returns((IEnumerator<Researcher>)researchers.GetEnumerator());

            mockContext = new Mock<BachelorBackEnd.bachelordbContext>();
            mockContext.Setup(c => c.Researcher).Returns(mockSet.Object);
        
        }

        [Test]
        public void getResearcher_WithValidInput_EmailsCorrect()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curResearcher = uut.GetResearcherDB(0);

            Assert.AreEqual(curResearcher.Email, "test@testo.com");

        }
        [Test]
        public void getResearcher_WithValidInput_PasswordIsCorrect()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curResearcher = uut.GetResearcherDB(0);

            Assert.AreEqual(curResearcher.Password, "123456");

        }

        [Test]
        public void getResearcher_WithValidInput_ResearcherIsNotNull()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curResearcher = uut.GetResearcherDB(0);

            Assert.That(curResearcher != null);

        }
        [Test]
        public void getResearcher_WithInvalidInput_ResearcherIsNull()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curResearcher = uut.GetResearcherDB(10);

            Assert.That(curResearcher == null);

        }

        [Test]
        public void getUnverifiedResearchersDB_WithInvalidInput_SuccesIsFalse()
        {
            uut= new UserHandler(mockContext.Object);
            var curResearcher = uut.UnverifyResearcherDB(1);
            Assert.IsFalse(curResearcher.success);
        }

        [Test]
        public void getUnverifiedResearchersDB_WithValidInput_SuccesIsTrue()
        {
            uut = new UserHandler(mockContext.Object);
            var curResearcher = uut.UnverifyResearcherDB(0);
            Assert.True(curResearcher.success);
        }

        [Test]
        public void GetAllResearchersDB_WithValidInput_ResulstIsCorrect()
        {
            uut = new UserHandler(mockContext.Object);
            List<Researcher> curResearcher = uut.GetAllVerifiedResearchersDB();
            Assert.AreEqual(curResearcher.Count,1);
        }

        [Test]
        public void GetAllResearchersDB_WithValidInput_ResulstIsNotCorrect()
        {
            uut = new UserHandler(mockContext.Object);
            List<Researcher> curResearcher = uut.GetAllVerifiedResearchersDB();
            Assert.AreNotEqual(curResearcher.Count, 2);
        }
        //Verify Researcher

        [Test]
        public void VerifyAResearcherDB_WithValidInput_SuccesIsTrue()
        {
            uut = new UserHandler(mockContext.Object);
            var dbresponse = uut.VerifyResearcherDB(1);

            Assert.IsTrue(dbresponse.success);
        }

        [Test]
        public void VerifyAResearcherDB_WithInvalidInput_ResearcherDoesNotExsists()
        {
            uut = new UserHandler(mockContext.Object);
            var dbresponse = uut.VerifyResearcherDB(10);

            Assert.AreEqual(dbresponse.errormessage, "Researcher with this ID does not exists");
        }

        [Test]
        public void VerifyAResearcherDB_WithInvalidInput_SuccesIsFalse()
        {
            uut = new UserHandler(mockContext.Object);
            var dbresponse = uut.VerifyResearcherDB(0);

            Assert.IsFalse(dbresponse.success);
        }

        [Test]
        public void VerifyAResearcherDB_WithInvalidInput_ResearcherIsAllReadyVerified()
        {
            uut = new UserHandler(mockContext.Object);
            var dbresponse = uut.VerifyResearcherDB(0);

            Assert.AreEqual(dbresponse.errormessage, "Researcher is all ready verified");
        }

        //Unverify Researcher
        [Test]
        public void UnverifyResearcherDB_WithValidInput_SuccesIsTrue()
        {
            uut = new UserHandler(mockContext.Object);
            var dbresponse = uut.UnverifyResearcherDB(0);

            Assert.IsTrue(dbresponse.success);
        }

        [Test]
        public void UnverifyResearcherDB_WithInvalidInput_ResearcherDoesNotExsists()
        {
            uut = new UserHandler(mockContext.Object);
            var dbresponse = uut.UnverifyResearcherDB(10);

            Assert.AreEqual(dbresponse.errormessage, "Researcher with this ID does not exists");
        }

        [Test]
        public void UnverifyResearcherDB_WithInvalidInput_SuccesIsFalse()
        {
            uut = new UserHandler(mockContext.Object);
            var dbresponse = uut.UnverifyResearcherDB(1);

            Assert.IsFalse(dbresponse.success);
        }

        [Test]
        public void UnverifyResearcherDB_WithInvalidInput_ResearcherIsAllReadyVerified()
        {
            uut = new UserHandler(mockContext.Object);
            var dbresponse = uut.UnverifyResearcherDB(1);

            Assert.AreEqual(dbresponse.errormessage, "Researcher is not verified");
        }

    }
}
