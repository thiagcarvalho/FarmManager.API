using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FarmManager.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var animal = _animalService.GetAnimal(id);
            return animal is null 
                ? NotFound($"Animal with ID {id} not found.")
                : Ok(animal);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var animals = _animalService.GetAllAnimals();
            return Ok(animals);
        }

        [HttpPost]
        public ActionResult Post([Required] AnimalInputModel animalInputModel)
        {
            return Created($"/api/v1/Animal/{_animalService.SaveAnimal(animalInputModel)}", null);
        }

    }
}
