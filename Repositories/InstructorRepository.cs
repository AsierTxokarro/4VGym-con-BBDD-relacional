using _4GYM.Models;

namespace _4GYM.Repositories
{
    public class InstructorRepository
    {
        public IEnumerable<Instructor> GetAll()
        {
            return _instructors.Values;
        }


        // Método para obtener introuctores por su inicial
        public IEnumerable<Instructor> GetByFirstInitial(string? firstInitial = null)
        {
            IEnumerable<Instructor> _allInstructors = _instructors.Values;

            // Si se recibe la inicial, aplicamos el filtro sobre la colección
            if (!string.IsNullOrWhiteSpace(firstInitial))
            {
                IEnumerable<Instructor> filteredInstructors =
                    _allInstructors
                        .Where(instructor =>
                                     !string.IsNullOrEmpty(instructor.Name) &&
                                     instructor.Name.StartsWith(firstInitial, StringComparison.OrdinalIgnoreCase)
                        );

                return filteredInstructors;
            }

            return _allInstructors;
        }

        public Instructor Add(Instructor instructor)
        {
            // Asignamos el ID autoincremental en la capa de datos, o sea que machacamos el que venia
            instructor.Id = _nextId++;

            _instructors.Add(instructor.Id, instructor);

            // Devolvemos el objeto completo tal como se ha guardado
            return instructor;
        }

        public Instructor? GetById(int id)
        {
            _instructors.TryGetValue(id, out Instructor? instructor);
            return instructor;
        }
        public Instructor? getByIdInterno(long id)
        {
            return _instructors.Values.SingleOrDefault(i => i.Id == id);
        }

        // TODO: No es necesario, lo podemos quitar, pero es para ver alternativas en LinQ
        internal Instructor? GetByIdLinq(int id)
        {
            return _instructors.Values
                .SingleOrDefault(i => i.Id == id); // FistOrDefault (Where devolvería una colección y queremos solo 1 posible elemento)
        }
        public Instructor? GetByName(string name)
        {
            return _instructors.Values.SingleOrDefault(i => i.Name == name);
        }
        public Instructor? GetByEmail(string email)
        {
            return _instructors.Values.SingleOrDefault(i => i.Email == email);
        }
        public Instructor? GetByPhoto_url(string photo_url)
        {
            return _instructors.Values.SingleOrDefault(i => i.PhotoUrl == photo_url);
        }
        public bool FormatoCorrecto(string photo_url)
        {
            if (photo_url.StartsWith("https://") && photo_url.EndsWith(".jpg"))
            {
                return true;
            }
            return false;
        }
        public Instructor? GetByPhone(string phone)
        {
            return _instructors.Values.SingleOrDefault(i => i.Phone == phone);
        }
    }
}
