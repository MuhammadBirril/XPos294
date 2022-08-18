using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class BookViewModel
    {
        public long Id { get; set; }

        public string KodeBuku { get; set; }

        public string NamaBuku { get; set; }

        public bool Active { get; set; }
    }
}
