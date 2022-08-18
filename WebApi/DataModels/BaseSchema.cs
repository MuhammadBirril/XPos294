using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DataModels
{
    public class BaseSchema
    {
        [Required, MaxLength(50)]
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        [MaxLength(50)]
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
