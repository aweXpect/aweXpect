using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	private const string ExpectIsPositive = "is positive";

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsPositive(
		this IThat<sbyte> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<sbyte>(
					it,
					0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsPositive(
		this IThat<short> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<short>(
					it,
					0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsPositive(
		this IThat<int> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<int>(
					it,
					0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsPositive(
		this IThat<long> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<long>(
					it,
					0L,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsPositive(
		this IThat<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<float>(
					it,
					0.0F,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsPositive(
		this IThat<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<double>(
					it,
					0.0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsPositive(
		this IThat<decimal> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<decimal>(
					it,
					0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsPositive(
		this IThat<sbyte?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<sbyte>(
					it,
					0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsPositive(
		this IThat<short?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<short>(
					it,
					0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsPositive(
		this IThat<int?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<int>(
					it,
					0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsPositive(
		this IThat<long?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<long>(
					it,
					0L,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsPositive(
		this IThat<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<float>(
					it,
					0.0F,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsPositive(
		this IThat<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<double>(
					it,
					0.0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsPositive(
		this IThat<decimal?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<decimal>(
					it,
					0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
