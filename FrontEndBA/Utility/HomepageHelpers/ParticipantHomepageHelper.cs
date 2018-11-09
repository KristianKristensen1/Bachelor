using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using FrontEndBA.Models;
using StudyManagementSystem.DAOImplementations;
using StudyManagementSystem.DAOInterfaces;

namespace FrontEndBA.Utility.HomepageHelpers
{
    public class ParticipantHomepageHelper
    {

        public ParticipantHomepageModel CreateParticipantHomepageModel(int partID)
        {
            bachelordbContext db = new bachelordbContext();
            Participant participant = getParticipant(partID);
            IViewStudyHandler vsh = new ViewStudyHandler(new bachelordbContext());
            ParticipantHomepageModel participantHomepageModel = new ParticipantHomepageModel();

            //Gets the relevant Study
            participantHomepageModel.relevantStudies = vsh.GetRelevantStudiesDB(participant);

            //Gets the Study that the participant is enrolled in. 
            participantHomepageModel.myParticipantStudies = vsh.GetMyParticipantStudiesDB(participant.IdParticipant);

            return participantHomepageModel;
        }

        public Participant getParticipant(int id)
        {
            UserHandler userHandler = new UserHandler(new bachelordbContext());
            Participant participant = userHandler.getParticipant(id);
            return participant;
        }
    }
}
