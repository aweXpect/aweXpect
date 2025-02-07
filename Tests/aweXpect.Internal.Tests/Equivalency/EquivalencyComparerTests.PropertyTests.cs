using aweXpect.Equivalency;

namespace aweXpect.Internal.Tests.Equivalency;

public sealed partial class EquivalencyComparerTests
{
	public sealed class PropertyTests
	{
		[Fact]
		public async Task ShouldBeEquivalentToClassWithSameProperties()
		{
			MyClass actual = new()
			{
				MyValue = "foo"
			};
			MyClass expected = new()
			{
				MyValue = "foo"
			};
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);

			await That(result).IsTrue();
		}

		[Fact]
		public async Task ShouldBeEquivalentToDynamicWithProperties()
		{
			MyClass actual = new()
			{
				MyValue = "foo"
			};
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, new
			{
				MyValue = "foo"
			});

			await That(result).IsTrue();
		}

		[Theory]
		[InlineData("foo", null)]
		[InlineData(null, "bar")]
		public async Task ShouldNotBeEquivalentToClassWhenOnePropertyIsNull(
			string? actualValue, string? expectedValue)
		{
			MyClass actual = new()
			{
				MyValue = actualValue
			};
			MyClass expected = new()
			{
				MyValue = expectedValue
			};
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo($"""
			                               it was not:
			                                 Property MyValue was {Formatter.Format(actualValue)} instead of {Formatter.Format(expectedValue)}
			                               """);
		}

		[Fact]
		public async Task ShouldNotBeEquivalentToClassWithDifferentProperties()
		{
			MyClass actual = new()
			{
				MyValue = "foo"
			};
			MyClass expected = new()
			{
				MyValue = "bar"
			};
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not:
			                                Property MyValue differed:
			                                     Found: "foo"
			                                  Expected: "bar"
			                              """);
		}
	}
}
