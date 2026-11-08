using _4GYM.Models;
using _4GYM.Repositories;
using _4VGymAPI.Repositories;
using _4VGymEF.Models;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController]
    [Route("activity-types")]
    public class ActivityTypesController : ControllerBase
    {
        private readonly ActivityTypeRepository _activityTypeRepository;

        // Inyectamos la clase concreta del repositorio
        public ActivityTypesController(ActivityTypeRepository activityTypeRepository)
        {
            _activityTypeRepository = activityTypeRepository;
        }

        /// <summary>
        /// GET /activity-types
        /// Devuelve el listado de tipos de actividades de 4VGym.
        /// </summary>
        [HttpGet("AllActivityTypes")]
        public async Task<ActionResult<List<ActivityType>>> GetActivityTypes()
        {
            try
            {
                var listaActivityTypes = await _activityTypeRepository.GetActivityTypes();

                return Ok(listaActivityTypes);
            }
            catch (Exception exc)
            {
                var error = new ErrorResponse(500, $"Error al obtener las Grs: {exc.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        /// <summary>
        /// GET /activity-typeByName
        /// Devuelve una actividad por su nombre
        /// </summary>
        [HttpGet("ActivityTypeByName")]
        public async Task<IActionResult> GetByName([FromQuery] string nombre)
        {
            try
            {
                var actividadPorNombre = await _activityTypeRepository.GetByName(nombre);
                if (actividadPorNombre == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(actividadPorNombre);
                }
            }
            catch (Exception exc)
            {
                var error = new ErrorResponse(500, $"Error al obtener la actividad por nombre: {exc.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        /// <summary>
        /// GET /activity-typeById
        /// Devuelve una actividad por su nombre
        /// </summary>
        [HttpGet("ActivityTypeById")]
        public IActionResult GetById([FromQuery] long id)
        {
            try
            {
                var actividadPorId = _activityTypeRepository.GetById(id);
                if (actividadPorId == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(actividadPorId);
                }
            }
            catch (Exception exc)
            {
                var error = new ErrorResponse(500, $"Error al obtener la actividad por ID: {exc.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
        /// <summary>
        /// POST /activity-types
        /// Añade un nuevo tipo de actividad en 4VGym.
        /// </summary>
        /// <param name="activityType">ObjetoActivityType enviado en el cuerpo de la petición (JSON).</param>
        [HttpPost]
        public async Task<ActionResult<ActivityType>> CreateActivityType([FromBody] ActivityType activityType)
        {
            try
            {
                // 1. Validación básica de la entrada, objeto no nulo y obligatorio nombre de actividad
                // Esto se llaman condiciones de barrera!!!
                if (activityType == null)
                {
                    return BadRequest(new ErrorResponse(0, "Error debes poner algo"));
                }
                if (_activityTypeRepository.GetById(activityType.Id) != null)
                {
                    return BadRequest(new ErrorResponse(1, "the id is unique, ther is already another activty with that id"));
                }
                if (activityType.Name == null)
                {
                    return BadRequest(new ErrorResponse(4, "The name of the activity is mandatory"));
                }
                if (activityType.NumberMonitors < 1)
                {
                    return BadRequest(new ErrorResponse(5, "The number of monitors are less tha 1"));

                }
                // No puede haber tipos de actividad con el mismo nombre
                if (await _activityTypeRepository.GetByName(activityType.Name) != null)
                {
                    return BadRequest(new ErrorResponse(6, "The name is unique, there is already another ActivityType with the same Name"));

                }


                // 2. Insertar/Guardar el objeto usando el repositorio
                //ActivityType createdActivityType = await _activityTypeRepository.Add(activityType);

                // Todas las validaciones pasadas.
                // Todavía no insertamos en BD (no hemos visto inserciones con EF),
                // así que devolvemos el objeto recibido como si se hubiera creado.
                return StatusCode(StatusCodes.Status201Created, activityType);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse(500, "An internal server error occurred"));

            }
        }


    }

}
