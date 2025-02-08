using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.Sources;
using aweXpect.Results;

namespace aweXpect.Delegates;

public abstract partial class ThatDelegate
{
	/// <summary>
	///     A delegate without value.
	/// </summary>
	public sealed class WithoutValue(ExpectationBuilder expectationBuilder)
		: ThatDelegate(expectationBuilder),
			IThat<WithoutValue>,
			IThatDoes<WithoutValue>,
			IThatHas<WithoutValue>,
			IThatIs<WithoutValue>
	{
		
		/// <summary>
		///     Verifies that the delegate does not throw any exception.
		/// </summary>
		public ExpectationResult DoesNotThrow()
			=> new(ExpectationBuilder.AddConstraint((_,_) => new DoesNotThrowConstraint()));
		private readonly struct DoesNotThrowConstraint : IValueConstraint<DelegateValue>
		{
			public ConstraintResult IsMetBy(DelegateValue actual)
			{
				if (actual.IsNull)
				{
					return new ConstraintResult.Failure(ToString(), ItWasNull);
				}

				if (actual.Exception is { } exception)
				{
					return new ConstraintResult.Failure(ToString(),
						$"it did throw {FormatForMessage(exception)}");
				}

				return new ConstraintResult.Success(ToString());
			}

			public override string ToString()
				=> DoesNotThrowExpectation;
		}
	}
}
