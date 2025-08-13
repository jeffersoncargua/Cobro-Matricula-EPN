using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.User
{
    public class UpdateUserDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]{2,30}$", ErrorMessage = "El nombre debe tener al menos 30 caracteres y deben ser alfabeticas")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]{2,30}$", ErrorMessage = "El apellido debe tener al menos 30 caracteres y deben ser alfabeticas")]
        public string LastName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}", ErrorMessage = "El numero de telefono debe cumplir con el formato de diez digitos")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "No cumple con el formato de un correo")]
        public string Email { get; set; }
    }
}
