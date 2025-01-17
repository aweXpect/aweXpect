using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	private const string ExpectIsNaN = "be NaN";
	private const string ExpectIsNotNaN = "not be NaN";

	/// <summary>
	///     Verifies that the subject is seen as not a number (<see cref="float.NaN" />).
	/// </summary>
	public static AndOrResult<float, IExpectSubject<float>> IsNaN(this IExpectSubject<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<double, IExpectSubject<double>> IsNaN(this IExpectSubject<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<float?, IExpectSubject<float?>> IsNaN(this IExpectSubject<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<double?, IExpectSubject<double?>> IsNaN(this IExpectSubject<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<float, IExpectSubject<float>> IsNotNaN(this IExpectSubject<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<double, IExpectSubject<double>> IsNotNaN(this IExpectSubject<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<float?, IExpectSubject<float?>> IsNotNaN(this IExpectSubject<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<double?, IExpectSubject<double?>> IsNotNaN(this IExpectSubject<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<double>(
					it,
					double.NaN,
					_ => ExpectIsNotNaN,
					(a, _) => a == null || !double.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
