using System;
using aweXpect.Core.Constraints;
using aweXpect.Core.Helpers;
using aweXpect.Core.Sources;

namespace aweXpect.Delegates;

public abstract partial class ThatDelegate
{
	/// <summary>
	///     Verifies that the delegate throws exactly an exception of type <typeparamref name="TException" />.
	/// </summary>
	public ThatDelegateThrows<TException> ThrowsExactly<TException>()
		where TException : Exception
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<TException>(ExpectationBuilder
				.AddConstraint((_, _) => new DelegateIsNotNullConstraint())
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint((_, _) => new ThrowsExactlyCastConstraint<TException>(throwOptions))
				.And(" "),
			throwOptions);
	}

	/// <summary>
	///     Verifies that the delegate throws exactly an exception of type <paramref name="exceptionType" />.
	/// </summary>
	public ThatDelegateThrows<Exception> ThrowsExactly(Type exceptionType)
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<Exception>(ExpectationBuilder
				.AddConstraint((_, _) => new DelegateIsNotNullConstraint())
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint((_, _) => new ThrowsExactlyCastConstraint(exceptionType, throwOptions))
				.And(" "),
			throwOptions);
	}

	private readonly struct ThrowsExactlyCastConstraint<TException>(ThrowsOption throwOptions)
		: IValueConstraint<Exception?>
		where TException : Exception
	{
		public ConstraintResult IsMetBy(Exception? value)
		{
			if (!throwOptions.DoCheckThrow)
			{
				return DoesNotThrowResult<Exception>(value);
			}

			if (value is TException typedException && value.GetType() == typeof(TException))
			{
				return new ConstraintResult.Success<TException?>(typedException, ToString());
			}

			if (value is null)
			{
				return new ConstraintResult.Failure<TException?>(null, ToString(), "it did not throw any exception");
			}

			return new ConstraintResult.Failure<TException?>(null, ToString(),
				$"it did throw {FormatForMessage(value)}");
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"throws exactly {typeof(TException).Name.PrependAOrAn()}";
	}

	private readonly struct ThrowsExactlyCastConstraint(
		Type exceptionType,
		ThrowsOption throwOptions)
		: IValueConstraint<Exception?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? value)
		{
			if (!throwOptions.DoCheckThrow)
			{
				return DoesNotThrowResult<Exception>(value);
			}

			if (value?.GetType() == exceptionType)
			{
				return new ConstraintResult.Success<Exception?>(value, ToString());
			}

			if (value is null)
			{
				return new ConstraintResult.Failure<Exception?>(null, ToString(), "it did not throw any exception");
			}

			return new ConstraintResult.Failure<Exception?>(null, ToString(),
				$"it did throw {FormatForMessage(value)}");
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"throws exactly {exceptionType.Name.PrependAOrAn()}";
	}
}
