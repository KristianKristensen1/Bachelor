using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models.ProfileModel;
using FrontEndBA.Utility.HomepageHelpers;

namespace FrontEndBA.Utility.ProfileHelper
{
    public static class ParticipantHelper
    {
        public static ParticipantProfileModel getdefaultParticipant(int id)
        {
            ParticipantHomepageHelper helper = new ParticipantHomepageHelper();
            ParticipantProfileModel ppm = new ParticipantProfileModel();
            var participant = helper.getParticipant(id);

            ppm.Email = participant.Email;
            ppm.Id = id;
            ppm.English = participant.English;
            ppm.Password = participant.Password;
            ppm.ValidInput = false;

            return ppm;

        }
    }
}
