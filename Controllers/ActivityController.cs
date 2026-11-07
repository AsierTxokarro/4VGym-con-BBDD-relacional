
using _4VGymAPI.Repositories;
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
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Activity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Activity>> FindActivities()
        {
            try
            {
                IEnumerable<Activity> activities = _repository.GetAll();

                if (activities == null || activities.Count() == 0)
                {
                    return BadRequest(new ErrorResponse(2, "No activity types found"));
                }

                return Ok(activities);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }

        /// <summary>
        /// POST /activities
        /// Añade una nueva actividad en 4VGym.
        /// </summary>
        /// <param name="activity">ObjetoActivity enviado en el cuerpo de la petición (JSON).</param>
        [HttpPost]
        [ProducesResponseType(typeof(Activity), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Activity> CreateActivity([FromBody] Activity activity)
        {
            try
            {
                if (activity == null)
                {
                    return BadRequest(new ErrorResponse(0, "Fail you must write an activity"));
                }
                if (_repository.GetById(activity.Id) != null)
                {
                    return BadRequest(new ErrorResponse(2, "The id is unique, ther is already another activty with that id"));
                }
                if (_repository.NumMax(activity.MaxParticipants) == false)
                {
                    return BadRequest(new ErrorResponse(3, "The number of participants is incorrect, must be less than 25"));
                }
                if (activity.DateStart < DateTime.Today || activity.DateEnd < activity.DateStart)
                {
                    return BadRequest(new ErrorResponse(4, "The dates are incorrect"));
                }

                List<ErrorResponse> errores;
                Activity? createdActivity = _repository.Add(activity, out errores);
                if (errores.Count > 0)
                {
                    return BadRequest(errores);
                }

                return StatusCode(StatusCodes.Status201Created, activity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse(1, "An internal server error occurred"));
            }
        }


    }
}
