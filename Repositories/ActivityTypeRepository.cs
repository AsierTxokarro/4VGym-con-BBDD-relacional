using _4GYM.Models;

namespace _4GYM.Repositories
{
    public class ActivityTypeRepository
    {
        private List<ActivityType> _activityTypes = new();
        public InMemoryActivityTypeRepository()
        {
            SeedData(); // Precargamos algunos datos de prueba
        }
        private void SeedData()
        {
            _activityTypes = new List<ActivityType>()
            {
                new ActivityType (10, "BodyPump",  2 ),
                new ActivityType ( 20, "Spinning",  1 ),
                new ActivityType ( 30, "Dancing", 1 ),
                new ActivityType (40, "CrossFit", 3)
            };

        }
        // Método para obtener todos los tipos de actividades
        public IEnumerable<ActivityType> GetAll()
        {
            return _activityTypes;
        }

        private int _nextId = 1; // Para el autonumerico

        /// <summary>
        /// Guarda un nuevo tipo de actividad generando su ID de forma automática. (Autonumerico)
        /// </summary>
        /// <param name="activityType">Objeto con los datos a guardar (sin ID o con ID no asignado).</param>
        /// <returns>La entidad guardada con su ID autogenerado.</returns>
        public ActivityType Add(ActivityType activityType)
        {
            // Asignamos el ID autoincremental en la capa de datos, o sea que machacamos el que venia
            activityType.Id = _nextId++;

            _activityTypes.Add(activityType);

            // Devolvemos el objeto completo tal como se ha guardado
            return activityType;
        }

        public ActivityType? GetByName(string name)
        {
            return _activityTypes.SingleOrDefault(at => string.Equals(at.Name, name, StringComparison.OrdinalIgnoreCase));

        }
        public ActivityType? GetById(long id)
        {
            return _activityTypes.SingleOrDefault(at => at.Id == id);
        }

    }
}
