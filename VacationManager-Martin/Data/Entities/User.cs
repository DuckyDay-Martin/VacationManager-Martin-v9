using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using VacationManager_Martin.Data.Entities.TimeOffs;

namespace VacationManager_Martin.Data.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            LedTeams = new HashSet<Team>();
            PaidTimeOffRequests = new HashSet<PaidTimeOff>();
            UnpaidTimeOffRequests = new HashSet<UnpaidTimeOff>();
            SickTimeOffRequests = new HashSet<SickTimeOff>();

        }
        //[Required]
        //public string Password { get; set; }

        [Required]
        [StringLength(10,ErrorMessage = "Name lenght can't be more than 10 letters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Name lenght can't be more than 10 letters")]
        public string LastName { get; set; }


        [Required]
        public int? TeamId { get; set; }
        [Required]
        public virtual Team Team { get; set; }
        
        [Required]
        public virtual ICollection<Team> LedTeams { get; set; }

        [Required]
        public virtual ICollection<PaidTimeOff> PaidTimeOffRequests { get; set; }

        [Required]
        public virtual ICollection<UnpaidTimeOff> UnpaidTimeOffRequests { get; set; }

        [Required]
        public virtual ICollection<SickTimeOff> SickTimeOffRequests { get; set; }
    }
}
