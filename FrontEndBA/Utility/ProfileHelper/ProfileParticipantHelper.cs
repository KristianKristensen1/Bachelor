using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.ProfileModel;
using FrontEndBA.Utility.HomepageHelpers;

namespace FrontEndBA.Utility.ProfileHelper
{
    public static class ProfileParticipantHelper
    {
        public static ParticipantProfileModel getdefaultParticipant(int id)
        {
            ParticipantHomepageHelper helper = new ParticipantHomepageHelper();
            ParticipantProfileModel ppm = new ParticipantProfileModel();
            var participant = helper.getParticipant(id);

            ppm.Email = participant.Email;
            ppm.Id = id;
            ppm.English = participant.English;
            ppm.ValidInput = false;

            return ppm;

        }

        public static ParticipantProfileModel convertToModel(Participant participant,bool status,bool validInput)
        {
            ParticipantProfileModel ppm = new ParticipantProfileModel();
            ppm.Id = participant.IdParticipant;
            ppm.Email = participant.Email;
            ppm.ValidInput = validInput;
            ppm.SuccesChangePassword = status;

            return ppm;
        }
    }
}
