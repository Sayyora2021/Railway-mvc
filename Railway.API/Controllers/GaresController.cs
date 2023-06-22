using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Railway.Core.Entities;
using Railway.Core.Seedwork;

namespace Railway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaresController : ControllerBase
    {
        private readonly IGareRepository Repository;

        public GaresController(IGareRepository repository)
        {
            Repository = repository;
        }

        // GET: api/Gares
        [HttpGet, Route("")]
        public async Task<ActionResult<List<Gare>>> Index()
        {
              return !await Repository.IsEmpty()? 
                          await Repository.ListAll() :
                          Problem("Entity set 'RailwayContext.Gares'  is null.");
        }

        // GET: api/Gares/Details/5
        [HttpGet, Route("Details/{id}")]
        public async Task<ActionResult<Gare>> Details(int? id)
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

            return gare;
        }

        

        // POST: Gares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Create")]
     
        public async Task<ActionResult<Gare>> Create( Gare gare)
        {
            if (ModelState.IsValid)
            {
               
                await Repository.Create(gare);
                return gare;
            }
            return BadRequest();
        }

       

        // POST: Gares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Edit/{id}")]
        
        public async Task<ActionResult<Gare>> Edit(int id, [Bind("Email,MotDePasse,Id")] Gare gare)
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
                    if (!await GareExists(gare.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return gare;
            }
            return BadRequest();
        }

        // GET: Gares/Delete/5
        [HttpDelete, Route("Delete/{id}")]
        public async Task<ActionResult<Gare>> Delete(int id)
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

            return gare;
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> GareExists(int id)
        {
            return await Repository.Exists(id);
        }
    }
}
