using CursoProgressao.Shared.Utils;
using Xunit;

namespace CursoProgressao.Server.Test.Utils;

public class StringExtentionTests
{
    [Theory]
    [InlineData("FirstName", "firstName")]
    [InlineData("ResponsibleId", "responsibleId")]
    [InlineData("SomeThirdExample", "someThirdExample")]
    public void ShouldTransformCamelCaseIntoPascalCase(string expected, string actual) => Assert.Equal(expected, actual.ToPascalCase());

    [Theory]
    [InlineData("First name", "FirstName")]
    [InlineData("Another big sentence example", "AnotherBigSentenceExample")]
    [InlineData("Some third example", "SomeThirdExample")]
    public void ShouldTransformPascalCaseToSentence(string expected, string actual) => Assert.Equal(expected, actual.ToSentence());
}
