#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class All
	{
		public sealed class Are
		{
			public sealed class ItemTests
			{
				[Fact]
				public async Task DoesNotEnumerateTwice()
				{
					ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

					async Task Act()
						=> await That(subject).All().Are(1)
							.And.All().Are(1);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task DoesNotMaterializeAsyncEnumerable()
				{
					IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

					async Task Act()
						=> await That(subject).All().Are(1);

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
					IAsyncEnumerable<int?> subject = Factory.GetConstantValueAsyncEnumerable<int?>(null, 20);

					async Task Act()
						=> await That(subject).All().Are((int?)null);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task ShouldUseCustomComparer()
				{
					IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

					async Task Act()
						=> await That(subject).All().Are(5).Using(new AllEqualComparer());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
				{
					IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

					async Task Act()
						=> await That(subject).All().Are(5);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to 5,
						             but not all were
						             """);
				}

				[Fact]
				public async Task WhenNoItemsDiffer_ShouldSucceed()
				{
					int constantValue = 42;
					IAsyncEnumerable<int> subject = Factory.GetConstantValueAsyncEnumerable(constantValue, 20);

					async Task Act()
						=> await That(subject).All().Are(constantValue);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					int constantValue = 42;
					IAsyncEnumerable<int>? subject = null;

					async Task Act()
						=> await That(subject).All().Are(constantValue);

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
				public async Task DoesNotMaterializeAsyncEnumerable()
				{
					IAsyncEnumerable<string> subject = Factory.GetAsyncFibonacciNumbers(i => $"item-{i}");

					async Task Act()
						=> await That(subject).All().Are("item-1");

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
					IAsyncEnumerable<string?> subject = Factory.GetConstantValueAsyncEnumerable<string?>(null, 20);

					async Task Act()
						=> await That(subject).All().Are(null);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task ShouldUseCustomComparer()
				{
					IAsyncEnumerable<string> subject = Factory.GetAsyncFibonacciNumbers(i => $"item-{i}", 20);

					async Task Act()
						=> await That(subject).All().Are("item-5").Using(new AllEqualComparer());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
				{
					IAsyncEnumerable<string> subject = Factory.GetAsyncFibonacciNumbers(i => $"item-{i}", 10);

					async Task Act()
						=> await That(subject).All().Are("item-5");

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to "item-5",
						             but not all were
						             """);
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenItemsDifferInCase_ShouldSucceedWhenIgnoringCase(bool ignoreCase)
				{
					IAsyncEnumerable<string> subject = ToAsyncEnumerable(["foo", "FOO"]);

					async Task Act()
						=> await That(subject).All().Are("foo").IgnoringCase(ignoreCase);

					await That(Act).Throws<XunitException>().OnlyIf(!ignoreCase)
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo",
						             but not all were
						             """);
				}

				[Fact]
				public async Task WhenNoItemsDiffer_ShouldSucceed()
				{
					string constantValue = "foo";
					IAsyncEnumerable<string> subject = Factory.GetConstantValueAsyncEnumerable(constantValue, 20);

					async Task Act()
						=> await That(subject).All().Are(constantValue);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					string constantValue = "foo";
					IAsyncEnumerable<string>? subject = null;

					async Task Act()
						=> await That(subject).All().Are(constantValue);

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
#endif
