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
    public class ExemplairesController : ControllerBase
    {
        private readonly IExemplaireRepository Repository;
        private readonly IBuilletRepository BuilletRepository;

        public ExemplairesController(IExemplaireRepository repository, IBuilletRepository builletRepository)
        {
            Repository = repository;
            BuilletRepository = builletRepository;
        }

        // GET: Exemplaires
        [HttpGet, Route("")]
        public async Task<ActionResult<List<Exemplaire>>> Index()
        {
              return await Repository.IsEmpty() ? 
                          await Repository.ListAll() :
                          Problem("Entity set 'RailwayContext.Exemplaires'  is null.");
        }

        // GET: Exemplaires/Details/5
        [HttpGet, Route("Details/{id}")]
        public async Task<ActionResult<Exemplaire>> Details(int? id)
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

            return exemplaire;
        }


        // POST: Exemplaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route("Create")]
        public async Task<ActionResult<Exemplaire>> Create(string numero, int builletId)
        {
            if (numero != "" && builletId != 0)
            {
                Exemplaire exemplaire = new Exemplaire()
                {
                    NumeroInventaire = numero,
                    MiseEnService = DateTime.Now,
                    Buillet = await BuilletRepository.GetById(builletId)
                };
                await Repository.Create(exemplaire);
                return exemplaire;
            }
            return BadRequest();
        }

        
        // POST: Exemplaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, Route ("Edit")]
       
        public async Task<ActionResult<Exemplaire>> Edit(int id, string numero, int builletId)
        {
            Exemplaire exemplaire = new Exemplaire()
            {
                NumeroInventaire = numero,
                MiseEnService = DateTime.Now,
                Buillet = await BuilletRepository.GetById(builletId)
            };
            if (id != 0)
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
                return Ok();
            }
            return BadRequest();
        }

        // GET: Exemplaires/Delete/5
        [HttpDelete, Route("Delete/{id}")]
        public async Task<ActionResult<Exemplaire>> DeleteConfirmed(int id)
        {
            if (await Repository.IsEmpty())
            {
                return Problem("Entity set 'RailwayContext.Exemplaires'  is null.");
            }

            var exemplaire = await Repository.GetById(id);
                
            if (exemplaire != null)
            {
                await Repository.Delete(exemplaire);
                return Ok();
            }
            else
            {
              return  Problem("Erreur d'effacement.");
            }

        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> ExemplaireExists(int id)
        {
            return await Repository.Exists(id);
        }

       
    }
}
