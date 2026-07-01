using AIStudyPlanner.Models;

namespace AIStudyPlanner.Repositories
{
    public interface IStudyPlanRepository
    {
        Task<StudyPlan?> GetByIdAsync(int id);
        Task<IEnumerable<StudyPlan>> GetUserPlansAsync(int userId);
        Task AddAsync(StudyPlan plan);
        Task UpdateAsync(StudyPlan plan);
        Task DeleteAsync(int id);
        Task UpdateTaskStatusAsync(int taskId, bool isCompleted);
    }
}
