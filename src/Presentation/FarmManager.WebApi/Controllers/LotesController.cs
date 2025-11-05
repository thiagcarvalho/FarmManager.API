using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FarmManager.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LotesController : ControllerBase
    {
        private readonly ILoteService _loteService;

        public LotesController(ILoteService loteService)
        {
            _loteService = loteService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetAll()
        {
            var lotes = _loteService.GetAllLotes();
            return Ok(lotes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            var lote = _loteService.GetLoteNameById(id);
            return Ok(lote);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Post([Required] LoteInputModel loteInputModel)
        {
            return Created($"/api/v1/Lotes/{_loteService.SaveLote(loteInputModel)}", null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _loteService.DeleteLote(id);
            return NoContent();
        }

        [HttpGet("{loteId}/animals")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAnimalsByLote(int loteId)
        {
            var animals = _loteService.GetAnimalsByLoteId(loteId);
            return Ok(animals);
        }

        [HttpGet("summary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetLoteSummaries()
        {
            var summaries = _loteService.GetLoteSummary();
            return Ok(summaries);
        }
    }
}
