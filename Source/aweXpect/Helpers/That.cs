using System;
using System.Diagnostics;
using aweXpect.Core;

namespace aweXpect.Helpers;

internal class That
{
	public static readonly Action<ExpectationBuilder> WithoutAction = _ => { };

	[DebuggerDisplay("Expect.ThatSubject<{typeof(T)}>: {ExpectationBuilder}")]
	internal readonly struct Subject<T>(ExpectationBuilder expectationBuilder)
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
