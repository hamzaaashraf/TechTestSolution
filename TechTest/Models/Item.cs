using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechTest.Models
{
    public class Item
    {
        public int Id { get; set; }

            [Required(ErrorMessage = "Name is required.")]
            [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Description is required.")]
            [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
            public string Description { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.Now; // Default to the current date and time

            [Required]
            public bool IsActive { get; set; } = true; // Status flag for soft deletion
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Price must contain only digits.")]
        [Range(1, 1000000, ErrorMessage = "Price must be between 1 and 1,000,000.")]
            public decimal? Price { get; set; } // Optional property for pricing
        
    }
}