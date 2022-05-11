using System.Text.RegularExpressions;

namespace CursoProgressao.Api.Utils;

public static class StringExtention
{
    public static string ToPascalCase(this string value, Exception? error = null)
    {
        string pattern = "^[a-zA-Z]{1}";
        Match match = Regex.Match(value, pattern);

        if (!match.Success)
        {
            if (error is not null) throw error;

            return value;
        }

        string firstLetter = match.Captures[0].Value;

        return Regex.Replace(value, pattern, firstLetter.ToUpper());
    }
}
