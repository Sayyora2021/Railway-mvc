using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;
using Railway.Infrastructure.Data;

namespace Railway.Controllers
{
    public class TrainsController : Controller
    {
        private readonly ITrainRepository Repository;
       // private readonly IBuilletRepository BuilletRepository;
        
        public TrainsController(ITrainRepository repository)
        {
            Repository = repository;
           // this.BuilletRepository = builletRepository;
        }
        //public async Task SetupViewBags()
        //{
        //    if (!await BuilletRepository.IsEmpty())
        //    {
        //        ViewBag.Buillets = new SelectList(await BuilletRepository.ListAll(), nameof(Buillet.Titre));

        //    }
        //}
        // GET: Trains
        public async Task<IActionResult> Index()
        {
              return ! await Repository.IsEmpty() ? 
                          View(await Repository.ListAll()) :
                          Problem("Entity set 'RailwayContext.Trains'  is null.");
        }

        // GET: Trains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var train = await Repository.GetById(id.Value);
            if (train == null) 
            {
                return NotFound();
            }

            return View(train);
        }

        // GET: Trains/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] //fonctionne
        public async Task<IActionResult> Create(Train train)
        {
            if (ModelState.IsValid)
            {

                await Repository.Create(train);
                return RedirectToAction(nameof(Index));
            }
            return View(train);
        }

        //nouvelle version
        //public async Task<IActionResult> Create(Train train, int billettId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var buillet = await BuilletRepository.GetById(billettId);
        //        if (buillet != null)
        //        {
        //            train.Buillets.Add(buillet);
        //            await Repository.Create(train);
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    return View(train);
        //}


        // GET: Trains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var train = await Repository.GetById(id.Value);
            if (train == null)
            {
                return NotFound();
            }
            return View(train);
        }

        // POST: Trains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Numero,Nom,Id")] Train train)
        {
            if (id != train.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Repository.Update(train);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await TrainExists(train.Id))
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
            return View(train);
        }

        // GET: Trains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var train = await Repository.GetById(id.Value);
            if (train == null)
            {
                return NotFound();
            }

            return View(train);
        }

        // POST: Trains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await Repository.IsEmpty())
            {
                return Problem("Entity set 'RailwayContext.Trains'  is null.");
            }
            var train = await Repository.GetById(id);
            if (train != null)
            {
                await Repository.Delete(train);
            }
            
           return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TrainExists(int id)
        {
          return await Repository.Exists(id);
        }
    }
}
