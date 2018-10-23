﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BachelorBackEnd;

namespace FrontEndBA.Controllers
{
    public class ViewStudyController : Controller
    {
        public string Index()
        {
            return "Hallo";
        }

        public ActionResult ShowStudyParticipant(Study study)
        {
            return View("ViewStudy");
        }

        public string Participant(int? id)
        {
            return "Id is " + id;
        }

        public ActionResult ShowStudyResearcher(Study study)
        {
            return View("ViewStudy");
        }
    }
}