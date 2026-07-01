using AIStudyPlanner.Models;

namespace AIStudyPlanner.Services
{
    public interface IAISchedulingService
    {
        List<StudyTask> GenerateSchedule(StudyPlan plan);
    }

    public class AISchedulingService : IAISchedulingService
    {
        public List<StudyTask> GenerateSchedule(StudyPlan plan)
        {
            var tasks = new List<StudyTask>();
            DateTime startDate = DateTime.UtcNow.Date;
            int totalDays = (plan.TargetCompletionDate.Date - startDate).Days;

            if (totalDays <= 0) totalDays = 7; // Grace default

            // Pacing scaling factors based on skill levels
            int tasksPerDay = plan.SkillLevel switch
            {
                "Beginner" => 2,
                "Intermediate" => 3,
                "Advanced" => 4,
                _ => 2
            };

            for (int day = 0; day < totalDays; day++)
            {
                DateTime currentDay = startDate.AddDays(day);

                // Rule: Every 7th day is an automatic structural break/revision session
                if ((day + 1) % 7 == 0)
                {
                    tasks.Add(new StudyTask
                    {
                        Title = $"🔄 Deep Knowledge Consolidation & Revision",
                        Description = "Synthesize notes taken over the past week. Take a mandatory 30-minute walk.",
                        ScheduledDate = currentDay,
                        IsRevisionDay = true
                    });
                    continue;
                }

                // Normal structured curriculum generation
                for (int t = 1; t <= tasksPerDay; t++)
                {
                    tasks.Add(new StudyTask
                    {
                        Title = $"{plan.Subject}: Modules Progress unit {day + 1}.{t}",
                        Description = $"Focus Block. Practice deep reading or hands-on builds for {Math.Round(plan.StudyHoursPerDay / tasksPerDay, 1)} hrs. Keep Pomodoro intervals active.",
                        ScheduledDate = currentDay,
                        IsCompleted = false
                    });
                }
            }
            return tasks;
        }
    }
}
