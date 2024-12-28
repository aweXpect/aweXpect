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
	///     Verifies that the expected callback was not signaled.
	/// </summary>
	public static SignalCountResult NotBeSignaled(
		this IThat<SignalCounter> source)
	{
		SignalCounterOptions options = new();
		return new SignalCountResult(source.ExpectationBuilder.AddConstraint(it
				=> new NotSignaledConstraint(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was not signaled.
	/// </summary>
	public static SignalCountResult<TParameter> NotBeSignaled<TParameter>(
		this IThat<SignalCounter<TParameter>> source)
	{
		SignalCounterOptions<TParameter> options = new();
		return new SignalCountResult<TParameter>(source.ExpectationBuilder.AddConstraint(it
				=> new NotSignaledConstraint<TParameter>(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback was not signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static SignalCountResult NotBeSignaled(
		this IThat<SignalCounter> source,
		Times times)
	{
		SignalCounterOptions options = new();
		return new SignalCountResult(source.ExpectationBuilder.AddConstraint(it
				=> new NotSignaledConstraint(it, times.Value, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was not signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static SignalCountResult<TParameter> NotBeSignaled<TParameter>(
		this IThat<SignalCounter<TParameter>> source,
		Times times)
	{
		SignalCounterOptions<TParameter> options = new();
		return new SignalCountResult<TParameter>(source.ExpectationBuilder.AddConstraint(it
				=> new NotSignaledConstraint<TParameter>(it, times.Value, options)),
			source,
			options);
	}

	private readonly struct NotSignaledConstraint(string it, int count, SignalCounterOptions options)
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
				1 => $"not have recorded the callback{options}",
				_ => $"not have recorded the callback at least {count} times{options}"
			};

			if (!result.IsSuccess)
			{
				return new ConstraintResult.Success<SignalCounterResult>(result, expectation);
			}

			StringBuilder sb = new();
			sb.Append(it).Append(" was ");
			if (result.Count == 1)
			{
				sb.Append("recorded once");
			}
			else
			{
				sb.Append("recorded ").Append(result.Count).Append(" times");
			}

			return new ConstraintResult.Failure<SignalCounterResult>(result, expectation, sb.ToString());
		}
	}

	private readonly struct NotSignaledConstraint<TParameter>(
		string it,
		int count,
		SignalCounterOptions<TParameter> options)
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
						=> actual.Wait(timeout, cancellationToken, o.Matches),
					CancellationToken.None);
			}
			else
			{
				int amount = count;
				result = await Task.Run(()
						=> actual.Wait(amount, timeout, cancellationToken, o.Matches),
					CancellationToken.None);
			}

			int actualCount = result.Parameters.Count(p => o.Matches(p));

			string expectation = count switch
			{
				1 => $"not have recorded the callback{options}",
				_ => $"not have recorded the callback at least {count} times{options}"
			};

			if (!result.IsSuccess || actualCount < count)
			{
				return new ConstraintResult.Success<SignalCounterResult<TParameter>>(result, expectation);
			}

			StringBuilder sb = new();
			sb.Append(it).Append(" was ");
			if (actualCount == 1)
			{
				sb.Append("recorded once");
			}
			else
			{
				sb.Append("recorded ").Append(actualCount).Append(" times");
			}

			sb.Append(" in ");
			Formatter.Format(sb, result.Parameters, FormattingOptions.MultipleLines);

			return new ConstraintResult.Failure<SignalCounterResult<TParameter>>(result, expectation, sb.ToString());
		}
	}
}
