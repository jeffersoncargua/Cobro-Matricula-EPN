using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.Calculator
{
    public class CalculatorResponseDto
    {        
        public string Message { get; set; }

        public bool Success { get; set; }

        public CalculatorDto Calculator { get; set; }

        public HttpStatusCode StatusCode { get; set; }

    }
}
