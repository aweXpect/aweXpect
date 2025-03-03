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
		///     …are of type <typeparamref name="TType" />.
		/// </summary>
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
			Are<TType>()
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => grammars == ExpectationGrammars.None
							? $"is of type {Formatter.Format(typeof(TType))}"
							: $"are of type {Formatter.Format(typeof(TType))}",
						a => typeof(TType).IsAssignableFrom(a?.GetType()),
						"were")),
				_subject,
				options);
		}

		/// <summary>
		///     …are of type <paramref name="type" />.
		/// </summary>
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
			Are(Type type)
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => grammars == ExpectationGrammars.None
							? $"is of type {Formatter.Format(type)}"
							: $"are of type {Formatter.Format(type)}",
						a => type.IsAssignableFrom(a?.GetType()),
						"were")),
				_subject,
				options);
		}
	}
}
#endif
