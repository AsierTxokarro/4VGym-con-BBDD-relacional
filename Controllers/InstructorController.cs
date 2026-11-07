using _4GYM.Models;
using _4VGymAPI.Models;
using _4VGymAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController]
    [Route("instructors")]
    public class InstructorsController : ControllerBase
    {
        private readonly InMemoryInstructorRepository _repository;

        // Inyectamos la clase concreta del repositorio
        public InstructorsController(InMemoryInstructorRepository repository)
        {
            _repository = repository;
        }
        //Get todos los instructores

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Instructor>> FindInstructors()
        {
            try
            {
                IEnumerable<Instructor> instructors = _repository.GetAll();
                if (instructors == null || instructors.Count() == 0)
                {
                    return BadRequest(new ErrorResponse(2, "No existe ningun instructor"));
                }
                else
                {
                    return Ok(instructors);
                }
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Error"));
            }

        }

        /// <summary>
        /// GET /instructor
        /// GET /instructor?first_initial=M
        /// Devuelve el listado de monitores (filtrado opcionalmente por la inicial del nombre)
        /// </summary>
        /// <param name="firstInitial">Filtro opcional para la inicial del monitor</param>
        [HttpGet("byInitial")]
        [ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Instructor>> FindInstructors([FromQuery(Name = "first_initial")] string? firstInitial)
        {
            try
            {
                IEnumerable<Instructor> instructors = _repository.GetByFirstInitial(firstInitial);

                return Ok(instructors);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }


        // GET: api/instructors/2
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Instructor> GetInstructorById([FromRoute] int id)
        {
            try
            {
                Instructor? instructor = _repository.GetById(id);
                //Instructor? instructor = _repository.GetByIdLinq(id)
                if (instructor != null)
                {
                    return Ok(instructor);
                }
                return NotFound(new ErrorResponse(2, $"No existe el instructor con id: {id}"));

            }
            catch (Exception)
            {

                return BadRequest(new ErrorResponse(1, "Ocurrió un error en el servidor al procesar la solicitud."));
            }

        }

        //[HttpPost]
        //[ProducesResponseType(typeof(Instructor), StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        //public ActionResult<Instructor> CreateInstructor([FromBody] Instructor instructor)
        //{
        [HttpPost]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Instructor> CreateInstructor([FromBody] Instructor instructor)
        {
            try
            {
                if (instructor == null)
                {
                    return BadRequest(new ErrorResponse(0, "Error debes poner algo"));
                }

                if (instructor.Name == null || instructor.Name == "" || _repository.GetByName(instructor.Name) != null)
                {
                    return BadRequest(new ErrorResponse(4, "You must write a valid name"));
                }
                if (instructor.Email == null || instructor.Email == "" || _repository.GetByEmail(instructor.Email) != null)
                {
                    return BadRequest(new ErrorResponse(5, "You must write a valid email"));

                }
                if (instructor.Phone == null || instructor.Phone == "" || _repository.GetByPhone(instructor.Phone) != null)
                {
                    return BadRequest(new ErrorResponse(6, "You must write a valid phone"));
                }

                if (instructor.PhotoUrl == null || _repository.GetByPhoto_url(instructor.PhotoUrl) != null || instructor.PhotoUrl == "" || _repository.FormatoCorrecto(instructor.PhotoUrl) == false)
                {
                    return BadRequest(new ErrorResponse(8, "You must write a valid photo_url"));
                }

                Instructor createdInstructor = _repository.Add(instructor);

                return StatusCode(StatusCodes.Status201Created, createdInstructor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse(1, "An internal server error occurred"));
            }
        }
        //}

    }
}
