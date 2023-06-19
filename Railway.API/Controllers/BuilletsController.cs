using Microsoft.AspNetCore.Mvc;

using Railway.Core.Entities;
using Railway.Core.Seedwork;

namespace Railway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuilletsController : ControllerBase
    {
        private readonly IBuilletRepository BuilletRepository;
        private readonly IDestinationRepository DestinationReposioty;
        private readonly IExemplaireRepository ExemplaireRepository;
        private readonly ITrainRepository TrainRepository;


        public BuilletsController(IBuilletRepository builletRepository, IDestinationRepository destinationRepository, IExemplaireRepository exemplaireRepository, ITrainRepository trainRepository)
        {
            BuilletRepository = builletRepository;
            DestinationReposioty = destinationRepository;
            ExemplaireRepository = exemplaireRepository;
            TrainRepository = trainRepository;
        }

        // GET: Buillets
        [HttpGet, Route("")]
        public async Task<ActionResult<List<Buillet>>> Index()
        {
              return !await BuilletRepository.IsEmpty()?
                await BuilletRepository.ListAll():
                Problem("Entity set 'RailwayContext.Buillets'  is null.");
        }

        // GET: Buillets1/Details/5
        [HttpGet, Route("Details/{id}")]
        public async Task<ActionResult<Buillet>> Details(int? id)
        {
            if (id == null || await BuilletRepository.IsEmpty())
            {
                return NotFound();
            }

            var buillet = await BuilletRepository.GetById(id.Value);
               
            if (buillet == null)
            {
                return NotFound();
            }

            return buillet;
        }

        
        // POST: Buillets1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost, Route("Create")]
        public async Task<ActionResult<Buillet>> Create([FromBody] ApiBuillet apiBuillet)
        {
            Buillet buillet = new Buillet();
            buillet.Numero = apiBuillet.Numero;
            buillet.Titre = apiBuillet.Titre;

            buillet.Destinations = new List<Destination>();
            buillet.Exemplaires=new List<Exemplaire>();
            buillet.Trains = new List<Train>();

            await PopulateDestinationsExemplairesTrains(buillet, apiBuillet.destinationsIds, apiBuillet.exemplairesIds, apiBuillet.trainsIds);
            try
            {
                await BuilletRepository.Create(buillet);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
              
            }
            
        }


        // POST: Buillets1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Buillet>> Edit(int id, [FromBody] ApiBuillet apiBuillet)
        {

            try
            {
                Buillet buillet = await BuilletRepository.GetById(id);
                buillet.Numero = apiBuillet.Numero;
                buillet.Titre = apiBuillet.Titre;
                buillet.Destinations.Clear();
                buillet.Exemplaires.Clear();
                buillet.Trains.Clear();
                await PopulateDestinationsExemplairesTrains(buillet, apiBuillet.destinationsIds, apiBuillet.exemplairesIds, apiBuillet.trainsIds);

                await BuilletRepository.Update(buillet);
                return Ok();
            }
            catch (Exception ex) { return Problem(ex.Message); }
         
        }

        // GET: Buillets1/Delete/5
        [HttpDelete, Route("Delete/{id}")]
        public async Task<ActionResult<Buillet>> DeleteConfirmed(int id)
        {
            if (await BuilletRepository.IsEmpty())
            {
                return Problem("Entity set 'RailwayContext.Buillets'  is null.");
            }

            var buillet = await BuilletRepository.GetById(id);
            if (buillet == null)
            {
                await BuilletRepository.Delete(buillet);
            }

            return buillet;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> BuilletExists(int id)
        {
            return await BuilletRepository.Exists(id);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task PopulateDestinationsExemplairesTrains(Buillet buillet, int[] destinationsIds, int[] exemplairesIds, int[] trainsIds)
        {
            foreach (int destinationId in destinationsIds)
            {
                Destination destination = await DestinationReposioty.GetById(destinationId);
                buillet.Destinations.Add(destination);
            }

            foreach (int exemplaireId in exemplairesIds)
            {
                Exemplaire exemplaire = await ExemplaireRepository.GetById(exemplaireId);
                buillet.Exemplaires.Add(exemplaire);
            }

            foreach (int trainId in trainsIds)
            {
                Train train = await TrainRepository.GetById(trainId);
                buillet.Trains.Add(train);
            }
        }

        
    }
}
public class ApiBuillet
{
    public string? Numero { get; set; }
    public string? Titre { get; set; }

    public int[] destinationsIds { get; set; }
    public int[] exemplairesIds { get; set; }
    public int[] trainsIds { get; set; }
}