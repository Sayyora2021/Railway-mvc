using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;
using System.Diagnostics;

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


        // PUT: Trains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut, Route("Edit/{id}")]
      
        public async Task<ActionResult<Train>> Edit(int id, [FromBody] ApiTrain apiTrain)
        {
                      
                try
                {
                Train train = await Repository.GetById(id);
                train.Numero = apiTrain.Numero;
                train.Nom = apiTrain.Nom;
                await Repository.Update(train);
                return Ok();   
                }
                catch (Exception ex)
                {
                return Problem(ex.Message);
            }
               
            }


        // DELETE: Trains/Delete/5
        [HttpDelete, Route("Delete/{id}")]
        public async Task<ActionResult<Train>> DeleteConfirmed(int id)
        {
            if (await Repository.IsEmpty())
            {
                return Problem("Entity set 'RailwayContext.Trains'  is null.");
            }

            var train = await Repository.GetById(id);
                
            if (train != null)
            {
                await Repository.Delete(train);
                return Ok();
            }
            else
            {
                return Problem("Erreur effacement");
            }
           
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> TrainExists(int id)
        {
            return await Repository.Exists(id);
        }

    }
}
public class ApiTrain
{
    public string Numero { get; set; }
    public string Nom { get; set; }
}