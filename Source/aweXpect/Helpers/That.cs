using System;
using System.Diagnostics;
using aweXpect.Core;

namespace aweXpect.Helpers;

internal class That
{
	public static readonly Action<ExpectationBuilder> WithoutAction = _ => { };

	internal static readonly string ItWasNull = "it was <null>";

	[DebuggerDisplay("Expect.ThatSubject<{typeof(T)}>: {ExpectationBuilder}")]
	internal readonly struct Subject<T>(ExpectationBuilder expectationBuilder)
		: IExpectSubject<T>, IThatShould<T>, IThatIs<T>, IThatHas<T>
	{
		public ExpectationBuilder ExpectationBuilder { get; } = expectationBuilder;

		/// <inheritdoc />
		public IThatShould<T> Should(Action<ExpectationBuilder> builderOptions)
		{
			builderOptions.Invoke(ExpectationBuilder);
			return this;
		}
	}
}
