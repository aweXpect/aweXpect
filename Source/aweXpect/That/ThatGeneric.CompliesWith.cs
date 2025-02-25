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
				.AddConstraint((it, grammar) =>
					new ComplyWithConstraint<T>(it, expectations, options)),
			source,
			options);
	}

	private readonly struct ComplyWithConstraint<T> : IAsyncContextConstraint<T>
	{
		private readonly string _it;
		private readonly RepeatedCheckOptions _options;
		private readonly ManualExpectationBuilder<T> _itemExpectationBuilder;

		public ComplyWithConstraint(string it,
			Action<IThat<T>> expectations, RepeatedCheckOptions options)
		{
			_it = it;
			_options = options;
			_itemExpectationBuilder = new ManualExpectationBuilder<T>();
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
				return new ConstraintResult.Success<T>(actual, ToString());
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
						return new ConstraintResult.Success<T>(actual, ToString());
					}
				} while (sw.Elapsed <= _options.Timeout && !cancellationToken.IsCancellationRequested);
			}

			return new ConstraintResult.Failure(ToString(),
				$"{_it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"{_itemExpectationBuilder}{_options}";
	}
}
