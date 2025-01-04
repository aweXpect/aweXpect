﻿using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Options;
using aweXpect.Results;
using aweXpect.Signaling;

namespace aweXpect;

public static partial class ThatSignalerShould
{
	/// <summary>
	///     Verifies that the expected callback was not signaled.
	/// </summary>
	public static SignalCountResult NotBeSignaled(
		this IThat<Signaler> source)
	{
		SignalerOptions options = new();
		return new SignalCountResult(source.ExpectationBuilder.AddConstraint(it
				=> new NotSignaledConstraint(it, 1, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the expected callback with <typeparamref name="TParameter" /> was not signaled.
	/// </summary>
	public static SignalCountResult<TParameter> NotBeSignaled<TParameter>(
		this IThat<Signaler<TParameter>> source)
	{
		SignalerOptions<TParameter> options = new();
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
		this IThat<Signaler> source,
		Times times)
	{
		SignalerOptions options = new();
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
		this IThat<Signaler<TParameter>> source,
		Times times)
	{
		SignalerOptions<TParameter> options = new();
		return new SignalCountResult<TParameter>(source.ExpectationBuilder.AddConstraint(it
				=> new NotSignaledConstraint<TParameter>(it, times.Value, options)),
			source,
			options);
	}

	private readonly struct NotSignaledConstraint(string it, int count, SignalerOptions options)
		: IAsyncConstraint<Signaler>
	{
		public async Task<ConstraintResult> IsMetBy(Signaler actual, CancellationToken cancellationToken)
		{
			string expectation = count switch
			{
				1 => $"not have recorded the callback{options}",
				_ => $"not have recorded the callback at least {count} times{options}"
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
				1 => $"not have recorded the callback{options}",
				_ => $"not have recorded the callback at least {count} times{options}"
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
