using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CursoProgressao.Shared.Test.Attributes;

public class PriceAttributeTest
{
    [Theory]
    [InlineData(250)]
    [InlineData(0)]
    public void ShouldAcceptValue(float value)
    {
        Tester tester = new() { Value = value };
        ValidationContext context = new(tester);

        var expected = true;
        var actual = Validator.TryValidateObject(tester, context, null, true);

        Assert.True(expected == actual);
    }

    [Fact]
    public void ShouldNotAcceptValue()
    {
        Tester tester = new() { Value = -250 };
        ValidationContext context = new(tester);

        var expected = false;
        var actual = Validator.TryValidateObject(tester, context, null, true);

        Assert.True(expected == actual);
    }

    private class Tester
    {
        [Shared.Attributes.Price]
        public float Value { get; set; }
    }
}
