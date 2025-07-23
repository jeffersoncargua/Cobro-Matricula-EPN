using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO.Calculator
{
    public class CalculatorDto
    {
        public float ValorMatricula { get; set; }
        public float ValorArancel { get; set; }
        public float RecargoSegunda { get; set; }
        public float RecargoTercera { get; set; }
        public float RecargoMatriculaExtraordinaria { get; set; }
        public string Gratuidad { get; set; }
        public float Bancario { get; set; } = 1f;
        public float ValorTotal { get; set; }
    }
}
