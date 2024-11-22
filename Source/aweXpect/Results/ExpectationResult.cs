using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.TimeSystem;

namespace Testably.Expectations.Results;

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
		=> new(++index, $" [{index:00}] Expected {expectationBuilder.Subject} to",
			await expectationBuilder.IsMet());

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

		if (result is ConstraintResult.Failure failure)
		{
			Fail.Test(expectationBuilder.FromFailure(
				expectationBuilder.Subject, failure));
		}
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
	///     Sets the <see cref="CancellationToken" /> to be passed to expectations.
	/// </summary>
	public TSelf WithCancellation(CancellationToken cancellationToken)
	{
		expectationBuilder.WithCancellation(cancellationToken);
		return (TSelf)this;
	}

	/// <inheritdoc />
	internal override async Task<Result> GetResult(int index)
		=> new(++index, $" [{index:00}] Expected {expectationBuilder.Subject} to",
			await expectationBuilder.IsMet());

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

		if (result is ConstraintResult.Failure failure)
		{
			Fail.Test(expectationBuilder.FromFailure(
				expectationBuilder.Subject, failure));
		}
		else if (result is ConstraintResult.Success<TType> matchingSuccess)
		{
			return matchingSuccess.Value;
		}
		else if (result is ConstraintResult.Success success &&
		         success.TryGetValue(out TType? value))
		{
			return value;
		}

		throw new FailException("You should not be here (with value)!");
	}
}
