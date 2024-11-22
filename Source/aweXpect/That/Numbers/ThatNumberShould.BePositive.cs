using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNumberShould
{
	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> BePositive(
		this IThat<sbyte> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<sbyte>(
					it,
					0,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<short, IThat<short>> BePositive(
		this IThat<short> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<short>(
					it,
					0,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<int, IThat<int>> BePositive(
		this IThat<int> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<int>(
					it,
					0,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<long, IThat<long>> BePositive(
		this IThat<long> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<long>(
					it,
					0L,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<float, IThat<float>> BePositive(
		this IThat<float> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<float>(
					it,
					0.0F,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<double, IThat<double>> BePositive(
		this IThat<double> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<double>(
					it,
					0.0,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> BePositive(
		this IThat<decimal> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<decimal>(
					it,
					0,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> BePositive(
		this IThat<sbyte?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<sbyte>(
					it,
					0,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> BePositive(
		this IThat<short?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<short>(
					it,
					0,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> BePositive(
		this IThat<int?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<int>(
					it,
					0,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> BePositive(
		this IThat<long?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<long>(
					it,
					0L,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> BePositive(
		this IThat<float?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<float>(
					it,
					0.0F,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> BePositive(
		this IThat<double?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<double>(
					it,
					0.0,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> BePositive(
		this IThat<decimal?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<decimal>(
					it,
					0,
					_ => "be positive",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
