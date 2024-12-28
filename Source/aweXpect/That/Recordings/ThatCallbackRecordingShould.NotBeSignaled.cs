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
	///     Verifies that the expected callback was not signaled.
	/// </summary>
	public static CallbackTriggerResult<ISignalCounter> NotBeSignaled(
		this IThat<ISignalCounter> source)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ISignalCounter>(source.ExpectationBuilder.AddConstraint(it
				=> new NotSignaledConstraint(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was not signaled.
	/// </summary>
	public static CallbackTriggerResult<ISignalCounter<TParameter>> NotBeSignaled<TParameter>(
		this IThat<ISignalCounter<TParameter>> source)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ISignalCounter<TParameter>>(source.ExpectationBuilder.AddConstraint(it
				=> new NotSignaledConstraint<TParameter>(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback was not signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static CallbackTriggerResult<ISignalCounter> NotBeSignaled(
		this IThat<ISignalCounter> source,
		Times times)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ISignalCounter>(source.ExpectationBuilder.AddConstraint(it
				=> new NotSignaledConstraint(it, times.Value, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was not signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static CallbackTriggerResult<ISignalCounter<TParameter>> NotBeSignaled<TParameter>(
		this IThat<ISignalCounter<TParameter>> source,
		Times times)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ISignalCounter<TParameter>>(source.ExpectationBuilder.AddConstraint(it
				=> new NotSignaledConstraint<TParameter>(it, times.Value, options)),
			source,
			options);
	}

	private readonly struct NotSignaledConstraint(string it, int count, TriggerCallbackOptions options)
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
				1 => "not have recorded the callback",
				_ => $"not have recorded the callback at least {count} times"
			};
			if (timeout != null)
			{
				expectation += $" within {Formatter.Format(timeout.Value)}";
			}

			if (!result.IsSuccess)
			{
				return new ConstraintResult.Success<ISignalCounter>(actual, expectation);
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

			return new ConstraintResult.Failure<ISignalCounter>(actual, expectation, sb.ToString());
		}
	}

	private readonly struct NotSignaledConstraint<TParameter>(string it, int count, TriggerCallbackOptions options)
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
				1 => "not have recorded the callback",
				_ => $"not have recorded the callback at least {count} times"
			};
			if (timeout != null)
			{
				expectation += $" within {Formatter.Format(timeout.Value)}";
			}

			if (!result.IsSuccess)
			{
				return new ConstraintResult.Success<ISignalCounter<TParameter>>(actual, expectation);
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

			sb.Append(" with ");
			Formatter.Format(sb, result.Parameters, FormattingOptions.MultipleLines);

			return new ConstraintResult.Failure<ISignalCounter<TParameter>>(actual, expectation, sb.ToString());
		}
	}
}
