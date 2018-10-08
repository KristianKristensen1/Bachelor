using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    interface IQuestionnaireHandler
    {
        void AnswerQuestionnaireDB(Questionnaire questionnaire);

        void ShowQuestionnaireDB(Questionnaire questionnaire);

        void UpdateQuestionnaireDB(Questionnaire questionnaire, Participant participant);
    }
}
