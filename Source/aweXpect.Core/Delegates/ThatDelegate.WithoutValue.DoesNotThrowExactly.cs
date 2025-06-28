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
	public sealed partial class WithoutValue
	{
		/// <summary>
		///     Verifies that the delegate does not throw an exception of type <typeparamref name="TException" />.
		/// </summary>
		public ExpectationResult DoesNotThrowExactly<TException>()
			where TException : Exception
			=> new(ExpectationBuilder.AddConstraint((it, grammars) =>
					new DoesNotThrowExactlyConstraint(it, grammars, typeof(TException))));

		/// <summary>
		///     Verifies that the delegate does not throw an exception of type <paramref name="exceptionType" />.
		/// </summary>
		public ExpectationResult DoesNotThrowExactly(Type exceptionType)
			=> new(ExpectationBuilder.AddConstraint((it, grammars) =>
					new DoesNotThrowExactlyConstraint(it, grammars, exceptionType)));

		private sealed class DoesNotThrowExactlyConstraint(string it, ExpectationGrammars grammars, Type exceptionType)
			: ConstraintResult(grammars),
				IValueConstraint<DelegateValue>
		{
			private DelegateValue? _actual;
			private bool _isNegated;

			/// <inheritdoc />
			public ConstraintResult IsMetBy(DelegateValue value)
			{
				_actual = value;
				if (value.IsNull)
				{
					Outcome = Outcome.Failure;
					return this;
				}

				Outcome = _isNegated == (exceptionType != value.Exception?.GetType())
					? Outcome.Failure
					: Outcome.Success;
				return this;
			}

			public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
			{
				if (!_isNegated)
				{
					stringBuilder.Append("does not throw exactly ")
						.Append(Formatter.Format(exceptionType).PrependAOrAn());
				}
				else
				{
					stringBuilder.Append("throws exactly ")
						.Append(Formatter.Format(exceptionType).PrependAOrAn());
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
				value = default;
				return false;
			}

			public override ConstraintResult Negate()
			{
				_isNegated = !_isNegated;
				return this;
			}
		}
	}
}
