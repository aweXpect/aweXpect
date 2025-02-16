using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;
using aweXpect.Signaling;

namespace aweXpect;

public static partial class ThatSignaler
{
	/// <summary>
	///     Verifies that the expected callback was signaled at least once.
	/// </summary>
	public static SignalCountResult Signaled(
		this IThat<Signaler> source)
	{
		SignalerOptions options = new();
		return new SignalCountResult(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new TriggerConstraint(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was signaled at least once.
	/// </summary>
	public static SignalCountWhoseResult<TParameter> Signaled<TParameter>(
		this IThat<Signaler<TParameter>> source)
	{
		SignalerOptions<TParameter> options = new();
		return new SignalCountWhoseResult<TParameter>(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new TriggerConstraint<TParameter>(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback was signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static SignalCountResult Signaled(
		this IThat<Signaler> source,
		Times times)
	{
		SignalerOptions options = new();
		return new SignalCountResult(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new TriggerConstraint(it, times.Value, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static SignalCountWhoseResult<TParameter> Signaled<TParameter>(
		this IThat<Signaler<TParameter>> source,
		Times times)
	{
		SignalerOptions<TParameter> options = new();
		return new SignalCountWhoseResult<TParameter>(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new TriggerConstraint<TParameter>(it, times.Value, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback was not signaled.
	/// </summary>
	public static SignalCountResult DidNotSignal(
		this IThat<Signaler> source)
	{
		SignalerOptions options = new();
		return new SignalCountResult(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new NotSignaledConstraint(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was not signaled.
	/// </summary>
	public static SignalCountResult<TParameter> DidNotSignal<TParameter>(
		this IThat<Signaler<TParameter>> source)
	{
		SignalerOptions<TParameter> options = new();
		return new SignalCountResult<TParameter>(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new NotSignaledConstraint<TParameter>(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback was not signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static SignalCountResult DidNotSignal(
		this IThat<Signaler> source,
		Times times)
	{
		SignalerOptions options = new();
		return new SignalCountResult(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new NotSignaledConstraint(it, times.Value, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was not signaled
	///     at least the given number of <paramref name="times" />.
	/// </summary>
	public static SignalCountResult<TParameter> DidNotSignal<TParameter>(
		this IThat<Signaler<TParameter>> source,
		Times times)
	{
		SignalerOptions<TParameter> options = new();
		return new SignalCountResult<TParameter>(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new NotSignaledConstraint<TParameter>(it, times.Value, options)),
			source,
			options);
	}

	private readonly struct TriggerConstraint(string it, int count, SignalerOptions options)
		: IAsyncConstraint<Signaler>
	{
		public async Task<ConstraintResult> IsMetBy(Signaler actual, CancellationToken cancellationToken)
		{
			string expectation = count switch
			{
				1 => $"has recorded the callback at least once{options}",
				_ => $"has recorded the callback at least {count} times{options}",
			};

			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<SignalerResult>(null!, expectation, $"{it} was <null>");
			}

			SignalerResult result;
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

			if (result.IsSuccess)
			{
				return new ConstraintResult.Success<SignalerResult>(result, expectation);
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

			return new ConstraintResult.Failure<SignalerResult>(result, expectation, sb.ToString());
		}
	}

	private readonly struct TriggerConstraint<TParameter>(string it, int count, SignalerOptions<TParameter> options)
		: IAsyncConstraint<Signaler<TParameter>>
	{
		public async Task<ConstraintResult> IsMetBy(
			Signaler<TParameter> actual,
			CancellationToken cancellationToken)
		{
			string expectation = count switch
			{
				1 => $"has recorded the callback at least once{options}",
				_ => $"has recorded the callback at least {count} times{options}",
			};

			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<SignalerResult>(null!, expectation, $"{it} was <null>");
			}

			SignalerOptions<TParameter> o = options;
			SignalerResult<TParameter> result;
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

			if (result.IsSuccess && actualCount >= count)
			{
				return new ConstraintResult.Success<SignalerResult<TParameter>>(result, expectation);
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

			return new ConstraintResult.Failure<SignalerResult<TParameter>>(result, expectation, sb.ToString());
		}
	}

	private readonly struct NotSignaledConstraint(string it, int count, SignalerOptions options)
		: IAsyncConstraint<Signaler>
	{
		public async Task<ConstraintResult> IsMetBy(Signaler actual, CancellationToken cancellationToken)
		{
			string expectation = count switch
			{
				1 => $"does not have recorded the callback{options}",
				_ => $"does not have recorded the callback at least {count} times{options}",
			};

			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<SignalerResult>(null!, expectation, $"{it} was <null>");
			}

			SignalerResult result;
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

			if (!result.IsSuccess)
			{
				return new ConstraintResult.Success<SignalerResult>(result, expectation);
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

			return new ConstraintResult.Failure<SignalerResult>(result, expectation, sb.ToString());
		}
	}

	private readonly struct NotSignaledConstraint<TParameter>(
		string it,
		int count,
		SignalerOptions<TParameter> options)
		: IAsyncConstraint<Signaler<TParameter>>
	{
		public async Task<ConstraintResult> IsMetBy(
			Signaler<TParameter> actual,
			CancellationToken cancellationToken)
		{
			string expectation = count switch
			{
				1 => $"does not have recorded the callback{options}",
				_ => $"does not have recorded the callback at least {count} times{options}",
			};

			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual is null)
			{
				return new ConstraintResult.Failure<SignalerResult>(null!, expectation, $"{it} was <null>");
			}

			SignalerOptions<TParameter> o = options;
			SignalerResult<TParameter> result;
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

			if (!result.IsSuccess || actualCount < count)
			{
				return new ConstraintResult.Success<SignalerResult<TParameter>>(result, expectation);
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

			return new ConstraintResult.Failure<SignalerResult<TParameter>>(result, expectation, sb.ToString());
		}
	}
}
