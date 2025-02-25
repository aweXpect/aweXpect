using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGeneric
{
	/// <summary>
	///     Verifies the actual value to satisfy the <paramref name="predicate" />.
	/// </summary>
	public static RepeatedCheckResult<T, IThat<T>> Satisfies<T>(this IThat<T> source,
		Func<T, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
	{
		RepeatedCheckOptions options = new();
		return new RepeatedCheckResult<T, IThat<T>>(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, grammar) =>
					new SatisfyConstraint<T>(it, predicate, doNotPopulateThisValue.TrimCommonWhiteSpace(), options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies the actual value to not satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<T, IThat<T>> DoesNotSatisfy<T>(this IThat<T> source,
		Func<T, bool> predicate,
		[CallerArgumentExpression("predicate")]
		string doNotPopulateThisValue = "")
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, grammar) =>
					new NotSatisfyConstraint<T>(it, predicate, doNotPopulateThisValue.TrimCommonWhiteSpace())),
			source);

	private readonly struct SatisfyConstraint<T>(
		string it,
		Func<T, bool> predicate,
		string predicateExpression,
		RepeatedCheckOptions options)
		: IAsyncConstraint<T>
	{
		public async Task<ConstraintResult> IsMetBy(T actual, CancellationToken cancellationToken)
		{
			if (predicate(actual))
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			if (options.Timeout > TimeSpan.Zero)
			{
				Stopwatch? sw = new();
				sw.Start();
				do
				{
					try
					{
						await Task.Delay(options.Interval.NextCheckInterval(), cancellationToken);
					}
					catch (TaskCanceledException)
					{
						break;
					}

					if (predicate(actual))
					{
						return new ConstraintResult.Success<T>(actual, ToString());
					}
				} while (sw.Elapsed <= options.Timeout && !cancellationToken.IsCancellationRequested);
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"satisfy {predicateExpression.TrimCommonWhiteSpace()}{options}";
	}

	private readonly struct NotSatisfyConstraint<T>(
		string it,
		Func<T, bool> predicate,
		string predicateExpression)
		: IValueConstraint<T>
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (!predicate(actual))
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"not satisfy {predicateExpression}";
	}
}
