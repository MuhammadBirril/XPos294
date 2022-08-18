using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModel
{
    public class OrderDetailViewModel
    {
        public OrderDetailViewModel()
        {
            Quantity = 1;
        }
        public long Id { get; set; }
        public long? HeaderId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }

    }
}