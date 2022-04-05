using System.ComponentModel.DataAnnotations;

namespace ADOCrud.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name field is required")] // setting custom validations
        [Display(Name ="User Name")] // Full name custom validations
        public string Name { get; set; }
        [Required(ErrorMessage ="Email field is required")]
        [EmailAddress(ErrorMessage ="Enter valid email")]
        [Display(Name ="User Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Age is required")]
        [Range(18,60,ErrorMessage ="Age should be in between 18 and 60")]
        [Display (Name ="User Age")]
        public int Age { get; set; }

    }
}
