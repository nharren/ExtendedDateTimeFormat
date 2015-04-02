using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ExtendedDateTimeFormat.Internal.Converters
{
    public sealed class ExtendedDateTimeCollectionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, Globalization.CultureInfo culture, object value)
        {
            if (value == null)
            {
                throw GetConvertFromException(value);
            }

            var source = value as string;

            if (source != null)
            {
                return ExtendedDateTimeFormatParser.ParseCollection(source);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != null && value is ExtendedDateTimeCollection)
            {
                var instance = (ExtendedDateTimeCollection)value;

                if (destinationType == typeof(string))
                {
                    return instance.ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
