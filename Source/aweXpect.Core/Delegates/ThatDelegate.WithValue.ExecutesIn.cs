﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Core.Sources;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect.Delegates;

public abstract partial class ThatDelegate
{
	public sealed partial class WithValue<T>
	{
		/// <summary>
		///     Verifies that the delegate executes in…
		/// </summary>
		public ExecutesInResult<AndResult<WithValue<T>>> ExecutesIn()
		{
			TimeSpanEqualityOptions options = new();
			return new ExecutesInResult<AndResult<WithValue<T>>>(
				new AndResult<WithValue<T>>(ExpectationBuilder.AddConstraint((it, grammars)
						=> new ExecutesInConstraint(it, grammars, options)),
					this),
				options);
		}

		private sealed class ExecutesInConstraint(
			string it,
			ExpectationGrammars grammars,
			TimeSpanEqualityOptions options)
			: ConstraintResult(grammars),
				IValueConstraint<DelegateValue<T>>
		{
			private DelegateValue<T>? _actual;

			/// <inheritdoc />
			public ConstraintResult IsMetBy(DelegateValue<T> value)
			{
				_actual = value;
				if (value.IsNull)
				{
					Outcome = Outcome.Failure;
					return this;
				}

				Outcome = options.IsWithinLimit(value.Duration) ? Outcome.Success : Outcome.Failure;
				return this;
			}

			public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
				=> stringBuilder.Append("executes in ").Append(options);

			public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			{
				if (_actual?.IsNull != false)
				{
					stringBuilder.ItWasNull(it);
				}
				else
				{
					stringBuilder.Append(it).Append(" took ");
					options.AppendFailureResult(stringBuilder, _actual.Duration);
				}
			}

			public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
			{
				if (_actual is { Value: TValue typedValue, })
				{
					value = typedValue;
					return true;
				}

				value = default;
				return typeof(TValue).IsAssignableFrom(typeof(T));
			}

			public override ConstraintResult Negate()
				=> throw new NotSupportedException($"Negation of {nameof(ExecutesIn)} is not supported.");
		}
	}
}
