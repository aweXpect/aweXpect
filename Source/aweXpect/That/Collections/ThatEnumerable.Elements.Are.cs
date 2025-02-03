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
		///     Verifies that all items in the collection are of type <typeparamref name="TType" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
			Are<TType>()
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => $"be of type {Formatter.Format(typeof(TType))}",
						a => a is TType,
						"were")),
				_subject,
				options);
		}

		/// <summary>
		///     Verifies that all items in the collection are of type <paramref name="type" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
			Are(Type type)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => $"be of type {Formatter.Format(type)}",
						a => type.IsInstanceOfType(a),
						"were")),
				_subject,
				options);
		}
	}
}
