using Microsoft.AspNetCore.Mvc;
using OptiStock.Repositories.Interfaces;
using OptiStock.Schemas.Dtos.Notificaciones;

namespace OptiStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionesRepository _notificacionesRepo;

        public NotificacionesController(INotificacionesRepository notificacionesRepo)
        {
            _notificacionesRepo = notificacionesRepo;
        }

        // GET: api/Notificaciones
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var registros = await _notificacionesRepo.GetAllAsync();
            return Ok(registros);
        }

        // GET: api/Notificaciones/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var registro = await _notificacionesRepo.GetByIdAsync(id);

            if (registro is null)
            {
                return NotFound();
            }

            return Ok(registro);
        }

        // POST: api/Notificaciones
        [HttpPost]
        public async Task<IActionResult> Post(NotificacionesCreateDto dto)
        {
            await _notificacionesRepo.CreateAsync(dto);

            return Ok(new
            {
                mensaje = "Notificación registrada correctamente."
            });
        }

        // PUT: api/Notificaciones/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, NotificacionesUpdateDto dto)
        {
            var existing = await _notificacionesRepo.GetByIdAsync(id);

            if (existing is null)
                return NotFound();

            await _notificacionesRepo.UpdateAsync(dto, id);
            return NoContent();
        }

        // DELETE: api/Notificaciones/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _notificacionesRepo.GetByIdAsync(id);

            if (existing is null)
            {
                return NotFound();
            }

            await _notificacionesRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
