using Testably.Expectations.Tests.TestHelpers;
#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
#endif

namespace Testably.Expectations.Tests;

public class ExpectTests
{
	public class ThatAllTests
	{
		[Theory]
		[InlineData(true, false)]
		[InlineData(false, true)]
		public async Task ShouldEvaluateAndDisplayAllExpectations(bool subjectA, bool subjectB)
		{
			async Task Act()
				=> await ThatAll(
					That(subjectA).Should().BeTrue(),
					That(subjectB).Should().BeTrue());

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected all of the following to succeed:
				               [01] Expected subjectA to be True
				               [02] Expected subjectB to be True
				              but
				               [{(subjectB ? "01" : "02")}] it was False
				              """);
		}

		[Fact]
		public async Task ShouldIncludeProperIndentationForMultilineResults()
		{
			string subjectA = "subject A";
			string subjectB = "subject C";
			string subjectC = "subject B";

			async Task Act()
				=> await ThatAll(
					That(subjectA).Should().Be("subject A"),
					That(subjectB).Should().Be("subject B"),
					That(subjectC).Should().Be("subject C"));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected all of the following to succeed:
				              [01] Expected subjectA to be equal to "subject A"
				              [02] Expected subjectB to be equal to "subject B"
				              [03] Expected subjectC to be equal to "subject C"
				             but
				              [02] it was "subject C" which differs at index 8:
				                              ↓ (actual)
				                     "subject C"
				                     "subject B"
				                              ↑ (expected)
				              [03] it was "subject B" which differs at index 8:
				                              ↓ (actual)
				                     "subject B"
				                     "subject C"
				                              ↑ (expected)
				             """);
		}

		[Fact]
		public async Task WhenAllConditionIsMet_ShouldSucceed()
		{
			bool subjectA = true;
			bool subjectB = true;

			async Task Act()
				=> await ThatAll(
					That(subjectA).Should().BeTrue(),
					That(subjectB).Should().BeTrue());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenBothConditionsAreNotMet_ShouldFail()
		{
			bool subjectA = false;
			bool subjectB = false;

			async Task Act()
				=> await ThatAll(
					That(subjectA).Should().BeTrue(),
					That(subjectB).Should().BeTrue());

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected all of the following to succeed:
				              [01] Expected subjectA to be True
				              [02] Expected subjectB to be True
				             but
				              [01] it was False
				              [02] it was False
				             """);
		}

		[Fact]
		public async Task WhenNested_ShouldIncludeProperIndentationForMultilineResults()
		{
			string subjectA = "subject A";
			string subjectB = "some unexpected value";
			string subjectC = "subject B";

			async Task Act()
				=> await ThatAll(
					That(true).Should().BeTrue(),
					ThatAll(
						That(subjectA).Should().Be("subject A"),
						That(subjectB).Should().Be("subject B"),
						That(subjectC).Should().Be("subject C")));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected all of the following to succeed:
				              [01] Expected true to be True
				               Expected all of the following to succeed:
				                [02] Expected subjectA to be equal to "subject A"
				                [03] Expected subjectB to be equal to "subject B"
				                [04] Expected subjectC to be equal to "subject C"
				             but
				                [03] it was "some unexpected value" which differs at index 1:
				                         ↓ (actual)
				                       "some unexpected value"
				                       "subject B"
				                         ↑ (expected)
				                [04] it was "subject B" which differs at index 8:
				                                ↓ (actual)
				                       "subject B"
				                       "subject C"
				                                ↑ (expected)
				             """);
		}

		[Fact]
		public async Task WhenNested_ShouldUseIncrementingNumber()
		{
			bool subjectA = false;
			bool subjectB = false;
			bool subjectC = true;
			bool subjectD = false;

			async Task Act()
				=> await ThatAll(
					ThatAny(
						That(subjectA).Should().BeTrue(),
						That(subjectB).Should().BeTrue()),
					That(subjectC).Should().BeTrue(),
					That(subjectD).Should().BeTrue());

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected all of the following to succeed:
				               Expected any of the following to succeed:
				                [01] Expected subjectA to be True
				                [02] Expected subjectB to be True
				              [03] Expected subjectC to be True
				              [04] Expected subjectD to be True
				             but
				                [01] it was False
				                [02] it was False
				              [04] it was False
				             """);
		}

		[Fact]
		public async Task WhenNoExpectationWasProvided_ShouldThrowArgumentException()
		{
			async Task Act()
				=> await ThatAll();

			await That(Act).Should().Throw<ArgumentException>()
				.WithParamName("expectations").And
				.WithMessage("You must provide at least one expectation*").AsWildcard();
		}
	}

	public class ThatAnyTests
	{
		[Fact]
		public async Task ShouldEvaluateAndDisplayAllExpectations()
		{
			bool subjectA = false;
			bool subjectB = false;

			async Task Act()
				=> await ThatAny(
					That(subjectA).Should().BeTrue(),
					That(subjectB).Should().BeTrue());

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected any of the following to succeed:
				              [01] Expected subjectA to be True
				              [02] Expected subjectB to be True
				             but
				              [01] it was False
				              [02] it was False
				             """);
		}

		[Fact]
		public async Task ShouldIncludeProperIndentationForMultilineResults()
		{
			string subjectA = "subject X";
			string subjectB = "subject Y";
			string subjectC = "subject Z";

			async Task Act()
				=> await ThatAny(
					That(subjectA).Should().Be("subject A"),
					That(subjectB).Should().Be("subject B"),
					That(subjectC).Should().Be("subject C"));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected any of the following to succeed:
				              [01] Expected subjectA to be equal to "subject A"
				              [02] Expected subjectB to be equal to "subject B"
				              [03] Expected subjectC to be equal to "subject C"
				             but
				              [01] it was "subject X" which differs at index 8:
				                              ↓ (actual)
				                     "subject X"
				                     "subject A"
				                              ↑ (expected)
				              [02] it was "subject Y" which differs at index 8:
				                              ↓ (actual)
				                     "subject Y"
				                     "subject B"
				                              ↑ (expected)
				              [03] it was "subject Z" which differs at index 8:
				                              ↓ (actual)
				                     "subject Z"
				                     "subject C"
				                              ↑ (expected)
				             """);
		}

		[Theory]
		[InlineData(true, true)]
		[InlineData(true, false)]
		[InlineData(false, true)]
		public async Task WhenAnyConditionIsMet_ShouldSucceed(bool subjectA, bool subjectB)
		{
			async Task Act()
				=> await ThatAny(
					That(subjectA).Should().BeTrue(),
					That(subjectB).Should().BeTrue());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenNested_ShouldIncludeProperIndentationForMultilineResults()
		{
			string subjectA = "subject X";
			string subjectB = "some unexpected value";
			string subjectC = "subject Z";

			async Task Act()
				=> await ThatAny(
					That(false).Should().BeTrue(),
					ThatAny(
						That(subjectA).Should().Be("subject A"),
						That(subjectB).Should().Be("subject B"),
						That(subjectC).Should().Be("subject C")));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected any of the following to succeed:
				              [01] Expected false to be True
				               Expected any of the following to succeed:
				                [02] Expected subjectA to be equal to "subject A"
				                [03] Expected subjectB to be equal to "subject B"
				                [04] Expected subjectC to be equal to "subject C"
				             but
				              [01] it was False
				                [02] it was "subject X" which differs at index 8:
				                                ↓ (actual)
				                       "subject X"
				                       "subject A"
				                                ↑ (expected)
				                [03] it was "some unexpected value" which differs at index 1:
				                         ↓ (actual)
				                       "some unexpected value"
				                       "subject B"
				                         ↑ (expected)
				                [04] it was "subject Z" which differs at index 8:
				                                ↓ (actual)
				                       "subject Z"
				                       "subject C"
				                                ↑ (expected)
				             """);
		}

		[Fact]
		public async Task WhenNested_ShouldIndentMultiLineResults()
		{
			async Task Act()
				=> await ThatAll(That(true).Should().IsMyConstraint(
					"""
					expectation
					over
					multiple
					lines
					""",
					_ => false,
					"""
					result
					also
					on multiple
					lines
					"""
				));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected all of the following to succeed:
				              [01] Expected true to expectation
				                   over
				                   multiple
				                   lines
				             but
				              [01] result
				                   also
				                   on multiple
				                   lines
				             """);
		}

		[Fact]
		public async Task WhenNested_ShouldUseIncrementingNumber()
		{
			bool subjectA = false;
			bool subjectB = true;
			bool subjectC = false;
			bool subjectD = false;

			async Task Act()
				=> await ThatAny(
					ThatAll(
						That(subjectA).Should().BeTrue(),
						That(subjectB).Should().BeTrue()),
					That(subjectC).Should().BeTrue(),
					That(subjectD).Should().BeTrue());

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected any of the following to succeed:
				               Expected all of the following to succeed:
				                [01] Expected subjectA to be True
				                [02] Expected subjectB to be True
				              [03] Expected subjectC to be True
				              [04] Expected subjectD to be True
				             but
				                [01] it was False
				              [03] it was False
				              [04] it was False
				             """);
		}

		[Fact]
		public async Task WhenNoExpectationWasProvided_ShouldThrowArgumentException()
		{
			async Task Act()
				=> await ThatAny();

			await That(Act).Should().Throw<ArgumentException>()
				.WithParamName("expectations").And
				.WithMessage("You must provide at least one expectation*").AsWildcard();
		}
	}

#if NET6_0_OR_GREATER
	/// <summary>
	///     Returns an <see cref="IAsyncEnumerable{T}" /> with incrementing numbers, starting with 0, which cancels the
	///     <paramref name="cancellationTokenSource" /> after <paramref name="cancelAfter" /> iteration.
	/// </summary>
	private static async IAsyncEnumerable<int> GetCancellingAsyncEnumerable(
		int cancelAfter,
		CancellationTokenSource cancellationTokenSource,
		[EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		int index = 0;
		while (!cancellationToken.IsCancellationRequested)
		{
			if (index == cancelAfter)
			{
				cancellationTokenSource.Cancel();
			}

			await Task.Yield();
			yield return index++;
		}
	}

	[Fact]
	public async Task ShouldWithExpressionBuilder_ShouldApplyMethods()
	{
		using CancellationTokenSource cts = new();
		CancellationToken token = cts.Token;
		IAsyncEnumerable<int>
			subject = GetCancellingAsyncEnumerable(6, cts, CancellationToken.None);

		async Task Act()
			=> await That(subject).Should(b => b.WithCancellation(token)).All().Satisfy(x => x < 6);

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected subject to
			             all satisfy "x => x < 6",
			             but could not verify, because it was cancelled early
			             """);
	}
#endif
}
