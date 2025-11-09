using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Services;
using FarmManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmManager.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ObservationsController : ControllerBase
    {
        private readonly IObservationService _observationService;

        public ObservationsController(IObservationService observationService)
        {
            _observationService = observationService;
        }

        [HttpGet("{animalId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(Guid animalId)
        {
            var observations = _observationService.GetByAnimalId(animalId);
            return observations is null
                ? NotFound($"Observation with Animal-ID {animalId} not found.")
                : Ok(observations);
        }
    }
}
