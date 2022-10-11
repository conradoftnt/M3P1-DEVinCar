using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;

namespace DEVinCar.Domain.Enuns;

public enum Roles
{
    [XmlEnumAttribute("C")]
    [Display(Name = "Cliente")]
    Cliente = 1,

    [XmlEnumAttribute("V")]
    [Display(Name = "Vendedor")]
    Vendedor,

    [XmlEnumAttribute("G")]
    [Display(Name = "Gerente")]
    Gerente
}

public static class EnumExtensions
{
    public static string GetName(this Enum enumValue)
    {
        string displayName;
        displayName = enumValue.GetType()
          .GetMember(enumValue.ToString())
          .First()
          ?.GetCustomAttribute<DisplayAttribute>()
          ?.GetName();

        if (String.IsNullOrEmpty(displayName))
        {
            displayName = enumValue.ToString();
        }
        return displayName;
    }
}

