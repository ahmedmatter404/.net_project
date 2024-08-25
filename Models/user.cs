using System.ComponentModel.DataAnnotations;

namespace AspTask.Models
{
    public class user
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "user name")]
      public string Name { get; set; }

        [Required(ErrorMessage = "Job is required")]
        public string Job { get; set; }


       [Required(AllowEmptyStrings =true )]

       [EmailAddress(ErrorMessage = "it doesn't match the email format")]
        public string Email { get; set; }

            [Required(ErrorMessage = "Age is required")]
           [Range(12, 60, ErrorMessage = "age must be between 12 and 60")]
        public int Age { get; set; }

        [MaxLength(11)]
        public string Phone { get; set; }


    }
}
