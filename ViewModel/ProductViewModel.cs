using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel
{
    public class ProductViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Variant")]
        public long VariantId { get; set; }

        [Display(Name = "Variant")]
        public string VariantName { get; set; }

        [Required, MaxLength(10)]
        public string Initial { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Stock { get; set; }

        public bool Active { get; set; }

    }
}

