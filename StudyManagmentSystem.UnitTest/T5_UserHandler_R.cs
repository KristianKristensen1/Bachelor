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
                    Isverified = true
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
        public void getResearcher_WithValidId_EmailsCorrect()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curResearcher = uut.GetResearcherDB(0);

            Assert.AreEqual(curResearcher.Email, "test@testo.com");

        }
        [Test]
        public void getResearcher_WithValidId_PasswordIsCorrect()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curResearcher = uut.GetResearcherDB(0);

            Assert.AreEqual(curResearcher.Password, "123456");

        }

        [Test]
        public void getResearcher_WithValidId_ResearcherIsNotNull()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curResearcher = uut.GetResearcherDB(0);

            Assert.That(curResearcher != null);

        }
        [Test]
        public void getResearcher_WithInvalidId_ResearcherIsNull()
        {
            //Act
            uut = new UserHandler(mockContext.Object);
            var curResearcher = uut.GetResearcherDB(1);

            Assert.That(curResearcher == null);

        }

     
    }
}
