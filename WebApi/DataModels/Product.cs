using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataModels
{
    public class Product : BaseSchema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long VariantId { get; set; }

        [Required, MaxLength(10)]
        public string Initial { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public bool Active { get; set; }


        [ForeignKey("VariantId")]
        public virtual Variant Variant { get; set; }
    }
}