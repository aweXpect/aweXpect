#if NET8_0_OR_GREATER
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
		///     …are equal to the <paramref name="expected" /> value.
		/// </summary>
		public StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>> AreEqualTo(
			string? expected)
		{
			StringEqualityOptions options = new();
			return new StringEqualityResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<string?>(
						it,
						_quantifier,
						() => grammars == ExpectationGrammars.None
							? $"is equal to {Formatter.Format(expected)}{options}"
							: $"are equal to {Formatter.Format(expected)}{options}",
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
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
			AreEqualTo(TItem expected)
		{
			ObjectEqualityOptions<TItem> options = new();
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => grammars == ExpectationGrammars.None
							? $"is equal to {Formatter.Format(expected)}{options}"
							: $"are equal to {Formatter.Format(expected)}{options}",
						a => options.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				options);
		}
	}
}
#endif
