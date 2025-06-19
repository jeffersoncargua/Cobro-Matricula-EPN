using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.User
{
    public class RegistrationRequestDto
    {
        [Required(ErrorMessage ="El nombre es requerido")]

        [RegularExpression(@"^[a-zA-Z\s]{2,30}$", ErrorMessage = "El nombre no debe contener más de 30 caracteres y debe ser alfabeticos")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [RegularExpression(@"^[a-zA-Z\s]{2,30}$", ErrorMessage = "El apellido no debe contener más de 30 caracteres y debe ser alfabeticos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage = "El correo no tiene el formato adecuado")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La ciudad en la que reside es requerida")]
        [StringLength(20, ErrorMessage = "EL nombre de la ciudad no debe pasar de 20 caracteres")]
        public string City { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        [RegularExpression(@"^[0-9]{10}",ErrorMessage ="El telefono debe contener 10 caracteres numericos")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z\d\s])(?=.{8,}).*$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de la contraseña es requerida")]
        [Compare("Password",ErrorMessage ="Las contraseñas no coinciden")]
        public string ConfirmPass { get; set; }
      
        public string? Role { get; set; } 

    }
}
