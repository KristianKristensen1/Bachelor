using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BachelorBackEnd;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests
{
    // Inspiration: https://entityframework.net/how-to-mock-data, https://medium.com/@metse/entity-framework-core-unit-testing-3c412a0a997c
    public class T2_RegisterHandler_R
    {
        public IRegisterHandler uut;
        public Researcher researcher;
        public IQueryable researchers;
        public Mock<DbSet<Researcher>> mockSet;
        public Mock<BachelorBackEnd.bachelordbContext> mockContext;

        [SetUp]
        public void Setup()
        {
            researcher = new Researcher()
            {
                Email = "test@register.com",
                Password = "123456"
            };

            researchers = new List<Researcher>
            {
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
        public void RegisterResearcherDB_AddingResearcher_WithValidInputInDB()
        {
            //Setup           

            //Act - Add the participant
            uut = new RegisterHandler(mockContext.Object);
            uut.RegisterResearcherDB(researcher);

            //Assert - Checking and see if we saved our changes. 
            mockSet.Verify(m => m.Add(It.IsAny<Researcher>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }        

        [Test]
        public void RegisterResearcherDB_AddingResearcher_WithValidInputErrorMessage()
        {
            //Setup            

            //Act - Add the participant
            uut = new RegisterHandler(mockContext.Object);
            uut.RegisterResearcherDB(researcher);

            //Assert - Checking and see if we saved our changes. 
            mockSet.Verify(m => m.Add(It.IsAny<Researcher>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);

            //Assert.AreEqual(ErrorMessage, "No problems");
        }
    }
}