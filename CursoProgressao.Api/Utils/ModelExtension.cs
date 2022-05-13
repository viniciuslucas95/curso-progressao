using CursoProgressao.Api.Exceptions.Common;
using CursoProgressao.Api.Models;
using System.Reflection;

namespace CursoProgressao.Api.Utils;

public static class ModelExtension
{
    public static void SetPropsToNull<T>(this T model, string[] props) where T : Model
    {
        foreach (string propName in props)
        {
            if (string.IsNullOrEmpty(propName)) throw new InvalidPropertyException($"Property name cannot be null or empty");

            string propNamePascalCase = propName.ToPascalCase();

            PropertyInfo? propInfo = model.GetType().GetProperty(propNamePascalCase);

            if (propInfo is null) throw new InvalidPropertyException($"Property {propName} doesn't exist on the model");

            if (!propInfo.IsNullable()) throw new InvalidPropertyException($"{propNamePascalCase} is not nullable");

            MethodInfo? setMethod = propInfo.SetMethod;

            InvalidPropertyException error = new($"{propNamePascalCase} cannot be set to null");

            if (setMethod is null) throw error;

            if (setMethod.IsPrivate || setMethod.IsInitOnly()) throw error;

            propInfo.SetValue(model, null);
        }
    }
}
