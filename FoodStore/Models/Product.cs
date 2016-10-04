using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FoodStore.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Product Name:")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool Perishble { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpiresOn { get; set; }

        public int? OnHandQty { get; set; }

        public bool IsDeleted { get; set; }

    }
}