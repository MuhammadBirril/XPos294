using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataModels
{
    public class Category : BaseSchema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(10)]
        public string Initial { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public bool Active { get; set; }

    }
}
