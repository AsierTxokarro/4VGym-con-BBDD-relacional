using _4GYM.Models;
using _4GYM.Repositories;
using _4VGymEF.Models;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController]
    [Route("instructors")]
    public class InstructorsController : ControllerBase
    {
        private readonly InstructorRepository _instructorRepository;
        public InstructorsController(InstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }
        //Get todos los instructores

        [HttpGet("all")]
        public async Task<ActionResult<List<Instructor>>> GetAll()
        {
            try
            {
               var instructors = await _instructorRepository.GetAll();
                    return Ok(instructors);
               
            }
            catch (Exception exc)
            {
                var error = new ErrorResponse(500, $"Error al obtener los instructores: {exc.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

        }

        /// <summary>
        /// GET /instructor
        /// GET /instructor?first_initial=M
        /// Devuelve el listado de monitores (filtrado opcionalmente por la inicial del nombre)
        /// </summary>
        /// <param name="firstInitial">Filtro opcional para la inicial del monitor</param>
        [HttpGet("byInitial")]
        public async Task<ActionResult<List<Instructor>>> FindInstructors([FromQuery(Name = "first_initial")] string? firstInitial)
        {
            try
            {
                var instructors = await _instructorRepository.GetByFirstInitial(firstInitial);

                return Ok(instructors);
            }
            catch (Exception exc)
            {
                var error = new ErrorResponse(500, $"Error al obtener los instructores por su inicial: {exc.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }


        // GET: api/instructors/2
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Instructor?>> GetInstructorById([FromRoute] int id)
        {
            try
            {
                var instructor = await _instructorRepository.GetById(id);
                if (instructor == null)
                {
                    return NotFound();
                }

                return Ok(instructor); ;

            }
            catch (Exception exc)
            {
                var error = new ErrorResponse(500, $"Error al obtener el instructor por id: {exc.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }

        }

        //[HttpPost]
        //[ProducesResponseType(typeof(Instructor), StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        //public ActionResult<Instructor> CreateInstructor([FromBody] Instructor instructor)
        [HttpPost]
        public async Task <ActionResult<Instructor>> CreateInstructor([FromBody] Instructor instructor)
        {
            try
            {
                if (instructor == null)
                {
                    return BadRequest(new ErrorResponse(0, "Error debes poner algo"));
                }

                if (instructor.Name == null || instructor.Name == "" || await _instructorRepository.GetByName(instructor.Name) != null)
                {
                    return BadRequest(new ErrorResponse(4, "You must write a valid name"));
                }
                if (instructor.Email == null || instructor.Email == "" || await _instructorRepository.GetByEmail(instructor.Email) != null)
                {
                    return BadRequest(new ErrorResponse(5, "You must write a valid email"));

                }
                if (instructor.Phone == null || instructor.Phone == "" || await _instructorRepository.GetByPhone(instructor.Phone) != null)
                {
                    return BadRequest(new ErrorResponse(6, "You must write a valid phone"));
                }

                if (instructor.PhotoUrl == null || _instructorRepository.GetByPhoto_url(instructor.PhotoUrl) != null || instructor.PhotoUrl == "" || await _instructorRepository.FormatoCorrecto(instructor.PhotoUrl) == false)
                {
                    return BadRequest(new ErrorResponse(8, "You must write a valid photo_url"));
                }

                Instructor createdInstructor = await _instructorRepository.Add(instructor);

                return StatusCode(StatusCodes.Status201Created, createdInstructor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse(1, "An internal server error occurred"));
            }
        }
       
    }
}
