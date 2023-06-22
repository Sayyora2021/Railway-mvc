using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Railway.Core.Entities;
using Railway.Core.Seedwork;
using Railway.Infrastructure.Data;

namespace Railway.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationRepository ReservationRepository;
        private readonly IPassagerRepository PassagerRepository;
        private readonly IExemplaireRepository ExemplaireRepository;

        public async Task SetupViewBags()
        {
            if(!( await ExemplaireRepository.IsEmpty() && await PassagerRepository.IsEmpty()))
            {
                ViewBag.Exemplaires = await ExemplaireRepository.ListAll();
                ViewBag.Passager = new SelectList(await PassagerRepository.ListAll(), nameof(Passager.Id), nameof(Passager.Nom));  
            }
        }
        public ReservationsController(IReservationRepository reservationRepository, IPassagerRepository passagerRepository, IExemplaireRepository exemplaireRepository)
        {
            ReservationRepository = reservationRepository;
            PassagerRepository = passagerRepository;
            ExemplaireRepository = exemplaireRepository;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
              return ! await PassagerRepository.IsEmpty() ? 
                          View(await ReservationRepository.ListAll()) :
                          Problem("Entity set 'RailwayContext.Reservations'  is null.");
        }

      
        public async Task<IActionResult> Create()
        {
            await SetupViewBags();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int PassagerId, int ExemplaireId)
        {
            var p = new Reservation()
            {
                Passager = await PassagerRepository.GetById(PassagerId),
                Exemplaire = await ExemplaireRepository.GetById(ExemplaireId),
                DateAller = DateTime.Now,
                DateRetour = DateTime.Now.AddMonths(1),
                DateModification = DateTime.Now
            };
            
                await ReservationRepository.AddBuilletToPassager(p);
                return RedirectToAction(nameof(Index));
         
           
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Retour(int? id)
        {
            Reservation reservation = await ReservationRepository.GetById(id.Value);
            await ReservationRepository.RemoveBuilletFromPassager(reservation);
            return RedirectToAction(nameof(Index));
            
        }

       



    }
}
