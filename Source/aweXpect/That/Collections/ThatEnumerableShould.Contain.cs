﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that the actual enumerable contains the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		Contain<TItem>(
			this IThat<IEnumerable<TItem>> source,
			TItem expected)
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new ContainConstraint<TItem>(it, expected)),
			source);

	/// <summary>
	///     Verifies that the actual enumerable contains an item that satisfies the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		Contain<TItem>(
			this IThat<IEnumerable<TItem>> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new ContainPredicateConstraint<TItem>(it, predicate, doNotPopulateThisValue)),
			source);

	/// <summary>
	///     Verifies that the actual enumerable does not contain the <paramref name="unexpected" /> value.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		NotContain<TItem>(
			this IThat<IEnumerable<TItem>> source,
			TItem unexpected)
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new NotContainConstraint<TItem>(it, unexpected)),
			source);

	/// <summary>
	///     Verifies that the actual enumerable contains no item that satisfies the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		NotContain<TItem>(
			this IThat<IEnumerable<TItem>> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new NotContainPredicateConstraint<TItem>(it, predicate, doNotPopulateThisValue)),
			source);

	private readonly struct ContainConstraint<TItem>(string it, TItem expected)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem>? materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			foreach (TItem item in materializedEnumerable)
			{
				if (item?.Equals(expected) == true)
				{
					return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
						ToString());
				}
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(materializedEnumerable.Take(10).ToArray())}");
		}

		public override string ToString()
			=> $"contain {Formatter.Format(expected)}";
	}

	private readonly struct ContainPredicateConstraint<TItem>(
		string it,
		Func<TItem, bool> predicate,
		string predicateExpression)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem>? materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			foreach (TItem item in materializedEnumerable)
			{
				if (predicate(item))
				{
					return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
						ToString());
				}
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(materializedEnumerable.Take(10).ToArray())}");
		}

		public override string ToString()
			=> $"contain item matching {predicateExpression}";
	}

	private readonly struct NotContainConstraint<TItem>(string it, TItem unexpected)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem>? materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			foreach (TItem item in materializedEnumerable)
			{
				if (item?.Equals(unexpected) == true)
				{
					return new ConstraintResult.Failure(ToString(),
						$"{it} did");
				}
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		public override string ToString()
			=> $"not contain {Formatter.Format(unexpected)}";
	}

	private readonly struct NotContainPredicateConstraint<TItem>(
		string it,
		Func<TItem, bool> predicate,
		string predicateExpression)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem>? materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			foreach (TItem item in materializedEnumerable)
			{
				if (predicate(item))
				{
					return new ConstraintResult.Failure(ToString(),
						$"{it} did");
				}
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		public override string ToString()
			=> $"not contain item matching {predicateExpression}";
	}
}
