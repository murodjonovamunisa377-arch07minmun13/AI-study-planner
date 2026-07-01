using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIStudyPlanner.Models
{
    public class StudyTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudyPlanId { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; } = false;

        public bool IsRevisionDay { get; set; } = false;

        // Navigation Property
        [ForeignKey("StudyPlanId")]
        public StudyPlan? StudyPlan { get; set; }
    }
}
