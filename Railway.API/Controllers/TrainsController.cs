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
    public class TrainsController : ControllerBase
    {
        private readonly ITrainRepository Repository;

        public TrainsController(ITrainRepository repository)
        {
            Repository = repository;
        }

        // GET: Trains
        [HttpGet, Route("")]
        public async Task<ActionResult<List<Train>>> Index()
        {
              return !await Repository.IsEmpty() ? 
                          await Repository.ListAll() :
                          Problem("Entity set 'RailwayContext.Trains'  is null.");
        }

        // GET: Trains/Details/5
        [HttpGet, Route("Details/{id}")]
        public async Task<ActionResult<Train>> Details(int? id)
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

            return train;
        }


        // POST: Trains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Create")]
        
        public async Task<ActionResult<Train>> Create(Train train)
        {
            if (ModelState.IsValid)
            {
                
                await Repository.Create(train);
                return train;
            }
            return NoContent();
        }


        // POST: Trains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Edit")]
      
        public async Task<ActionResult<Train>> Edit(int id, string numero, string nom)
        {
            Train train = new Train()
            {
                Id = id,
                Numero = numero,
                Nom = nom,
                Buillets = new List<Buillet>()


            };
            if (id != 0)
            {
            
                try
                {
                   
                    await Repository.Update(train);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TrainExists(train.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return train;
            }
            return BadRequest();
        }

        // GET: Trains/Delete/5
        [HttpDelete, HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Train>> Delete(int id)
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

            return train;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> TrainExists(int id)
        {
            return await Repository.Exists(id);
        }

    }
}
