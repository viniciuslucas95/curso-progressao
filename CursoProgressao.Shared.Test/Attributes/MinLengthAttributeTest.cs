using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CursoProgressao.Shared.Test.Attributes;

public class MinLengthAttributeTest
{
    [Fact]
    public void ShouldAcceptValue()
    {
        Tester tester = new() { Value = "Carlos" };
        ValidationContext context = new(tester);

        var expected = true;
        var actual = Validator.TryValidateObject(tester, context, null, true);

        Assert.True(expected == actual);
    }

    [Theory]
    [InlineData("C")]
    [InlineData("")]
    public void ShouldNotAcceptValue(string value)
    {
        Tester tester = new() { Value = value };
        ValidationContext context = new(tester);

        var expected = false;
        var actual = Validator.TryValidateObject(tester, context, null, true);

        Assert.True(expected == actual);
    }

    private class Tester
    {
        [Shared.Attributes.MinLength(2)]
        public string Value { get; set; } = null!;
    }
}
