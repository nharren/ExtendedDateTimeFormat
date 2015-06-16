using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ISO8601.Internal.Conversion
{
    internal static class StartEndTimeIntervalConverter
    {
        internal static TimeSpan ToTimeSpan(StartEndTimeInterval interval)
        {
            return interval.End - interval.Start;
        }
    }
}