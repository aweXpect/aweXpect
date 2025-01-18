using System;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatDelegate
{
	/// <summary>
	///     Start expectations for the current <see cref="Action" /> <paramref name="subject" />.
	/// </summary>
	public static Delegates.ThatDelegate.WithoutValue Does(
		this IThat<Delegates.ThatDelegate.WithoutValue> subject)
		=> new(subject.ThatIs().ExpectationBuilder);

	/// <summary>
	///     Start expectations for the current <see cref="Func{TValue}" /> <paramref name="subject" />.
	/// </summary>
	public static Delegates.ThatDelegate.WithValue<TValue> Does<TValue>(
		this IThat<Delegates.ThatDelegate.WithValue<TValue>> subject)
		=> new(subject.ThatIs().ExpectationBuilder);
}
