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
		///     …are of type <typeparamref name="TType" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
			Are<TType>()
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => grammar == ExpectationGrammars.None
							? $"is of type {Formatter.Format(typeof(TType))}"
							: $"are of type {Formatter.Format(typeof(TType))}",
						a => a is TType,
						"were")),
				_subject,
				options);
		}

		/// <summary>
		///     …are of type <paramref name="type" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
			Are(Type type)
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => grammar == ExpectationGrammars.None
							? $"is of type {Formatter.Format(type)}"
							: $"are of type {Formatter.Format(type)}",
						a => type.IsInstanceOfType(a),
						"were")),
				_subject,
				options);
		}
	}
}
