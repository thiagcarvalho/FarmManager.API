using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FarmManager.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(Guid id)
        {
            var animal = _animalService.GetAnimal(id);
            return animal is null
                ? NotFound($"Animal with ID {id} not found.")
                : Ok(animal);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetAll()
        {
            var animals = _animalService.GetAllAnimals();
            return Ok(animals);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Post([Required] AnimalInputModel animalInputModel)
        {
            return Created($"/api/v1/Animal/{_animalService.SaveAnimal(animalInputModel)}", null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(
            [FromRoute] Guid id, 
            [FromBody, Required] AnimalInputModel animalInputModel)
        {
            animalInputModel.Id = id;
            _animalService.UpdateAnimal(id, animalInputModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(Guid id)
        {
            _animalService.DeleteAnimal(id);
            return NoContent();
        }

        [HttpPost("cows")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult PostCow([FromBody, Required] CowInputModel cowInputModel)
        {
            return Created($"/api/v1/Animal/{_animalService.SaveCow(cowInputModel)}", null);
        }

        [HttpGet("cows/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetCow(Guid id)
        {
            var animal = _animalService.GetCow(id);
            return animal is null
                ? NotFound($"Cow with ID {id} not found.")
                : Ok(animal);
        }

    }
}
