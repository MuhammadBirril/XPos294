using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            Success = true;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Entity { get; set; }
    }
}
