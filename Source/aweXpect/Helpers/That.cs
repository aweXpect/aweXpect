using System;
using System.Diagnostics;
using aweXpect.Core;

namespace aweXpect.Helpers;

internal static class That
{
	public static readonly Action<ExpectationBuilder> WithoutAction = _ => { };

	internal static readonly string ItWasNull = "it was <null>";
}
