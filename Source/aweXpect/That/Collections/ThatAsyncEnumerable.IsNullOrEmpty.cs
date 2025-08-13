#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Customization;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that the async enumerable is <see langword="null" /> or empty.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>?, IThat<IAsyncEnumerable<TItem>?>> IsNullOrEmpty<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> source)
		=> new(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) => new IsNullOrEmptyConstraint<TItem>(it, grammars)),
			source);

	/// <summary>
	///     Verifies that the async enumerable is not <see langword="null" /> or empty.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>> IsNotNullOrEmpty<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> source)
		=> new(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) => new IsNullOrEmptyConstraint<TItem>(it, grammars).Invert()),
			source);

	private sealed class IsNullOrEmptyConstraint<TItem>(string it, ExpectationGrammars grammars)
		: ConstraintResult.WithValue<IAsyncEnumerable<TItem>?>(grammars),
			IAsyncContextConstraint<IAsyncEnumerable<TItem>?>
	{
		private IAsyncEnumerable<TItem>? _materializedEnumerable;
		private readonly List<TItem> _items = [];

		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem>? actual, IEvaluationContext context, CancellationToken cancellationToken)
		{
			Actual = actual;
			if (actual is null)
			{
				_materializedEnumerable = null;
				Outcome = Outcome.Success;
				return this;
			}

			_materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			await using IAsyncEnumerator<TItem> enumerator = _materializedEnumerable.GetAsyncEnumerator(cancellationToken);
			if (await enumerator.MoveNextAsync())
			{
				int maximumNumberOfCollectionItems =
					Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
				_items.Add(enumerator.Current);
				while (await enumerator.MoveNextAsync())
				{
					_items.Add(enumerator.Current);
					if (_items.Count > maximumNumberOfCollectionItems)
					{
						break;
					}
				}

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
			if (_materializedEnumerable is null)
			{
				stringBuilder.Append("<null>");
			}
			else
			{
				Formatter.Format(stringBuilder, _items, FormattingOptions.MultipleLines);
			}
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
				Formatter.Format(stringBuilder, _items, FormattingOptions.MultipleLines);
			}
		}
	}
}
#endif