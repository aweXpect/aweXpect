using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Core.Sources;

namespace aweXpect.Delegates;

public abstract partial class ThatDelegate
{
	/// <summary>
	///     Verifies that the delegate throws an exception of type <typeparamref name="TException" />.
	/// </summary>
	public ThatDelegateThrows<TException> Throws<TException>()
		where TException : Exception
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<TException>(ExpectationBuilder
				.AddConstraint((it, grammars) => new DelegateIsNotNullConstraint(it, grammars))
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint((it, grammars) => new ThrowsConstraint(it, grammars, typeof(TException), throwOptions))
				.And(" "),
			throwOptions);
	}

	/// <summary>
	///     Verifies that the delegate throws an exception of type <paramref name="exceptionType" />.
	/// </summary>
	public ThatDelegateThrows<Exception> Throws(Type exceptionType)
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<Exception>(ExpectationBuilder
				.AddConstraint((it, grammars) => new DelegateIsNotNullConstraint(it, grammars))
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint((it, grammars) => new ThrowsConstraint(it, grammars, exceptionType, throwOptions))
				.And(" "),
			throwOptions);
	}

	private sealed class ThrowsConstraint(
		string it,
		ExpectationGrammars grammars,
		Type exceptionType,
		ThrowsOption throwOptions)
		: ConstraintResult(grammars),
			IValueConstraint<Exception?>
	{
		private Exception? _actual;

		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? value)
		{
			_actual = value;

			if (!throwOptions.DoCheckThrow)
			{
				FurtherProcessingStrategy = FurtherProcessingStrategy.IgnoreCompletely;
				Outcome = value is null ? Outcome.Success : Outcome.Failure;
				return this;
			}

			if (value is null)
			{
				FurtherProcessingStrategy = FurtherProcessingStrategy.IgnoreResult;
			}
			else if (exceptionType.IsAssignableFrom(value.GetType()))
			{
				Outcome = Outcome.Success;
				return this;
			}

			Outcome = Outcome.Failure;
			return this;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			if (!throwOptions.DoCheckThrow)
			{
				stringBuilder.Append("does not throw any exception");
			}
			else if (exceptionType == typeof(Exception))
			{
				stringBuilder.Append("throws an exception");
			}
			else
			{
				stringBuilder.Append("throws ").Append(Formatter.Format(exceptionType).PrependAOrAn());
			}
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (throwOptions.DoCheckThrow && _actual is null)
			{
				stringBuilder.Append(it).Append(" did not throw any exception");
			}
			else
			{
				stringBuilder.Append(it).Append(" did throw ");
				stringBuilder.Append(FormatForMessage(_actual));
			}
		}

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_actual is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			value = default;
			return typeof(TValue).IsAssignableFrom(exceptionType);
		}

		public override ConstraintResult Negate()
		{
			throwOptions.CheckThrow(!throwOptions.DoCheckThrow);
			return this;
		}
	}
}
