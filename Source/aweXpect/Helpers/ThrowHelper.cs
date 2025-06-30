using System;

namespace aweXpect.Helpers;

internal static class ThrowHelper
{
	public static ArgumentException EmptyCollection()
		=> new("You have to provide at least one expected value!");
}
