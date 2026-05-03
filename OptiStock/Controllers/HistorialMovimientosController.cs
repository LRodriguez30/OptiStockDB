using Microsoft.AspNetCore.Mvc;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas.Dtos.HistorialMovimientos;

namespace OptiStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialMovimientosController : ControllerBase
    {
        private readonly IHistorialMovimientosRepository _histMovimientosRepo;

        public HistorialMovimientosController(IHistorialMovimientosRepository histMovimientosRepo)
        {
            _histMovimientosRepo = histMovimientosRepo;
        }

        // GET: api/HistorialMovimientos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var registros = await _histMovimientosRepo.GetAllAsync();
            return Ok(registros);
        }

        // GET: api/HistorialMovimientos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var registro = await _histMovimientosRepo.GetByIdAsync(id);
            if (registro is null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        // POST: api/HistorialMovimientos
        [HttpPost]
        public async Task<IActionResult> Post(HistorialMovimientosCreateDto dto)
        {
            await _histMovimientosRepo.CreateAsync(dto);

            return Ok(new
            {
                mensaje = "Historial de movimientos registrado correctamente."
            });
        }

        // PUT: api/HistorialMovimientos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, HistorialMovimientosUpdateDto dto)
        {
            var existing = await _histMovimientosRepo.GetByIdAsync(id);

            if (existing is null)
                return NotFound();

            await _histMovimientosRepo.UpdateAsync(dto, id);
            return NoContent();
        }

        // DELETE: api/HistorialMovimientos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _histMovimientosRepo.GetByIdAsync(id);

            if (existing is null)
            {
                return NotFound();
            }

            await _histMovimientosRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
