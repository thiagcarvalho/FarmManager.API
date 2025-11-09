using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FarmManager.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ToquesController : ControllerBase
    {
        private readonly IToqueService _toqueService;

        public ToquesController(IToqueService toqueService)
        {
            _toqueService = toqueService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            var toques = _toqueService.GetAll();
            return Ok(toques);
        }

        [HttpGet("{toqueId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int toqueId)
        {
            var toque = _toqueService.GetToque(toqueId);
            return toque is null
                ? NotFound($"Toque with ID {toqueId} not found.")
                : Ok(toque);
        }

        [HttpGet("cow/{cowId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetByCowId(int cowId)
        {
            var toques = _toqueService.GetByAnimalId(cowId);
            return toques is null || !toques.Any()
                ? NotFound($"No toques found for Cow with ID {cowId}.")
                : Ok(toques);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Post([Required] ToqueInputModel toqueInputModel)
        {
            return Created($"/api/v1/Toques/{_toqueService.AddToque(toqueInputModel)}", null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _toqueService.DeleteToque(id);
            return NoContent();
        }

        [HttpDelete("cleanup-expired")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeleteExpiredToques()
        {
            var deletedCount = _toqueService.DeleteExpiredToques();
            return Ok(new
            {
                DeletedCount = deletedCount,
                Message = $"{deletedCount} toque(s) expirado(s) removido(s)."
            });
        }
    }
}
