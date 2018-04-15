using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Models {
    public class Product {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public string Manufacturer { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string ProductUrl { get; set; }

        [Required]
        public string ProductImage { get; set; }
    }
}
