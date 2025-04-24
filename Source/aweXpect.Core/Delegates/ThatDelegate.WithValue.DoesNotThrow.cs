﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Core.Sources;
using aweXpect.Results;

namespace aweXpect.Delegates;

public abstract partial class ThatDelegate
{
	public sealed partial class WithValue<T>
	{
		/// <summary>
		///     Verifies that the delegate does not throw any exception.
		/// </summary>
		public AndResult<T, WithValue<T>> DoesNotThrow()
			=> new(ExpectationBuilder.AddConstraint((it, grammars) =>
					new DoesNotThrowConstraint(it, grammars, typeof(Exception))),
				this);

		/// <summary>
		///     Verifies that the delegate does not throw an exception of type <typeparamref name="TException" />.
		/// </summary>
		public AndResult<T, WithValue<T>> DoesNotThrow<TException>()
			where TException : Exception
			=> new(ExpectationBuilder.AddConstraint((it, grammars) =>
					new DoesNotThrowConstraint(it, grammars, typeof(TException))),
				this);

		/// <summary>
		///     Verifies that the delegate does not throw an exception of type <paramref name="exceptionType" />.
		/// </summary>
		public AndResult<T, WithValue<T>> DoesNotThrow(Type exceptionType)
			=> new(ExpectationBuilder.AddConstraint((it, grammars) =>
					new DoesNotThrowConstraint(it, grammars, exceptionType)),
				this);

		private sealed class DoesNotThrowConstraint(
			string it,
			ExpectationGrammars grammars,
			Type exceptionType)
			: ConstraintResult(grammars),
				IValueConstraint<DelegateValue<T>>
		{
			private DelegateValue<T>? _actual;
			private bool _isNegated;

			/// <inheritdoc />
			public ConstraintResult IsMetBy(DelegateValue<T> value)
			{
				_actual = value;
				if (value.IsNull)
				{
					Outcome = Outcome.Failure;
					return this;
				}

				Outcome = _isNegated == (value.Exception is null ||
				                         !exceptionType.IsAssignableFrom(value.Exception.GetType()))
					? Outcome.Failure
					: Outcome.Success;
				return this;
			}

			public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			{
				if (!_isNegated)
				{
					if (exceptionType == typeof(Exception))
					{
						stringBuilder.Append("does not throw any exception");
					}
					else
					{
						stringBuilder.Append("does not throw ")
							.Append(ValueFormatters.Format(Formatter, exceptionType).PrependAOrAn());
					}
				}
				else
				{
					if (exceptionType == typeof(Exception))
					{
						stringBuilder.Append("throws an exception");
					}
					else
					{
						stringBuilder.Append("throws ")
							.Append(ValueFormatters.Format(Formatter, exceptionType).PrependAOrAn());
					}
				}
			}

			public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
			{
				if (_actual?.IsNull != false)
				{
					stringBuilder.ItWasNull(it);
				}
				else
				{
					switch (_isNegated)
					{
						case true when _actual.Exception is null:
							stringBuilder.Append(it).Append(" did not throw any exception");
							break;
						case false when _actual.Exception is not null:
							stringBuilder.Append(it).Append(" did throw ");
							stringBuilder.Append(FormatForMessage(_actual.Exception));
							break;
					}
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
			{
				_isNegated = !_isNegated;
				return this;
			}
		}
	}
}
