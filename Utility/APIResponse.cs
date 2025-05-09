using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class APIResponse
    {

        public APIResponse()
        {
            Message = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public object Result { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Message { get; set; }
    }
}
