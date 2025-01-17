#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	public partial class Elements
	{
		/// <summary>
		///     Verifies that the items in the collection are equal to the <paramref name="expected" /> value.
		/// </summary>
		public StringEqualityResult<IAsyncEnumerable<string?>, IExpectSubject<IAsyncEnumerable<string?>>> Are(
			string? expected)
		{
			StringEqualityOptions options = new();
			return new StringEqualityResult<IAsyncEnumerable<string?>, IExpectSubject<IAsyncEnumerable<string?>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<string?>(
						it,
						_quantifier,
						() => $"equal to {Formatter.Format(expected)}",
						a => options.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				options);
		}
	}

	public partial class Elements<TItem>
	{
		/// <summary>
		///     Verifies that the items in the collection are equal to the <paramref name="expected" /> value.
		/// </summary>
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>
			Are(TItem expected)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => $"equal to {Formatter.Format(expected)}",
						a => options.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				options);
		}
		/// <summary>
		///     Verifies that the items in the collection satisfy the <paramref name="expectations" />.
		/// </summary>
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>
			Are(Action<IExpectSubject<TItem>> expectations)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new AsyncCollectionConstraint<TItem>(it, _quantifier, expectations)),
				_subject,
				options);
		}
		
		/// <summary>
		///     Verifies that all items in the collection are of type <typeparamref name="TType" />.
		/// </summary>
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>
			Are<TType>()
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => $"be of type {Formatter.Format(typeof(TType))}",
						a => typeof(TType).IsAssignableFrom(a?.GetType()),
						"were")),
				_subject,
				options);
		}
		
		/// <summary>
		///     Verifies that all items in the collection are of type <paramref name="type" />.
		/// </summary>
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>
			Are(Type type)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => $"be of type {Formatter.Format(type)}",
						a => type.IsAssignableFrom(a?.GetType()),
						"were")),
				_subject,
				options);
		}
	}
}
#endif
