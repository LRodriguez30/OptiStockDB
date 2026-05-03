using Microsoft.AspNetCore.Mvc;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas.Dtos.HistorialCajas;


namespace OptiStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialCajasController : ControllerBase
    {
        private readonly IHistorialCajasRepository _histCajasRepo;

        public HistorialCajasController(IHistorialCajasRepository histCajasRepo)
        {
            _histCajasRepo = histCajasRepo;
        }

        // GET: api/HistorialCajas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var registros = await _histCajasRepo.GetAllAsync();
            return Ok(registros);
        }

        // GET: api/HistorialCajas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var registro = await _histCajasRepo.GetByIdAsync(id);

            if (registro is null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        // POST: api/HistorialCajas
        [HttpPost]
        public async Task<IActionResult> Post(HistorialCajasCreateDto dto)
        {
            await _histCajasRepo.CreateAsync(dto);

            return Ok(new
            {
                mensaje = "Historial de cajas registrado correctamente."
            });
        }

        // PUT: api/HistorialCajas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, HistorialCajasUpdateDto dto)
        {
            var existing = await _histCajasRepo.GetByIdAsync(id);
            if (existing is null)
                return NotFound();

            await _histCajasRepo.UpdateAsync(dto, id);
            return NoContent();
        }

        // DELETE: api/HistorialCajas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _histCajasRepo.GetByIdAsync(id);
            if (existing is null)
            {
                return NotFound();
            }

            await _histCajasRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}