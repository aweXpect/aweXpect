using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	private const string ExpectIsNaN = "is NaN";
	private const string ExpectIsNotNaN = "is not NaN";

	/// <summary>
	///     Verifies that the subject is seen as not a number (<see cref="float.NaN" />).
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsNaN(this IThat<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<float>(
					it,
					float.NaN,
					_ => ExpectIsNaN,
					(a, _) => float.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as not a number (<see cref="double.NaN" />).
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsNaN(this IThat<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<double>(
					it,
					double.NaN,
					_ => ExpectIsNaN,
					(a, _) => double.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as not a number (not <see langword="null" /> and <see cref="float.NaN" />).
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsNaN(this IThat<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<float>(
					it,
					float.NaN,
					_ => ExpectIsNaN,
					(a, _) => a != null && float.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as not a number (not <see langword="null" /> and <see cref="double.NaN" />).
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsNaN(this IThat<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<double>(
					it,
					double.NaN,
					_ => ExpectIsNaN,
					(a, _) => a != null && double.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as not a number (not <see cref="float.NaN" />).
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsNotNaN(this IThat<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<float>(
					it,
					float.NaN,
					_ => ExpectIsNotNaN,
					(a, _) => !float.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as not a number (not <see cref="double.NaN" />).
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsNotNaN(this IThat<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<double>(
					it,
					double.NaN,
					_ => ExpectIsNotNaN,
					(a, _) => !double.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as not a number (<see langword="null" /> or not not <see cref="float.NaN" />
	///     ).
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsNotNaN(this IThat<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<float>(
					it,
					float.NaN,
					_ => ExpectIsNotNaN,
					(a, _) => a == null || !float.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as not a number (<see langword="null" /> or not <see cref="double.NaN" />).
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsNotNaN(this IThat<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<double>(
					it,
					double.NaN,
					_ => ExpectIsNotNaN,
					(a, _) => a == null || !double.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
