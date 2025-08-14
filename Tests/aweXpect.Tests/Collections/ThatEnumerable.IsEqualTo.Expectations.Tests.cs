using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class IsEqualTo
	{
		public sealed class ExpectationsTests
		{
			[Fact]
			public async Task AllItemsMatchingExpectations_ShouldSucceed()
			{
				IEnumerable<int> subject = new[] { 2, 4, 6 };

				async Task Act()
					=> await That(subject).IsEqualToWithExpectations(new Action<IThat<int>>[]
					{
						x => x.IsGreaterThan(1),
						x => x.IsEqualTo(4),
						x => x.IsLessThan(10)
					});

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task OneItemNotMatchingExpectation_ShouldFail()
			{
				IEnumerable<int> subject = new[] { 2, 4, 6 };

				async Task Act()
					=> await That(subject).IsEqualToWithExpectations(new Action<IThat<int>>[]
					{
						x => x.IsGreaterThan(1),   // should pass for 2
						x => x.IsEqualTo(5),       // should fail for 4
						x => x.IsLessThan(10)      // should pass for 6
					});

				await That(Act).ThrowsException()
					.WithMessage("*had 1 item(s) that did not match their corresponding expectation*Item at index 1: 4*");
			}

			[Fact]
			public async Task CountMismatch_ShouldFail()
			{
				IEnumerable<int> subject = new[] { 2, 4, 6 };

				async Task Act()
					=> await That(subject).IsEqualToWithExpectations(new Action<IThat<int>>[]
					{
						x => x.IsGreaterThan(0)  // only one expectation for 3 items
					});

				await That(Act).ThrowsException()
					.WithMessage("*had 3 items but expected 1 expectations*");
			}

			[Fact]
			public async Task NullCollection_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).IsEqualToWithExpectations(new Action<IThat<int>>[]
					{
						x => x.IsPositive()
					});

				await That(Act).ThrowsException()
					.WithMessage("*subject was <null>*");
			}

			[Fact]
			public async Task EmptyCollectionWithEmptyExpectations_ShouldSucceed()
			{
				IEnumerable<int> subject = Enumerable.Empty<int>();

				async Task Act()
					=> await That(subject).IsEqualToWithExpectations(new Action<IThat<int>>[0]);

				await That(Act).DoesNotThrow();
			}
		}
	}
}