using System.Diagnostics.CodeAnalysis;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Recording;

namespace aweXpect;

/// <summary>
///     Expectations on event <see cref="IEventRecording{TSubject}" />.
/// </summary>
public static partial class ThatEventRecording
{
	private sealed class HaveTriggeredConstraint<TSubject>(
		string it,
		ExpectationGrammars grammars,
		string eventName,
		TriggerEventFilter filter,
		Quantifier quantifier)
		: ConstraintResult(grammars),
			IValueConstraint<IEventRecording<TSubject>>
		where TSubject : notnull
	{
		private IEventRecording<TSubject>? _actual;
		private Quantifier _quantifier = quantifier;
		private IEventRecordingResult? _result;

		public ConstraintResult IsMetBy(IEventRecording<TSubject> actual)
		{
			_actual = actual;
			// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
			if (actual == null)
			{
				Outcome = Outcome.Failure;
				return this;
			}

			_result = actual.Stop();
			int eventCount = _result.GetEventCount(eventName, filter.IsMatch);
			Outcome = _quantifier.Check(eventCount, true) != true ? Outcome.Failure : Outcome.Success;
			return this;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_quantifier.ToString() == "never")
			{
				stringBuilder.Append("has never recorded the ").Append(eventName).Append(" event");
				if (_actual != null)
				{
					stringBuilder.Append(" on ").Append(_actual);
				}

				stringBuilder.Append(filter);
			}
			else
			{
				stringBuilder.Append("has recorded the ").Append(eventName).Append(" event");
				if (_actual != null)
				{
					stringBuilder.Append(" on ").Append(_actual);
				}

				stringBuilder.Append(filter).Append(' ').Append(_quantifier);
			}
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_actual == null)
			{
				stringBuilder.ItWasNull(it);
				return;
			}

			int? eventCount = _result?.GetEventCount(eventName, filter.IsMatch);
			stringBuilder.Append(it).Append(" was ");
			if (eventCount == 0)
			{
				stringBuilder.Append("never recorded ");
			}
			else if (eventCount == 1)
			{
				stringBuilder.Append("recorded once ");
			}
			else
			{
				stringBuilder.Append("recorded ").Append(eventCount).Append(" times ");
			}

			stringBuilder.Append("in ");
			stringBuilder.Append(_result?.ToString(eventName));
		}

		/// <inheritdoc cref="ConstraintResult.TryGetValue{TValue}(out TValue)" />
		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_actual is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			value = default;
			return typeof(TValue).IsAssignableFrom(typeof(IEventRecording<TSubject>));
		}

		public override ConstraintResult Negate()
		{
			_quantifier.Negate();
			Outcome = Outcome switch
			{
				Outcome.Failure => Outcome.Success,
				Outcome.Success => Outcome.Failure,
				_ => Outcome,
			};
			return this;
		}
	}
}
