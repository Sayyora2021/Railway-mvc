using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Railway.Infrastructure.Data;
using System.Linq;

namespace Railway.Controllers
{
    public class BuilletsController : Controller
    {
        private readonly IBuilletRepository BuilletRepository;
        private readonly ITrainRepository TrainRepository;
        private readonly IDestinationRepository DestinationRepository;
        private readonly IExemplaireRepository ExemplaireRepository;



        public BuilletsController(IBuilletRepository builletrepository, ITrainRepository trainrepository, IDestinationRepository destinationRepository, IExemplaireRepository exemplaireRepository)
        {
            BuilletRepository = builletrepository;
            TrainRepository = trainrepository;
            DestinationRepository = destinationRepository;
            ExemplaireRepository = exemplaireRepository;
        }

        private async Task SetupViewBags()
        {
            if (!(await TrainRepository.IsEmpty() && await DestinationRepository.IsEmpty() && await ExemplaireRepository.IsEmpty()))
            {
                ViewBag.Trains = new SelectList(await TrainRepository.ListAll(), nameof(Train.Id), nameof(Train.Nom));
                ViewBag.Destinations = new SelectList(await DestinationRepository.ListAll(), nameof(Destination.Id), nameof(Destination.Aller), nameof(Destination.Retour));
               ViewBag.Exemplaire = new SelectList(await ExemplaireRepository.ListAll(), nameof(Exemplaire.Id), nameof(Exemplaire.NumeroInventaire));
            }
        }


        // GET: Buillets
        public async Task<IActionResult> Index()
        {
            return ! await BuilletRepository.IsEmpty()?
                        View(await BuilletRepository.ListAll()) :
                        Problem("Entity set 'RailwayContext.Buillets'  is null.");
        }

        // GET: Buillets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || BuilletRepository.IsEmpty == null)
            {
                return NotFound();
            }

            var buillet = await BuilletRepository.GetById(id.Value);

            if (buillet == null)
            {
                return NotFound();
            }

            return View(buillet);
        }

        // GET: Buillets/Create
        public async Task<IActionResult> Create()
        {
            await SetupViewBags();
            return View();
        }

        // POST: Buillets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Buillet buillet, int[] trains, int[] destinations)
        {
            await SetupViewBags();
            buillet.Trains = await TrainRepository.GetList(m => trains.Contains(m.Id));
            buillet.Destinations = await DestinationRepository.GetList(m => destinations.Contains(m.Id));
          
            

            if (ModelState.IsValid)
            {
                await BuilletRepository.Create(buillet);
                return RedirectToAction(nameof(Index));
            }
            return View(buillet);
        }

        // GET: Buillets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await BuilletRepository.IsEmpty())
            {
                return NotFound();
            }

            var buillet = await BuilletRepository.GetById(id.Value);
            await SetupViewBags();
            if (buillet == null)
            {
                return NotFound();
            }
            ViewBag.Trains = await TrainRepository.ListAll();
            ViewBag.Destinations = await DestinationRepository.ListAll();
            return View(buillet);
        }

        // POST: Buillets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Buillet buillet, int[] trains, int[] destinations)
        {
            buillet.Trains = await TrainRepository.GetList(m => trains.Contains(m.Id));
            buillet.Destinations = await DestinationRepository.GetList(m => destinations.Contains(m.Id));

            if (ModelState.IsValid)
            {
                try
                {
                    await BuilletRepository.Update(buillet);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BuilletExists(buillet.Id))
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
            return View(buillet);
        }

        // GET: Buillets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await BuilletRepository.IsEmpty())
            {
                return NotFound();
            }

            var buillet = await BuilletRepository.GetById(id.Value);


            if (buillet == null)
            {
                return NotFound();
            }

            return View(buillet);
        }

        // POST: Buillets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await BuilletRepository.IsEmpty())
            {
                return Problem("Entity set 'RailwayContext.Buillets'  is null.");
            }
            var buillet = await BuilletRepository.GetById(id);
            if (buillet != null)
            {
                await BuilletRepository.Delete(buillet);
            }


            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BuilletExists(int id)
        {
            return await BuilletRepository.Exists(id);
        }
        public async Task<IActionResult> Recherche()
        {
            await SetupViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RechercheResultats(String Numero, String Titre)
        {
            var r = await BuilletRepository.ListAll();
            if (Numero != null && !String.IsNullOrWhiteSpace(Numero))
            {
                r = r.Where(l => l.Numero.ToLower().Contains(Numero.ToLower())).ToList();
            }
            if (Titre != null && !String.IsNullOrWhiteSpace(Titre))
            {
                r = r.Where(l => l.Titre.ToLower().Contains(Titre.ToLower())).ToList();
            }
            return View(r);
        }
    }
}
