using Testably.Expectations.Core;
using Testably.Expectations.Formatting;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatObjectShould
{
	/// <summary>
	///     Verifies that the subject is null.
	/// </summary>
	public static AndOrResult<object?, IThat<object?>> BeNull(
		this IThat<object?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<object?>(
					it,
					null,
					"be null",
					(a, _) => a is null,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not null.
	/// </summary>
	public static AndOrResult<object, IThat<object?>> NotBeNull(
		this IThat<object?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<object?>(
					it,
					null,
					"not be null",
					(a, _) => a is not null,
					(_, _, i) => $"{i} was")),
			source);
}
