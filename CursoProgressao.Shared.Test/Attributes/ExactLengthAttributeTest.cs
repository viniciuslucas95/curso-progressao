using CursoProgressao.Shared.Attributes;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CursoProgressao.Shared.Test.Attributes;

public class ExactLengthAttributeTest
{
    [Fact]
    public void ShouldAcceptValue()
    {
        Tester tester = new() { Value = "469879469" };
        ValidationContext context = new(tester);

        var expected = true;
        var actual = Validator.TryValidateObject(tester, context, null, true);

        Assert.True(expected == actual);
    }

    [Theory]
    [InlineData("26498746")]
    [InlineData("2649874612")]
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
        [ExactLength(9)]
        public string Value { get; set; } = null!;
    }
}
