using System.Reflection;

namespace CursoProgressao.Api.Utils;

public static class MethodExtension
{
    public static bool IsInitOnly(this MethodInfo method)
    {
        Type[] customModifiers = method.ReturnParameter.GetRequiredCustomModifiers();

        foreach (Type modifier in customModifiers)
        {
            if (modifier.Name == "IsExternalInit") return true;
        }

        return false;
    }
}
