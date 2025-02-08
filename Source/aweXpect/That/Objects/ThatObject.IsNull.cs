using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<T?, IThat<T?>> IsNull<T>(
		this IThat<T?> source)
		where T : class
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<T?>(
					it,
					null,
					"is null",
					(a, _) => a is null,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is <see langword="null" />.
	/// </summary>
	public static AndOrResult<T?, IThat<T?>> IsNull<T>(
		this IThat<T?> source)
		where T : struct
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<T?>(
					it,
					null,
					"is null",
					(a, _) => a is null,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<T, IThat<T?>> IsNotNull<T>(
		this IThat<T?> source)
		where T : class
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<T?>(
					it,
					null,
					"is not null",
					(a, _) => a is not null,
					(_, _, i) => $"{i} was")),
			source);

	/// <summary>
	///     Verifies that the subject is not <see langword="null" />.
	/// </summary>
	public static AndOrResult<T, IThat<T?>> IsNotNull<T>(
		this IThat<T?> source)
		where T : struct
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<T?>(
					it,
					null,
					"is not null",
					(a, _) => a is not null,
					(_, _, i) => $"{i} was")),
			source);
}
