using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BachelorBackEnd;
using Microsoft.EntityFrameworkCore;
using StudyManagementSystem.Models;
using Moq;

namespace Tests
{
    public class T2_RegisterHandler_P
    {
        public IRegisterHandler uut;
        public Participant particpant;
        public IQueryable participants;
        public Mock<DbSet<Participant>> mockSet;
        public Mock<BachelorBackEnd.bachelordbContext> mockContext;

        [SetUp]
        public void Setup()
        {
            // Participant Setup
            particpant = new Participant
            {
                Email = "test@register.com",
                Age = DateTime.Now,
                English = true,
                Gender = true,
                IdParticipant = 0,
                Password = "123456"

            };

           participants = new List<Participant>
            {
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
        public void RegisterParticipantDB_AddingParticipant_WithValidInputInDB()
        {
            //Setup

            //Act - Add the participant

            uut = new RegisterHandler(mockContext.Object);
            uut.RegisterParticipantDB(particpant);

            //Assert - Checking and see if we saved our changes. 
            mockSet.Verify(m=>m.Add(It.IsAny<Participant>()),Times.Once);
            mockContext.Verify(m=>m.SaveChanges(),Times.Once);
        }        

        [Test]
        public void RegisterParticipantDB_AddingParticipant_WithValidInputErrorMessage()
        {
            //Setup
            var participant = new Participant
            {
                Email = "test@register.com",
                Age = DateTime.Now,
                English = true,
                Gender = true,
                IdParticipant = 0,
                Password = "123456"

            };

            // Required to do this. If not the "mock" does not recognize "part" in uut.RegisterParticipantDB
            IQueryable participants = new List<Participant>
            {
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Participant>>();
            mockSet.As<IQueryable<Participant>>().Setup(m => m.Provider).Returns(participants.Provider);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.Expression).Returns(participants.Expression);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.ElementType).Returns(participants.ElementType);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Participant>)participants.GetEnumerator());
            // Required to do this. If not the "mock" does not recognize "part" in uut.RegisterParticipantDB
            var mockContext = new Mock<BachelorBackEnd.bachelordbContext>();
            mockContext.Setup(c => c.Participant).Returns(mockSet.Object);

            //Act - Add the participant
            uut = new RegisterHandler(mockContext.Object);
            uut.RegisterParticipantDB(participant);

            //Assert - Checking and see if we saved our changes. 
            mockSet.Verify(m => m.Add(It.IsAny<Participant>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);

            //Assert.AreEqual(ErrorMessage, "No problems");
        }
    }
}