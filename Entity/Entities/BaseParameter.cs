using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Entity.Entities
{
    public class BaseParameter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FormacionAcademica { get; set; }
        public float CostoOptimo { get; set; }
        public float CostoOptimoPeriodo { get; set; }
        public float ValorMin { get; set; }
        public float ValorMatriculaMin { get; set; }
        public float ValorArancelMin { get; set; }
        public float ValorMax { get; set; }
        public float ValorMatriculaMax { get; set; }
        public float ValorArancelMax { get; set; }
        public int HoraPeriodoAcademico { get; set; }
        public float HoraPromedioPeriodoAcademico { get; set; }
        public int CreditoPeriodoAcademico { get; set; }
        public float CreditoPerdidaTemporal { get; set; }
        public float CostoHoraPeriodo { get; set; }
        public float PorcentajeCostoOptimoAnual { get; set; }
        public float PorcentajeValorMin { get; set; }
        public float PorcentajeValorMax { get; set; }
        public float PorcentajeValorArancel { get; set; }
        public float PorcentajePromedioAcademico { get; set; }
        public float PorcentajePerdidaTemporal { get; set; }
        public float PorcentajeMatriculaExtraordinario { get; set; }
        public float PorcentajeMatriculaEspecial { get; set; }
        public float PorcentajeRecargoSegunda { get; set; }
        public float PorcentajeRecargoTercera { get; set; }



    }
}
