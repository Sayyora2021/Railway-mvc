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
    public class PassagersController : ControllerBase
    {
        private readonly IPassagerRepository Repository;

        public PassagersController(IPassagerRepository repository)
        {
            Repository = repository;
        }

        // GET: Passagers
        [HttpGet, Route("")]
        public async Task<ActionResult<List<Passager>>> Index()
        {
              return !await Repository.IsEmpty() ? 
                          await Repository.ListAll() :
                          Problem("Entity set 'RailwayContext.Passagers'  is null.");
        }

        // GET: Passagers/Details/5
        [HttpGet, Route("Details/{id}")]
        public async Task<ActionResult<Passager>> Details(int? id)
        {
            if (id == null ||await Repository.IsEmpty())
            {
                return NotFound();
            }

            var passager = await Repository.GetById(id.Value);
               
            if (passager == null)
            {
                return NotFound();
            }

            return passager;
        }


        // POST: Passagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Create")]
        
        public async Task<ActionResult<Passager>> Create(Passager passager)
        {
            if (ModelState.IsValid)
            {
               
                await Repository.Create(passager);
                return passager;
            }
            return BadRequest();
        }

       
        // POST: Passagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Edit/{id}")]
        public async Task<ActionResult<Passager>> Edit(int id, Passager passager)
        {
            if (id != passager.Id)
            {
                return NotFound();
            }
            try
            {
                await Repository.Update(passager);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!await PassagerExists(passager.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return passager;
        }

        // GET: Passagers/Delete/5
        [HttpDelete, Route("Delete/{id}")]
        public async Task<ActionResult<Passager>> DeleteConfirmed(int id)
        {
            if (await Repository.IsEmpty())
            {
                return Problem("Entity set 'RailwayContext.Passagers'  is null."); ;
            }

            var passager = await Repository.GetById(id);
                
            if (passager != null)
            {
                await Repository.Delete(passager);
            }

            return passager;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> PassagerExists(int id)
        {
            return await Repository.Exists(id);
        }
    }
}
