using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndBA.DALAccess;
using FrontEndBA.Models.ParticipantModel.AccountViewModels;
using FrontEndBA.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    public class ParticipantController : Controller
    {
        public IDALAccess.IDALParticipant DataAcess;
        // GET: ParticipantLogin
        public ActionResult Index()
        {
            return View();
        }

        // GET: ParticipantLogin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ParticipantLogin/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult LoginParticipent()
        {
            return View();
        }

        public ActionResult ParticipentRegister()
        {


            return View();
        }

        // POST: ParticipantLogin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ParticipentRegister([Bind("Email,Firstname,Lastname,Password")] ParticipantRegisterViewModel participantRegisterobj)
        {

            BachelorBackEnd.Participant currentParticipants = RegisterConverter.ParticipantobjFromViewToDto(participantRegisterobj);
            DataAcess = new DalParticipant();
            DataAcess.SaveRegisterDto(currentParticipants);


            try
            {
                

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ParticipantLogin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ParticipantLogin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ParticipantLogin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ParticipantLogin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}