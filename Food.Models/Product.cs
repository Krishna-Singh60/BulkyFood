using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Range(1,100)]
        public double ListPrice { get; set; }
        [Required]
        [DisplayName("List Price")]
        [Range(1, 100)]
        public double Price { get; set; }

    }
}
