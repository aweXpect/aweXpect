using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IExpectSubject<byte>> IsGreaterThan(
		this IExpectSubject<byte> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<byte>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IExpectSubject<sbyte>> IsGreaterThan(
		this IExpectSubject<sbyte> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<sbyte>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IExpectSubject<short>> IsGreaterThan(
		this IExpectSubject<short> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<short>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IExpectSubject<ushort>> IsGreaterThan(
		this IExpectSubject<ushort> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<ushort>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IExpectSubject<int>> IsGreaterThan(
		this IExpectSubject<int> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<int>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IExpectSubject<uint>> IsGreaterThan(
		this IExpectSubject<uint> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<uint>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IExpectSubject<long>> IsGreaterThan(
		this IExpectSubject<long> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<long>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IExpectSubject<ulong>> IsGreaterThan(
		this IExpectSubject<ulong> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<ulong>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IExpectSubject<float>> IsGreaterThan(
		this IExpectSubject<float> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<float>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IExpectSubject<double>> IsGreaterThan(
		this IExpectSubject<double> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<double>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IExpectSubject<decimal>> IsGreaterThan(
		this IExpectSubject<decimal> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<decimal>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IExpectSubject<byte?>> IsGreaterThan(
		this IExpectSubject<byte?> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<byte>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IExpectSubject<sbyte?>> IsGreaterThan(
		this IExpectSubject<sbyte?> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<sbyte>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IExpectSubject<short?>> IsGreaterThan(
		this IExpectSubject<short?> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<short>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IExpectSubject<ushort?>> IsGreaterThan(
		this IExpectSubject<ushort?> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<ushort>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IExpectSubject<int?>> IsGreaterThan(
		this IExpectSubject<int?> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<int>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IExpectSubject<uint?>> IsGreaterThan(
		this IExpectSubject<uint?> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<uint>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IExpectSubject<long?>> IsGreaterThan(
		this IExpectSubject<long?> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<long>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IExpectSubject<ulong?>> IsGreaterThan(
		this IExpectSubject<ulong?> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<ulong>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IExpectSubject<float?>> IsGreaterThan(
		this IExpectSubject<float?> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<float>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IExpectSubject<double?>> IsGreaterThan(
		this IExpectSubject<double?> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<double>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IExpectSubject<decimal?>> IsGreaterThan(
		this IExpectSubject<decimal?> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<decimal>(
					it,
					expected,
					e => $"be greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
