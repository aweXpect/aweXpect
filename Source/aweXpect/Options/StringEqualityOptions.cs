using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect.Options;

/// <summary>
///     Quantifier an occurrence.
/// </summary>
public class StringEqualityOptions : IOptionsEquality<string?>
{
	private IEqualityComparer<string>? _comparer;

	/// <summary>
	///     The <see cref="IEqualityComparer{T}" /> to use for comparing <see langword="string" />s.
	/// </summary>
	public IEqualityComparer<string> Comparer
		=> _comparer ?? UseDefaultComparer(IgnoreCase);

	/// <summary>
	///     Flag indicating, if casing is ignored when comparing the <see langword="string" />s.
	/// </summary>
	public bool IgnoreCase { get; private set; }

	/// <summary>
	///     Flag indicating, if the newline style is ignored when comparing the <see langword="string" />s.
	/// </summary>
	public bool IgnoreNewlineStyle { get; private set; }

	/// <inheritdoc />
	public bool AreConsideredEqual(string? a, string? b)
	{
		if (IgnoreNewlineStyle)
		{
			a = a.RemoveNewlineStyle();
			b = b.RemoveNewlineStyle();
		}

		return Comparer.Equals(a, b);
	}

	/// <summary>
	///     Ignores casing when comparing the <see langword="string" />s.
	/// </summary>
	public StringEqualityOptions IgnoringCase(bool ignoreCase = true)
	{
		IgnoreCase = ignoreCase;
		return this;
	}

	/// <summary>
	///     Ignores the newline style when comparing <see langword="string" />s.
	/// </summary>
	/// <remarks>
	///     Enabling this option will replace all occurrences of <c>\r\n</c> and <c>\r</c> with <c>\n</c> in the strings before
	///     comparing them.
	/// </remarks>
	public StringEqualityOptions IgnoringNewlineStyle(bool ignoreNewlineStyle)
	{
		IgnoreNewlineStyle = ignoreNewlineStyle;
		return this;
	}

	/// <inheritdoc />
	public override string ToString()
	{
		if (_comparer != null)
		{
			return $" using {Formatter.Format(_comparer.GetType())}";
		}

		if (IgnoreCase)
		{
			return " ignoring case";
		}

		return "";
	}

	/// <summary>
	///     Specifies a specific <see cref="IEqualityComparer{T}" /> to use for comparing <see langword="string" />s.
	/// </summary>
	/// <remarks>
	///     If set to <see langword="null" /> (default), uses the <see cref="StringComparer.Ordinal" /> or
	///     <see cref="StringComparer.OrdinalIgnoreCase" /> depending on whether the casing is ignored.
	/// </remarks>
	public StringEqualityOptions UsingComparer(IEqualityComparer<string>? comparer)
	{
		_comparer = comparer;
		return this;
	}

	private static StringComparer UseDefaultComparer(bool ignoreCase)
		=> ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;
}
