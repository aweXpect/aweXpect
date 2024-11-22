using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNumberShould
{
	/// <summary>
	///     Verifies that the subject is seen as finite (neither <see cref="float.IsInfinity" /> nor <see cref="float.IsNaN" />
	///     ).
	/// </summary>
	public static AndOrResult<float, IThat<float>> BeFinite(
		this IThat<float> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => "be finite",
					(a, _) => !float.IsInfinity(a) && !float.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as finite (neither <see cref="double.IsInfinity" /> nor
	///     <see cref="double.IsNaN" />).
	/// </summary>
	public static AndOrResult<double, IThat<double>> BeFinite(
		this IThat<double> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => "be finite",
					(a, _) => !double.IsInfinity(a) && !double.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as finite (neither <see cref="float.IsInfinity" /> nor
	///     <see cref="float.IsNaN" /> nor <see langword="null" />).
	/// </summary>
	public static AndOrResult<float, IThat<float?>> BeFinite(
		this IThat<float?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => "be finite",
					(a, _) => a != null && !float.IsInfinity(a.Value) && !float.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is seen as finite (neither <see cref="double.IsInfinity" /> nor
	///     <see cref="double.IsNaN" /> nor <see langword="null" />).
	/// </summary>
	public static AndOrResult<double, IThat<double?>> BeFinite(
		this IThat<double?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => "be finite",
					(a, _) => a != null && !double.IsInfinity(a.Value) && !double.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as finite (either <see cref="float.IsInfinity" /> or
	///     <see cref="float.IsNaN" />).
	/// </summary>
	public static AndOrResult<float, IThat<float>> NotBeFinite(
		this IThat<float> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => "not be finite",
					(a, _) => float.IsInfinity(a) || float.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as finite (either <see cref="double.IsInfinity" /> or
	///     <see cref="double.IsNaN" />).
	/// </summary>
	public static AndOrResult<double, IThat<double>> NotBeFinite(
		this IThat<double> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => "not be finite",
					(a, _) => double.IsInfinity(a) || double.IsNaN(a),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as finite (either <see cref="float.IsInfinity" /> or
	///     <see cref="float.IsNaN" /> or <see langword="null" />).
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> NotBeFinite(
		this IThat<float?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<float>(
					it,
					float.PositiveInfinity,
					_ => "not be finite",
					(a, _) => a == null || float.IsInfinity(a.Value) || float.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is not seen as finite (either <see cref="double.IsInfinity" /> or
	///     <see cref="double.IsNaN" /> or <see langword="null" />).
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> NotBeFinite(
		this IThat<double?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<double>(
					it,
					double.PositiveInfinity,
					_ => "not be finite",
					(a, _) => a == null || double.IsInfinity(a.Value) || double.IsNaN(a.Value),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
