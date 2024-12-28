using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Options;
using aweXpect.Recording;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatCallbackRecordingShould
{
	/// <summary>
	///     Verifies that the expected callback was signaled at least once.
	/// </summary>
	public static CallbackTriggerResult<ISignalCounter> BeSignaled(
		this IThat<ISignalCounter> source)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ISignalCounter>(source.ExpectationBuilder.AddConstraint(it
				=> new TriggerConstraint(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was signaled at least once.
	/// </summary>
	public static CallbackTriggerResult<ISignalCounter<TParameter>> BeSignaled<TParameter>(
		this IThat<ISignalCounter<TParameter>> source)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ISignalCounter<TParameter>>(source.ExpectationBuilder.AddConstraint(it
				=> new TriggerConstraint<TParameter>(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback was signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static CallbackTriggerResult<ISignalCounter> BeSignaled(
		this IThat<ISignalCounter> source,
		Times times)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ISignalCounter>(source.ExpectationBuilder.AddConstraint(it
				=> new TriggerConstraint(it, times.Value, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static CallbackTriggerResult<ISignalCounter<TParameter>> BeSignaled<TParameter>(
		this IThat<ISignalCounter<TParameter>> source,
		Times times)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ISignalCounter<TParameter>>(source.ExpectationBuilder.AddConstraint(it
				=> new TriggerConstraint<TParameter>(it, times.Value, options)),
			source,
			options);
	}

	private readonly struct TriggerConstraint(string it, int count, TriggerCallbackOptions options)
		: IAsyncConstraint<ISignalCounter>
	{
		public async Task<ConstraintResult> IsMetBy(ISignalCounter actual, CancellationToken cancellationToken)
		{
			ISignalCounterResult result;
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
				1 => "have recorded the callback at least once",
				_ => $"have recorded the callback at least {count} times"
			};
			if (timeout != null)
			{
				expectation += $" within {Formatter.Format(timeout.Value)}";
			}

			if (result.IsSuccess)
			{
				return new ConstraintResult.Success<ISignalCounter>(actual, expectation);
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

			return new ConstraintResult.Failure<ISignalCounter>(actual, expectation, sb.ToString());
		}
	}

	private readonly struct TriggerConstraint<TParameter>(string it, int count, TriggerCallbackOptions options)
		: IAsyncConstraint<ISignalCounter<TParameter>>
	{
		public async Task<ConstraintResult> IsMetBy(
			ISignalCounter<TParameter> actual,
			CancellationToken cancellationToken)
		{
			ISignalCounterResult<TParameter> result;
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
				1 => "have recorded the callback at least once",
				_ => $"have recorded the callback at least {count} times"
			};
			if (timeout != null)
			{
				expectation += $" within {Formatter.Format(timeout.Value)}";
			}

			if (result.IsSuccess)
			{
				return new ConstraintResult.Success<ISignalCounter<TParameter>>(actual, expectation);
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

			if (result.Count > 0)
			{
				sb.Append(" with ");
				Formatter.Format(sb, result.Parameters, FormattingOptions.MultipleLines);
			}

			return new ConstraintResult.Failure<ISignalCounter<TParameter>>(actual, expectation, sb.ToString());
		}
	}
}
