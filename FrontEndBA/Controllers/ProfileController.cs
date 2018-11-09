using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Participant()
        {
            return View();
        }

        public ActionResult Researcher()
        {
            return View();
        }
    }
}