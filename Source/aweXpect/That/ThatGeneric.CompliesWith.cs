using System;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatGeneric
{
	/// <summary>
	///     Verifies that the actual value complies with the <paramref name="expectations" />.
	/// </summary>
	public static AndOrResult<T, IThat<T>> CompliesWith<T>(this IThat<T> source,
		Action<IThat<T>> expectations)
		=> new(source.ThatIs().ExpectationBuilder
				.AddConstraint((it, grammar) =>
					new ComplyWithConstraint<T>(it, expectations)),
			source);

	private readonly struct ComplyWithConstraint<T> : IAsyncContextConstraint<T>
	{
		private readonly string _it;
		private readonly ManualExpectationBuilder<T> _itemExpectationBuilder;

		public ComplyWithConstraint(string it,
			Action<IThat<T>> expectations)
		{
			_it = it;
			_itemExpectationBuilder = new ManualExpectationBuilder<T>();
			expectations.Invoke(new ThatSubject<T>(_itemExpectationBuilder));
		}

		public async Task<ConstraintResult> IsMetBy(
			T actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			ConstraintResult isMatch = await _itemExpectationBuilder.IsMetBy(actual, context, cancellationToken);
			if (isMatch.Outcome == Outcome.Success)
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{_it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> $"{_itemExpectationBuilder}";
	}
}
