using System;
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
		///     Verifies that all items in the collection are equal to the <paramref name="expected" /> value.
		/// </summary>
		public StringEqualityResult<IEnumerable<string?>, IExpectSubject<IEnumerable<string?>>> Are(
			string? expected)
		{
			StringEqualityOptions options = new();
			return new StringEqualityResult<IEnumerable<string?>, IExpectSubject<IEnumerable<string?>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new SyncCollectionConstraint2<string?>(
						it,
						_quantifier,
						() => $"equal to {Formatter.Format(expected)}",
						a => options.AreConsideredEqual(a, expected))),
				_subject,
				options);
		}
	}

	public partial class Elements<TItem>
	{
		/// <summary>
		///     Verifies that all items in the collection are equal to the <paramref name="expected" /> value.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>
			Are(TItem expected)
		{
			ObjectEqualityOptions options = new();
			return new ObjectEqualityResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new SyncCollectionConstraint2<TItem>(
						it,
						_quantifier,
						() => $"equal to {Formatter.Format(expected)}",
						a => options.AreConsideredEqual(a, expected))),
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
					=> new SyncCollectionConstraint2<TItem>(
						it,
						_quantifier,
						() => $"be of type {Formatter.Format(typeof(TType))}",
						a => typeof(TType).IsAssignableFrom(a?.GetType()))),
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
					=> new SyncCollectionConstraint2<TItem>(
						it,
						_quantifier,
						() => $"be of type {Formatter.Format(type)}",
						a => type.IsAssignableFrom(a?.GetType()))),
				_subject,
				options);
		}
	}

	private readonly struct AllBeConstraint<TItem>(
		string it,
		Func<string> expectationText,
		Func<TItem, bool> predicate)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>>(actual!, ToString(), $"{it} was <null>");
			}

			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			int maximumNumberOfCollectionItems =
				Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
			List<TItem> notMatchingItems = new(maximumNumberOfCollectionItems + 1);
			foreach (TItem item in materializedEnumerable)
			{
				if (!predicate(item))
				{
					notMatchingItems.Add(item);
					if (notMatchingItems.Count > maximumNumberOfCollectionItems)
					{
						int displayCount = Math.Min(maximumNumberOfCollectionItems, notMatchingItems.Count);
						return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
							$"{it} contained at least {displayCount} other {(displayCount == 1 ? "item" : "items")}: {Formatter.Format(notMatchingItems, FormattingOptions.MultipleLines)}");
					}
				}
			}

			if (notMatchingItems.Count == 0)
			{
				return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
					ToString());
			}

			return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(),
				$"{it} contained {notMatchingItems.Count} other {(notMatchingItems.Count == 1 ? "item" : "items")}: {Formatter.Format(notMatchingItems, FormattingOptions.MultipleLines)}");
		}

		public override string ToString()
			=> expectationText();
	}
}
