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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<sbyte>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<short>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<int>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<long>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<float>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<double>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<decimal>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<sbyte>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<short>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<int>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<long>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<float>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<double>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<decimal>(
					it,
					grammars,
					0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
