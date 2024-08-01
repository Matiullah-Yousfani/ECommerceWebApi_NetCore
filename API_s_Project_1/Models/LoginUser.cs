using System.ComponentModel.DataAnnotations;

namespace API_s_Project_1.Models
{
    public class LoginUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
