using System.ComponentModel.DataAnnotations;

namespace VacationManager_Martin.Data.Entities
{
    public class Team
    {
        public Team()
        {
            Developers = new HashSet<User>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Team name lenght can't be more than 20 letters")]
        public string Name { get; set; }

        [Required]
        //[StringLength(20)]
        public int ProjectId { get; set; }

        [Required]     
        public virtual Project Project { get; set; }

        [Required]       
        public virtual ICollection<User> Developers { get; set; }

        [Required]       
        public string TeamLeadId { get; set; }

        [Required]
        public virtual User TeamLead { get; set; }
    }
}
