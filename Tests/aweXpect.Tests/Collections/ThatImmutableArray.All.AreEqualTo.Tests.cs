#if NET8_0_OR_GREATER
using System.Collections.Immutable;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatImmutableArray
{
	public sealed partial class All
	{
		public sealed class AreEqualTo
		{
			public sealed class ItemTests
			{
				[Fact]
				public async Task ShouldSupportNullableValues()
				{
					ImmutableArray<int?> subject = Factory.GetConstantValueImmutableArray<int?>(null, 20);

					async Task Act()
						=> await That(subject).All().AreEqualTo(null);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
				{
					ImmutableArray<int> subject = Factory.GetFibonacciNumbersImmutableArray(20);

					async Task Act()
						=> await That(subject).All().AreEqualTo(5);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to 5 for all items,
						             but only 1 of 20 were
						             """);
				}

				[Fact]
				public async Task WhenNoItemsDiffer_ShouldSucceed()
				{
					int constantValue = 42;
					ImmutableArray<int> subject = Factory.GetConstantValueImmutableArray(constantValue, 20);

					async Task Act()
						=> await That(subject).All().AreEqualTo(constantValue);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					int constantValue = 42;
					ImmutableArray<int>? subject = null!;

					async Task Act()
						=> await That(subject).All().AreEqualTo(constantValue);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to 42 for all items,
						             but it was <null>
						             """);
				}
			}

			public sealed class StringItemTests
			{
				[Fact]
				public async Task ShouldSupportNullableValues()
				{
					ImmutableArray<string?> subject = Factory.GetConstantValueImmutableArray<string?>(null, 20);

					async Task Act()
						=> await That(subject).All().AreEqualTo(null);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
				{
					ImmutableArray<string> subject = Factory.GetFibonacciNumbersImmutableArray(i => $"item-{i}", 10);

					async Task Act()
						=> await That(subject).All().AreEqualTo("item-5");

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to "item-5" for all items,
						             but only 1 of 10 were
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldShowAllConfigurationsInMessage()
				{
					ImmutableArray<string?> subject = ["bar",];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo")
							.IgnoringCase()
							.IgnoringNewlineStyle()
							.IgnoringLeadingWhiteSpace()
							.IgnoringTrailingWhiteSpace();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to "foo" ignoring case, white-space and newline style for all items,
						             but only 0 of 1 were
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldShowIgnoringCaseInMessage()
				{
					ImmutableArray<string> subject = ["bar",];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringCase();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to "foo" ignoring case for all items,
						             but only 0 of 1 were
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldShowIgnoringLeadingWhiteSpaceInMessage()
				{
					ImmutableArray<string> subject = ["bar",];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringLeadingWhiteSpace();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to "foo" ignoring leading white-space for all items,
						             but only 0 of 1 were
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldShowIgnoringNewlineStyleInMessage()
				{
					ImmutableArray<string> subject = ["bar",];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringNewlineStyle();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to "foo" ignoring newline style for all items,
						             but only 0 of 1 were
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldShowIgnoringTrailingWhiteSpaceInMessage()
				{
					ImmutableArray<string> subject = ["bar",];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringTrailingWhiteSpace();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to "foo" ignoring trailing white-space for all items,
						             but only 0 of 1 were
						             """);
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenItemsDifferInCase_ShouldSucceedWhenIgnoringCase(bool ignoreCase)
				{
					ImmutableArray<string> subject = ["foo", "FOO",];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringCase(ignoreCase);

					await That(Act).Throws<XunitException>().OnlyIf(!ignoreCase)
						.WithMessage("""
						             Expected that subject
						             is equal to "foo" for all items,
						             but only 1 of 2 were
						             """);
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenItemsDifferInLeadingWhiteSpace_ShouldSucceedWhenIgnoringLeadingWhiteSpace(
					bool ignoreLeadingWhiteSpace)
				{
					ImmutableArray<string> subject = [" foo", "foo", "\tfoo",];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo")
							.IgnoringLeadingWhiteSpace(ignoreLeadingWhiteSpace);

					await That(Act).Throws<XunitException>().OnlyIf(!ignoreLeadingWhiteSpace)
						.WithMessage("""
						             Expected that subject
						             is equal to "foo" for all items,
						             but only 1 of 3 were
						             """);
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenItemsDifferInNewlineStyle_ShouldSucceedWhenIgnoringNewlineStyle(
					bool ignoreNewlineStyle)
				{
					ImmutableArray<string> subject = ["foo\r\nbar", "foo\nbar", "foo\rbar",];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo\nbar").IgnoringNewlineStyle(ignoreNewlineStyle);

					await That(Act).Throws<XunitException>().OnlyIf(!ignoreNewlineStyle)
						.WithMessage("""
						             Expected that subject
						             is equal to "foo\nbar" for all items,
						             but only 1 of 3 were
						             """);
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenItemsDifferInTrailingWhiteSpace_ShouldSucceedWhenIgnoringTrailingWhiteSpace(
					bool ignoreTrailingWhiteSpace)
				{
					ImmutableArray<string> subject = ["foo ", "foo", "foo\t",];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo")
							.IgnoringTrailingWhiteSpace(ignoreTrailingWhiteSpace);

					await That(Act).Throws<XunitException>().OnlyIf(!ignoreTrailingWhiteSpace)
						.WithMessage("""
						             Expected that subject
						             is equal to "foo" for all items,
						             but only 1 of 3 were
						             """);
				}

				[Fact]
				public async Task WhenNoItemsDiffer_ShouldSucceed()
				{
					string constantValue = "foo";
					ImmutableArray<string> subject = Factory.GetConstantValueImmutableArray(constantValue, 20);

					async Task Act()
						=> await That(subject).All().AreEqualTo(constantValue);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					string constantValue = "foo";
					ImmutableArray<string>? subject = null;

					async Task Act()
						=> await That(subject).All().AreEqualTo(constantValue);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to "foo" for all items,
						             but it was <null>
						             """);
				}
			}
		}
	}
}
#endif
