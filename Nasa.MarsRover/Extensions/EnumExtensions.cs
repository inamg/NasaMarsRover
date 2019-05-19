using System;
using System.ComponentModel;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            Check.NotNull(value, nameof(value));

            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
