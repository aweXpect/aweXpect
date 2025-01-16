using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumberShould
{
	private const string ExpectBeInfinite = "be infinite";
	private const string ExpectNotBeInfinite = "not be infinite";

	/// <summary>
	///     Verifies that the subject is seen as infinite (<see cref="float.IsInfinity" />).
	/// </summary>
	public static AndOrResult<float, IThatShould<float>> BeInfinite(this IThatShould<float> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectBeInfinite,
					(a, _) => float.IsInfinity(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as infinite (<see cref="double.IsInfinity" />).
	/// </summary>
	public static AndOrResult<double, IThatShould<double>> BeInfinite(
		this IThatShould<double> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectBeInfinite,
					(a, _) => double.IsInfinity(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as infinite (not <see langword="null" /> and <see cref="float.IsInfinity" />).
	/// </summary>
	public static AndOrResult<float?, IThatShould<float?>> BeInfinite(this IThatShould<float?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectBeInfinite,
					(a, _) => a != null && float.IsInfinity(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as infinite (not <see langword="null" /> and <see cref="double.IsInfinity" />).
	/// </summary>
	public static AndOrResult<double?, IThatShould<double?>> BeInfinite(
		this IThatShould<double?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectBeInfinite,
					(a, _) => a != null && double.IsInfinity(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as infinite (not <see cref="float.IsInfinity" />).
	/// </summary>
	public static AndOrResult<float, IThatShould<float>> NotBeInfinite(
		this IThatShould<float> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectNotBeInfinite,
					(a, _) => !float.IsInfinity(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as infinite (not <see cref="double.IsInfinity" />).
	/// </summary>
	public static AndOrResult<double, IThatShould<double>> NotBeInfinite(
		this IThatShould<double> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectNotBeInfinite,
					(a, _) => !double.IsInfinity(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as infinite (<see langword="null" /> or not <see cref="float.IsInfinity" />).
	/// </summary>
	public static AndOrResult<float?, IThatShould<float?>> NotBeInfinite(
		this IThatShould<float?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => ExpectNotBeInfinite,
					(a, _) => a == null || !float.IsInfinity(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as infinite (<see langword="null" /> or not <see cref="double.IsInfinity" />
	///     ).
	/// </summary>
	public static AndOrResult<double?, IThatShould<double?>> NotBeInfinite(
		this IThatShould<double?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => ExpectNotBeInfinite,
					(a, _) => a == null || !double.IsInfinity(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
