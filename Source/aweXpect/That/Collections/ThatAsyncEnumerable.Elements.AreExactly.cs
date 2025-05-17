﻿#if NET8_0_OR_GREATER
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
		///     …are exactly of type <typeparamref name="TType" />.
		/// </summary>
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
			AreExactly<TType>()
		{
			ObjectEqualityOptions<TItem> options = new();
			ExpectationBuilder expectationBuilder = _subject.Get().ExpectationBuilder;
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						expectationBuilder,
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
		public ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>
			AreExactly(Type type)
		{
			type.ThrowIfNull();
			ObjectEqualityOptions<TItem> options = new();
			ExpectationBuilder expectationBuilder = _subject.Get().ExpectationBuilder;
			return new ObjectEqualityResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>, TItem>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						expectationBuilder,
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
#endif
