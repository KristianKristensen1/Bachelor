using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndBA.Models.ResearcherModel.EmailModels
{
    public class EmailModel
    {
        public string To { get; set; }
        public string From { get; set; }
        [Required(ErrorMessage = "Missing Email Subject")]
        [Display(Name = "Email Subject")]
        public string Subject { get; set; }
        public string MailBody { get; set; }

    }
}
