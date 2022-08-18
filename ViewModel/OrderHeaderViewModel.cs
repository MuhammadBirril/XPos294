using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ViewModel
{
    public class OrderHeaderViewModel
    {
        public long Id { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
        public List<OrderDetailViewModel> Details { get; set; }
    }
}
