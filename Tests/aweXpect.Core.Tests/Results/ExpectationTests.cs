using aweXpect.Results;

namespace aweXpect.Core.Tests.Results;

public class ExpectationTests
{
	[Fact]
	public async Task Equals_ShouldThrowNotSupportedException()
	{
		Expectation sut = That(true).IsTrue();

		bool Act() => sut.Equals(That(true).IsTrue());

		await That(Act).Does().Throw<NotSupportedException>()
			.WithMessage("Equals is not supported. Did you mean Be() instead?");
	}

	[Fact]
	public async Task GetHashCode_ShouldThrowNotSupportedException()
	{
		Expectation sut = That(true).IsTrue();

		int Act() => sut.GetHashCode();

		await That(Act).Does().Throw<NotSupportedException>()
			.WithMessage("GetHashCode is not supported.");
	}
}
