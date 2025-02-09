using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	public partial class Elements
	{
		/// <summary>
		///     …are equal to the <paramref name="expected" /> value.
		/// </summary>
		public StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>> AreEqualTo(
			string? expected)
		{
			StringEqualityOptions options = new();
			return new StringEqualityResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
					=> new CollectionConstraint<string?>(
						it,
						_quantifier,
						() => grammar.HasFlag(ExpectationGrammars.Plural)
							? $"are equal to {Formatter.Format(expected)}{options}"
							: $"is equal to {Formatter.Format(expected)}{options}",
						a => options.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				options);
		}
	}

	public partial class Elements<TItem>
	{
		/// <summary>
		///     …are equal to the <paramref name="expected" /> value.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
			AreEqualTo(TItem expected)
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => grammar.HasFlag(ExpectationGrammars.Plural)
							? $"are equal to {Formatter.Format(expected)}{options}"
							: $"is equal to {Formatter.Format(expected)}{options}",
						a => options.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				options);
		}
	}
}
