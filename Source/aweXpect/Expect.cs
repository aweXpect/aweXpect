using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Sources;
using Testably.Expectations.Results;

namespace Testably.Expectations;

/// <summary>
///     The starting point for checking expectations.
/// </summary>
public static class Expect
{
	/// <summary>
	///     Specify expectations for the current <paramref name="subject" />.
	/// </summary>
	public static IExpectSubject<T> That<T>(T? subject,
		[CallerArgumentExpression("subject")] string doNotPopulateThisValue = "")
	{
		return new ThatSubject<T>(new ExpectationBuilder<T?>(new ValueSource<T?>(subject),
			doNotPopulateThisValue));
	}

	/// <summary>
	///     Specify expectations for the current <see cref="Action" /> <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithoutValue> That(Action @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
	{
		return new ThatSubject<ThatDelegate.WithoutValue>(
			new ExpectationBuilder<DelegateValue>(
				new DelegateSource(_ => @delegate()), doNotPopulateThisValue));
	}

	/// <summary>
	///     Specify expectations for the current <see cref="Action{CancellationToken}" /> <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithoutValue> That(
		Action<CancellationToken> @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
	{
		return new ThatSubject<ThatDelegate.WithoutValue>(
			new ExpectationBuilder<DelegateValue>(
				new DelegateSource(@delegate), doNotPopulateThisValue));
	}

	/// <summary>
	///     Specify expectations for the current <see cref="Func{Task}" /> <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithoutValue> That(Func<Task> @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
		=> new ThatSubject<ThatDelegate.WithoutValue>(
			new ExpectationBuilder<DelegateValue>(
				new DelegateAsyncSource(_ => @delegate()), doNotPopulateThisValue));

	/// <summary>
	///     Specify expectations for the current <see cref="Func{CancellationToken, Task}" /> <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithoutValue> That(
		Func<CancellationToken, Task> @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
		=> new ThatSubject<ThatDelegate.WithoutValue>(
			new ExpectationBuilder<DelegateValue>(
				new DelegateAsyncSource(@delegate), doNotPopulateThisValue));

	/// <summary>
	///     Specify expectations for the current <see cref="Task" /> <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithoutValue> That(Task @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
		=> new ThatSubject<ThatDelegate.WithoutValue>(
			new ExpectationBuilder<DelegateValue>(
				new DelegateAsyncSource(_ => @delegate), doNotPopulateThisValue));

#if NET6_0_OR_GREATER
	/// <summary>
	///     Specify expectations for the current <see cref="ValueTask" /> <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithoutValue> That(ValueTask @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
		=> new ThatSubject<ThatDelegate.WithoutValue>(
			new ExpectationBuilder<DelegateValue>(
				new DelegateAsyncSource(async _ => await @delegate), doNotPopulateThisValue));
#endif

	/// <summary>
	///     Specify expectations for the current <see cref="Func{TValue}" /> <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithValue<TValue>> That<TValue>(
		Func<TValue> @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
		=> new ThatSubject<ThatDelegate.WithValue<TValue>>(
			new ExpectationBuilder<DelegateValue<TValue>>(
				new DelegateValueSource<TValue>(_ => @delegate()), doNotPopulateThisValue));

	/// <summary>
	///     Specify expectations for the current <see cref="Func{CancellationToken, TValue}" /> <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithValue<TValue>> That<TValue>(
		Func<CancellationToken, TValue> @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
		=> new ThatSubject<ThatDelegate.WithValue<TValue>>(
			new ExpectationBuilder<DelegateValue<TValue>>(
				new DelegateValueSource<TValue>(@delegate), doNotPopulateThisValue));

	/// <summary>
	///     Specify expectations for the current <see cref="Func{T}" /> of <see cref="Task{TValue}" />
	///     <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithValue<TValue>> That<TValue>(
		Func<Task<TValue>> @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
		=> new ThatSubject<ThatDelegate.WithValue<TValue>>(
			new ExpectationBuilder<DelegateValue<TValue>>(
				new DelegateAsyncValueSource<TValue>(_ => @delegate()),
				doNotPopulateThisValue));

	/// <summary>
	///     Specify expectations for the current <see cref="Func{CancellationToken, T}" /> of <see cref="Task{TValue}" />
	///     <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithValue<TValue>> That<TValue>(
		Func<CancellationToken, Task<TValue>> @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
		=> new ThatSubject<ThatDelegate.WithValue<TValue>>(
			new ExpectationBuilder<DelegateValue<TValue>>(
				new DelegateAsyncValueSource<TValue>(@delegate),
				doNotPopulateThisValue));

	/// <summary>
	///     Specify expectations for the current <see cref="Task{TValue}" /> <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithValue<TValue>> That<TValue>(
		Task<TValue> @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
		=> new ThatSubject<ThatDelegate.WithValue<TValue>>(
			new ExpectationBuilder<DelegateValue<TValue>>(
				new DelegateAsyncValueSource<TValue>(_ => @delegate), doNotPopulateThisValue));

#if NET6_0_OR_GREATER
	/// <summary>
	///     Specify expectations for the current <see cref="ValueTask{TValue}" /> <paramref name="delegate" />.
	/// </summary>
	public static IExpectSubject<ThatDelegate.WithValue<TValue>> That<TValue>(
		ValueTask<TValue> @delegate,
		[CallerArgumentExpression("delegate")] string doNotPopulateThisValue = "")
		=> new ThatSubject<ThatDelegate.WithValue<TValue>>(
			new ExpectationBuilder<DelegateValue<TValue>>(
				new DelegateAsyncValueSource<TValue>(
					async _ => await @delegate),
				doNotPopulateThisValue));
#endif

	/// <summary>
	///     Verifies that all provided <paramref name="expectations" /> are met.
	/// </summary>
	public static Expectation.Combination.All ThatAll(params Expectation[] expectations)
		=> new(expectations);

	/// <summary>
	///     Verifies that any of the provided <paramref name="expectations" /> are met.
	/// </summary>
	public static Expectation.Combination.Any ThatAny(params Expectation[] expectations)
		=> new(expectations);

	[DebuggerDisplay("Expect.ThatSubject<{typeof(T)}>: {ExpectationBuilder}")]
	internal readonly struct ThatSubject<T>(ExpectationBuilder expectationBuilder)
		: IExpectSubject<T>, IThat<T>
	{
		public ExpectationBuilder ExpectationBuilder { get; } = expectationBuilder;

		/// <inheritdoc />
		public IThat<T> Should(Action<ExpectationBuilder> builderOptions)
		{
			builderOptions.Invoke(ExpectationBuilder);
			return this;
		}
	}
}
