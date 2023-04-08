//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace VacationManager_Martin.Data.Entities
{
    public class Project
    {
        public Project()
        {
            Teams= new HashSet<Team>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20,ErrorMessage = "Project name can't be more than 20 letters")]
        public string Name { get; set; }

        [Required]
        [StringLength(100,ErrorMessage = "Description can't be more than 100 letters")]
        public string Description { get; set; }

        [Required]
        public virtual ICollection<Team> Teams { get; set; }
    }
}
