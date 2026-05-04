using Microsoft.AspNetCore.Mvc;
using OptiStock.Repositories;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas.Dtos.Reportes;

namespace OptiStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReportesRepository _reportesRepo;

        public ReportesController(IReportesRepository reportesRepo)
        {
            _reportesRepo = reportesRepo;
        }

        // GET: api/Reportes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var registros = await _reportesRepo.GetAllAsync();
            return Ok(registros);
        }

        // GET: api/Reportes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var registro = await _reportesRepo.GetByIdAsync(id);

            if (registro is null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        // POST: api/Reportes
        [HttpPost]
        public async Task<IActionResult> Post(ReportesCreateDto dto)
        {
            await _reportesRepo.CreateAsync(dto);

            return Ok(new
            {
                mensaje = "Reporte registrado correctamente."
            });
        }

        // DELETE: api/Reportes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var existing = await _reportesRepo.GetByIdAsync(id);

            if (existing is null)
            {
                return NotFound();
            }

            await _reportesRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}