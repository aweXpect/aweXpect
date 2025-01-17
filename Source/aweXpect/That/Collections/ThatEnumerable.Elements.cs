using System.Collections.Generic;
using aweXpect.Core;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="IEnumerable{TItem}" />.
/// </summary>
public static partial class ThatEnumerable
{
	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IEnumerable{T}" /> of <see langword="string" />.
	/// </summary>
	public partial class Elements
	{
		private readonly EnumerableQuantifier _quantifier;
		private readonly IExpectSubject<IEnumerable<string?>> _subject;

		internal Elements(IExpectSubject<IEnumerable<string?>> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}
	}

	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IEnumerable{TItem}" /> of
	///     <typeparamref name="TItem" />.
	/// </summary>
	public partial class Elements<TItem>
	{
		private readonly EnumerableQuantifier _quantifier;
		private readonly IExpectSubject<IEnumerable<TItem>> _subject;

		internal Elements(IExpectSubject<IEnumerable<TItem>> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}
	}
}
