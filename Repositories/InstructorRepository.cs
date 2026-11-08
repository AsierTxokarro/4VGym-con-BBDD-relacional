using _4GYM.Data;
using _4GYM.Models;
using Microsoft.EntityFrameworkCore;

namespace _4GYM.Repositories
{
    public class InstructorRepository
    {

        private readonly Bd4VgymContext _bd4VgymContext;

        public InstructorRepository(Bd4VgymContext bd4VgymContext)
        {
            _bd4VgymContext = bd4VgymContext;
        }
        public async Task<List<Instructor>> GetAll()
        {
            return await _bd4VgymContext.Instructors.ToListAsync();
        }

        // Método para obtener intructores por su inicial
        public async Task <List<Instructor>> GetByFirstInitial(string? firstInitial = null)
        {
            // Si se recibe la inicial, aplicamos el filtro sobre la colección
            if (!string.IsNullOrWhiteSpace(firstInitial))
            {
                var instructoresFiltrados = _bd4VgymContext.Instructors
                        .Where(instructor =>
                                     !string.IsNullOrEmpty(instructor.Name) &&
                                     instructor.Name.ToLower().StartsWith(firstInitial.ToLower())
                        );
                return await instructoresFiltrados.ToListAsync();
            }

            return await _bd4VgymContext.Instructors.ToListAsync();
        }

        public async Task <Instructor> Add(Instructor instructor)
        {
            _bd4VgymContext.Instructors.Add(instructor);

            await _bd4VgymContext.SaveChangesAsync();

            return instructor;
        }

        public async Task <Instructor?> GetById(long id)
        {
            return await _bd4VgymContext.Instructors.SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task <Instructor?> GetByName(string name)
        {
            return await _bd4VgymContext.Instructors.SingleOrDefaultAsync(i => i.Name == name);
        }
        public async Task <Instructor?> GetByEmail(string email)
        {
            return await _bd4VgymContext.Instructors.SingleOrDefaultAsync(i => i.Email == email);
        }
        public async Task <Instructor?> GetByPhoto_url(string photo_url)
        {
            return await _bd4VgymContext.Instructors.SingleOrDefaultAsync(i => i.PhotoUrl == photo_url);
        }
        public bool FormatoCorrecto(string photo_url)
        {
            if (photo_url.StartsWith("https://") && photo_url.EndsWith(".jpg"))
            {
                return true;
            }
            return false;
        }
        public async Task <Instructor?> GetByPhone(string phone)
        {
            return await _bd4VgymContext.Instructors.SingleOrDefaultAsync(i => i.Phone == phone);
        }
    }
}
