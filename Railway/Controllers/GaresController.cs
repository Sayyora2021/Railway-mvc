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
    public class GaresController : Controller
    {
        private readonly IGareRepository Repository;

        public GaresController(IGareRepository repository)
        {
            Repository = repository;
        }

        // GET: Gares
        public async Task<IActionResult> Index()
        {
              return ! await Repository.IsEmpty() ? 
                          View(await Repository.ListAll()) :
                          Problem("Entity set 'RailwayContext.Gares'  is null.");
        }

        // GET: Gares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var gare = await Repository.GetById(id.Value);
            if (gare == null)
            {
                return NotFound();
            }

            return View(gare);
        }

        // GET: Gares/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,MotDePasse,Id")] Gare gare)
        {
            if (ModelState.IsValid)
            {
                await Repository.Create(gare);
                return RedirectToAction(nameof(Index));
            }
            return View(gare);
        }

        // GET: Gares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var gare = await Repository.GetById(id.Value);
            if (gare == null)
            {
                return NotFound();
            }
            return View(gare);
        }

        // POST: Gares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Email,MotDePasse,Id")] Gare gare)
        {
            if (id != gare.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Repository.Update(gare);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await GareExists(gare.Id))
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
            return View(gare);
        }

        // GET: Gares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var gare = await Repository.GetById(id.Value);
            if (gare == null)
            {
                return NotFound();
            }

            return View(gare);
        }

        // POST: Gares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await Repository.IsEmpty())
            {
                return Problem("Entity set 'RailwayContext.Gares'  is null.");
            }
            var gare = await Repository.GetById(id);
            if (gare != null)
            {
                await Repository.Delete(gare);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GareExists(int id)
        {
          return await Repository.Exists(id);
        }
    }
}
