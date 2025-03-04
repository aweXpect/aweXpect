using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.TimeSystem;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation without an underlying value.
/// </summary>
[StackTraceHidden]
public class ExpectationResult(ExpectationBuilder expectationBuilder) : Expectation
{
	/// <summary>
	///     Provide a <paramref name="reason" /> explaining why the constraint is needed.<br />
	///     If the phrase does not start with the word <i>because</i>, it is prepended automatically.
	/// </summary>
	public ExpectationResult Because(string reason)
	{
		expectationBuilder.AddReason(reason);
		return this;
	}

	/// <summary>
	///     Sets the <see cref="CancellationToken" /> to be passed to expectations.
	/// </summary>
	/// <remarks>
	///     Use
	///     <c>
	///         Customize.aweXpect.Settings().TestCancellation
	///         .Set(TestCancellation.FromCancellationToken(() => cancellationToken))
	///     </c>
	///     to apply the <paramref name="cancellationToken" /> globally.
	/// </remarks>
	public ExpectationResult WithCancellation(CancellationToken cancellationToken)
	{
		expectationBuilder.WithCancellation(cancellationToken);
		return this;
	}

	/// <summary>
	///     Sets the <paramref name="timeout" /> to be passed to expectations.
	/// </summary>
	/// <remarks>
	///     Use
	///     <c>
	///         Customize.aweXpect.Settings().TestCancellation
	///         .Set(TestCancellation.FromTimeout(timeout))
	///     </c>
	///     to apply the <paramref name="timeout" /> globally.
	/// </remarks>
	public ExpectationResult WithTimeout(TimeSpan timeout)
	{
		expectationBuilder.WithTimeout(timeout);
		return this;
	}

	/// <summary>
	///     By awaiting the result, the expectations are verified.
	///     <para />
	///     Will throw an exception, when the expectations are not met.
	/// </summary>
	public TaskAwaiter GetAwaiter()
	{
		Task result = GetResultOrThrow();
		return result.GetAwaiter();
	}

	/// <inheritdoc />
	internal override async Task<Result> GetResult(int index)
		=> new(++index, $" [{index:00}] Expected that {expectationBuilder.Subject}",
			await expectationBuilder.IsMet());

	/// <inheritdoc />
	internal override IEnumerable<ResultContext> GetContexts(int index)
		=> expectationBuilder.GetContexts();

	/// <summary>
	///     Specifies a <see cref="ITimeSystem" /> to use for the expectation.
	/// </summary>
	internal ExpectationResult UseTimeSystem(ITimeSystem timeSystem)
	{
		expectationBuilder.UseTimeSystem(timeSystem);
		return this;
	}

	private async Task GetResultOrThrow()
	{
		ConstraintResult result = await expectationBuilder.IsMet();

		if (result.Outcome == Outcome.Success)
		{
			return;
		}

		Fail.Test(await expectationBuilder.FromFailure(result));
	}
}

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
/// </summary>
public class ExpectationResult<TType>(ExpectationBuilder expectationBuilder)
	: ExpectationResult<TType, ExpectationResult<TType>>(expectationBuilder);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
/// </summary>
[StackTraceHidden]
public class ExpectationResult<TType, TSelf>(ExpectationBuilder expectationBuilder) : Expectation
	where TSelf : ExpectationResult<TType, TSelf>
{
	/// <summary>
	///     Provide a <paramref name="reason" /> explaining why the constraint is needed.<br />
	///     If the phrase does not start with the word <i>because</i>, it is prepended automatically.
	/// </summary>
	public TSelf Because(string reason)
	{
		expectationBuilder.AddReason(reason);
		return (TSelf)this;
	}

	/// <summary>
	///     By awaiting the result, the expectations are verified.
	///     <para />
	///     Will throw an exception, when the expectations are not met.<br />
	///     Otherwise, it will return the <typeparamref name="TType" />.
	/// </summary>
	[StackTraceHidden]
	public TaskAwaiter<TType> GetAwaiter()
	{
		Task<TType> result = GetResultOrThrow();
		return result.GetAwaiter();
	}

	/// <summary>
	///     Sets the <paramref name="cancellationToken" /> to be passed to expectations.
	/// </summary>
	/// <remarks>
	///     Use
	///     <c>
	///         Customize.aweXpect.Settings().TestCancellation
	///         .Set(TestCancellation.FromCancellationToken(() => cancellationToken))
	///     </c>
	///     to apply the <paramref name="cancellationToken" /> globally.
	/// </remarks>
	public TSelf WithCancellation(CancellationToken cancellationToken)
	{
		expectationBuilder.WithCancellation(cancellationToken);
		return (TSelf)this;
	}

	/// <summary>
	///     Sets the <paramref name="timeout" /> to be passed to expectations.
	/// </summary>
	/// <remarks>
	///     Use
	///     <c>
	///         Customize.aweXpect.Settings().TestCancellation
	///         .Set(TestCancellation.FromTimeout(timeout))
	///     </c>
	///     to apply the <paramref name="timeout" /> globally.
	/// </remarks>
	public TSelf WithTimeout(TimeSpan timeout)
	{
		expectationBuilder.WithTimeout(timeout);
		return (TSelf)this;
	}

	/// <inheritdoc />
	internal override async Task<Result> GetResult(int index)
		=> new(++index, $" [{index:00}] Expected that {expectationBuilder.Subject}",
			await expectationBuilder.IsMet());

	/// <inheritdoc />
	internal override IEnumerable<ResultContext> GetContexts(int index)
		=> expectationBuilder.GetContexts();

	/// <summary>
	///     Specifies a <see cref="ITimeSystem" /> to use for the expectation.
	/// </summary>
	internal TSelf UseTimeSystem(ITimeSystem timeSystem)
	{
		expectationBuilder.UseTimeSystem(timeSystem);
		return (TSelf)this;
	}

	[StackTraceHidden]
	private async Task<TType> GetResultOrThrow()
	{
		ConstraintResult result = await expectationBuilder.IsMet();

		if (result.Outcome == Outcome.Success &&
		    result.TryGetValue(out TType? value))
		{
			return value;
		}

		if (result.Outcome == Outcome.Undecided)
		{
			//TODO: Add support for undecided
			Fail.Test(await expectationBuilder.FromFailure(result));
		}

		if (result.Outcome == Outcome.Failure)
		{
			Fail.Test(await expectationBuilder.FromFailure(result));
		}

		throw new FailException(
			$"The value in {Formatter.Format(result.GetType())} did not match expected type {Formatter.Format(typeof(TType))}.");
	}
}
