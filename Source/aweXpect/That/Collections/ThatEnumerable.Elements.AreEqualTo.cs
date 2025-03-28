using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	public partial class Elements<TCollection>
	{
		/// <summary>
		///     …are equal to the <paramref name="expected" /> value.
		/// </summary>
		public StringEqualityResult<TCollection, IThat<TCollection>> AreEqualTo(
			string? expected)
		{
			StringEqualityOptions options = new();
			return new StringEqualityResult<TCollection, IThat<TCollection>>(
				_subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<string?, TCollection>(
						it, grammars,
						_quantifier,
						g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
								g.IsNegated()) switch
							{
								(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
								(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
								(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
								(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
							},
						a => options.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				options);
		}
	}

	public partial class Elements<TItem, TCollection>
	{
		/// <summary>
		///     …are equal to the <paramref name="expected" /> value.
		/// </summary>
		public ObjectEqualityResult<TCollection, IThat<TCollection>, TItem>
			AreEqualTo(TItem expected)
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
								(true, false) => $"are equal to {Formatter.Format(expected)}{options}",
								(false, false) => $"is equal to {Formatter.Format(expected)}{options}",
								(true, true) => $"are not equal to {Formatter.Format(expected)}{options}",
								(false, true) => $"is not equal to {Formatter.Format(expected)}{options}",
							},
						a => options.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				options);
		}
	}
}
