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
	public partial class Elements<TItem>
	{
		/// <summary>
		///     Verifies that all items in the collection are of type <typeparamref name="TType" />.
		/// </summary>
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>
			Are<TType>()
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(
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
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>
			Are(Type type)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(
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
