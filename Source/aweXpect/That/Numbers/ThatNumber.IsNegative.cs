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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<sbyte>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<short>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<int>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<long>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<float>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<double>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<decimal>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<sbyte>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<short>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<int>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<long>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<float>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<double>(
					it,
					grammars,
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
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<decimal>(
					it,
					grammars,
					0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
