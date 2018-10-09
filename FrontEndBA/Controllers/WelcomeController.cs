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

       
  

        public ActionResult WelcomePageParticipant()
        {
            return View(("WelcomePageParticipant"));
        }

        // POST: Welcome/LoginParticipant
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("LoginParticipant")]
        [Route("Welcome/LoginParticipant")]
        public ActionResult LoginParticipant([Bind("Email,Password")] Participant participant)
        {
           
            LoginHandler loginhandler = new LoginHandler();
            var status = loginhandler.LoginParticipantDB(participant.Email, participant.Password);
            if (status.LoginStatus.IsSuccess)
            {
                return View("../HomePage/index",loginhandler.LoginStatus.participant);
            }
            else
            {
                // Handle error jacob
                return View("WelcomePageParticipant");
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
                return View("../HomePage/index", status.LoginStatus.researcher);
            }
            else
            {
                // Handle error jacob
                return View("WelcomePageParticipant");
            }

        }

        public ActionResult WelcomePageResearcher()
        {
            return View();
        }

        // POST: Welcome/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Welcome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Welcome/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Welcome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Welcome/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}