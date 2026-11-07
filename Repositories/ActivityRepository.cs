using _4GYM.Data;
using _4GYM.Models;
using Microsoft.EntityFrameworkCore;

namespace _4VGymAPI.Repositories
{
    public class ActivityRepository
    {
        private readonly Bd4VgymContext _bd4VgymContext;

        public ActivityRepository(Bd4VgymContext bd4VgymContext)
        {
            _bd4VgymContext = bd4VgymContext;
        }
        //Método para obtener todas las actividades
        public async Task<List<Activity>> GetAll()
        {
            return await _bd4VgymContext.Activities.ToListAsync();
        }

        public async Task <Activity> Add(Activity activity)
        {

            _bd4VgymContext.Activities.Add(activity);

            await _bd4VgymContext.SaveChangesAsync();
            // Devolvemos el objeto completo tal como se ha guardado
            return activity;
        }

        public async Task <Activity?> GetById(long id)
        {
            return await _bd4VgymContext.Activities.SingleOrDefaultAsync(a => a.Id == id);
        }
        public bool NumMax(int numero)
        {
            return numero > 0 && numero <= 25;
        }
    }
}
