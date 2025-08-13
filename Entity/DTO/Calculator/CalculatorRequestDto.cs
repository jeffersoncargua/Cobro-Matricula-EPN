using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.Calculator
{
    public class CalculatorRequestDto
    {
        [Required(ErrorMessage = "Debe seleccionar alguna opcion de la Formación Académica ")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Debe elegir la Formación Académica")]
        public int FormacionAcademica { get; set; }
        [Required(ErrorMessage = "Debe elegir alguna opcion del Regimen de estudio")]
        [RegularExpression(@"^[a-z]+$", ErrorMessage = "El regimen solo acepta cadenas de letras")]
        public string Regimen { get; set; }
        [Required(ErrorMessage = "Debe elegir laguna opcion del quintil al que pertenece")]
        [RegularExpression(@"^[1-5]$", ErrorMessage = "Debe elegir su quintil")]
        public int Quintil { get; set; }
        [Required(ErrorMessage = "Debe seleccionar si tiene o no Gratuidad")]
        public bool Gratuidad { get; set; }
        [Required(ErrorMessage = "Debe colocar algun valor en el campo de Primera Matricula")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El campo de Primera Matricula debe ser un numero entero positivo")]
        public int Primera { get; set; }
        [Required(ErrorMessage = "Debe colocar algun valor en el campo de Segunda Matricula")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El campo de Segunda Matricula debe ser un numero entero positivo")]
        public int Segunda { get; set; }
        [Required(ErrorMessage = "Debe colocar algun valor en el campo de Tercera Matricula")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El campo de Tercera Matricula debe ser un numero entero positivo")]
        public int Tercera { get; set; }

    }
}
