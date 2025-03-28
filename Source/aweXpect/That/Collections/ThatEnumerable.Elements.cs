using System.Collections.Generic;
using aweXpect.Core;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IEnumerable{T}" /> of <see langword="string" />.
	/// </summary>
	public partial class Elements<TCollection>
		where TCollection : IEnumerable<string?>?
	{
		private readonly EnumerableQuantifier _quantifier;
		private readonly IThat<TCollection> _subject;

		internal Elements(IThat<TCollection> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}
	}

	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IEnumerable{TItem}" /> of
	///     <typeparamref name="TItem" />.
	/// </summary>
	public partial class Elements<TItem, TCollection>
	where TCollection : IEnumerable<TItem>?
	{
		private readonly EnumerableQuantifier _quantifier;
		private readonly IThat<TCollection> _subject;

		internal Elements(IThat<TCollection> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}
	}
}
