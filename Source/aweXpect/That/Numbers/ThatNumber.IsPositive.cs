using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	private const string ExpectIsPositive = "be positive";

	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<sbyte, IExpectSubject<sbyte>> IsPositive(
		this IExpectSubject<sbyte> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<short, IExpectSubject<short>> IsPositive(
		this IExpectSubject<short> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<int, IExpectSubject<int>> IsPositive(
		this IExpectSubject<int> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<long, IExpectSubject<long>> IsPositive(
		this IExpectSubject<long> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<float, IExpectSubject<float>> IsPositive(
		this IExpectSubject<float> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<double, IExpectSubject<double>> IsPositive(
		this IExpectSubject<double> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<decimal, IExpectSubject<decimal>> IsPositive(
		this IExpectSubject<decimal> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<sbyte?, IExpectSubject<sbyte?>> IsPositive(
		this IExpectSubject<sbyte?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<short?, IExpectSubject<short?>> IsPositive(
		this IExpectSubject<short?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<int?, IExpectSubject<int?>> IsPositive(
		this IExpectSubject<int?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<long?, IExpectSubject<long?>> IsPositive(
		this IExpectSubject<long?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<float?, IExpectSubject<float?>> IsPositive(
		this IExpectSubject<float?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<double?, IExpectSubject<double?>> IsPositive(
		this IExpectSubject<double?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
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
	public static AndOrResult<decimal?, IExpectSubject<decimal?>> IsPositive(
		this IExpectSubject<decimal?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<decimal>(
					it,
					0,
					_ => ExpectIsPositive,
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
