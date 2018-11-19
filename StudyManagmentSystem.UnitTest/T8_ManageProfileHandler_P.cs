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
    public class T8_ManageProfileHandler_P
    {
        public IManageProfileHandler uut;
        public Mock<DbSet<Participant>> mockSetParticipants;
        public Mock<bachelordbContext> mockContext;
        public List<Participant> participantCollection;


        [SetUp]
        public void SetUp()
        {
            participantCollection = new List<Participant>
            {
                new Participant
                {
                Email = "test@register.com",
                Age = DateTime.Now.Date,
                English = true,
                Gender = true,
                IdParticipant = 0,
                Password = "123456"
                },
                new Participant
                {
                Email = "test2@register.com",
                Age = DateTime.Now.Date,
                English = false,
                Gender = false,
                IdParticipant = 1,
                Password = "789012"
                }
            };

            var QueryableParticipants = participantCollection.AsQueryable();

            mockSetParticipants = new Mock<DbSet<Participant>>();
            mockSetParticipants.As<IQueryable<Participant>>().Setup(m => m.Provider).Returns(QueryableParticipants.Provider);
            mockSetParticipants.As<IQueryable<Participant>>().Setup(m => m.Expression).Returns(QueryableParticipants.Expression);
            mockSetParticipants.As<IQueryable<Participant>>().Setup(m => m.ElementType).Returns(QueryableParticipants.ElementType);
            mockSetParticipants.As<IQueryable<Participant>>().Setup(m => m.GetEnumerator()).Returns((IEnumerator<Participant>)QueryableParticipants.GetEnumerator());
            mockSetParticipants.Setup(d => d.Remove(It.IsAny<Participant>())).Callback<Participant>((s) => participantCollection.Remove(s));

            mockContext = new Mock<bachelordbContext>();
            mockContext.Setup(c => c.Participant).Returns(mockSetParticipants.Object);
        }

       [Test]
       public void ChangeProfileParticipant_Change_Email()
        {
            //Arrange
            uut = new ManageProfileHandler(mockContext.Object);
            Participant participant = new Participant
            {
                Email = "changed@changed.com",
                Age = DateTime.Now.Date,
                English = true,
                Gender = true,
                IdParticipant = 0,
                Password = "123456",
            };

            //Act
            uut.ChangeProfileParticipantDB(participant);
            Participant newParticipant = mockContext.Object.Participant.FirstOrDefault(part => part.IdParticipant == participant.IdParticipant);

            //Assert
            Assert.That(newParticipant.Email == "changed@changed.com");
        }

        [Test]
        public void ChangePassword_Valid_Old_Password()
        {
            //Arrange
            uut = new ManageProfileHandler(mockContext.Object);
            Participant participant = new Participant
            {
                Email = "changed@changed.com",
                Age = DateTime.Now.Date,
                English = true,
                Gender = true,
                IdParticipant = 0,
                Password = "newpassword",
            };

            string oldPassword = "123456";

            //Act
            uut.ChangePasswordParticipantDB(participant, oldPassword);
            Participant newParticipant = mockContext.Object.Participant.FirstOrDefault(part => part.IdParticipant == participant.IdParticipant);

            //Assert
            Assert.That(newParticipant.Password == participant.Password);
        }

        [Test]
        public void ChangePassword_Invalid_Old_Password()
        {
            //Arrange
            uut = new ManageProfileHandler(mockContext.Object);
            Participant participant = new Participant
            {
                Email = "changed@changed.com",
                Age = DateTime.Now.Date,
                English = true,
                Gender = true,
                IdParticipant = 0,
                Password = "newpassword",
            };

            string oldPassword = "invalid";

            //Act
            DbStatus status = uut.ChangePasswordParticipantDB(participant, oldPassword);
            Participant newParticipant = mockContext.Object.Participant.FirstOrDefault(part => part.IdParticipant == participant.IdParticipant);

            //Assert
            Assert.That(newParticipant.Password != participant.Password);
            Assert.That(status.success == false);
            Assert.That(status.errormessage == "The old password was incorrect. Please try again");
        }

        [Test]
        public void DeleteAccount()
        {
            //Arrange
            uut = new ManageProfileHandler(mockContext.Object);

            //Act
            uut.DeleteAccountParticipantDB(0);

            //Assert
            Assert.That(mockContext.Object.Participant.Count() == 1);
        }

    }
}
