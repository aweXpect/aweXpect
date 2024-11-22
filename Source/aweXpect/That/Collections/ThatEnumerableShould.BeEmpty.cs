using System.Collections.Generic;
using System.Linq;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.EvaluationContext;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that the actual enumerable is empty.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		BeEmpty<TItem>(this IThat<IEnumerable<TItem>> source)
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new BeEmptyConstraint<TItem>(it)),
			source);

	/// <summary>
	///     Verifies that the actual enumerable is not empty.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		NotBeEmpty<TItem>(
			this IThat<IEnumerable<TItem>> source)
		=> new(source.ExpectationBuilder
				.AddConstraint(it => new NotBeEmptyConstraint<TItem>(it)),
			source);

	private readonly struct BeEmptyConstraint<TItem>(string it)
		: IValueConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual)
		{
			using IEnumerator<TItem> enumerator = actual.GetEnumerator();
			if (enumerator.MoveNext())
			{
				return new ConstraintResult.Failure(ToString(),
					$"{it} was {Formatter.Format(actual.Take(11))}");
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(actual, ToString());
		}

		public override string ToString()
			=> "be empty";
	}

	private readonly struct NotBeEmptyConstraint<TItem>(string it)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem>? materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			using IEnumerator<TItem> enumerator = materializedEnumerable.GetEnumerator();
			if (enumerator.MoveNext())
			{
				return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
					ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was");
		}

		public override string ToString()
			=> "not be empty";
	}
}
