using System.Collections;
using System.Collections.Generic;
using aweXpect.Core;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Interface for <see cref="StringElements" /> to get access to the <see cref="Quantifier" />
	///     and the <see cref="Subject" />.
	/// </summary>
	public interface IStringElements
	{
		/// <summary>
		///     The quantifier for the elements.
		/// </summary>
		public EnumerableQuantifier Quantifier { get; }

		/// <summary>
		///     The subject of the expectation.
		/// </summary>
		public IThat<IEnumerable<string?>?> Subject { get; }
	}

	/// <summary>
	///     Interface for <see cref="Elements{TItem, TEnumerable}" /> to get access to the <see cref="Quantifier" />
	///     and the <see cref="Subject" />.
	/// </summary>
	public interface IElements<TSubject>
	{
		/// <summary>
		///     The quantifier for the elements.
		/// </summary>
		public EnumerableQuantifier Quantifier { get; }

		/// <summary>
		///     The subject of the expectation.
		/// </summary>
		public IThat<TSubject> Subject { get; }
	}

	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IEnumerable{T}" /> of <see langword="string" />.
	/// </summary>
	public partial class StringElements : IStringElements
	{
		private readonly EnumerableQuantifier _quantifier;
		private readonly IThat<IEnumerable<string?>?> _subject;

		internal StringElements(IThat<IEnumerable<string?>?> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}

		EnumerableQuantifier IStringElements.Quantifier => _quantifier;
		IThat<IEnumerable<string?>?> IStringElements.Subject => _subject;
	}

	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IEnumerable{TItem}" /> of
	///     <typeparamref name="TItem" />.
	/// </summary>
	public partial class Elements<TItem, TEnumerable> : IElements<TEnumerable>
	where TEnumerable : IEnumerable<TItem>?
	{
		internal readonly EnumerableQuantifier _quantifier;
		internal readonly IThat<TEnumerable> _subject;

		internal Elements(IThat<TEnumerable> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}

		EnumerableQuantifier IElements<TEnumerable>.Quantifier => _quantifier;
		IThat<TEnumerable> IElements<TEnumerable>.Subject => _subject;
	}

	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IEnumerable" />.
	/// </summary>
	public partial class NonGenericElements<TEnumerable> : IElements<TEnumerable>
	where TEnumerable : IEnumerable
	{
		private readonly EnumerableQuantifier _quantifier;
		private readonly IThat<TEnumerable> _subject;

		internal NonGenericElements(IThat<TEnumerable> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}

		EnumerableQuantifier IElements<TEnumerable>.Quantifier => _quantifier;
		IThat<TEnumerable> IElements<TEnumerable>.Subject => _subject;
	}
}
