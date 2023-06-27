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
    public class ExemplairesController : Controller
    {
        private readonly IExemplaireRepository Repository;
        private readonly IBuilletRepository BuilletRepository;

        public ExemplairesController(IExemplaireRepository repository, IBuilletRepository builletRepository)
        {
            Repository = repository;
            BuilletRepository = builletRepository;
        }
        private async Task SetupViewBags()
        {
            if (!await BuilletRepository.IsEmpty())
            {
                ViewBag.Buillets = new SelectList(await BuilletRepository.ListAll(), nameof(Buillet.Id), nameof(Buillet.Titre));
            }
        }
        // GET: Exemplaires
        public async Task<IActionResult> Index()
        {
              return ! await Repository.IsEmpty() ? 
                          View(await Repository.ListAll()) :
                          Problem("Entity set 'RailwayContext.Exemplaires'  is null.");
        }

        // GET: Exemplaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var exemplaire = await Repository.GetById(id.Value);
                
            if (exemplaire == null)
            {
                return NotFound();
            }

            return View(exemplaire);
        }

        // GET: Exemplaires/Create
        public async Task<IActionResult> Create()
        {
            await SetupViewBags();
            return View();
        }

        // POST: Exemplaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exemplaire exemplaire, int builletId)
        {
            await SetupViewBags();
            exemplaire.Buillet= await BuilletRepository.GetById(builletId);
           if (ModelState.IsValid)
            {
                await Repository.Create(exemplaire);
                return RedirectToAction(nameof(Index));
            }
            return View(exemplaire);
        }

        // GET: Exemplaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var exemplaire = await Repository.GetById(id.Value);
            if (exemplaire == null)
            {
                return NotFound();
            }
            return View(exemplaire);
        }

        // POST: Exemplaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumeroInventaire,MiseEnService,Id")] Exemplaire exemplaire)
        {
            if (id != exemplaire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Repository.Update(exemplaire);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ExemplaireExists(exemplaire.Id))
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
            return View(exemplaire);
        }

        // GET: Exemplaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var exemplaire = await Repository.GetById(id.Value);
                
            if (exemplaire == null)
            {
                return NotFound();
            }

            return View(exemplaire);
        }

        // POST: Exemplaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await Repository.IsEmpty())
            {
                return Problem("Entity set 'RailwayContext.Exemplaires'  is null.");
            }
            var exemplaire = await Repository.GetById(id);
            if (exemplaire != null)
            {
                await Repository.Delete(exemplaire);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ExemplaireExists(int id)
        {
            return await Repository.Exists(id);
        }
    }
}
