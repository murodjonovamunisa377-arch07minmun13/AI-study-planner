using System.ComponentModel.DataAnnotations;

namespace AIStudyPlanner.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
    }
}
