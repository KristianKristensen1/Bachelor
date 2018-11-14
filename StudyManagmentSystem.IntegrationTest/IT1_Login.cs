using System;
using System.Collections.Generic;
using System.Linq;
using BachelorBackEnd;
using FrontEndBA.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace StudyManagmentSystem.IntegrationTest
{
   
    public class IT1_LoginHandler
    {
        //public ILoginHandler uut;
        //public IQueryable participants;
        //public Mock<DbSet<Participant>> mockSet;
        //public Mock<BachelorBackEnd.bachelordbContext> mockContext;
        //public WelcomeController 
        //[SetUp]
        //public void Setup()
        //{
        //    // Participant Setup
        //    participants = new List<Participant>
        //    {
        //        new Participant
        //        {
        //            Email = "test@testo.com",
        //            Age = DateTime.Now,
        //            English = true,
        //            Gender = true,
        //            IdParticipant = 0,
        //            Password = "123456"
        //        }
        //    }.AsQueryable();

        //    // Required to do this. If not the "mock" does not recognize "part" in uut.RegisterParticipantDB
        //    mockSet = new Mock<DbSet<Participant>>();
        //    mockSet.As<IQueryable<Participant>>().Setup(m => m.Provider).Returns(participants.Provider);
        //    mockSet.As<IQueryable<Participant>>().Setup(m => m.Expression).Returns(participants.Expression);
        //    mockSet.As<IQueryable<Participant>>().Setup(m => m.ElementType).Returns(participants.ElementType);
        //    mockSet.As<IQueryable<Participant>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Participant>)participants.GetEnumerator());

        //    mockContext = new Mock<BachelorBackEnd.bachelordbContext>();
        //    mockContext.Setup(c => c.Participant).Returns(mockSet.Object);
        //}

        //[Test]
        //public void LoginHandlerTry()
        //{

        //}
    }
}
