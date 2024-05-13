using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum en)
        {
            var field = en.GetType().GetField(en.ToString());
            var attributes = (DescriptionAttribute[])(field?.GetCustomAttributes(typeof(DescriptionAttribute), false) ?? Array.Empty<object>());

            return attributes != null && attributes.Length > 0
                ? attributes[0].Description
                : en.ToString();
        }
    }
}
