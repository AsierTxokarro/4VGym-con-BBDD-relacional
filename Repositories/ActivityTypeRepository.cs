using _4GYM.Data;
using _4GYM.Models;
using Microsoft.EntityFrameworkCore;

namespace _4GYM.Repositories
{
    public class ActivityTypeRepository
    {
        private readonly Bd4VgymContext _bd4VgymContext;
        
        public ActivityTypeRepository(Bd4VgymContext bd4VgymContext)
        {
            _bd4VgymContext = bd4VgymContext;
        }
        // Método para obtener todos los tipos de actividades
        public async Task<List<ActivityType>> GetActivityTypes()
        {
            return await _bd4VgymContext.ActivityTypes.ToListAsync();
        }


        public async Task <ActivityType?> GetByName(string name)
        {
            return await _bd4VgymContext.ActivityTypes.SingleOrDefaultAsync(n => string.Equals(n.Name.ToLower(), name.ToLower()));
        }
        public ActivityType? GetById(long id)
        {
            return _bd4VgymContext.ActivityTypes.SingleOrDefault(at => at.Id == id);
        }
        public async Task<ActivityType> Add(ActivityType activityType)
        {
            _bd4VgymContext.ActivityTypes.Add(activityType);

            await _bd4VgymContext.SaveChangesAsync();

            return activityType;
        }

    }
}
