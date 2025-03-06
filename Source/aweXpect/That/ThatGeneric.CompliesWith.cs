using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGeneric
{
	/// <summary>
	///     Verifies that the actual value complies with the <paramref name="expectations" />.
	/// </summary>
	public static RepeatedCheckResult<T, IThat<T>> CompliesWith<T>(this IThat<T> source,
		Action<IThat<T>> expectations)
	{
		RepeatedCheckOptions options = new();
		return new RepeatedCheckResult<T, IThat<T>>(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new ComplyWithConstraint<T>(it, grammars, expectations, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the actual value does not comply with the <paramref name="expectations" />.
	/// </summary>
	public static RepeatedCheckResult<T, IThat<T>> DoesNotComplyWith<T>(this IThat<T> source,
		Action<IThat<T>> expectations)
	{
		RepeatedCheckOptions options = new();
		return new RepeatedCheckResult<T, IThat<T>>(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new DoesNotComplyWithConstraint<T>(it, grammars, expectations, options)),
			source,
			options);
	}

	private readonly struct ComplyWithConstraint<T> : IAsyncContextConstraint<T>
	{
		private readonly string _it;
		private readonly RepeatedCheckOptions _options;
		private readonly ManualExpectationBuilder<T> _itemExpectationBuilder;

		public ComplyWithConstraint(string it, ExpectationGrammars grammars,
			Action<IThat<T>> expectations, RepeatedCheckOptions options)
		{
			_it = it;
			_options = options;
			_itemExpectationBuilder = new ManualExpectationBuilder<T>(grammars);
			expectations.Invoke(new ThatSubject<T>(_itemExpectationBuilder));
		}

		public async Task<ConstraintResult> IsMetBy(
			T actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			ConstraintResult isMatch = await _itemExpectationBuilder.IsMetBy(actual, context, cancellationToken);
			if (isMatch.Outcome == Outcome.Success)
			{
				return isMatch.SuffixExpectation(_options.ToString());
			}

			if (_options.Timeout > TimeSpan.Zero)
			{
				Stopwatch? sw = new();
				sw.Start();
				do
				{
					try
					{
						await Task.Delay(_options.Interval.NextCheckInterval(), cancellationToken);
					}
					catch (TaskCanceledException)
					{
						break;
					}

					isMatch = await _itemExpectationBuilder.IsMetBy(actual, context, cancellationToken);
					if (isMatch.Outcome == Outcome.Success)
					{
						return isMatch.SuffixExpectation(_options.ToString());
					}
				} while (sw.Elapsed <= _options.Timeout && !cancellationToken.IsCancellationRequested);
			}

			return isMatch.SuffixExpectation(_options.ToString());
		}
	}

	private readonly struct DoesNotComplyWithConstraint<T> : IAsyncContextConstraint<T>
	{
		private readonly string _it;
		private readonly RepeatedCheckOptions _options;
		private readonly ManualExpectationBuilder<T> _itemExpectationBuilder;

		public DoesNotComplyWithConstraint(string it, ExpectationGrammars grammars,
			Action<IThat<T>> expectations, RepeatedCheckOptions options)
		{
			_it = it;
			_options = options;
			_itemExpectationBuilder = new ManualExpectationBuilder<T>(grammars.Negate());
			expectations.Invoke(new ThatSubject<T>(_itemExpectationBuilder));
		}

		public async Task<ConstraintResult> IsMetBy(
			T actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			ConstraintResult isMatch = await _itemExpectationBuilder.IsMetBy(actual, context, cancellationToken);
			if (isMatch.Outcome != Outcome.Success)
			{
				return isMatch.Negate();
			}

			if (_options.Timeout > TimeSpan.Zero)
			{
				Stopwatch? sw = new();
				sw.Start();
				do
				{
					try
					{
						await Task.Delay(_options.Interval.NextCheckInterval(), cancellationToken);
					}
					catch (TaskCanceledException)
					{
						break;
					}

					isMatch = await _itemExpectationBuilder.IsMetBy(actual, context, cancellationToken);
					if (isMatch.Outcome != Outcome.Success)
					{
						return isMatch.Negate().SuffixExpectation(_options.ToString());
					}
				} while (sw.Elapsed <= _options.Timeout && !cancellationToken.IsCancellationRequested);
			}

			return isMatch.Negate().SuffixExpectation(_options.ToString());
		}
	}
}
