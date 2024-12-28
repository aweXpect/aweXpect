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
	///     Verifies that the expected callback was not triggered.
	/// </summary>
	public static CallbackTriggerResult<ICallbackRecording> NotTrigger(
		this IThat<ICallbackRecording> source)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ICallbackRecording>(source.ExpectationBuilder.AddConstraint(it
				=> new NotTriggerConstraint(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was not triggered.
	/// </summary>
	public static CallbackTriggerResult<ICallbackRecording<TParameter>> NotTrigger<TParameter>(
		this IThat<ICallbackRecording<TParameter>> source)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ICallbackRecording<TParameter>>(source.ExpectationBuilder.AddConstraint(it
				=> new NotTriggerConstraint<TParameter>(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback was not triggered
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static CallbackTriggerResult<ICallbackRecording> NotTrigger(
		this IThat<ICallbackRecording> source,
		Times times)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ICallbackRecording>(source.ExpectationBuilder.AddConstraint(it
				=> new NotTriggerConstraint(it, times.Value, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was not triggered
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static CallbackTriggerResult<ICallbackRecording<TParameter>> NotTrigger<TParameter>(
		this IThat<ICallbackRecording<TParameter>> source,
		Times times)
	{
		TriggerCallbackOptions options = new();
		return new CallbackTriggerResult<ICallbackRecording<TParameter>>(source.ExpectationBuilder.AddConstraint(it
				=> new NotTriggerConstraint<TParameter>(it, times.Value, options)),
			source,
			options);
	}

	private readonly struct NotTriggerConstraint(string it, int count, TriggerCallbackOptions options)
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
				1 => "not have recorded the callback",
				_ => $"not have recorded the callback at least {count} times"
			};
			if (timeout != null)
			{
				expectation += $" within {Formatter.Format(timeout.Value)}";
			}

			if (!result.IsSuccess)
			{
				return new ConstraintResult.Success<ICallbackRecording>(actual, expectation);
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

			return new ConstraintResult.Failure<ICallbackRecording>(actual, expectation, sb.ToString());
		}
	}

	private readonly struct NotTriggerConstraint<TParameter>(string it, int count, TriggerCallbackOptions options)
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
				1 => "not have recorded the callback",
				_ => $"not have recorded the callback at least {count} times"
			};
			if (timeout != null)
			{
				expectation += $" within {Formatter.Format(timeout.Value)}";
			}

			if (!result.IsSuccess)
			{
				return new ConstraintResult.Success<ICallbackRecording<TParameter>>(actual, expectation);
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

			return new ConstraintResult.Failure<ICallbackRecording<TParameter>>(actual, expectation, sb.ToString());
		}
	}
}
