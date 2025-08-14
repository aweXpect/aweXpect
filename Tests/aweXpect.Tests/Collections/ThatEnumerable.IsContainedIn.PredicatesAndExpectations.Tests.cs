using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class IsContainedIn
	{
		public sealed class PredicatesTests
		{
			[Fact]
			public async Task AllItemsSatisfyingAtLeastOnePredicate_ShouldSucceed()
			{
				IEnumerable<int> subject = new[] { 2, 4 };

				async Task Act()
					=> await That(subject).IsContainedInWithPredicates(new Func<int, bool>[]
					{
						x => x % 2 == 0,  // all are even
						x => x < 10,      // all are less than 10  
						x => x > 0        // all are positive
					});

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task SomeItemsNotSatisfyingAnyPredicate_ShouldFail()
			{
				IEnumerable<int> subject = new[] { 2, 7 };

				async Task Act()
					=> await That(subject).IsContainedInWithPredicates(new Func<int, bool>[]
					{
						x => x % 2 == 0,  // even
						x => x < 5        // less than 5
					});

				await That(Act).ThrowsException()
					.WithMessage("*had 1 item(s) that did not satisfy any predicate*Item at index 1: 7*");
			}

			[Fact]
			public async Task NullCollection_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).IsContainedInWithPredicates(new Func<int, bool>[]
					{
						x => x > 0
					});

				await That(Act).ThrowsException()
					.WithMessage("*subject was <null>*");
			}

			[Fact]
			public async Task EmptyCollectionWithAnyPredicates_ShouldSucceed()
			{
				IEnumerable<int> subject = Enumerable.Empty<int>();

				async Task Act()
					=> await That(subject).IsContainedInWithPredicates(new Func<int, bool>[]
					{
						x => x > 0
					});

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class ExpectationsTests
		{
			[Fact]
			public async Task AllItemsSatisfyingAtLeastOneExpectation_ShouldSucceed()
			{
				IEnumerable<string> subject = new[] { "hello", "hi" };

				async Task Act()
					=> await That(subject).IsContainedInWithExpectations(new Action<IThat<string>>[]
					{
						x => x.HasLength().EqualTo(5),      // "hello"
						x => x.HasLength().EqualTo(2)       // "hi"
					});

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task SomeItemsNotSatisfyingAnyExpectation_ShouldFail()
			{
				IEnumerable<string> subject = new[] { "hello", "verylongstring" };

				async Task Act()
					=> await That(subject).IsContainedInWithExpectations(new Action<IThat<string>>[]
					{
						x => x.HasLength().EqualTo(5),      // "hello" matches
						x => x.HasLength().EqualTo(2)       // "verylongstring" doesn't match either
					});

				await That(Act).ThrowsException()
					.WithMessage("*had 1 item(s) that did not satisfy any expectation*Item at index 1: \"verylongstring\"*");
			}

			[Fact]
			public async Task NullCollection_ShouldFail()
			{
				IEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).IsContainedInWithExpectations(new Action<IThat<string>>[]
					{
						x => x.IsNotEmpty()
					});

				await That(Act).ThrowsException()
					.WithMessage("*subject was <null>*");
			}

			[Fact]
			public async Task EmptyCollectionWithAnyExpectations_ShouldSucceed()
			{
				IEnumerable<string> subject = Enumerable.Empty<string>();

				async Task Act()
					=> await That(subject).IsContainedInWithExpectations(new Action<IThat<string>>[]
					{
						x => x.IsNotEmpty()
					});

				await That(Act).DoesNotThrow();
			}
		}
	}
}