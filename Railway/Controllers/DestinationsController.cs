using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;

namespace Railway.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly IDestinationRepository Repository;

        public DestinationsController(IDestinationRepository repository)
        {
            Repository = repository;
        }

        // GET: Destinations
        public async Task<IActionResult> Index()
        {
            return View(await Repository.ListAll());
              //return await Repository.IsEmpty()? 
              //            View(await Repository.ListAll()) :
              //            Problem("Entity set 'RailwayContext.Destinations'  is null.");
        }

        // GET: Destinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var destination = await Repository.GetById(id.Value);
               
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // GET: Destinations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Destinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Destination destination)
        {
            if (ModelState.IsValid)
            {
                await Repository.Create(destination);
                return RedirectToAction(nameof(Index));
            }
            return View(destination);
        }

        // GET: Destinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var destination = await Repository.GetById(id.Value);
            if (destination == null)
            {
                return NotFound();
            }
            return View(destination);
        }

        // POST: Destinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Aller,Retour,Id")] Destination destination)
        {
            if (id != destination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Repository.Update(destination);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await DestinationExists(destination.Id))
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
            return View(destination);
        }

        // GET: Destinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var destination = await Repository.GetById(id.Value);
                
            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        // POST: Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await Repository.IsEmpty())
            {
                return Problem("Entity set 'RailwayContext.Destinations'  is null.");
            }
            var destination = await Repository.GetById(id);
            if (destination != null)
            {
                await Repository.Delete(destination);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DestinationExists(int id)
        {
          return await Repository.Exists(id);
        }
    }
}
