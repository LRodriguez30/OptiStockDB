using Microsoft.AspNetCore.Mvc;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas.Dtos.Auditorias;

namespace OptiStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditoriasController : ControllerBase
    {
        private readonly IAuditoriasRepository _auditoriasRepo;

        public AuditoriasController(IAuditoriasRepository auditoriasRepo)
        {
            _auditoriasRepo = auditoriasRepo;
        }

        // GET: api/Auditorias
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var registros = await _auditoriasRepo.GetAllAsync();
            return Ok(registros);
        }

        // GET: api/Auditorias/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var registro = await _auditoriasRepo.GetByIdAsync(id);

            if (registro is null)
                return NotFound();

            return Ok(registro);
        }

        // POST: api/Auditorias
        [HttpPost]
        public async Task<IActionResult> Post(AuditoriasCreateDto dto)
        {
            await _auditoriasRepo.CreateAsync(dto);

            return Ok(new
            {
                mensaje = "Auditorias de precios registrado correctamente."
            });
        }

        // PUT: api/Auditorias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, AuditoriasUpdateDto dto)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var existing = await _auditoriasRepo.GetByIdAsync(id);

            if (existing is null)
                return NotFound();

            await _auditoriasRepo.UpdateAsync(dto, id);

            return NoContent();
        }

        // DELETE: api/Auditorias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!MongoDB.Bson.ObjectId.TryParse(id, out _))
                return BadRequest("Id inválido.");

            var existing = await _auditoriasRepo.GetByIdAsync(id);

            if (existing is null)
                return NotFound();

            await _auditoriasRepo.DeleteAsync(id);

            return NoContent();
        }
    }
}