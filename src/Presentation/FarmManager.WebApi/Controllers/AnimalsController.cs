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
            return Created($"/api/v1/Animals/{_animalService.SaveAnimal(animalInputModel)}", null);
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

        [HttpGet("cows/by-number/{number:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetCowByNumber(int number)
        {
            var cow = _animalService.GetCowByRegisterNumber(number);
            return cow is null
                ? NotFound($"Cow with number {number} not found.")
                : Ok(cow);
        }

        [HttpGet("cows")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetAllCows()
        {
            var cows = _animalService.GetAllCows();
            return Ok(cows);
        }

        [HttpPost("cows")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult PostCow([FromBody, Required] CowInputModel cowInputModel)
        {
            return Created($"/api/v1/Animals/cows/{_animalService.SaveCow(cowInputModel)}", null);
        }

        [HttpPut("cows/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PutCow(
            [FromRoute] Guid id,
            [FromBody, Required] CowInputModel cowInputModel)
        {
            cowInputModel.Id = id;
            _animalService.UpdateCow(id, cowInputModel);
            return NoContent();
        }

        [HttpGet("calves/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetCalf(Guid id)
        {
            var animal = _animalService.GetCalf(id);
            return animal is null
                ? NotFound($"Calf with ID {id} not found.")
                : Ok(animal);
        }

        [HttpGet("calves")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetAllCalves()
        {
            var cows = _animalService.GetAllCalves();
            return Ok(cows);
        }

        [HttpGet("calves/by-mother/{motherNumber:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetCalfByMotherNumber(int motherNumber)
        {
            var calf = _animalService.GetCalfByMotherNumber(motherNumber);
            return calf is null
                ? NotFound($"Calf with mother number {motherNumber} not found.")
                : Ok(calf);
        }

        [HttpPost("calves")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult PostCalves([FromBody, Required] CalfInputModel calfInputModel)
        {
            return Created($"/api/v1/Animals/calves/{_animalService.SaveCalf(calfInputModel)}", null);
        }

        [HttpPut("calves/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PutCalf(
            [FromRoute] Guid id,
            [FromBody, Required] CalfInputModel calfInputModel)
        {
            calfInputModel.Id = id;
            _animalService.UpdateCalf(id, calfInputModel);
            return NoContent();
        }

        [HttpGet("bulls/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetBull(Guid id)
        {
            var animal = _animalService.GetBull(id);
            return animal is null
                ? NotFound($"Bull with ID {id} not found.")
                : Ok(animal);
        }

        [HttpGet("bulls")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetAllBulls()
        {
            var animals = _animalService.GetAllBulls();
            return Ok(animals);
        }

        [HttpPost("bulls")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult PostBull([Required] BullInputModel bullInputModel)
        {
            return Created($"/api/v1/Animals/bulls/{_animalService.SaveBull(bullInputModel)}", null);
        }

        [HttpPut("bulls/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PutBull(
            [FromRoute] Guid id,
            [FromBody, Required] BullInputModel bullInputModel)
        {
            bullInputModel.Id = id;
            _animalService.UpdateBull(id, bullInputModel);
            return NoContent();
        }

    }
}
