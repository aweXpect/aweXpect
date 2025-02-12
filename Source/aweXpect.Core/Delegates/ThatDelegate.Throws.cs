using System;
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
				.AddConstraint((_, _) => new DelegateIsNotNullConstraint())
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint((_, _) => new ThrowExceptionOfTypeConstraint<TException>(throwOptions))
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
				.AddConstraint((_, _) => new DelegateIsNotNullConstraint())
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint((_, _) => new ThrowsCastConstraint(exceptionType, throwOptions))
				.And(" "),
			throwOptions);
	}

	private readonly struct ThrowsCastConstraint(Type exceptionType, ThrowsOption throwOptions)
		: IValueConstraint<Exception?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? value)
		{
			if (!throwOptions.DoCheckThrow)
			{
				return DoesNotThrowResult<Exception>(value);
			}

			if (exceptionType.IsAssignableFrom(value?.GetType()))
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

		public override string ToString()
			=> exceptionType == typeof(Exception)
				? "throws an exception"
				: $"throws {exceptionType.Name.PrependAOrAn()}";
	}

	private readonly struct ThrowExceptionOfTypeConstraint<TException>(ThrowsOption throwOptions)
		: IValueConstraint<Exception?>
		where TException : Exception
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? value)
		{
			if (!throwOptions.DoCheckThrow)
			{
				return DoesNotThrowResult<TException>(value);
			}

			if (value is TException typedException)
			{
				return new ConstraintResult.Success<TException?>(typedException, ToString());
			}

			if (value is null)
			{
				return new ConstraintResult.Failure<TException?>(null, ToString(),
					"it did not throw any exception",
					FurtherProcessingStrategy.IgnoreResult);
			}

			return new ConstraintResult.Failure<TException?>(null, ToString(),
				$"it did throw {FormatForMessage(value)}");
		}

		public override string ToString()
			=> typeof(TException) == typeof(Exception)
				? "throws an exception"
				: $"throws {typeof(TException).Name.PrependAOrAn()}";
	}
}
