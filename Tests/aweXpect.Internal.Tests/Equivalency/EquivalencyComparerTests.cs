using System.Collections.Generic;
using aweXpect.Equivalency;
using aweXpect.Options;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable NotAccessedField.Local
namespace aweXpect.Internal.Tests.Equivalency;

public class EquivalencyComparerTests
{
	public sealed class Tests
	{
		[Fact]
		public async Task ShouldBeEquivalentToSelf()
		{
			MyClass actual = new()
			{
				MyValue = "foo"
			};
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, actual);

			await That(result).IsTrue();
		}

		[Fact]
		public async Task ShouldPreventCyclicReferences()
		{
			MyClass actual = new()
			{
				MyValue = "foo"
			};
			MyClass expected = new()
			{
				MyValue = "bar"
			};
			actual.Nested = expected;
			expected.Nested = actual;
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not equivalent:
			                                Property MyValue differed:
			                                     Found: "foo"
			                                  Expected: "bar"
			                              and
			                                Property Nested.MyValue differed:
			                                     Found: "bar"
			                                  Expected: "foo"
			                              """);
		}

		[Fact]
		public async Task WhenActualAndExpectedAreNull_ShouldBeConsideredEqual()
		{
			MyClass? actual = null;
			MyClass? expected = null;
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);

			await That(result).IsTrue();
		}

		[Fact]
		public async Task WhenOnlyActualIsNull_ShouldNotBeConsideredEqual()
		{
			MyClass? actual = null;
			MyClass expected = new();
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was <null> instead of MyClass {
			                                  MyValue = <null>,
			                                  Nested = <null>
			                                }
			                              """);
		}

		[Fact]
		public async Task WhenOnlyExpectedIsNull_ShouldNotBeConsideredEqual()
		{
			MyClass actual = new();
			MyClass? expected = null;
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was MyClass {
			                                  MyValue = <null>,
			                                  Nested = <null>
			                                } instead of <null>
			                              """);
		}
	}

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
			                               it was not equivalent:
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
			await That(failure).IsEqualTo($"""
			                               it was not equivalent:
			                                 Property MyValue differed:
			                                      Found: "foo"
			                                   Expected: "bar"
			                               """);
		}
	}

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
			                               it was not equivalent:
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
			await That(failure).IsEqualTo($"""
			                               it was not equivalent:
			                                 Field MyValue differed:
			                                      Found: "foo"
			                                   Expected: "bar"
			                               """);
		}
	}

	public sealed class CollectionTests
	{
		[Fact]
		public async Task ArrayAndListWithSameValues_ShouldBeConsideredEqual()
		{
			int[] actual = [1, 2, 3, 4, 5];
			List<int> expected = [1, 2, 3, 4, 5];
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);

			await That(result).IsTrue();
		}

		[Fact]
		public async Task ListAndArrayWithSameValues_ShouldBeConsideredEqual()
		{
			List<int> actual = [1, 2, 3, 4, 5];
			int[] expected = [1, 2, 3, 4, 5];
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);

			await That(result).IsTrue();
		}

		[Fact]
		public async Task WhenActualHasFewerValues_ShouldNotBeConsideredEqual()
		{
			int[] actual = [1, 2, 3, 4];
			List<int> expected = [1, 2, 3, 4, 5];
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not equivalent:
			                                Element [4] was missing 5
			                              """);
		}

		[Fact]
		public async Task WhenActualHasMoreValues_ShouldNotBeConsideredEqual()
		{
			int[] actual = [1, 2, 3, 4, 5];
			List<int> expected = [1, 2, 3, 4];
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not equivalent:
			                                Element [4] had superfluous 5
			                              """);
		}

		[Fact]
		public async Task WhenCollectionsDifferInOrder_ShouldNotBeConsideredEqual()
		{
			int[] actual = [1, 2, 3, 4, 5];
			List<int> expected = [1, 5, 2, 3, 4];
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not equivalent:
			                                Element [1] differed:
			                                     Found: 2
			                                  Expected: 5
			                              and
			                                Element [2] differed:
			                                     Found: 3
			                                  Expected: 2
			                              and
			                                Element [3] differed:
			                                     Found: 4
			                                  Expected: 3
			                              and
			                                Element [4] differed:
			                                     Found: 5
			                                  Expected: 4
			                              """);
		}
	}

	public sealed class CollectionInAnyOrderTests
	{
		[Fact]
		public async Task ArrayAndListWithSameValues_ShouldBeConsideredEqual()
		{
			int[] actual = [3, 2, 4, 1, 5];
			List<int> expected = [1, 4, 3, 2, 5];
			EquivalencyComparer sut = new(new EquivalencyOptions().IgnoringCollectionOrder());

			bool result = sut.AreConsideredEqual(actual, expected);

			await That(result).IsTrue();
		}

		[Fact]
		public async Task ListAndArrayWithSameValues_ShouldBeConsideredEqual()
		{
			List<int> actual = [1, 5, 3, 4, 2];
			int[] expected = [5, 4, 3, 2, 1];
			EquivalencyComparer sut = new(new EquivalencyOptions().IgnoringCollectionOrder());

			bool result = sut.AreConsideredEqual(actual, expected);

			await That(result).IsTrue();
		}

		[Fact]
		public async Task WhenActualHasFewerValues_ShouldNotBeConsideredEqual()
		{
			int[] actual = [1, 4, 3, 2];
			List<int> expected = [1, 5, 3, 4, 2];
			EquivalencyComparer sut = new(new EquivalencyOptions().IgnoringCollectionOrder());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not equivalent:
			                                Element [4] was missing 5
			                              """);
		}

		[Fact]
		public async Task WhenActualHasMoreValues_ShouldNotBeConsideredEqual()
		{
			int[] actual = [1, 5, 4, 3, 2];
			List<int> expected = [1, 3, 2, 4];
			EquivalencyComparer sut = new(new EquivalencyOptions().IgnoringCollectionOrder());

			bool result = sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not equivalent:
			                                Element [1] had superfluous 5
			                              """);
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public async Task WhenCollectionsDifferInOrder_ShouldBeConsideredEqualWhenCollectionOrderIsIgnored(
			bool ignoreCollectionOrder)
		{
			int[] actual = [1, 2, 3, 4, 5];
			List<int> expected = [1, 5, 2, 3, 4];
			EquivalencyComparer sut = new(new EquivalencyOptions().IgnoringCollectionOrder(ignoreCollectionOrder));

			bool result = sut.AreConsideredEqual(actual, expected);

			await That(result).IsEqualTo(ignoreCollectionOrder);
		}
	}


	private class MyClass
	{
		public string? MyValue { get; set; }
		public MyClass? Nested { get; set; }
	}

	private class MyClassWithField
	{
		public string? MyValue;
	}
}
