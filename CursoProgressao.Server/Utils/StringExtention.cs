using System.Text;
using System.Text.RegularExpressions;

namespace CursoProgressao.Server.Utils;

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

    public static string ToSentence(this string value)
    {
        string pattern = "[A-Z][a-z]*";
        MatchCollection matches = Regex.Matches(value, pattern);
        StringBuilder stringBuilder = new(matches[0].Value);

        for (int i = 1; i < matches.Count; i++)
        {
            stringBuilder.Append($" {matches[i].Value.ToLower()}");
        }

        return stringBuilder.ToString();
    }
}
