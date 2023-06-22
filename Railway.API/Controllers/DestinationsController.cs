using Railway.Core.Entities;
using Railway.Core.Seedwork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Railway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationRepository Repository;

        public DestinationsController(IDestinationRepository repository)
        {
            Repository = repository;
        }

        // GET: Destinations
        [HttpGet, Route("")]
        public async Task<ActionResult<List<Destination>>> Index()
        {
              return !await Repository.IsEmpty() ? 
                          await Repository.ListAll() :
                          Problem("Entity set 'RailwayContext.Destinations'  is null.");
        }

        // GET: Destinations/Details/5
        [HttpGet, Route("Details/{id}")]
        public async Task<ActionResult<Destination>> Details(int? id)
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

            return destination;
        }


        // POST: Destinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Create")]
        public async Task<ActionResult<Destination>> Create(Destination destination)
        {
            if (ModelState.IsValid)
            {
                await Repository.Create(destination);
                return destination;
            }
            return NoContent();
        }

       

        // POST: Destinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route ("Edit")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<Destination>> Edit(int id, string aller, string retour)
        {
            Destination destination = new Destination()

            {
                Id = id,
                Aller = aller,
                Retour = retour,
                Buillets = new List<Buillet>()
            };

            if (id != 0)
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
                return destination;
            }
            return BadRequest();
        }

        // POST: Destinations/Delete/5
        [HttpDelete, Route("Delete/{id}")]
        public async Task<ActionResult<Destination>> DeleteConfirmed(int id)
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

            return destination;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> DestinationExists(int id)
        {
            return await Repository.Exists(id);
        }
    }
}
