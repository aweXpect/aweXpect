using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Results;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection is <see langword="null" /> or empty.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>?, IThat<IEnumerable<TItem>?>> IsNullOrEmpty<TItem>(
		this IThat<IEnumerable<TItem>?> source)
		=> new(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) => new IsNullOrEmptyConstraint<TItem>(it, grammars)),
			source);

	/// <summary>
	///     Verifies that the collection is <see langword="null" /> or empty.
	/// </summary>
	public static AndOrResult<TEnumerable?, IThat<TEnumerable?>> IsNullOrEmpty<TEnumerable>(
		this IThat<TEnumerable?> source)
		where TEnumerable : IEnumerable
		=> new(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) => new IsNullOrEmptyForEnumerableConstraint<TEnumerable>(it, grammars)),
			source);

	/// <summary>
	///     Verifies that the collection is not <see langword="null" /> or empty.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>> IsNotNullOrEmpty<TItem>(
		this IThat<IEnumerable<TItem>?> source)
		=> new(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) => new IsNullOrEmptyConstraint<TItem>(it, grammars).Invert()),
			source);

	/// <summary>
	///     Verifies that the collection is not <see langword="null" /> or empty.
	/// </summary>
	public static AndOrResult<TEnumerable, IThat<TEnumerable?>> IsNotNullOrEmpty<TEnumerable>(
		this IThat<TEnumerable?> source)
		where TEnumerable : IEnumerable
		=> new(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) => new IsNullOrEmptyForEnumerableConstraint<TEnumerable>(it, grammars).Invert()),
			source);

	private sealed class IsNullOrEmptyConstraint<TItem>(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithValue<IEnumerable<TItem>?>(grammars),
			IContextConstraint<IEnumerable<TItem>?>
	{
		private IEnumerable<TItem>? _materializedEnumerable;

		public ConstraintResult IsMetBy(IEnumerable<TItem>? actual, IEvaluationContext context)
		{
			Actual = actual;
			if (actual is null)
			{
				_materializedEnumerable = null;
				Outcome = Outcome.Success;
				return this;
			}

			if (actual is ICollection<TItem> collectionOfT)
			{
				_materializedEnumerable = actual;
				if (collectionOfT.Count == 0)
				{
					Outcome = Outcome.Success;
					return this;
				}

				Outcome = Outcome.Failure;
				return this;
			}

			_materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			using IEnumerator<TItem> enumerator = _materializedEnumerable.GetEnumerator();
			if (enumerator.MoveNext())
			{
				Outcome = Outcome.Failure;
				return this;
			}

			Outcome = Outcome.Success;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Grammars.HasFlag(ExpectationGrammars.Nested))
			{
				stringBuilder.Append("are null or empty");
			}
			else
			{
				stringBuilder.Append("is null or empty");
			}
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, _materializedEnumerable, FormattingOptions.MultipleLines);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Grammars.HasFlag(ExpectationGrammars.Nested))
			{
				stringBuilder.Append("are not null or empty");
			}
			else
			{
				stringBuilder.Append("is not null or empty");
			}
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			if (_materializedEnumerable is null)
			{
				stringBuilder.Append("<null>");
			}
			else
			{
				Formatter.Format(stringBuilder, _materializedEnumerable, FormattingOptions.MultipleLines);
			}
		}
	}

	private sealed class IsNullOrEmptyForEnumerableConstraint<TEnumerable>(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithValue<TEnumerable?>(grammars),
			IContextConstraint<TEnumerable?>
		where TEnumerable : IEnumerable?
	{
		private IEnumerable? _materializedEnumerable;

		public ConstraintResult IsMetBy(TEnumerable? actual, IEvaluationContext context)
		{
			Actual = actual;
			if (actual is null)
			{
				_materializedEnumerable = null;
				Outcome = Outcome.Success;
				return this;
			}

			if (actual is ICollection collectionOfT)
			{
				_materializedEnumerable = actual;
				if (collectionOfT.Count == 0)
				{
					Outcome = Outcome.Success;
					return this;
				}

				Outcome = Outcome.Failure;
				return this;
			}

			_materializedEnumerable = context.UseMaterializedEnumerable(actual);
			if (_materializedEnumerable.Cast<object?>().Any())
			{
				Outcome = Outcome.Failure;
				return this;
			}

			Outcome = Outcome.Success;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Grammars.HasFlag(ExpectationGrammars.Nested))
			{
				stringBuilder.Append("are null or empty");
			}
			else
			{
				stringBuilder.Append("is null or empty");
			}
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, _materializedEnumerable, FormattingOptions.MultipleLines);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (Grammars.HasFlag(ExpectationGrammars.Nested))
			{
				stringBuilder.Append("are not null or empty");
			}
			else
			{
				stringBuilder.Append("is not null or empty");
			}
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			if (_materializedEnumerable is null)
			{
				stringBuilder.Append("<null>");
			}
			else
			{
				Formatter.Format(stringBuilder, _materializedEnumerable, FormattingOptions.MultipleLines);
			}
		}
	}
}