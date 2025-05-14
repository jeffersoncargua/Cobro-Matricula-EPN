using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.User
{
    public class UserDto
    {
        //public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]{2,30}$", ErrorMessage = "El nombre no debe contener más de 30 caracteres y debe ser alfabeticos")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]{2,30}$", ErrorMessage = "El apellido no debe contener más de 30 caracteres y debe ser alfabeticos")]
        public string LastName { get; set; }

        [Required]
        //[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public string? City { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}", ErrorMessage = "El numero de telefono debe ser compatible con la de Ecuador. No es necesario colocar +593")]
        public string Phone { get; set; }

        //public string? Role { get; set; }
    }
}
