using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TakeANumberApi.Extensions;
public static class EnumExtension
{
    public static string GetDisplayName(this Enum value)
    {
        var info = value.GetType().GetMember(value.ToString());
        if (info.Length > 0)
        {
            var attribute = info[0].GetCustomAttributes<DisplayAttribute>().FirstOrDefault();
            if (attribute != null)
                return attribute.Name;
        }
        return value.ToString();
    }
}
