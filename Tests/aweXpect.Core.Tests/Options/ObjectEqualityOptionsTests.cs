using aweXpect.Equivalency;
using aweXpect.Options;

namespace aweXpect.Core.Tests.Options;

public class ObjectEqualityOptionsTests
{
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async Task WhenToleranceIsNegative_ShouldThrowArgumentOutOfRangeException(bool isEqual)
	{
		ObjectEqualityOptions<object?> sut = new ObjectEqualityOptions<object?>().Equivalent(new EquivalencyOptions());
		EqualsObject a = new(isEqual);
		EqualsObject b = new(isEqual);

		sut.Equals();

		await That(sut.AreConsideredEqual(a, b)).IsEqualTo(isEqual);
	}

	private class EqualsObject(bool isEqual)
	{
		public override bool Equals(object? obj) => isEqual;

		public override int GetHashCode() => throw new NotSupportedException();
	}
}
