using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.User
{
    public class RegisterResponseDto
    {
        //public RegisterResponseDto()
        //{
        //    MessageResponse = new List<string>();
        //}
        public bool Success { get; set; }
        public List<string> MessageResponse { get; set; }

        public string Token { get; set; }
    }
}
