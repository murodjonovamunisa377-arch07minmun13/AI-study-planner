using AIStudyPlanner.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AIStudyPlanner.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IStudyPlanRepository _planRepository;
        private const int MockUserId = 1; // Assuming authenticated user baseline ID 1 for runtime validation

        public DashboardController(IStudyPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<IActionResult> Index()
        {
            var plans = await _planRepository.GetUserPlansAsync(MockUserId);
            
            // Collect global analytics metrics directly via linq pipeline
            var allTasks = plans.SelectMany(p => p.StudyTasks).ToList();
            
            ViewBag.TotalTasks = allTasks.Count;
            ViewBag.CompletedTasks = allTasks.Count(t => t.IsCompleted);
            ViewBag.RemainingTasks = allTasks.Count(t => !t.IsCompleted);
            ViewBag.CompletionRate = allTasks.Any() 
                ? (int)Math.Round((double)allTasks.Count(t => t.IsCompleted) / allTasks.Count * 100) 
                : 0;

            return View(plans);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleTask(int taskId, bool isCompleted)
        {
            await _planRepository.UpdateTaskStatusAsync(taskId, isCompleted);
            return Json(new { success = true });
        }
    }
}
