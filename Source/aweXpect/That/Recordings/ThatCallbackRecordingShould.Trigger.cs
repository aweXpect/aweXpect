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
	///     Verifies that the expected callback was triggered at least once.
	/// </summary>
	public static CallbackTriggerResult<ICallbackRecording> Trigger(
		this IThat<ICallbackRecording> source)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ICallbackRecording>(source.ExpectationBuilder.AddConstraint(it
				=> new HaveTriggeredConstraint(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was triggered at least once.
	/// </summary>
	public static CallbackTriggerResult<ICallbackRecording<TParameter>> Trigger<TParameter>(
		this IThat<ICallbackRecording<TParameter>> source)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ICallbackRecording<TParameter>>(source.ExpectationBuilder.AddConstraint(it
				=> new HaveTriggeredConstraint<TParameter>(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback was triggered
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static CallbackTriggerResult<ICallbackRecording> Trigger(
		this IThat<ICallbackRecording> source,
		Times times)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ICallbackRecording>(source.ExpectationBuilder.AddConstraint(it
				=> new HaveTriggeredConstraint(it, times.Value, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was triggered
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static CallbackTriggerResult<ICallbackRecording<TParameter>> Trigger<TParameter>(
		this IThat<ICallbackRecording<TParameter>> source,
		Times times)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ICallbackRecording<TParameter>>(source.ExpectationBuilder.AddConstraint(it
				=> new HaveTriggeredConstraint<TParameter>(it, times.Value, options)),
			source,
			options);
	}

	private readonly struct HaveTriggeredConstraint(string it, int count, TriggerCallbackOptions options)
		: IAsyncConstraint<ICallbackRecording>
	{
		public async Task<ConstraintResult> IsMetBy(ICallbackRecording actual, CancellationToken cancellationToken)
		{
			ICallbackRecordingResult result;
			TimeSpan? timeout = options.Timeout;
			if (count == 1)
			{
				result = await Task.Run(()
						=> actual.Wait(timeout, cancellationToken),
					cancellationToken);
			}
			else
			{
				int amount = count;
				result = await Task.Run(()
						=> actual.WaitMultiple(amount, timeout, cancellationToken),
					cancellationToken);
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
				return new ConstraintResult.Success<ICallbackRecording>(actual, expectation);
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

			return new ConstraintResult.Failure<ICallbackRecording>(actual, expectation, sb.ToString());
		}
	}

	private readonly struct HaveTriggeredConstraint<TParameter>(string it, int count, TriggerCallbackOptions options)
		: IAsyncConstraint<ICallbackRecording<TParameter>>
	{
		public async Task<ConstraintResult> IsMetBy(
			ICallbackRecording<TParameter> actual,
			CancellationToken cancellationToken)
		{
			ICallbackRecordingResult<TParameter> result;
			TimeSpan? timeout = options.Timeout;
			if (count == 1)
			{
				result = await Task.Run(()
						=> actual.Wait(timeout, cancellationToken),
					cancellationToken);
			}
			else
			{
				int amount = count;
				result = await Task.Run(()
						=> actual.WaitMultiple(amount, timeout, cancellationToken),
					cancellationToken);
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
				return new ConstraintResult.Success<ICallbackRecording<TParameter>>(actual, expectation);
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

			return new ConstraintResult.Failure<ICallbackRecording<TParameter>>(actual, expectation, sb.ToString());
		}
	}
}
