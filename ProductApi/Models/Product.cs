using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Models {
    public class Product {
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public double price { get; set; }

        public string manufacturer { get; set; }

        [Required]
        public string productCode { get; set; }

        [Required]
        public string category { get; set; }
    }
}
