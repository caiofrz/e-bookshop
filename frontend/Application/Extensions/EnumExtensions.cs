namespace frontend.Application.Extensions;

using System.ComponentModel;
using System.Globalization;

public static class EnumExtension
{
    public static string Description<T>(this T e) where T : IConvertible
    {
        string result = null;

        if (e is Enum)
        {
            Type type = e.GetType();
            foreach (int value in Enum.GetValues(type))
            {
                if (value == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    object[] customAttributes = type.GetMember(type.GetEnumName(value))[0].GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);

                    if (customAttributes.Length != 0)
                    {
                        result = ((DescriptionAttribute)customAttributes[0]).Description;
                    }
                    break;
                }
            }
        }
        return result;
    }
}
