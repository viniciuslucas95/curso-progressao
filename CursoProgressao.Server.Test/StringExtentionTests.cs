using CursoProgressao.Server.Utils;
using Xunit;

namespace CursoProgressao.Server.Test;

public class StringExtentionTests
{
    [Fact]
    public void ShouldTransformCamelCaseIntoPascalCase()
    {
        string expected = "FirstName";

        string actual = "firstName".ToPascalCase();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ShouldTransformPascalCaseToSentence()
    {
        string expected = "First name";

        string actual = "FirstName".ToSentence();

        Assert.Equal(expected, actual);
    }
}
