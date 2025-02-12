using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect.Delegates;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the thrown exception has an inner exception of type <typeparamref name="TInnerException" /> which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>>
		WithInner<TInnerException>(
			Action<IThat<TInnerException?>> expectations)
		where TInnerException : Exception
		=> new(ExpectationBuilder
				.ForMember<Exception, Exception?>(e => e.InnerException,
					$"with an inner {(typeof(TInnerException) == typeof(Exception) ? ExceptionString : Formatter.Format(typeof(TInnerException)))} whose",
					false)
				.Validate(it
					=> new InnerExceptionIsTypeConstraint<TInnerException>(it))
				.AddExpectations(e => expectations(new ThatSubject<TInnerException?>(e)), ExpectationGrammars.Nested),
			this);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <typeparamref name="TException" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> WithInner<
		TInnerException>()
		where TInnerException : Exception?
		=> new(ExpectationBuilder
				.AddConstraint((it, grammar) =>
					new HasInnerExceptionValueConstraint<TInnerException>(
						"with", it)),
			this);

	/// <summary>
	///     Verifies that the thrown exception has an inner exception of type <paramref name="innerExceptionType" /> which
	///     satisfies the <paramref name="expectations" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> WithInner(
		Type innerExceptionType,
		Action<IThat<Exception?>> expectations)
		=> new(ExpectationBuilder
				.ForMember<Exception, Exception?>(e => e.InnerException,
					$"with an inner {(innerExceptionType == typeof(Exception) ? ExceptionString : Formatter.Format(innerExceptionType))} whose",
					false)
				.Validate(it
					=> new InnerExceptionIsTypeConstraint(it,
						innerExceptionType))
				.AddExpectations(e => expectations(new ThatSubject<Exception?>(e)), ExpectationGrammars.Nested),
			this);

	/// <summary>
	///     Verifies that the actual exception has an inner exception of type <paramref name="innerExceptionType" />.
	/// </summary>
	public AndOrResult<TException, ThatDelegateThrows<TException>> WithInner(
		Type innerExceptionType)
		=> new(ExpectationBuilder
				.AddConstraint((it, grammar) =>
					new HasInnerExceptionValueConstraint(innerExceptionType,
						"with", it)),
			this);


	internal class InnerExceptionIsTypeConstraint<TInnerException>(string it)
		: IValueConstraint<Exception?>
		where TInnerException : Exception?
	{
		#region IValueConstraint<Exception?> Members

		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? actual)
		{
			if (actual?.InnerException is TInnerException)
			{
				return new ConstraintResult.Success<Exception?>(actual, "");
			}

			return new ConstraintResult.Failure<Exception?>(actual, "",
				$"{it} was {ThatDelegate.FormatForMessage(actual?.InnerException)}");
		}

		#endregion
	}

	internal class InnerExceptionIsTypeConstraint(string it, Type exceptionType)
		: IValueConstraint<Exception?>
	{
		#region IValueConstraint<Exception?> Members

		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? actual)
		{
			if (exceptionType.IsAssignableFrom(actual?.InnerException?.GetType()))
			{
				return new ConstraintResult.Success<Exception>(actual, "");
			}

			return new ConstraintResult.Failure<Exception?>(actual, "",
				$"{it} was {ThatDelegate.FormatForMessage(actual)}");
		}

		#endregion
	}

	internal readonly struct HasInnerExceptionValueConstraint<TInnerException>(
		string verb,
		string it)
		: IValueConstraint<Exception?>
		where TInnerException : Exception?
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? actual)
		{
			Exception? innerException = actual?.InnerException;
			if (actual?.InnerException is TInnerException)
			{
				return new ConstraintResult.Success<Exception?>(actual, ToString());
			}

			if (innerException is not null)
			{
				return new ConstraintResult.Failure<Exception?>(actual, ToString(),
					$"{it} was {ThatDelegate.FormatForMessage(innerException)}");
			}

			return new ConstraintResult.Failure<Exception?>(actual, ToString(),
				$"{it} was <null>");
		}

		public override string ToString()
			=> $"{verb} an inner {(typeof(TInnerException) == typeof(Exception) ? ExceptionString : Formatter.Format(typeof(TInnerException)))}";
	}

	internal readonly struct HasInnerExceptionValueConstraint(
		Type innerExceptionType,
		string verb,
		string it)
		: IValueConstraint<Exception?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(Exception? actual)
		{
			Exception? innerException = actual?.InnerException;
			if (innerExceptionType.IsAssignableFrom(actual?.InnerException?.GetType()))
			{
				return new ConstraintResult.Success<Exception?>(actual, ToString());
			}

			if (innerException is not null)
			{
				return new ConstraintResult.Failure<Exception?>(actual, ToString(),
					$"{it} was {ThatDelegate.FormatForMessage(innerException)}");
			}

			return new ConstraintResult.Failure<Exception?>(actual, ToString(),
				$"{it} was <null>");
		}

		public override string ToString()
			=> $"{verb} an inner {(innerExceptionType == typeof(Exception) ? ExceptionString : Formatter.Format(innerExceptionType))}";
	}
}
