using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts enum to display name based upon DisplayName attribute
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayName<T>(this T value) where T : Enum
        {
            object[] attrs = typeof(T).GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayNameAttribute), false);
            if (attrs.Any())
            {
                DisplayNameAttribute descAttr = (DisplayNameAttribute)attrs[0];
                return descAttr.DisplayName;
            }

            // ToString() if attribute doesn't exist
            return value.ToString();
        }
    }
}
