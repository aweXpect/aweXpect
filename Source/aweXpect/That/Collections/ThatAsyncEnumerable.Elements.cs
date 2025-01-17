#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Options;

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
		private readonly IExpectSubject<IAsyncEnumerable<string?>> _subject;
		private readonly EnumerableQuantifier _quantifier;

		internal Elements(IExpectSubject<IAsyncEnumerable<string?>> subject, EnumerableQuantifier quantifier)
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
		private readonly IExpectSubject<IAsyncEnumerable<TItem>> _subject;
		private readonly EnumerableQuantifier _quantifier;

		internal Elements(IExpectSubject<IAsyncEnumerable<TItem>> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}
	}
}
#endif
