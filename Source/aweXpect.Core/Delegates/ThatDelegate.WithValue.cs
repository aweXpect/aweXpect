using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Sources;
using aweXpect.Results;

namespace aweXpect.Delegates;

public abstract partial class ThatDelegate
{
	/// <summary>
	///     A delegate with value of type <typeparamref name="TValue" />.
	/// </summary>
	public sealed class WithValue<TValue>(ExpectationBuilder expectationBuilder)
		: ThatDelegate(expectationBuilder),
			IThat<WithValue<TValue>>,
			IThatDoes<WithValue<TValue>>,
			IThatHas<WithValue<TValue>>,
			IThatIs<WithValue<TValue>>
	{
		/// <summary>
		///     Verifies that the delegate does not throw any exception.
		/// </summary>
		public ExpectationResult<TValue> DoesNotThrow()
			=> new(ExpectationBuilder.AddConstraint((_, _) => new DoesNotThrowConstraint()));

		private readonly struct DoesNotThrowConstraint : IValueConstraint<DelegateValue<TValue>>
		{
			public ConstraintResult IsMetBy(DelegateValue<TValue> actual)
			{
				if (actual.IsNull)
				{
					return new ConstraintResult.Failure<TValue?>(default, ToString(), ItWasNull);
				}

				if (actual.Exception is { } exception)
				{
					return new ConstraintResult.Failure<TValue?>(actual.Value, ToString(),
						$"it did throw {FormatForMessage(exception)}");
				}

				return new ConstraintResult.Success<TValue?>(actual.Value, ToString());
			}

			public override string ToString()
				=> DoesNotThrowExpectation;
		}
	}
}
