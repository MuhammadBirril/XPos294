using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataModels
{
    public class Book : BaseSchema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, MaxLength(5)]
        public string KodeBuku { get; set; }

        [Required, MaxLength(50)]
        public string NamaBuku { get; set; }

        [Required, MaxLength(50)]
        public string NamaPengarang { get; set; }

        public bool Active { get; set; }
    }
}

