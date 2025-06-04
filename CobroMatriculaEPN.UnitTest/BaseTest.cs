using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobroMatriculaEPN.UnitTest
{
    public class BaseTest
    {
        public List<ValidationResult> ValidateModel(object model) 
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true );

            return validationResults;
        }
    }
}
