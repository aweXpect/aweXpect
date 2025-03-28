using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	public partial class Elements<TItem, TCollection>
	{
		/// <summary>
		///     …are of type <typeparamref name="TType" />.
		/// </summary>
		public ObjectEqualityResult<TCollection, IThat<TCollection>, TItem>
			Are<TType>()
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<TCollection, IThat<TCollection>, TItem>(
				_subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem, TCollection>(
						it, grammars,
						_quantifier,
						g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
								g.IsNegated()) switch
							{
								(true, false) => $"are of type {Formatter.Format(typeof(TType))}",
								(false, false) => $"is of type {Formatter.Format(typeof(TType))}",
								(true, true) => $"are not of type {Formatter.Format(typeof(TType))}",
								(false, true) => $"is not of type {Formatter.Format(typeof(TType))}",
							},
						a => a is TType,
						"were")),
				_subject,
				options);
		}

		/// <summary>
		///     …are of type <paramref name="type" />.
		/// </summary>
		public ObjectEqualityResult<TCollection, IThat<TCollection>, TItem>
			Are(Type type)
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<TCollection, IThat<TCollection>, TItem>(
				_subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem, TCollection>(
						it, grammars,
						_quantifier,
						g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
								g.IsNegated()) switch
							{
								(true, false) => $"are of type {Formatter.Format(type)}",
								(false, false) => $"is of type {Formatter.Format(type)}",
								(true, true) => $"are not of type {Formatter.Format(type)}",
								(false, true) => $"is not of type {Formatter.Format(type)}",
							},
						a => type.IsInstanceOfType(a),
						"were")),
				_subject,
				options);
		}
	}
}
