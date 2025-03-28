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
		///     …are exactly of type <typeparamref name="TType" />.
		/// </summary>
		public ObjectEqualityResult<TCollection, IThat<TCollection>, TItem>
			AreExactly<TType>()
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
								(true, false) => $"are exactly of type {Formatter.Format(typeof(TType))}",
								(false, false) => $"is exactly of type {Formatter.Format(typeof(TType))}",
								(true, true) => $"are not exactly of type {Formatter.Format(typeof(TType))}",
								(false, true) => $"is not exactly of type {Formatter.Format(typeof(TType))}",
							},
						a => a?.GetType() == typeof(TType),
						"were")),
				_subject,
				options);
		}

		/// <summary>
		///     …are exactly of type <paramref name="type" />.
		/// </summary>
		public ObjectEqualityResult<TCollection, IThat<TCollection>, TItem>
			AreExactly(Type type)
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
								(true, false) => $"are exactly of type {Formatter.Format(type)}",
								(false, false) => $"is exactly of type {Formatter.Format(type)}",
								(true, true) => $"are not exactly of type {Formatter.Format(type)}",
								(false, true) => $"is not exactly of type {Formatter.Format(type)}",
							},
						a => a?.GetType() == type,
						"were")),
				_subject,
				options);
		}
	}
}
