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
    public class T8_ManageProfileHandler_R
    {
        public IManageProfileHandler uut;
        public IQueryable researchers;
        public Mock<DbSet<Researcher>> mockSetResearchers;
        public Mock<bachelordbContext> mockContext;

        [SetUp]
        public void SetUp()
        {

            researchers = new List<Researcher>
            {
                new Researcher
                {
                Email = "test@register.com",
                FirstName = "Test1",
                LastName = "Testesen",
                Password = "123456",
                IdResearcher = 0,
                },
                new Researcher
                {
                Email = "test2@register.com",
                FirstName = "Test2",
                LastName = "Testesen",
                Password = "789012",
                IdResearcher = 1,
                }
            }.AsQueryable();

            mockSetResearchers = new Mock<DbSet<Researcher>>();
            mockSetResearchers.As<IQueryable<Researcher>>().Setup(m => m.Provider).Returns(researchers.Provider);
            mockSetResearchers.As<IQueryable<Researcher>>().Setup(m => m.Expression).Returns(researchers.Expression);
            mockSetResearchers.As<IQueryable<Researcher>>().Setup(m => m.ElementType).Returns(researchers.ElementType);
            mockSetResearchers.As<IQueryable<Researcher>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Researcher>)researchers.GetEnumerator());

            mockContext = new Mock<bachelordbContext>();
            mockContext.Setup(c => c.Researcher).Returns(mockSetResearchers.Object);
        }

        [Test]
        public void ChangeProfileResearcher_ChangeEmail()
        {
            //Arrange
            uut = new ManageProfileHandler(mockContext.Object);
            Researcher researcher = new Researcher
            {
                Email = "changed@changed.com",
                FirstName = "Test1",
                LastName = "Testesen",
                Password = "123456",
                IdResearcher = 0,
            };

            //Act
            uut.ChangeProfileResearcherDB(researcher);
            Researcher newResearcher = mockContext.Object.Researcher.FirstOrDefault(res => res.IdResearcher == researcher.IdResearcher);

            //Assert
            Assert.That(newResearcher.Email == researcher.Email);
        }

        [Test]
        public void ChangeProfileResearcher_ChangeFirstname()
        {
            //Arrange
            uut = new ManageProfileHandler(mockContext.Object);
            Researcher researcher = new Researcher
            {
                Email = "test@register.com",
                FirstName = "changedName",
                LastName = "Testesen",
                Password = "123456",
                IdResearcher = 0,
            };

            //Act
            uut.ChangeProfileResearcherDB(researcher);
            Researcher newResearcher = mockContext.Object.Researcher.FirstOrDefault(res => res.IdResearcher == researcher.IdResearcher);

            //Assert
            Assert.That(newResearcher.FirstName == researcher.FirstName);
        }

        [Test]
        public void ChangeProfileResearcher_ChangeLastname()
        {
            //Arrange
            uut = new ManageProfileHandler(mockContext.Object);
            Researcher researcher = new Researcher
            {
                Email = "test@register.com",
                FirstName = "Test1",
                LastName = "ChangedLastname",
                Password = "123456",
                IdResearcher = 0,
            };

            //Act
            uut.ChangeProfileResearcherDB(researcher);
            Researcher newResearcher = mockContext.Object.Researcher.FirstOrDefault(res => res.IdResearcher == researcher.IdResearcher);

            //Assert
            Assert.That(newResearcher.LastName == researcher.LastName);
        }

        [Test]
        public void ChangePasswordResearcher_ValidOldPassword()
        {
            //Arrange
            uut = new ManageProfileHandler(mockContext.Object);
            Researcher researcher = new Researcher
            {
                Email = "test@register.com",
                FirstName = "Test1",
                LastName = "ChangedLastname",
                Password = "newPassword",
                IdResearcher = 0,
            };

            string oldPassword = "123456";

            //Act
            DbStatus status = uut.ChangePasswordResearcherDB(researcher, oldPassword);
            Researcher newResearcher = mockContext.Object.Researcher.FirstOrDefault(res => res.IdResearcher == researcher.IdResearcher);

            //Assert
            Assert.That(newResearcher.Password == researcher.Password);
            Assert.That(status.success == true);
        }

        [Test]
        public void ChangePasswordResearcher_InvalidOldPassword()
        {
            //Arrange
            uut = new ManageProfileHandler(mockContext.Object);
            Researcher researcher = new Researcher
            {
                Email = "test@register.com",
                FirstName = "Test1",
                LastName = "ChangedLastname",
                Password = "newPassword",
                IdResearcher = 0,
            };

            string oldPassword = "WrongPassword";

            //Act
            DbStatus status = uut.ChangePasswordResearcherDB(researcher, oldPassword);
            Researcher newResearcher = mockContext.Object.Researcher.FirstOrDefault(res => res.IdResearcher == researcher.IdResearcher);

            //Assert
            Assert.That(newResearcher.Password != researcher.Password);
            Assert.That(status.success == false);
            Assert.That(status.errormessage == "The old password was incorrect. Please try again");
        }
    }
}
