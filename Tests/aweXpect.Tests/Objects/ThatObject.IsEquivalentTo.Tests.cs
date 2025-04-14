﻿using System.Collections.Generic;
using aweXpect.Equivalency;

namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed class IsEquivalentTo
	{
		public sealed class Tests
		{
			[Fact]
			public async Task BasicObjects_ShouldBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
				};
				OuterClass expected = new()
				{
					Value = "Foo",
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task MismatchedObjects_ShouldNotBeEquivalent()
			{
				OuterClass subject = new();
				OuterClass expected = new()
				{
					Value = "Foo",
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Property Value was <null> instead of "Foo"

					             Equivalency options:
					              - include public fields and properties
					             """);
			}

			[Fact]
			public async Task ObjectsWithNestedEnumerableMatches_ShouldBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3",],
						},
					},
				};
				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3",],
						},
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ObjectsWithNestedEnumerableMismatch_ShouldNotBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3",],
						},
					},
				};

				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3", "4",],
						},
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Element Inner.Inner.Collection[3] was missing "4"

					             Equivalency options:
					              - include public fields and properties
					             """);
			}

			[Fact]
			public async Task
				ObjectsWithNestedEnumerableMismatch_WithIgnoreRule_ShouldBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3",],
						},
					},
				};

				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3", "4",],
						},
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IgnoringMember("Inner.Inner.Collection[3]"));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task
				ObjectsWithNestedEnumerableMismatch_WithIncorrectIgnoreRule_ShouldFail()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3",],
						},
					},
				};

				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Bart",
							Collection = ["1", "2", "3", "4",],
						},
					},
				};

				async Task Act()
					=> await That(subject)
						.IsEquivalentTo(expected, o => o.IgnoringMember("Inner.Inner.Collection.[3]"));

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Element Inner.Inner.Collection[3] was missing "4"
					             and
					               Property Inner.Inner.Value differed:
					                    Found: "Baz"
					                 Expected: "Bart"

					             Equivalency options:
					              - include public fields and properties
					              - ignore members: ["Inner.Inner.Collection.[3]"]
					             """);
			}

			[Fact]
			public async Task ObjectsWithNestedMatches_ShouldBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
						},
					},
				};
				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
						},
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ObjectsWithNestedMismatch_ShouldNotBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass(),
					},
				};
				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
						},
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Property Inner.Inner.Value was <null> instead of "Baz"

					             Equivalency options:
					              - include public fields and properties
					             """);
			}
		}

		public sealed class CollectionTests
		{
			[Fact]
			public async Task WhenDifferentValues_ShouldFail()
			{
				int[] subject = [1, 2, 3,];
				int[] expected = [4, 3, 2,];

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IgnoringCollectionOrder());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Element [0] differed:
					                    Found: 1
					                 Expected: 2
					             and
					               Element [1] differed:
					                    Found: 2
					                 Expected: 3
					             and
					               Element [2] differed:
					                    Found: 3
					                 Expected: 4

					             Equivalency options:
					              - include public fields and properties
					              - ignore collection order
					             """);
			}

			[Fact]
			public async Task WhenInDifferentOrder_ShouldFail()
			{
				int[] subject = [1, 2, 3,];
				int[] expected = [1, 3, 2,];

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Element [1] differed:
					                    Found: 2
					                 Expected: 3
					             and
					               Element [2] differed:
					                    Found: 3
					                 Expected: 2

					             Equivalency options:
					              - include public fields and properties
					             """);
			}

			[Fact]
			public async Task WhenInDifferentOrder_WhenIgnoringCollectionOrder_ShouldSucceed()
			{
				int[] subject = [1, 2, 3,];
				int[] expected = [1, 3, 2,];

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IgnoringCollectionOrder());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenInSameOrder_ShouldSucceed()
			{
				int[] subject = [1, 2, 3,];
				int[] expected = [1, 2, 3,];

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class DictionaryTests
		{
			[Fact]
			public async Task WhenDifferentKeys_ShouldFail()
			{
				Dictionary<int, int> subject = new()
				{
					{
						1, 2
					},
					{
						2, 3
					},
					{
						3, 4
					},
				};
				Dictionary<int, int> expected = new()
				{
					{
						1, 2
					},
					{
						3, 3
					},
					{
						4, 4
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IgnoringCollectionOrder());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Element [2] had superfluous 3
					             and
					               Element [3] differed:
					                    Found: 4
					                 Expected: 3
					             and
					               Element [4] was missing 4

					             Equivalency options:
					              - include public fields and properties
					              - ignore collection order
					             """);
			}

			[Fact]
			public async Task WhenDifferentValues_ShouldFail()
			{
				Dictionary<int, int> subject = new()
				{
					{
						1, 2
					},
					{
						2, 3
					},
					{
						3, 4
					},
				};
				Dictionary<int, int> expected = new()
				{
					{
						1, 2
					},
					{
						2, 4
					},
					{
						3, 5
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IgnoringCollectionOrder());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Element [2] differed:
					                    Found: 3
					                 Expected: 4
					             and
					               Element [3] differed:
					                    Found: 4
					                 Expected: 5

					             Equivalency options:
					              - include public fields and properties
					              - ignore collection order
					             """);
			}

			[Fact]
			public async Task WhenSameEntries_ShouldBeEquivalent()
			{
				Dictionary<int, int> subject = new()
				{
					{
						2, 3
					},
					{
						1, 4
					},
				};

				Dictionary<int, int> expected = new()
				{
					{
						2, 3
					},
					{
						1, 4
					},
				};

				await That(subject).IsEquivalentTo(expected);
			}

			[Fact]
			public async Task WhenSameEntriesInDifferentOrder_ShouldBeEquivalent()
			{
				Dictionary<string, string> subject = new(StringComparer.OrdinalIgnoreCase)
				{
					{
						"A", "A"
					},
					{
						"B", "B"
					},
				};

				Dictionary<string, string> expected = new(StringComparer.OrdinalIgnoreCase)
				{
					{
						"B", "B"
					},
					{
						"A", "A"
					},
				};

				await That(subject).IsEquivalentTo(expected);
			}
		}

		public sealed class FieldTests
		{
			[Theory]
			[InlineData(0, 0, 0, true)]
			[InlineData(0, 0, 1, true)]
			[InlineData(0, 1, 0, true)]
			[InlineData(1, 0, 0, false)]
			[InlineData(0, 1, 1, true)]
			[InlineData(1, 0, 1, false)]
			[InlineData(1, 1, 0, false)]
			[InlineData(1, 1, 1, false)]
			public async Task ShouldIgnoreInternalAndPrivateFields(int publicDifference, int internalDifference,
				int privateDifference, bool expectSuccess)
			{
				MyClass subject = new(1, 2, 3);
				MyClass expected = new(
					1 + publicDifference,
					2 + internalDifference,
					3 + privateDifference);

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected that subject
					              is equivalent to expected,
					              but it was not:
					                Field PublicValue differed:
					                     Found: 1
					                  Expected: {1 + publicDifference}

					              Equivalency options:
					               - include public fields and properties
					              """);
			}

			[Theory]
			[InlineData(0, 0, 0, true)]
			[InlineData(0, 0, 1, true)]
			[InlineData(0, 1, 0, false)]
			[InlineData(1, 0, 0, true)]
			[InlineData(0, 1, 1, false)]
			[InlineData(1, 0, 1, true)]
			[InlineData(1, 1, 0, false)]
			[InlineData(1, 1, 1, false)]
			public async Task WithInternalFields_ShouldFailWhenInternalFieldIsDifferent(int publicDifference,
				int internalDifference, int privateDifference,
				bool expectSuccess)
			{
				MyClass subject = new(1, 2, 3);
				MyClass expected = new(
					1 + publicDifference,
					2 + internalDifference,
					3 + privateDifference);

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IncludingFields(IncludeMembers.Internal));

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected that subject
					              is equivalent to expected,
					              but it was not:
					                Field InternalValue differed:
					                     Found: 2
					                  Expected: {2 + internalDifference}

					              Equivalency options:
					               - include internal fields and public properties
					              """);
			}

			[Fact]
			public async Task WithNoFields_ShouldConsiderProperties()
			{
				MyClass subject = new(1, 2, 3);
				MyClass expected = new(4, 5, 6);

				expected.MyProperty = !subject.MyProperty;

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IncludingFields(IncludeMembers.None));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Property MyProperty differed:
					                    Found: False
					                 Expected: True

					             Equivalency options:
					              - include no fields and public properties
					             """);
			}

			[Fact]
			public async Task WithNoFields_ShouldSucceed()
			{
				MyClass subject = new(1, 2, 3);
				MyClass expected = new(4, 5, 6);

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IncludingFields(IncludeMembers.None));

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(0, 0, 0, true)]
			[InlineData(0, 0, 1, false)]
			[InlineData(0, 1, 0, true)]
			[InlineData(1, 0, 0, true)]
			[InlineData(0, 1, 1, false)]
			[InlineData(1, 0, 1, false)]
			[InlineData(1, 1, 0, true)]
			[InlineData(1, 1, 1, false)]
			public async Task WithPrivateFields_ShouldFailWhenPrivateFieldIsDifferent(int publicDifference,
				int internalDifference, int privateDifference,
				bool expectSuccess)
			{
				MyClass subject = new(1, 2, 3);
				MyClass expected = new(
					1 + publicDifference,
					2 + internalDifference,
					3 + privateDifference);

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IncludingFields(IncludeMembers.Private));

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected that subject
					              is equivalent to expected,
					              but it was not:
					                Field PrivateValue differed:
					                     Found: 3
					                  Expected: {3 + privateDifference}

					              Equivalency options:
					               - include private fields and public properties
					              """);
			}

			private sealed class MyClass(int publicValue, int internalValue, int privateValue)
			{
				internal int InternalValue = internalValue;
				private int PrivateValue = privateValue;
				public int PublicValue = publicValue;
				public bool MyProperty { get; set; }
			}
		}

		public sealed class PropertyTests
		{
			[Theory]
			[InlineData(0, 0, 0, true)]
			[InlineData(0, 0, 1, true)]
			[InlineData(0, 1, 0, true)]
			[InlineData(1, 0, 0, false)]
			[InlineData(0, 1, 1, true)]
			[InlineData(1, 0, 1, false)]
			[InlineData(1, 1, 0, false)]
			[InlineData(1, 1, 1, false)]
			public async Task ShouldIgnoreInternalAndPrivateProperties(int publicDifference, int internalDifference,
				int privateDifference, bool expectSuccess)
			{
				MyClass subject = new(1, 2, 3);
				MyClass expected = new(
					1 + publicDifference,
					2 + internalDifference,
					3 + privateDifference);

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected that subject
					              is equivalent to expected,
					              but it was not:
					                Property PublicValue differed:
					                     Found: 1
					                  Expected: {1 + publicDifference}

					              Equivalency options:
					               - include public fields and properties
					              """);
			}

			[Theory]
			[InlineData(0, 0, 0, true)]
			[InlineData(0, 0, 1, true)]
			[InlineData(0, 1, 0, false)]
			[InlineData(1, 0, 0, true)]
			[InlineData(0, 1, 1, false)]
			[InlineData(1, 0, 1, true)]
			[InlineData(1, 1, 0, false)]
			[InlineData(1, 1, 1, false)]
			public async Task WithInternalProperties_ShouldFailWhenInternalPropertyIsDifferent(int publicDifference,
				int internalDifference, int privateDifference,
				bool expectSuccess)
			{
				MyClass subject = new(1, 2, 3);
				MyClass expected = new(
					1 + publicDifference,
					2 + internalDifference,
					3 + privateDifference);

				async Task Act()
					=> await That(subject)
						.IsEquivalentTo(expected, o => o.IncludingProperties(IncludeMembers.Internal));

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected that subject
					              is equivalent to expected,
					              but it was not:
					                Property InternalValue differed:
					                     Found: 2
					                  Expected: {2 + internalDifference}

					              Equivalency options:
					               - include public fields and internal properties
					              """);
			}

			[Fact]
			public async Task WithNoProperties_ShouldConsiderFields()
			{
				MyClass subject = new(1, 2, 3);
				MyClass expected = new(4, 5, 6);

				expected.MyField = !subject.MyField;

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IncludingProperties(IncludeMembers.None));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Field MyField differed:
					                    Found: False
					                 Expected: True

					             Equivalency options:
					              - include public fields and no properties
					             """);
			}

			[Fact]
			public async Task WithNoProperties_ShouldSucceed()
			{
				MyClass subject = new(1, 2, 3);
				MyClass expected = new(4, 5, 6);

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IncludingProperties(IncludeMembers.None));

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(0, 0, 0, true)]
			[InlineData(0, 0, 1, false)]
			[InlineData(0, 1, 0, true)]
			[InlineData(1, 0, 0, true)]
			[InlineData(0, 1, 1, false)]
			[InlineData(1, 0, 1, false)]
			[InlineData(1, 1, 0, true)]
			[InlineData(1, 1, 1, false)]
			public async Task WithPrivateProperties_ShouldFailWhenPrivatePropertyIsDifferent(int publicDifference,
				int internalDifference, int privateDifference,
				bool expectSuccess)
			{
				MyClass subject = new(1, 2, 3);
				MyClass expected = new(
					1 + publicDifference,
					2 + internalDifference,
					3 + privateDifference);

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IncludingProperties(IncludeMembers.Private));

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected that subject
					              is equivalent to expected,
					              but it was not:
					                Property PrivateValue differed:
					                     Found: 3
					                  Expected: {3 + privateDifference}

					              Equivalency options:
					               - include public fields and private properties
					              """);
			}

			private sealed class MyClass(int publicValue, int internalValue, int privateValue)
			{
				public bool MyField;
				internal int InternalValue { get; set; } = internalValue;
				private int PrivateValue { get; set; } = privateValue;
				public int PublicValue { get; set; } = publicValue;
			}
		}
	}
}
