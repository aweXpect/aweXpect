using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
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
					ThrowWhenIteratingTwiceEnumerable subject = new();

					async Task Act()
						=> await That(subject).All().Are(1)
							.And.All().Are(1);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IEnumerable<int> subject = Factory.GetFibonacciNumbers();

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
					IEnumerable<int?> subject = Factory.GetConstantValueEnumerable<int?>(null, 20);

					async Task Act()
						=> await That(subject).All().Are((int?)null);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task ShouldUseCustomComparer()
				{
					int[] subject = Factory.GetFibonacciNumbers(20).ToArray();

					async Task Act()
						=> await That(subject).All().Are(5).Using(new AllEqualComparer());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
				{
					int[] subject = Factory.GetFibonacciNumbers(20).ToArray();

					async Task Act()
						=> await That(subject).All().Are(5);

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
						=> await That(subject).All().Are(constantValue);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					int constantValue = 42;
					IEnumerable<int>? subject = null!;

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
				public async Task DoesNotMaterializeEnumerable()
				{
					IEnumerable<string> subject = Factory.GetFibonacciNumbers(i => $"item-{i}");

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
					IEnumerable<string?> subject = Factory.GetConstantValueEnumerable<string?>(null, 20);

					async Task Act()
						=> await That(subject).All().Are(null);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task ShouldUseCustomComparer()
				{
					string[] subject = Factory.GetFibonacciNumbers(i => $"item-{i}", 20).ToArray();

					async Task Act()
						=> await That(subject).All().Are("item-5").Using(new AllEqualComparer());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
				{
					string[] subject = Factory.GetFibonacciNumbers(i => $"item-{i}", 10).ToArray();

					async Task Act()
						=> await That(subject).All().Are("item-5");

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items equal to "item-5",
						             but only 1 of 10 were
						             """);
				}

				[Theory]
				[InlineData(true)]
				[InlineData(false)]
				public async Task WhenItemsDifferInCase_ShouldSucceedWhenIgnoringCase(bool ignoreCase)
				{
					string[] subject = ["foo", "FOO"];

					async Task Act()
						=> await That(subject).All().Are("foo").IgnoringCase(ignoreCase);

					await That(Act).Throws<XunitException>().OnlyIf(!ignoreCase)
						.WithMessage("""
						             Expected subject to
						             have all items equal to "foo",
						             but only 1 of 2 were
						             """);
				}

				[Fact]
				public async Task WhenNoItemsDiffer_ShouldSucceed()
				{
					string constantValue = "foo";
					string[] subject = Factory.GetConstantValueEnumerable(constantValue, 20).ToArray();

					async Task Act()
						=> await That(subject).All().Are(constantValue);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					string constantValue = "foo";
					IEnumerable<string>? subject = null;

					async Task Act()
						=> await That(subject!).All().Are(constantValue);

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
