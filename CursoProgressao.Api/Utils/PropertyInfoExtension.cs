using System.Reflection;

namespace CursoProgressao.Api.Utils;

public static class PropertyInfoExtension
{
    public static bool IsNullable(this PropertyInfo prop)
    {
        IEnumerable<CustomAttributeData> customAttributes = prop.CustomAttributes;

        foreach (CustomAttributeData customAttribute in customAttributes)
        {
            if (customAttribute.AttributeType.Name == "NullableAttribute") return true;
        }

        return false;
    }
}
