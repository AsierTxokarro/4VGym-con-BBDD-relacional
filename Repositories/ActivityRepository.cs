using _4VGymAPI.Models;
using _4VGymEF.Models;

namespace _4VGymAPI.Repositories
{
    public class ActivityRepository
    {
        private List<Activity> _activities = new();

        private readonly InMemoryActivityTypeRepository _repositoryActivityType;
        private readonly InMemoryInstructorRepository _repositoryInstructor;

        //Método para obtener todas las actividades
        public IEnumerable<Activity> GetAll()
        {
            return _activities;
        }
        private int _nextId = 1; // Para el autonumerico
        public ActivityRepository(InMemoryActivityTypeRepository repositoryActivityType, InMemoryInstructorRepository repositoryInstructor)
        {
            _repositoryActivityType = repositoryActivityType;
            _repositoryInstructor = repositoryInstructor;
        }

        public Activity? Add(Activity activity, out List<ErrorResponse> errorResponse)
        {

            errorResponse = new List<ErrorResponse>();

            // Validamos todas los referente a las entidades cruzadas
            // Validación 1: Que exista el tipo de actividad
            if (_repositoryActivityType.GetById(activity.ActivityTypeId) == null)
            {
                errorResponse.Add(new ErrorResponse(34, "EL tipo de actividad debe de funcionar"));
            }
            // Validacion 2: Que existan todos los monitores
            foreach (int instuctorId in activity.InstructorsId)
            {
                if (_repositoryInstructor.GetById(instuctorId) == null)
                {
                    errorResponse.Add(new ErrorResponse(35, "El instructor: " + instuctorId + " no existe"));
                }
            }
            if (errorResponse.Count > 0)
            {
                return null;
            }

            // Asignamos el ID autoincremental en la capa de datos, o sea que machacamos el que venia
            activity.Id = _nextId++;

            _activities.Add(activity);

            // Devolvemos el objeto completo tal como se ha guardado
            return activity;
        }


        public Activity? GetById(long id)
        {
            return _activities.SingleOrDefault(at => at.Id == id);
        }
        public bool NumMax(int numero)
        {
            return numero > 0 && numero <= 25;
        }
    }
}
