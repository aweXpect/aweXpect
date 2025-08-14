using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class IsEqualTo
	{
		public sealed class PredicatesTests
		{
			[Fact]
			public async Task AllItemsMatchingPredicates_ShouldSucceed()
			{
				IEnumerable<int> subject = new[] { 2, 4, 6 };

				async Task Act()
					=> await That(subject).IsEqualToWithPredicates(new Func<int, bool>[]
					{
						x => x % 2 == 0,  // even
						x => x > 3,       // greater than 3
						x => x < 10       // less than 10
					});

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task OneItemNotMatchingPredicate_ShouldFail()
			{
				IEnumerable<int> subject = new[] { 2, 4, 6 };

				async Task Act()
					=> await That(subject).IsEqualToWithPredicates(new Func<int, bool>[]
					{
						x => x % 2 == 0,  // even (should pass for 2)
						x => x % 2 != 0,  // odd (should fail for 4)
						x => x < 10       // less than 10 (should pass for 6)
					});

				await That(Act).ThrowsException()
					.WithMessage("*had 1 item(s) that did not match their corresponding predicate*Item at index 1: 4*");
			}

			[Fact]
			public async Task CountMismatch_ShouldFail()
			{
				IEnumerable<int> subject = new[] { 2, 4, 6 };

				async Task Act()
					=> await That(subject).IsEqualToWithPredicates(new Func<int, bool>[]
					{
						x => x % 2 == 0  // only one predicate for 3 items
					});

				await That(Act).ThrowsException()
					.WithMessage("*had 3 items but expected 1 predicates*");
			}

			[Fact]
			public async Task NullCollection_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).IsEqualToWithPredicates(new Func<int, bool>[]
					{
						x => x > 0
					});

				await That(Act).ThrowsException()
					.WithMessage("*subject was <null>*");
			}

			[Fact]
			public async Task EmptyCollectionWithEmptyPredicates_ShouldSucceed()
			{
				IEnumerable<int> subject = Enumerable.Empty<int>();

				async Task Act()
					=> await That(subject).IsEqualToWithPredicates(new Func<int, bool>[0]);

				await That(Act).DoesNotThrow();
			}
		}
	}
}