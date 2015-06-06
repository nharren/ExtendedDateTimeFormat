﻿using System.Text;

namespace System.ISO8601.Internal.Serializers
{
    internal static class OrdinalDateDurationSerializer
    {
        internal static string Serialize(OrdinalDateDuration ordinalDateDuration, bool withComponentSeparators)
        {
            var output = new StringBuilder("P");
            output.AppendFormat("{0:D4}", ordinalDateDuration.Years);

            if (withComponentSeparators)
            {
                output.Append('-');
            }

            output.AppendFormat("{0:D3}", ordinalDateDuration.Days);

            return output.ToString();
        }
    }
}