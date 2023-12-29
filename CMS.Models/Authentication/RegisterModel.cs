using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.Authentication
{
    public class RegisterModel
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }
        
        [Required, StringLength(100)]
        public string LastName { get; set; } 
        
        [Required, StringLength(100)]
        public string Email { get; set; }
        
        [Required, StringLength(100)]
        public string Password { get; set; }

        [Required, StringLength(100)]
        public string Username { get; set; }

        [Required]
        [ValidRole]
        public List<String> Roles { get; set; }

    }

    public class ValidRoleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is List<string> strtArray)
            {
                foreach (var item in strtArray)
                {

                    if (item.ToString() != "User" && item.ToString() != "Admin" && item.ToString() != "Contributor" && item.ToString() != "Editor")
                    {
                        return new ValidationResult(ErrorMessage = item + " , Role is not valid");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
