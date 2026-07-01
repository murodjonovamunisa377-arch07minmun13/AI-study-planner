using AIStudyPlanner.Data;
using AIStudyPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace AIStudyPlanner.Repositories
{
    public class StudyPlanRepository : IStudyPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public StudyPlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudyPlan?> GetByIdAsync(int id) =>
            await _context.StudyPlans.Include(p => p.StudyTasks).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<StudyPlan>> GetUserPlansAsync(int userId) =>
            await _context.StudyPlans.Where(p => p.UserId == userId).Include(p => p.StudyTasks).ToListAsync();

        public async Task AddAsync(StudyPlan plan)
        {
            await _context.StudyPlans.AddAsync(plan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StudyPlan plan)
        {
            _context.StudyPlans.Update(plan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await _context.StudyPlans.FindAsync(id);
            if (plan != null)
            {
                _context.StudyPlans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTaskStatusAsync(int taskId, bool isCompleted)
        {
            var task = await _context.StudyTasks.FindAsync(taskId);
            if (task != null)
            {
                task.IsCompleted = isCompleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}
