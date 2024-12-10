using System.Collections.Generic;
using System.Text.RegularExpressions;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

public partial class StringEqualityOptions
{
	private sealed class RegexMatchType : IMatchType
	{
		#region IMatchType Members

		/// <inheritdoc />
		public string GetExtendedFailure(string it, string? actual, string? pattern,
			bool ignoreCase,
			IEqualityComparer<string> comparer)
			=> $"{it} was {Formatter.Format(actual.TruncateWithEllipsisOnWord(LongMaxLength).Indent(indentFirstLine: false))}";

		public bool Matches(string? value, string? pattern, bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (value is null || pattern is null)
			{
				return false;
			}

			RegexOptions options = RegexOptions.Multiline;
			if (ignoreCase)
			{
				options |= RegexOptions.IgnoreCase;
			}

			return Regex.IsMatch(value, pattern, options, RegexTimeout);
		}

		#endregion
	}
}
