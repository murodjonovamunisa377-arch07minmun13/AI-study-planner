using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIStudyPlanner.Models
{
    public class StudyPlan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string SkillLevel { get; set; } = "Beginner"; // Beginner, Intermediate, Advanced

        [Required]
        [Range(1, 24, ErrorMessage = "Study hours must be between 1 and 24.")]
        public double StudyHoursPerDay { get; set; }

        [Required]
        public DateTime TargetCompletionDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public User? User { get; set; }
        
        public ICollection<StudyTask> StudyTasks { get; set; } = new List<StudyTask>();
    }
}
