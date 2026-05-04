using Microsoft.AspNetCore.Mvc;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas.Dtos.HistorialCostos;


namespace OptiStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialCostosController : ControllerBase
    {
        private readonly IHistorialCostosRepository _histCostosRepo;

        public HistorialCostosController(IHistorialCostosRepository histCostosRepo)
        {
            _histCostosRepo = histCostosRepo;
        }

        // GET: api/HistorialCostos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var registros = await _histCostosRepo.GetAllAsync();
            return Ok(registros);
        }

        // GET: api/HistorialCostos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var registro = await _histCostosRepo.GetByIdAsync(id);

            if (registro is null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        // POST: api/HistorialCostos
        [HttpPost]
        public async Task<IActionResult> Post(HistorialCostosCreateDto dto)
        {
            await _histCostosRepo.CreateAsync(dto);

            return Ok(new
            {
                mensaje = "Historial de costos registrado correctamente."
            });
        }

        // PUT: api/HistorialCostos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, HistorialCostosUpdateDto dto)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var existing = await _histCostosRepo.GetByIdAsync(id);

            if (existing is null)
                return NotFound();

            await _histCostosRepo.UpdateAsync(dto, id);
            return NoContent();
        }

        // DELETE: api/HistorialCostos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var existing = await _histCostosRepo.GetByIdAsync(id);

            if (existing is null)
            {
                return NotFound();
            }

            await _histCostosRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}