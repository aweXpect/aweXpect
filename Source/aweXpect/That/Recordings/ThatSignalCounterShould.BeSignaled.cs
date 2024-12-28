using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Options;
using aweXpect.Recording;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatSignalCounterShould
{
	/// <summary>
	///     Verifies that the expected callback was signaled at least once.
	/// </summary>
	public static SignalCountResult BeSignaled(
		this IThat<SignalCounter> source)
	{
		SignalCounterOptions options = new();
		return new SignalCountResult(source.ExpectationBuilder.AddConstraint(it
				=> new TriggerConstraint(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was signaled at least once.
	/// </summary>
	public static SignalCountResult<TParameter> BeSignaled<TParameter>(
		this IThat<SignalCounter<TParameter>> source)
	{
		SignalCounterOptions<TParameter> options = new();
		return new SignalCountResult<TParameter>(source.ExpectationBuilder.AddConstraint(it
				=> new TriggerConstraint<TParameter>(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback was signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static SignalCountResult BeSignaled(
		this IThat<SignalCounter> source,
		Times times)
	{
		SignalCounterOptions options = new();
		return new SignalCountResult(source.ExpectationBuilder.AddConstraint(it
				=> new TriggerConstraint(it, times.Value, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static SignalCountResult<TParameter> BeSignaled<TParameter>(
		this IThat<SignalCounter<TParameter>> source,
		Times times)
	{
		SignalCounterOptions<TParameter> options = new();
		return new SignalCountResult<TParameter>(source.ExpectationBuilder.AddConstraint(it
				=> new TriggerConstraint<TParameter>(it, times.Value, options)),
			source,
			options);
	}

	private readonly struct TriggerConstraint(string it, int count, SignalCounterOptions options)
		: IAsyncConstraint<SignalCounter>
	{
		public async Task<ConstraintResult> IsMetBy(SignalCounter actual, CancellationToken cancellationToken)
		{
			SignalCounterResult result;
			TimeSpan? timeout = options.Timeout;
			if (count == 1)
			{
				result = await Task.Run(()
						=> actual.Wait(timeout, cancellationToken),
					CancellationToken.None);
			}
			else
			{
				int amount = count;
				result = await Task.Run(()
						=> actual.Wait(amount, timeout, cancellationToken),
					CancellationToken.None);
			}

			string expectation = count switch
			{
				1 => $"have recorded the callback at least once{options}",
				_ => $"have recorded the callback at least {count} times{options}"
			};

			if (result.IsSuccess)
			{
				return new ConstraintResult.Success<SignalCounterResult>(result, expectation);
			}

			StringBuilder sb = new();
			sb.Append(it).Append(" was ");
			if (result.Count == 0)
			{
				sb.Append("never recorded");
			}
			else if (result.Count == 1)
			{
				sb.Append("only recorded once");
			}
			else
			{
				sb.Append("only recorded ").Append(result.Count).Append(" times");
			}

			return new ConstraintResult.Failure<SignalCounterResult>(result, expectation, sb.ToString());
		}
	}

	private readonly struct TriggerConstraint<TParameter>(string it, int count, SignalCounterOptions<TParameter> options)
		: IAsyncConstraint<SignalCounter<TParameter>>
	{
		public async Task<ConstraintResult> IsMetBy(
			SignalCounter<TParameter> actual,
			CancellationToken cancellationToken)
		{
			SignalCounterOptions<TParameter> o = options;
			SignalCounterResult<TParameter> result;
			TimeSpan? timeout = options.Timeout;
			if (count == 1)
			{
				result = await Task.Run(()
						=> actual.Wait(o.Matches, timeout, cancellationToken),
					CancellationToken.None);
			}
			else
			{
				int amount = count;
				result = await Task.Run(()
						=> actual.Wait(amount, o.Matches, timeout, cancellationToken),
					CancellationToken.None);
			}

			int actualCount = result.Parameters.Count(p => o.Matches(p));

			string expectation = count switch
			{
				1 => $"have recorded the callback at least once{options}",
				_ => $"have recorded the callback at least {count} times{options}"
			};

			if (result.IsSuccess && actualCount >= count)
			{
				return new ConstraintResult.Success<SignalCounterResult<TParameter>>(result, expectation);
			}

			StringBuilder sb = new();
			sb.Append(it).Append(" was ");
			if (actualCount == 0)
			{
				sb.Append("never recorded");
			}
			else if (actualCount == 1)
			{
				sb.Append("only recorded once");
			}
			else
			{
				sb.Append("only recorded ").Append(actualCount).Append(" times");
			}

			if (result.Count > 0)
			{
				sb.Append(" in ");
				Formatter.Format(sb, result.Parameters, FormattingOptions.MultipleLines);
			}

			return new ConstraintResult.Failure<SignalCounterResult<TParameter>>(result, expectation, sb.ToString());
		}
	}
}
