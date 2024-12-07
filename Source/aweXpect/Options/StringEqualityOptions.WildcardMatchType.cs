using System.Collections.Generic;
using System.Text.RegularExpressions;
using aweXpect.Helpers;

namespace aweXpect.Options;

public partial class StringEqualityOptions
{
	private sealed class WildcardMatchType : IMatchType
	{
		private static string WildcardToRegularExpression(string value)
		{
			string regex = Regex.Escape(value)
				.Replace("\\?", ".")
				.Replace("\\*", "(.|\\n)*");
			return $"^{regex}$";
		}

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

			return Regex.IsMatch(value, WildcardToRegularExpression(pattern), options,
				RegexTimeout);
		}

		#endregion
	}
}
