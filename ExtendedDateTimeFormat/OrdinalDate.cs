using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ExtendedDateTimeFormat
{
    public class OrdinalDate : Date
    {
        public OrdinalDate(int year, int day)
        {
            if (year < 0 || year > 9999)
            {
                throw new ArgumentOutOfRangeException("year", "The year must be a value from 0 to 9999.");
            }

            var daysInYear = DateCalculator.DaysInYear(year);

            if (day < 1 || day > daysInYear)
            {
                throw new ArgumentOutOfRangeException("year", string.Format("The day must be a value from 1 to {0}.", daysInYear));
            }
        }
    }
}
