using System;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatDelegate
{
	/// <summary>
	///     Start expectations for the current <see cref="Action" /> <paramref name="subject" />.
	/// </summary>
	public static Core.ThatDelegate.WithoutValue Does(
		this IExpectSubject<Core.ThatDelegate.WithoutValue> subject)
		=> new(subject.Should(_ => { }).ExpectationBuilder);

	/// <summary>
	///     Start expectations for the current <see cref="Func{TValue}" /> <paramref name="subject" />.
	/// </summary>
	public static Core.ThatDelegate.WithValue<TValue> Does<TValue>(
		this IExpectSubject<Core.ThatDelegate.WithValue<TValue>> subject)
		=> new(subject.Should(_ => { }).ExpectationBuilder);
}
