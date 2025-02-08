using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	private const string ExpectIsInfinite = "is infinite";
	private const string ExpectIsNotInfinite = "is not infinite";

	/// <summary>
	///     Verifies that the subject is seen as infinite (<see cref="float.IsInfinity" />).
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsInfinite(this IThat<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
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
	public static AndOrResult<double, IThat<double>> IsInfinite(
		this IThat<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
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
	public static AndOrResult<float?, IThat<float?>> IsInfinite(this IThat<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
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
	public static AndOrResult<double?, IThat<double?>> IsInfinite(
		this IThat<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
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
	public static AndOrResult<float, IThat<float>> IsNotInfinite(
		this IThat<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
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
	public static AndOrResult<double, IThat<double>> IsNotInfinite(
		this IThat<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
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
	public static AndOrResult<float?, IThat<float?>> IsNotInfinite(
		this IThat<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
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
	public static AndOrResult<double?, IThat<double?>> IsNotInfinite(
		this IThat<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectIsNotInfinite,
					(a, _) => a == null || !double.IsInfinity(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
