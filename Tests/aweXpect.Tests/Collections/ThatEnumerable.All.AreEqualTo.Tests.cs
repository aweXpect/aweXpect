using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class All
	{
		public sealed class AreEqualTo
		{
			public sealed class ItemTests
			{
				[Fact]
				public async Task DoesNotEnumerateTwice()
				{
					ThrowWhenIteratingTwiceEnumerable subject = new();

					async Task Act()
						=> await That(subject).All().AreEqualTo(1)
							.And.All().AreEqualTo(1);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IEnumerable<int> subject = Factory.GetFibonacciNumbers();

					async Task Act()
						=> await That(subject).All().AreEqualTo(1);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to 1,
						             but not all were
						             """);
				}

				[Fact]
				public async Task ShouldSupportNullableValues()
				{
					IEnumerable<int?> subject = Factory.GetConstantValueEnumerable<int?>(null, 20);

					async Task Act()
						=> await That(subject).All().AreEqualTo(null);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task ShouldUseCustomComparer()
				{
					int[] subject = Factory.GetFibonacciNumbers(20).ToArray();

					async Task Act()
						=> await That(subject).All().AreEqualTo(5).Using(new AllEqualComparer());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
				{
					int[] subject = Factory.GetFibonacciNumbers(20).ToArray();

					async Task Act()
						=> await That(subject).All().AreEqualTo(5);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to 5,
						             but only 1 of 20 were
						             """);
				}

				[Fact]
				public async Task WhenNoItemsDiffer_ShouldSucceed()
				{
					int constantValue = 42;
					int[] subject = Factory.GetConstantValueEnumerable(constantValue, 20).ToArray();

					async Task Act()
						=> await That(subject).All().AreEqualTo(constantValue);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					int constantValue = 42;
					IEnumerable<int>? subject = null!;

					async Task Act()
						=> await That(subject).All().AreEqualTo(constantValue);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to 42,
						             but it was <null>
						             """);
				}
			}

			public sealed class StringItemTests
			{
				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IEnumerable<string> subject = Factory.GetFibonacciNumbers(i => $"item-{i}");

					async Task Act()
						=> await That(subject).All().AreEqualTo("item-1");

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to "item-1",
						             but not all were
						             """);
				}

				[Fact]
				public async Task ShouldSupportNullableValues()
				{
					IEnumerable<string?> subject = Factory.GetConstantValueEnumerable<string?>(null, 20);

					async Task Act()
						=> await That(subject).All().AreEqualTo(null);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task ShouldUseCustomComparer()
				{
					string[] subject = Factory.GetFibonacciNumbers(i => $"item-{i}", 20).ToArray();

					async Task Act()
						=> await That(subject).All().AreEqualTo("item-5").Using(new AllEqualComparer());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
				{
					string[] subject = Factory.GetFibonacciNumbers(i => $"item-{i}", 10).ToArray();

					async Task Act()
						=> await That(subject).All().AreEqualTo("item-5");

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to "item-5",
						             but only 1 of 10 were
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldShowAllConfigurationsInMessage()
				{
					string[] subject = ["bar"];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo")
							.IgnoringCase()
							.IgnoringNewlineStyle()
							.IgnoringLeadingWhiteSpace()
							.IgnoringTrailingWhiteSpace();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo" ignoring case, white-space and newline style,
						             but only 0 of 1 were
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldShowIgnoringCaseInMessage()
				{
					string[] subject = ["bar"];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringCase();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo" ignoring case,
						             but only 0 of 1 were
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldShowIgnoringLeadingWhiteSpaceInMessage()
				{
					string[] subject = ["bar"];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringLeadingWhiteSpace();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo" ignoring leading white-space,
						             but only 0 of 1 were
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldShowIgnoringNewlineStyleInMessage()
				{
					string[] subject = ["bar"];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringNewlineStyle();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo" ignoring newline style,
						             but only 0 of 1 were
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldShowIgnoringTrailingWhiteSpaceInMessage()
				{
					string[] subject = ["bar"];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringTrailingWhiteSpace();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo" ignoring trailing white-space,
						             but only 0 of 1 were
						             """);
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenItemsDifferInCase_ShouldSucceedWhenIgnoringCase(bool ignoreCase)
				{
					string[] subject = ["foo", "FOO"];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringCase(ignoreCase);

					await That(Act).Throws<XunitException>().OnlyIf(!ignoreCase)
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo",
						             but only 1 of 2 were
						             """);
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenItemsDifferInLeadingWhiteSpace_ShouldSucceedWhenIgnoringLeadingWhiteSpace(
					bool ignoreLeadingWhiteSpace)
				{
					string[] subject = [" foo", "foo", "\tfoo"];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringLeadingWhiteSpace(ignoreLeadingWhiteSpace);

					await That(Act).Throws<XunitException>().OnlyIf(!ignoreLeadingWhiteSpace)
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo",
						             but only 1 of 3 were
						             """);
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenItemsDifferInNewlineStyle_ShouldSucceedWhenIgnoringNewlineStyle(
					bool ignoreNewlineStyle)
				{
					string[] subject = ["foo\r\nbar", "foo\nbar", "foo\rbar"];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo\nbar").IgnoringNewlineStyle(ignoreNewlineStyle);

					await That(Act).Throws<XunitException>().OnlyIf(!ignoreNewlineStyle)
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo\nbar",
						             but only 1 of 3 were
						             """);
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenItemsDifferInTrailingWhiteSpace_ShouldSucceedWhenIgnoringTrailingWhiteSpace(
					bool ignoreTrailingWhiteSpace)
				{
					string[] subject = ["foo ", "foo", "foo\t"];

					async Task Act()
						=> await That(subject).All().AreEqualTo("foo").IgnoringTrailingWhiteSpace(ignoreTrailingWhiteSpace);

					await That(Act).Throws<XunitException>().OnlyIf(!ignoreTrailingWhiteSpace)
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo",
						             but only 1 of 3 were
						             """);
				}

				[Fact]
				public async Task WhenNoItemsDiffer_ShouldSucceed()
				{
					string constantValue = "foo";
					string[] subject = Factory.GetConstantValueEnumerable(constantValue, 20).ToArray();

					async Task Act()
						=> await That(subject).All().AreEqualTo(constantValue);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					string constantValue = "foo";
					IEnumerable<string>? subject = null;

					async Task Act()
						=> await That(subject).All().AreEqualTo(constantValue);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo",
						             but it was <null>
						             """);
				}
			}
		}
	}
}
