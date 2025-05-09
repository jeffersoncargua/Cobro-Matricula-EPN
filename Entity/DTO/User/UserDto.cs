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
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Limite superado!!. Asegurese de mantener maximo 20 caracteres")]
        public string Name { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Limite superado!!. Asegurese de mantener maximo 20 caracteres")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email  { get; set; }

        public string? City { get; set; }

        [Required]
        [RegularExpression(@"[0 - 9]{0, 3}", ErrorMessage = "El numero de telefono debe ser compatible con la de Ecuador. No es necesario colocar +593")]
        public string Phone { get; set; }

        public string? Role { get; set; }
    }
}
