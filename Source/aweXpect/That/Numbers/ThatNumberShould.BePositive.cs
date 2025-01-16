using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumberShould
{
	private const string ExpectBePositive = "be positive";

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<sbyte, IThatShould<sbyte>> BePositive(
		this IThatShould<sbyte> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<sbyte>(
					it,
					0,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<short, IThatShould<short>> BePositive(
		this IThatShould<short> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<short>(
					it,
					0,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<int, IThatShould<int>> BePositive(
		this IThatShould<int> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<int>(
					it,
					0,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<long, IThatShould<long>> BePositive(
		this IThatShould<long> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<long>(
					it,
					0L,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<float, IThatShould<float>> BePositive(
		this IThatShould<float> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<float>(
					it,
					0.0F,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<double, IThatShould<double>> BePositive(
		this IThatShould<double> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<double>(
					it,
					0.0,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<decimal, IThatShould<decimal>> BePositive(
		this IThatShould<decimal> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<decimal>(
					it,
					0,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<sbyte?, IThatShould<sbyte?>> BePositive(
		this IThatShould<sbyte?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<sbyte>(
					it,
					0,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<short?, IThatShould<short?>> BePositive(
		this IThatShould<short?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<short>(
					it,
					0,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<int?, IThatShould<int?>> BePositive(
		this IThatShould<int?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<int>(
					it,
					0,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<long?, IThatShould<long?>> BePositive(
		this IThatShould<long?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<long>(
					it,
					0L,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<float?, IThatShould<float?>> BePositive(
		this IThatShould<float?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<float>(
					it,
					0.0F,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<double?, IThatShould<double?>> BePositive(
		this IThatShould<double?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<double>(
					it,
					0.0,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<decimal?, IThatShould<decimal?>> BePositive(
		this IThatShould<decimal?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<decimal>(
					it,
					0,
					_ => ExpectBePositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
