using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CursoProgressao.Shared.Test.Attributes;

public class RequiredAttributeTest
{
    [Fact]
    public void ShouldAcceptValue()
    {
        Tester tester = new() { Value = "Pedro" };
        ValidationContext context = new(tester);

        var expected = true;
        var actual = Validator.TryValidateObject(tester, context, null, true);

        Assert.True(expected == actual);
    }

    [Fact]
    public void ShouldNotAcceptValue()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Tester tester = new() { Value = null };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        ValidationContext context = new(tester);

        var expected = false;
        var actual = Validator.TryValidateObject(tester, context, null, true);

        Assert.True(expected == actual);
    }

    private class Tester
    {
        [Shared.Attributes.Required]
        public string Value { get; set; } = null!;
    }
}
