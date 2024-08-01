using System.ComponentModel.DataAnnotations;

namespace API_s_Project_1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        public string? CategoryDescription { get; set; }

    }
}
