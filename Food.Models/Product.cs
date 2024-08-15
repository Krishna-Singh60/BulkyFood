using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; } 
        [Required]
        public string ISBN { get; set; } 
        [Required]
        public string Author { get; set; }
        [Required]
        [DisplayName("List Price")]
        [Range(1,1000)]
        public double ListPrice { get; set; }
        [Required]
        [DisplayName("Price")]
        [Range(1, 1000)]
        public double Price { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
