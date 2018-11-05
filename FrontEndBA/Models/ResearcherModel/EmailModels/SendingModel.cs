using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEndBA.Models.ResearcherModel.EmailModels
{
    public class SendingModel
    {
        [BindProperty]
        public EmailModel mails { get; set; }

        public ViewStudyModel studies { get; set; }


    }
}
