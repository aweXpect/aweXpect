using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	public partial class Elements<TItem>
	{
		/// <summary>
		///     …are exactly of type <typeparamref name="TType" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
			AreExactly<TType>()
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => grammars == ExpectationGrammars.None
							? $"is exactly of type {Formatter.Format(typeof(TType))}"
							: $"are exactly of type {Formatter.Format(typeof(TType))}",
						a => a?.GetType() == typeof(TType),
						"were")),
				_subject,
				options);
		}

		/// <summary>
		///     …are exactly of type <paramref name="type" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
			AreExactly(Type type)
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => grammars == ExpectationGrammars.None
							? $"is exactly of type {Formatter.Format(type)}"
							: $"are exactly of type {Formatter.Format(type)}",
						a => a?.GetType() == type,
						"were")),
				_subject,
				options);
		}
	}
}
