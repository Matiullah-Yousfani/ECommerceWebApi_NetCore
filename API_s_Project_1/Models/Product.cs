using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_s_Project_1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        public float ProductWeightInKgs { get; set; }

        public int Price { get; set; }

        public int ListPrice { get; set; }

        public int Price50 { get; set; }

        public int Price100 { get; set;}

        [ValidateNever]
        public string? imageUrl { get; set; }

        [ValidateNever]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


    }
}
