using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoTodoList.Models;

namespace ProyectoTodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ReactContext _dbcontext;

        public TareaController(ReactContext context)
        {
            _dbcontext = context;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {

            List<Tarea> lista = _dbcontext.Tareas.OrderByDescending(t => t.Id).ThenBy(t => t.FechaRegistro).ToList();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Tarea request)
        {
            await _dbcontext.Tareas.AddAsync(request);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");
        }

        [HttpDelete]
        [Route("Cerrar/{Id:int}")]
        public async Task<IActionResult> Cerrar(int Id)
        {
            Tarea tarea = _dbcontext.Tareas.Find(Id);
            _dbcontext.Tareas.Remove(tarea);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");
        }
    }
}