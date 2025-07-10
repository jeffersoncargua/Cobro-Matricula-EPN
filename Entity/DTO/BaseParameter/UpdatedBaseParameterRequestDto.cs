using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.BaseParameter
{
    public class UpdatedBaseParameterRequestDto
    {
        public int Id { get; set; }
        public string FormacionAcademica { get; set; }
        [Required(ErrorMessage ="El campo del costo óptimo es requerido")]
        [Range(0f,10000f , ErrorMessage = "Debe ser un numero decimal entre 0 y 10000")]
        //[RegularExpression(@"^(10000(\.0+)?|[0-9]{1,4}(\.[0-9]+)?)$", ErrorMessage = "Debe ser un numero decimal entre 0 y 10000")]
        public float CostoOptimo { get; set; }

        //[Required(ErrorMessage = "El campo del costo óptimo periodo es requerido")]
        //[Range(0f, 100f, ErrorMessage = "Debe ser un numero decimal entre 0 y 100")]
        //[RegularExpression(@"^[+-]?\d*(\.\d+)?([eE][+-]?\d+)?$")]
        //public float CostoOptimoPeriodo { get; set; }

        //[Required(ErrorMessage = "El campo del valor mínimo es requerido")]
        //[Range(0f, 10000f, ErrorMessage = "Debe ser un numero decimal")]
        //[RegularExpression(@"^[+-]?\d*(\.\d+)?([eE][+-]?\d+)?$")]
        //public float ValorMin { get; set; }

        //[Required(ErrorMessage = "El campo del valor matrícula mínimo es requerido")]
        //[Range(0f, 10000f, ErrorMessage = "Debe ser un numero decimal")]
        //[RegularExpression(@"^[+-]?\d*(\.\d+)?([eE][+-]?\d+)?$")]
        //public float ValorMatriculaMin { get; set; }

        //[Required(ErrorMessage = "El campo del valor arancel mínimo es requerido")]
        //[Range(0f, 10000f, ErrorMessage = "Debe ser un numero decimal")]
        //[RegularExpression(@"^[+-]?\d*(\.\d+)?([eE][+-]?\d+)?$")]
        //public float ValorArancelMin { get; set; }

        //[Required(ErrorMessage = "El campo del valor máximo es requerido")]
        //[Range(0f, 10000f, ErrorMessage = "Debe ser un numero decimal")]
        //[RegularExpression(@"^[+-]?\d*(\.\d+)?([eE][+-]?\d+)?$")]
        //public float ValorMax { get; set; }

        //[Required(ErrorMessage = "El campo del valor matrícula máximo es requerido")]
        //[Range(0f, 10000f, ErrorMessage = "Debe ser un numero decimal")]
        //[RegularExpression(@"^[+-]?\d*(\.\d+)?([eE][+-]?\d+)?$")]
        //public float ValorMatriculaMax { get; set; }

        //[Required(ErrorMessage = "El campo del valor arancel máximo es requerido")]
        //[Range(0f, 10000f, ErrorMessage = "Debe ser un numero decimal")]
        //[RegularExpression(@"^[+-]?\d*(\.\d+)?([eE][+-]?\d+)?$")]
        //public float ValorArancelMax { get; set; }

        [Required(ErrorMessage = "El campo de horas del periodo académico es requerido")]
        [Range(0, 1000, ErrorMessage = "Debe ser un número entero entre 0 y 1000")]
        //[RegularExpression(@"^(0|[1-9]\d{0,2}|1000)$", ErrorMessage = "Debe ser un número entero entre 0 y 1000")]
        public int HoraPeriodoAcademico { get; set; }

        //[Required(ErrorMessage = "El campo de horas promedio del periodo académico es requerido")]
        //[Range(0f, 1000f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1000")]
        //[RegularExpression(@"^[+-]?\d*(\.\d+)?([eE][+-]?\d+)?$", ErrorMessage = "Debe ser un número decimal")]
        //public float HoraPromedioPeriodoAcademico { get; set; }

        [Required(ErrorMessage = "El campo de créditos del periodo académico es requerido")]
        [Range(0, 100, ErrorMessage = "Debe ser un número entero entre 0 y 100")]
        //[RegularExpression(@"^([0-9]|[1-9][0-9]|100)$", ErrorMessage = "Debe ser un número entero")]
        public int CreditoPeriodoAcademico { get; set; }

        //[Required(ErrorMessage = "El campo de créditos promedio del periodo temporal es requerido")]
        //[Range(0f, 100f, ErrorMessage = "Debe ser un numero decimal entre 0 y 100")]
        //[RegularExpression(@"^[+-]?\d*(\.\d+)?([eE][+-]?\d+)?$", ErrorMessage = "Debe ser un número decimal")]
        //public float CreditoPerdidaTemporal { get; set; }

        //[Required(ErrorMessage = "El campo del costo hora periodo es requerido")]
        //[Range(0f, 1000f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1000")]
        //[RegularExpression(@"^[+-]?\d*(\.\d+)?([eE][+-]?\d+)?$", ErrorMessage = "Debe ser un número decimal")]
        //public float CostoHoraPeriodo { get; set; }

        [Required(ErrorMessage = "El campo del porcentaje costo costo óptimo anual es requerido")]
        [Range(0f, 1f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1")]
        //[RegularExpression(@"^(0(\.\d+)?|1(\.0+)?)$", ErrorMessage = "El valor debe ser un número decimal entre 0 y 1.")]
        public float PorcentajeCostoOptimoAnual { get; set; }

        [Required(ErrorMessage = "El campo del porcentaje valor mínimo es requerido")]
        [Range(0f, 1f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1")]
        //[RegularExpression(@"^(0(\.\d+)?|1(\.0+)?)$", ErrorMessage = "Debe ser un número decimal")]
        public float PorcentajeValorMin { get; set; }

        [Required(ErrorMessage = "El campo del porcentaje valor máximo es requerido")]
        [Range(0f, 1f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1")]
        //[RegularExpression(@"^(0(\.\d+)?|1(\.0+)?)$", ErrorMessage = "Debe ser un número decimal")]
        public float PorcentajeValorMax { get; set; }

        [Required(ErrorMessage = "El campo del porcentaje valor arancel es requerido")]
        [Range(0f, 1f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1")]
        //[RegularExpression(@"^(0(\.\d+)?|1(\.0+)?)$", ErrorMessage = "Debe ser un número decimal")]
        public float PorcentajeValorArancel { get; set; }

        [Required(ErrorMessage = "El campo del porcentaje promedio académico es requerido")]
        [Range(0f, 1f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1")]
        //[RegularExpression(@"^(0(\.\d+)?|1(\.0+)?)$", ErrorMessage = "Debe ser un número decimal")]
        public float PorcentajePromedioAcademico { get; set; }

        [Required(ErrorMessage = "El campo del porcentaje pérdida temporal es requerido")]
        [Range(0f, 1f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1")]
        //[RegularExpression(@"^(0(\.\d+)?|1(\.0+)?)$", ErrorMessage = "Debe ser un número decimal")]
        public float PorcentajePerdidaTemporal { get; set; }

        [Required(ErrorMessage = "El campo del porcentaje matrícula extraordinario es requerido")]
        [Range(0f, 1f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1")]
        //[RegularExpression(@"^(0(\.\d+)?|1(\.0+)?)$", ErrorMessage = "Debe ser un número decimal")]
        public float PorcentajeMatriculaExtraordinario { get; set; }

        [Required(ErrorMessage = "El campo del porcentaje arancel especial es requerido")]
        [Range(0f, 1f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1")]
        //[RegularExpression(@"^(0(\.\d+)?|1(\.0+)?)$", ErrorMessage = "Debe ser un número decimal")]
        public float PorcentajeMatriculaEspecial { get; set; }

        [Required(ErrorMessage = "El campo del porcentaje recargo segunda es requerido")]
        [Range(0f, 1f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1")]
        //[RegularExpression(@"^(0(\.\d+)?|1(\.0+)?)$", ErrorMessage = "Debe ser un número decimal")]
        public float PorcentajeRecargoSegunda { get; set; }

        [Required(ErrorMessage = "El campo del porcentaje recargo tercera es requerido")]
        [Range(0f, 1f, ErrorMessage = "Debe ser un numero decimal entre 0 y 1")]
        //[RegularExpression(@"^(0(\.\d+)?|1(\.0+)?)$", ErrorMessage = "Debe ser un número decimal")]
        public float PorcentajeRecargoTercera { get; set; }
    }
}
