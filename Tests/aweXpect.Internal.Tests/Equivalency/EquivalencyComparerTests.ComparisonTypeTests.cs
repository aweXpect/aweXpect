using aweXpect.Equivalency;

namespace aweXpect.Internal.Tests.Equivalency;

public sealed partial class EquivalencyComparerTests
{
	public sealed class ComparisonTypeTests
	{
		[Fact]
		public async Task WhenComparingByValue_ShouldUseObjectEqualsForClasses()
		{
			MyClass actual = new()
			{
				MyValue = "foo"
			};
			MyClass expected = new()
			{
				MyValue = "foo"
			};
			EquivalencyComparer sut = new(new EquivalencyOptions
			{
				TypeComparison = _ => EquivalencyOptions.ComparisonType.ByValue
			});

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not:
			                                It differed:
			                                     Found: MyClass { MyValue = "foo", Nested = <null> }
			                                  Expected: MyClass { MyValue = "foo", Nested = <null> }
			                              """);
		}
	}
}
