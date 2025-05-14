using System.Collections.Generic;
using aweXpect.Core;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Interface for <see cref="Elements" /> to get access to the <see cref="Quantifier" />
	///     and the <see cref="Subject" />.
	/// </summary>
	public interface IElements
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
	///     Interface for <see cref="Elements{TItem}" /> to get access to the <see cref="Quantifier" />
	///     and the <see cref="Subject" />.
	/// </summary>
	public interface IElements<TItem>
	{
		/// <summary>
		///     The quantifier for the elements.
		/// </summary>
		public EnumerableQuantifier Quantifier { get; }

		/// <summary>
		///     The subject of the expectation.
		/// </summary>
		public IThat<IEnumerable<TItem>?> Subject { get; }
	}

	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IEnumerable{T}" /> of <see langword="string" />.
	/// </summary>
	public partial class Elements : IElements
	{
		private readonly EnumerableQuantifier _quantifier;
		private readonly IThat<IEnumerable<string?>?> _subject;

		internal Elements(IThat<IEnumerable<string?>?> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}

		EnumerableQuantifier IElements.Quantifier => _quantifier;
		IThat<IEnumerable<string?>?> IElements.Subject => _subject;
	}

	/// <summary>
	///     Result class for expectations on the elements of a <see cref="IEnumerable{TItem}" /> of
	///     <typeparamref name="TItem" />.
	/// </summary>
	public partial class Elements<TItem> : IElements<TItem>
	{
		private readonly EnumerableQuantifier _quantifier;
		private readonly IThat<IEnumerable<TItem>?> _subject;

		internal Elements(IThat<IEnumerable<TItem>?> subject, EnumerableQuantifier quantifier)
		{
			_subject = subject;
			_quantifier = quantifier;
		}

		EnumerableQuantifier IElements<TItem>.Quantifier => _quantifier;
		IThat<IEnumerable<TItem>?> IElements<TItem>.Subject => _subject;
	}
}
