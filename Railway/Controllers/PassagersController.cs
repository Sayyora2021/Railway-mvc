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
    public class PassagersController : Controller
    {
        private readonly IPassagerRepository Repository;

        public PassagersController(IPassagerRepository repository)
        {
            Repository = repository;
        }

        // GET: Passagers
        public async Task<IActionResult> Index()
        {
            return !await Repository.IsEmpty() ?
                        View(await Repository.ListAll()) :
                        Problem("Entity set 'RailwayContext.Passagers'  is null.");
            
        }

        // GET: Passagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var passager = await Repository.GetById(id.Value);
            if (passager == null)
            {
                return NotFound();
            }

            return View(passager);
        }

        // GET: Passagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Passagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Passager passager)
        {
            if (ModelState.IsValid)

            {
                await Repository.Create(passager);
                return RedirectToAction(nameof(Index));
            }
            return View(passager);
        }

        // GET: Passagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var passager = await Repository.GetById(id.Value);
            if (passager == null)
            {
                return NotFound();
            }
            return View(passager);
        }

        // POST: Passagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Passager passager)
        {
            if (id != passager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Repository.Update(passager);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await PassagerExists(passager.Id))
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
            return View(passager);
        }

        // GET: Passagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await Repository.IsEmpty())
            {
                return NotFound();
            }

            var passager = await Repository.GetById(id.Value);
            if (passager == null)
            {
                return NotFound();
            }

            return View(passager);
        }

        // POST: Passagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await Repository.IsEmpty())
            {
                return Problem("Entity set 'RailwayContext.Passagers'  is null.");
            }
            var passager = await Repository.GetById(id);
            if (passager != null)
            {
                await Repository.Delete(passager);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PassagerExists(int id)
        {
          return await Repository.Exists(id);
        }
    }
}
