using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.ParticipantModel.AccountViewModels;
using FrontEndBA.Models.ResearcherModel.AccountViewModels;

namespace FrontEndBA.Utility
{
    public class RegisterConverter
    {
        public static BachelorBackEnd.Participants ParticipantobjFromViewToDto(ParticipantRegisterViewModel registerobj)
        {
            BachelorBackEnd.Participants currentParticipants = new Participants();
            if (registerobj != null)
            {
                currentParticipants.Firstname = registerobj.Firstname;
                currentParticipants.Email = registerobj.Email;
                currentParticipants.Lastname = registerobj.Lastname;
                currentParticipants.Password = registerobj.Password;
                currentParticipants.Pause = 0;

            }

            return currentParticipants;

        }
    }
}
