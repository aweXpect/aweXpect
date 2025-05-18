﻿using System.Collections.Generic;
using System.Linq;
using aweXpect.Equivalency;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class All
	{
		public sealed class AreEquivalentTo
		{
			public sealed class Tests
			{
				[Fact]
				public async Task DoesNotEnumerateTwice()
				{
					ThrowWhenIteratingTwiceEnumerable subject = new();

					async Task Act()
						=> await That(subject).All().AreEquivalentTo(1)
							.And.All().AreEquivalentTo(1);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IEnumerable<int> subject = Factory.GetFibonacciNumbers();

					async Task Act()
						=> await That(subject).All().AreEquivalentTo(1, o => o.IncludingFields());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equivalent to 1 for all items,
						             but not all were
						             
						             Not matching items:
						             [2, (… and maybe others)]
						             
						             Collection:
						             [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, (… and maybe others)]
						             
						             Equivalency options:
						              - include public fields and properties
						             """);
				}

				[Fact]
				public async Task ShouldUseCustomComparer()
				{
					int[] subject = Factory.GetFibonacciNumbers(20).ToArray();

					async Task Act()
						=> await That(subject).All().AreEquivalentTo(5).Using(new AllEqualComparer());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
				{
					int[] subject = Factory.GetFibonacciNumbers(20).ToArray();

					async Task Act()
						=> await That(subject).All().AreEquivalentTo(5);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equivalent to 5 for all items,
						             but only 1 of 20 were
						             
						             Not matching items:
						             [1, 1, 2, 3, 8, 13, 21, 34, 55, 89, …]
						             
						             Collection:
						             [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, …]
						             
						             Equivalency options:
						              - include public fields and properties
						             """);
				}

				[Fact]
				public async Task WhenNoItemsDiffer_ShouldSucceed()
				{
					int constantValue = 42;
					int[] subject = Factory.GetConstantValueEnumerable(constantValue, 20).ToArray();

					async Task Act()
						=> await That(subject).All().AreEquivalentTo(constantValue);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					int constantValue = 42;
					IEnumerable<int>? subject = null!;

					async Task Act()
						=> await That(subject).All().AreEquivalentTo(constantValue);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equivalent to constantValue for all items,
						             but it was <null>
						             
						             Equivalency options:
						              - include public fields and properties
						             """);
				}
			}
		}
	}
}
