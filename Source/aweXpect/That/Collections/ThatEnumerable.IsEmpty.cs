using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection is empty.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>> IsEmpty<TItem>(
		this IThat<IEnumerable<TItem>?> source)
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, form) => new IsEmptyConstraint<TItem>(it, form)),
			source);

	/// <summary>
	///     Verifies that the collection is not empty.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>> IsNotEmpty<TItem>(
		this IThat<IEnumerable<TItem>?> source)
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, form) => new IsNotEmptyConstraint<TItem>(it, form)),
			source);

	private readonly struct IsEmptyConstraint<TItem>(string it, ExpectationGrammar grammar)
		: IValueConstraint<IEnumerable<TItem>?>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			if (actual is ICollection<TItem> collectionOfT)
			{
				if (collectionOfT.Count > 0)
				{
					return new ConstraintResult.Failure(ToString(),
						$"{it} was {Formatter.Format(collectionOfT, FormattingOptions.MultipleLines)}");
				}

				return new ConstraintResult.Success<IEnumerable<TItem>>(actual, ToString());
			}

			using IEnumerator<TItem> enumerator = actual.GetEnumerator();
			if (enumerator.MoveNext())
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(actual, ToString());
		}

		public override string ToString()
			=> grammar switch
			{
				ExpectationGrammar.Nested => "are empty",
				_ => "is empty"
			};
	}

	private readonly struct IsNotEmptyConstraint<TItem>(string it, ExpectationGrammar grammar)
		: IContextConstraint<IEnumerable<TItem>?>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>?>(actual, ToString(), $"{it} was <null>");
			}

			if (actual is ICollection<TItem> collectionOfT)
			{
				if (collectionOfT.Count > 0)
				{
					return new ConstraintResult.Success<IEnumerable<TItem>>(actual, ToString());
				}

				return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(), $"{it} was");
			}

			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			using IEnumerator<TItem> enumerator = materializedEnumerable.GetEnumerator();
			if (enumerator.MoveNext())
			{
				return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
					ToString());
			}

			return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(), $"{it} was");
		}

		public override string ToString()
			=> grammar switch
			{
				ExpectationGrammar.Nested => "are not empty",
				_ => "is not empty"
			};
	}
}
