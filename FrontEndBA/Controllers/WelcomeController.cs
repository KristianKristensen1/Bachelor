using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BachelorBackEnd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;

namespace FrontEndBA.Controllers
{
    public class WelcomeController : Controller
    {

       
  

        public ActionResult Participant()
        {
            return View();
        }

        // POST: Welcome/LoginParticipant
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("LoginParticipant")]
        [Route("Welcome/LoginParticipant")]
        public ActionResult LoginParticipant([Bind("Email,Password")] Participant participant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoginHandler loginhandler = new LoginHandler();
                    var status = loginhandler.LoginParticipantDB(participant.Email, participant.Password);
                    if (status.LoginStatus.IsSuccess)
                    {
                        return RedirectToAction("Participant", "Homepage", loginhandler.LoginStatus.participant);
                       // return View("../HomePage/index", loginhandler.LoginStatus.participant);
                    }
                    else
                    {
                        var err = status.LoginStatus.ErrorMessage;
                        if (err == "Wrong password")
                            this.ModelState.AddModelError("Password", err.ToString());
                        else
                        {
                            this.ModelState.AddModelError("Email", err.ToString());
                        }
                       
                    }
                }

                return View("Participant");
            }
            catch (Exception e)
            {
                return View("Participant");
            }
      
      
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("LoginResearcher")]
        [Route("Welcome/LoginResearcher")]
        public ActionResult LoginResearcher([Bind("Email,Password")] Researcher researcher)
        {

            LoginHandler loginhandler = new LoginHandler();
            var status = loginhandler.LoginResearcherDB(researcher.Email, researcher.Password);
            if (status.LoginStatus.IsSuccess)
            {
                return RedirectToAction("Researcher", "Homepage", status.LoginStatus.researcher);
             
            }
            else
            {
                var err = status.LoginStatus.ErrorMessage;
                if (err == "Wrong password")
                    this.ModelState.AddModelError("Password", err.ToString());
                else
                {
                    this.ModelState.AddModelError("Email", err.ToString());
                }
                
            }
            return View("Researcher");

        }

        public ActionResult Researcher()
        {
            return View();
        }

        
    }
}