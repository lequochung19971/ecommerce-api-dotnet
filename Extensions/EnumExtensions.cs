using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Extensions;
public static class EnumExtensions
{
    public static String convertToString(this Enum eff)
    {
        return Enum.GetName(eff.GetType(), eff);
    }

    public static EnumType convertToEnum<EnumType>(this String enumValue)
    {
        return (EnumType)Enum.Parse(typeof(EnumType), enumValue);
    }
}