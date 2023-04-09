using System.ComponentModel.DataAnnotations;

namespace VacationManager_Martin.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Id = new Guid().ToString();
        }


        [Key]     
        public string Id { get; set; } 
        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Name lenght can't be more than 10 letters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Name lenght can't be more than 10 letters")]
        public string LastName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Name lenght can't be more than 10 letters")]
        public string UserName { get; set; }

       
        public int? TeamId { get; set; }
    }
}
