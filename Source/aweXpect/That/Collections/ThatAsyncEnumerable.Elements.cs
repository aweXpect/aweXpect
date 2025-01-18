#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="IAsyncEnumerable{TItem}" />.
/// </summary>
public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IAsyncEnumerable{T}" /> of <see langword="string" />.
	/// </summary>
	public partial class Elements
	{
		private readonly EnumerableQuantifier _quantifier;
		private readonly IThat<IAsyncEnumerable<string?>> _subject;

		internal Elements(IThat<IAsyncEnumerable<string?>> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}
	}

	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IAsyncEnumerable{TItem}" /> of
	///     <typeparamref name="TItem" />.
	/// </summary>
	public partial class Elements<TItem>
	{
		private readonly EnumerableQuantifier _quantifier;
		private readonly IThat<IAsyncEnumerable<TItem>> _subject;

		internal Elements(IThat<IAsyncEnumerable<TItem>> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}
	}
}
#endif
