using CursoProgressao.Shared.Attributes;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CursoProgressao.Shared.Test.Attributes;

public class DayAttributeTest
{
    [Fact]
    public void ShouldAcceptValue()
    {
        Tester tester = new() { Value = 15 };
        ValidationContext context = new(tester);

        var expected = true;
        var actual = Validator.TryValidateObject(tester, context, null, true);

        Assert.True(expected == actual);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(32)]
    [InlineData(-50)]
    [InlineData(50)]
    public void ShouldNotAcceptValue(int value)
    {
        Tester tester = new() { Value = value };
        ValidationContext context = new(tester);

        var expected = false;
        var actual = Validator.TryValidateObject(tester, context, null, true);

        Assert.True(expected == actual);
    }

    private class Tester
    {
        [Day]
        public int Value { get; set; }
    }
}
