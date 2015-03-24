using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ExtendedDateTimeFormat.Validators
{
    public struct ValidationResult
    {
        public List<string> Errors { get; set; }

        public ValidationResult()
        {
            Errors = new List<string>();
        }

        public static implicit operator bool(ValidationResult validationResult)                     // implicit conversion to bool.
        {
            return validationResult.Errors.Count == 0;
        }   
    }
}
