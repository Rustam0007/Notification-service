using System.ComponentModel;
using System.Reflection;
namespace CallbackService.Enums;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        return value.GetType()
            .GetMember(value.ToString())
            .FirstOrDefault()
            ?.GetCustomAttribute<DescriptionAttribute>()
            ?.Description;
    }
        
    public static string GetDescription<TEnum>(this int value) where TEnum : Enum
    {
        return Enum.IsDefined(typeof(TEnum), value) ? ((TEnum)(object)value).GetDescription() : value.ToString();
    }
}