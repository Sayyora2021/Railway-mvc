using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            if(! await ExemplaireRepository.IsEmpty() && await PassagerRepository.IsEmpty())
            {
                ViewBag.Exemplaires = await ExemplaireRepository.ListAll();
                ViewBag.Passager = await PassagerRepository.ListAll();  
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

        // GET: Reservations/Create
        public async Task<IActionResult> Create()
        {
            await SetupViewBags();
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            
                await ReservationRepository.Create(p);
                return RedirectToAction(nameof(Index));
         
           
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Rendre(int? id)
        {
            Reservation reservation = await ReservationRepository.GetById(id.Value);
            await ReservationRepository.RemoveBuilletFromPassager(reservation);
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await ReservationRepository.GetById(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            await SetupViewBags();
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await ReservationRepository.Update(reservation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            await SetupViewBags();
            return View(reservation);
        }

        private bool ReservationExists(int id)
        {
            return ReservationRepository.GetById(id) != null;
        }




    }
}
