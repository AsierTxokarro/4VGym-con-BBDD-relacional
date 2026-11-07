
using _4GYM.Models;
using _4VGymAPI.Repositories;
using _4VGymEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController]
    [Route("activities")]
    public class ActivityController : Controller
    {
        private readonly ActivityRepository _activityrepository;
        public ActivityController(ActivityRepository activityrepository)
        {
            _activityrepository = activityrepository;
        }

        //get todas sin filtrar
        [HttpGet("AllActivities")]
        public async Task<ActionResult<List<Activity>>> GetAll()
        {
            try
            {
               var activities = await _activityrepository.GetAll();
                return Ok(activities);
            }
            catch (Exception exc)
            {
                var error = new ErrorResponse(500, $"Error al obtener las actividades: {exc.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        /// <summary>
        /// POST /activities
        /// Añade una nueva actividad en 4VGym.
        /// </summary>
        /// <param name="activity">ObjetoActivity enviado en el cuerpo de la petición (JSON).</param>
        [HttpPost]
        public async Task<ActionResult<Activity>> CreateActivity([FromBody] Activity activity)
        {
            try
            {
                if (activity == null)
                {
                    return BadRequest(new ErrorResponse(0, "Fail you must write an activity"));
                }
                if (await _activityrepository.GetById(activity.Id) != null)
                {
                    return BadRequest(new ErrorResponse(2, "The id is unique, ther is already another activity with that id"));
                }
                if (_activityrepository.NumMax(activity.MaxParticipants) == false)
                {
                    return BadRequest(new ErrorResponse(3, "The number of participants is incorrect, must be less than 25"));
                }
                if (activity.DateStart < DateTime.Today || activity.DateEnd < activity.DateStart)
                {
                    return BadRequest(new ErrorResponse(4, "The dates are incorrect"));
                }

                
                Activity? createdActivity = await _activityrepository.Add(activity);
            

                return StatusCode(StatusCodes.Status201Created, createdActivity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse(1, "An internal server error occurred"));
            }
        }


    }
}
