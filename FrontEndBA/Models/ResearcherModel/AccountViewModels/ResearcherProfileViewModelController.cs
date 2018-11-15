using BachelorBackEnd;
using FrontEndBA.Models.SharedModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FrontEndBA.Models.ResearcherModel;

namespace FrontEndBA.Models.ResearcherModel.AccountViewModels
{
    public class ResearcherProfileViewModel
    {
        public SharedModels.ResearcherModel CurrentResearcher { get; set; }
    }
}
