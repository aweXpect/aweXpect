using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect.Tests.TestHelpers;

public static class MyConstraintExtensions
{
	public static AndOrResult<bool, IExpectSubject<bool>> IsMyConstraint(this IExpectSubject<bool> subject,
		string expectation,
		Func<bool, bool> isSuccess, string failureMessage)
		=> new(subject.Should(_ => { }).ExpectationBuilder.AddConstraint(_
				=> new MyConstraint(expectation, isSuccess, failureMessage)),
			subject);

	private readonly struct MyConstraint(
		string expectation,
		Func<bool, bool> isSuccess,
		string failureMessage)
		: IValueConstraint<bool>
	{
		#region IValueConstraint<bool> Members

		/// <inheritdoc />
		public ConstraintResult IsMetBy(bool actual)
		{
			if (isSuccess(actual))
			{
				return new ConstraintResult.Success<bool>(actual, expectation);
			}

			return new ConstraintResult.Failure<bool>(actual, expectation, failureMessage);
		}

		#endregion
	}
}
