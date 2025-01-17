using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	private const string ExpectIsInfinite = "be infinite";
	private const string ExpectIsNotInfinite = "not be infinite";

	/// <summary>
	///     Verifies that the subject is seen as infinite (<see cref="float.IsInfinity" />).
	/// </summary>
	public static AndOrResult<float, IExpectSubject<float>> IsInfinite(this IExpectSubject<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectIsInfinite,
					(a, _) => float.IsInfinity(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as infinite (<see cref="double.IsInfinity" />).
	/// </summary>
	public static AndOrResult<double, IExpectSubject<double>> IsInfinite(
		this IExpectSubject<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectIsInfinite,
					(a, _) => double.IsInfinity(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as infinite (not <see langword="null" /> and <see cref="float.IsInfinity" />).
	/// </summary>
	public static AndOrResult<float?, IExpectSubject<float?>> IsInfinite(this IExpectSubject<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectIsInfinite,
					(a, _) => a != null && float.IsInfinity(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as infinite (not <see langword="null" /> and <see cref="double.IsInfinity" />).
	/// </summary>
	public static AndOrResult<double?, IExpectSubject<double?>> IsInfinite(
		this IExpectSubject<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectIsInfinite,
					(a, _) => a != null && double.IsInfinity(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as infinite (not <see cref="float.IsInfinity" />).
	/// </summary>
	public static AndOrResult<float, IExpectSubject<float>> IsNotInfinite(
		this IExpectSubject<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectIsNotInfinite,
					(a, _) => !float.IsInfinity(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as infinite (not <see cref="double.IsInfinity" />).
	/// </summary>
	public static AndOrResult<double, IExpectSubject<double>> IsNotInfinite(
		this IExpectSubject<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectIsNotInfinite,
					(a, _) => !double.IsInfinity(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as infinite (<see langword="null" /> or not <see cref="float.IsInfinity" />).
	/// </summary>
	public static AndOrResult<float?, IExpectSubject<float?>> IsNotInfinite(
		this IExpectSubject<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectIsNotInfinite,
					(a, _) => a == null || !float.IsInfinity(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as infinite (<see langword="null" /> or not <see cref="double.IsInfinity" />
	///     ).
	/// </summary>
	public static AndOrResult<double?, IExpectSubject<double?>> IsNotInfinite(
		this IExpectSubject<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectIsNotInfinite,
					(a, _) => a == null || !double.IsInfinity(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
