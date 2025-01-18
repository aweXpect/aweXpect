using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IExpectSubject<byte>> IsLessThan(
		this IExpectSubject<byte> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<byte>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IExpectSubject<sbyte>> IsLessThan(
		this IExpectSubject<sbyte> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<sbyte>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IExpectSubject<short>> IsLessThan(
		this IExpectSubject<short> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<short>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IExpectSubject<ushort>> IsLessThan(
		this IExpectSubject<ushort> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<ushort>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IExpectSubject<int>> IsLessThan(
		this IExpectSubject<int> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<int>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IExpectSubject<uint>> IsLessThan(
		this IExpectSubject<uint> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<uint>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IExpectSubject<long>> IsLessThan(
		this IExpectSubject<long> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<long>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IExpectSubject<ulong>> IsLessThan(
		this IExpectSubject<ulong> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<ulong>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IExpectSubject<float>> IsLessThan(
		this IExpectSubject<float> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<float>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IExpectSubject<double>> IsLessThan(
		this IExpectSubject<double> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<double>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IExpectSubject<decimal>> IsLessThan(
		this IExpectSubject<decimal> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<decimal>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IExpectSubject<byte?>> IsLessThan(
		this IExpectSubject<byte?> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<byte>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IExpectSubject<sbyte?>> IsLessThan(
		this IExpectSubject<sbyte?> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<sbyte>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IExpectSubject<short?>> IsLessThan(
		this IExpectSubject<short?> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<short>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IExpectSubject<ushort?>> IsLessThan(
		this IExpectSubject<ushort?> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<ushort>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IExpectSubject<int?>> IsLessThan(
		this IExpectSubject<int?> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<int>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IExpectSubject<uint?>> IsLessThan(
		this IExpectSubject<uint?> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<uint>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IExpectSubject<long?>> IsLessThan(
		this IExpectSubject<long?> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<long>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IExpectSubject<ulong?>> IsLessThan(
		this IExpectSubject<ulong?> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<ulong>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IExpectSubject<float?>> IsLessThan(
		this IExpectSubject<float?> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<float>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IExpectSubject<double?>> IsLessThan(
		this IExpectSubject<double?> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<double>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IExpectSubject<decimal?>> IsLessThan(
		this IExpectSubject<decimal?> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<decimal>(
					it,
					expected,
					e => $"be less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
