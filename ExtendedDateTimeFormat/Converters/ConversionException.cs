using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ExtendedDateTimeFormat.Converters
{
    public class ConversionException : Exception
    {
        public ConversionException(string message) : base(message)
        {

        }
    }
}
