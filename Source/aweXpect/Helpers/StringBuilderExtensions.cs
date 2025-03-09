using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace aweXpect.Helpers;

internal static class StringBuilderExtensions
{
	public static void ItWasNull(this StringBuilder stringBuilder, string it)
	{
		stringBuilder.Append(it).Append(" was <null>");
	}
}
