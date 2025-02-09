using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	private const string ExpectIsNegative = "is negative";

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsNegative(
		this IThat<sbyte> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<sbyte>(
					it,
					0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsNegative(
		this IThat<short> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<short>(
					it,
					0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsNegative(
		this IThat<int> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<int>(
					it,
					0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsNegative(
		this IThat<long> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<long>(
					it,
					0L,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsNegative(
		this IThat<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<float>(
					it,
					0.0F,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsNegative(
		this IThat<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<double>(
					it,
					0.0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsNegative(
		this IThat<decimal> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<decimal>(
					it,
					0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsNegative(
		this IThat<sbyte?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<sbyte>(
					it,
					0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsNegative(
		this IThat<short?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<short>(
					it,
					0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsNegative(
		this IThat<int?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<int>(
					it,
					0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsNegative(
		this IThat<long?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<long>(
					it,
					0L,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsNegative(
		this IThat<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<float>(
					it,
					0.0F,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsNegative(
		this IThat<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<double>(
					it,
					0.0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsNegative(
		this IThat<decimal?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<decimal>(
					it,
					0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
