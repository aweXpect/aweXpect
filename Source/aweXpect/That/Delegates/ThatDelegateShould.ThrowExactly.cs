using System;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.Helpers;
using Testably.Expectations.Core.Sources;

namespace Testably.Expectations;

public static partial class ThatDelegateShould
{
	/// <summary>
	///     Verifies that the delegate throws exactly an exception of type <typeparamref name="TException" />.
	/// </summary>
	public static ThatDelegateThrows<TException> ThrowExactly<TException>(this ThatDelegate source)
		where TException : Exception
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<TException>(source.ExpectationBuilder
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint(_ => new ThrowsExactlyCastConstraint<TException>(throwOptions))
				.And(" "),
			throwOptions);
	}

	/// <summary>
	///     Verifies that the delegate throws exactly an exception of type <paramref name="exceptionType" />.
	/// </summary>
	public static ThatDelegateThrows<Exception> ThrowExactly(this ThatDelegate source,
		Type exceptionType)
	{
		ThrowsOption throwOptions = new();
		return new ThatDelegateThrows<Exception>(source.ExpectationBuilder
				.ForWhich<DelegateValue, Exception?>(d => d.Exception)
				.AddConstraint(_ => new ThrowsExactlyCastConstraint(exceptionType, throwOptions))
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
				return new ConstraintResult.Failure<TException?>(null, ToString(), "it did not");
			}

			return new ConstraintResult.Failure<TException?>(null, ToString(),
				$"it did throw {value.FormatForMessage()}");
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if (!throwOptions.DoCheckThrow)
			{
				return DoesNotThrowExpectation;
			}

			return $"throw exactly {typeof(TException).Name.PrependAOrAn()}";
		}
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
				return new ConstraintResult.Failure<Exception?>(null, ToString(), "it did not");
			}

			return new ConstraintResult.Failure<Exception?>(null, ToString(),
				$"it did throw {value.FormatForMessage()}");
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if (!throwOptions.DoCheckThrow)
			{
				return DoesNotThrowExpectation;
			}

			return $"throw exactly {exceptionType.Name.PrependAOrAn()}";
		}
	}
}
