using aweXpect.Equivalency;

namespace aweXpect.Internal.Tests.Equivalency;

public sealed partial class EquivalencyComparerTests
{
	public sealed class FieldTests
	{
		[Fact]
		public async Task ShouldBeEquivalentToClassWithSameFields()
		{
			MyClassWithField actual = new()
			{
				MyValue = "foo"
			};
			MyClassWithField expected = new()
			{
				MyValue = "foo"
			};
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);

			await That(result).IsTrue();
		}

		[Theory]
		[InlineData("foo", null)]
		[InlineData(null, "bar")]
		public async Task ShouldNotBeEquivalentToClassWhenOneFieldIsNull(
			string? actualValue, string? expectedValue)
		{
			MyClassWithField actual = new()
			{
				MyValue = actualValue
			};
			MyClassWithField expected = new()
			{
				MyValue = expectedValue
			};
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo($"""
			                               it was not:
			                                 Field MyValue was {Formatter.Format(actualValue)} instead of {Formatter.Format(expectedValue)}
			                               """);
		}

		[Fact]
		public async Task ShouldNotBeEquivalentToClassWithDifferentFields()
		{
			MyClassWithField actual = new()
			{
				MyValue = "foo"
			};
			MyClassWithField expected = new()
			{
				MyValue = "bar"
			};
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not:
			                                Field MyValue differed:
			                                     Found: "foo"
			                                  Expected: "bar"
			                              """);
		}
	}
}
