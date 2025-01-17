﻿using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	public partial class Elements
	{
		/// <summary>
		///     Verifies that the items in the collection are equal to the <paramref name="expected" /> value.
		/// </summary>
		public StringEqualityResult<IEnumerable<string?>, IExpectSubject<IEnumerable<string?>>> Are(
			string? expected)
		{
			StringEqualityOptions options = new();
			return new StringEqualityResult<IEnumerable<string?>, IExpectSubject<IEnumerable<string?>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<string?>(
						it,
						_quantifier,
						() => $"equal to {Formatter.Format(expected)}",
						a => options.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				options);
		}
	}

	public partial class Elements<TItem>
	{
		/// <summary>
		///     Verifies that the items in the collection are equal to the <paramref name="expected" /> value.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>
			Are(TItem expected)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => $"equal to {Formatter.Format(expected)}",
						a => options.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				options);
		}
		/// <summary>
		///     Verifies that the items in the collection satisfy the <paramref name="expectations" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>
			Are(Action<IExpectSubject<TItem>> expectations)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new SyncCollectionConstraint<TItem>(it, _quantifier, expectations)),
				_subject,
				options);
		}
		
		/// <summary>
		///     Verifies that all items in the collection are of type <typeparamref name="TType" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>
			Are<TType>()
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => $"be of type {Formatter.Format(typeof(TType))}",
						a => typeof(TType).IsAssignableFrom(a?.GetType()),
						"were")),
				_subject,
				options);
		}
		
		/// <summary>
		///     Verifies that all items in the collection are of type <paramref name="type" />.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>
			Are(Type type)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => $"be of type {Formatter.Format(type)}",
						a => type.IsAssignableFrom(a?.GetType()),
						"were")),
				_subject,
				options);
		}
	}
}
