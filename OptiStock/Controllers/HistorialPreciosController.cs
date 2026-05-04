using Microsoft.AspNetCore.Mvc;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas.Dtos.HistorialPrecios;


namespace OptiStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialPreciosController : ControllerBase
    {
        private readonly IHistorialPreciosRepository _histPreciosRepo;

        public HistorialPreciosController(IHistorialPreciosRepository histPreciosRepo)
        {
            _histPreciosRepo = histPreciosRepo;
        }

        // GET: api/HistorialPrecios
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var registros = await _histPreciosRepo.GetAllAsync();
            return Ok(registros);
        }

        // GET: api/HistorialPrecios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var registro = await _histPreciosRepo.GetByIdAsync(id);

            if (registro is null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        // POST: api/HistorialPrecios
        [HttpPost]
        public async Task<IActionResult> Post(HistorialPreciosCreateDto dto)
        {
            var creado = await _histPreciosRepo.CreateAsync(dto);

            if (!creado)
            {
                return BadRequest(new
                {
                    mensaje = "No se pudo registrar el historial."
                });
            }

            return Ok(new
            {
                mensaje = "Historial de precios registrado correctamente."
            });
        }


        // PUT: api/HistorialPrecios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, HistorialPreciosUpdateDto dto)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var existing = await _histPreciosRepo.GetByIdAsync(id);

            if (existing is null)
                return NotFound();

            await _histPreciosRepo.UpdateAsync(dto, id);
            return NoContent();
        }

        // DELETE: api/HistorialPrecios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var existing = await _histPreciosRepo.GetByIdAsync(id);

            if (existing is null)
            {
                return NotFound();
            }

            await _histPreciosRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}