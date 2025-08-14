using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class Contains
	{
		public sealed class PredicatesTests
		{
			[Fact]
			public async Task CollectionContainingItemsForAllPredicates_ShouldSucceed()
			{
				IEnumerable<int> subject = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

				async Task Act()
					=> await That(subject).ContainsWithPredicates(new Func<int, bool>[]
					{
						x => x % 2 == 0,  // has even numbers
						x => x > 5        // has numbers greater than 5
					});

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task CollectionMissingItemsForSomePredicates_ShouldFail()
			{
				IEnumerable<int> subject = new[] { 2, 4, 6, 8 };

				async Task Act()
					=> await That(subject).ContainsWithPredicates(new Func<int, bool>[]
					{
						x => x % 2 == 0,  // has even numbers (should pass)
						x => x % 2 != 0   // has odd numbers (should fail)
					});

				await That(Act).ThrowsException()
					.WithMessage("*did not contain items satisfying 1 of 2 predicates (predicates at indices: 1)*");
			}

			[Fact]
			public async Task NullCollection_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).ContainsWithPredicates(new Func<int, bool>[]
					{
						x => x > 0
					});

				await That(Act).ThrowsException()
					.WithMessage("*subject was <null>*");
			}

			[Fact]
			public async Task EmptyPredicatesCollection_ShouldSucceed()
			{
				IEnumerable<int> subject = new[] { 1, 2, 3 };

				async Task Act()
					=> await That(subject).ContainsWithPredicates(new Func<int, bool>[0]);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class ExpectationsTests
		{
			[Fact]
			public async Task CollectionContainingItemsForAllExpectations_ShouldSucceed()
			{
				IEnumerable<string> subject = new[] { "hello", "world", "123", "test" };

				async Task Act()
					=> await That(subject).ContainsWithExpectations(new Action<IThat<string>>[]
					{
						x => x.HasLength().EqualTo(5),      // "hello" or "world"
						x => x.IsEqualTo("123")             // "123"
					});

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task CollectionMissingItemsForSomeExpectations_ShouldFail()
			{
				IEnumerable<string> subject = new[] { "hello", "world" };

				async Task Act()
					=> await That(subject).ContainsWithExpectations(new Action<IThat<string>>[]
					{
						x => x.HasLength().EqualTo(5),      // "hello" or "world" (should pass)
						x => x.IsEqualTo("123")             // no "123" string (should fail)
					});

				await That(Act).ThrowsException()
					.WithMessage("*did not contain items satisfying 1 of 2 expectations*");
			}

			[Fact]
			public async Task NullCollection_ShouldFail()
			{
				IEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).ContainsWithExpectations(new Action<IThat<string>>[]
					{
						x => x.IsNotEmpty()
					});

				await That(Act).ThrowsException()
					.WithMessage("*subject was <null>*");
			}

			[Fact]
			public async Task EmptyExpectationsCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = new[] { "test" };

				async Task Act()
					=> await That(subject).ContainsWithExpectations(new Action<IThat<string>>[0]);

				await That(Act).DoesNotThrow();
			}
		}
	}
}