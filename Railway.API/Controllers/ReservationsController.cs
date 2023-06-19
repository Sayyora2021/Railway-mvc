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

namespace Railway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository ReservationRepository;
        private readonly IPassagerRepository PassagerRepository;
        private readonly IExemplaireRepository ExemplaireRepository;

        public ReservationsController(IReservationRepository reservationRepository, IPassagerRepository passagerRepository, IExemplaireRepository exemplaireRepository)
        {
            ReservationRepository = reservationRepository;
            PassagerRepository = passagerRepository;
            ExemplaireRepository = exemplaireRepository;
        }

        // GET: Reservations
        [HttpGet, Route("")]
        public async Task<IEnumerable<Reservation>> Index()
        {
            return await ReservationRepository.ListAll(); 
                         
        }

        [HttpPut, Route("Retour")]
        public async Task<ActionResult<Reservation>> Retour(int reservationId)
        {
            Reservation e = await ReservationRepository.GetById(reservationId);
            if (e == null ) 
            {
                return BadRequest("Reservation invalide");

                
            }
            await ReservationRepository.RemoveBuilletFromPassager(e);
            return Ok();

           
        }


        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Retour")]
        
        public async Task<ActionResult<Reservation>> Create(int passagerId, int exemplaireId)
        {
            if (ModelState.IsValid)
            {
                Passager passager =await PassagerRepository.GetById(passagerId);
                if(passager == null) { return BadRequest("PassagerId Invalide"); }
                Exemplaire exemplaire = await ExemplaireRepository.GetById(exemplaireId);
                if(exemplaire is null) { return BadRequest("ExemplaireId Invailde"); }
               
                Reservation reservation = new Reservation() { Exemplaire = exemplaire, DateModification=null, DateAller = DateTime.Now, DateRetour= DateTime.Now.AddMonths(1), Passager = passager };
                try
                {
                    await ReservationRepository.AddBuilletToPassager(reservation);
                    return reservation;
                }
                catch (Exception ex)
                {

                    return Problem(ex.Message);
                }
            }
            return BadRequest();
        }

               
    }
}
