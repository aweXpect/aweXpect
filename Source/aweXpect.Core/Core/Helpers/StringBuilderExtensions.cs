using System.Text;

namespace aweXpect.Core.Helpers;

internal static class StringBuilderExtensions
{
	public static void ItWasNull(this StringBuilder stringBuilder, string it)
		=> stringBuilder.Append(it).Append(" was <null>");
}
