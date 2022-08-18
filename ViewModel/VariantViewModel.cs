using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModel
{
    public class VariantViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Category")]
        public long CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Required, MaxLength(10)]
        public string Initial { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
