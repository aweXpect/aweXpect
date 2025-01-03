using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.Core.Constraints;

public class ConstraintResultTests
{
	[Fact]
	public async Task Success_T_TryGetValue_WhenTypeDoesNotMatch_ShouldReturnFalse()
	{
		ConstraintResult.Success<int> sut = new(1, "foo");

		bool result = sut.TryGetValue(out string? value);

		await That(result).Should().BeFalse();
		await That(value).Should().BeNull();
	}

	[Fact]
	public async Task Success_T_TryGetValue_WhenTypeMatches_ShouldReturnTrue()
	{
		ConstraintResult.Success<string> sut = new("bar", "foo");

		bool result = sut.TryGetValue(out string? value);

		await That(result).Should().BeTrue();
		await That(value).Should().Be("bar");
	}

	[Fact]
	public async Task Success_TryGetValue_ShouldReturnFalse()
	{
		ConstraintResult.Success sut = new("foo");

		bool result = sut.TryGetValue(out string? value);

		await That(result).Should().BeFalse();
		await That(value).Should().BeNull();
	}
}
