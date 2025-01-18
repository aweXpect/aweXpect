using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	private const string ExpectIsNegative = "be negative";

	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<sbyte, IExpectSubject<sbyte>> IsNegative(
		this IExpectSubject<sbyte> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<short, IExpectSubject<short>> IsNegative(
		this IExpectSubject<short> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<int, IExpectSubject<int>> IsNegative(
		this IExpectSubject<int> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<long, IExpectSubject<long>> IsNegative(
		this IExpectSubject<long> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<float, IExpectSubject<float>> IsNegative(
		this IExpectSubject<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<double, IExpectSubject<double>> IsNegative(
		this IExpectSubject<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<decimal, IExpectSubject<decimal>> IsNegative(
		this IExpectSubject<decimal> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<sbyte?, IExpectSubject<sbyte?>> IsNegative(
		this IExpectSubject<sbyte?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<short?, IExpectSubject<short?>> IsNegative(
		this IExpectSubject<short?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<int?, IExpectSubject<int?>> IsNegative(
		this IExpectSubject<int?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<long?, IExpectSubject<long?>> IsNegative(
		this IExpectSubject<long?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<float?, IExpectSubject<float?>> IsNegative(
		this IExpectSubject<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<double?, IExpectSubject<double?>> IsNegative(
		this IExpectSubject<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<decimal?, IExpectSubject<decimal?>> IsNegative(
		this IExpectSubject<decimal?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<decimal>(
					it,
					0,
					_ => ExpectIsNegative,
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
