using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IThat<byte>> IsGreaterThanOrEqualTo(
		this IThat<byte> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<byte>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsGreaterThanOrEqualTo(
		this IThat<sbyte> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<sbyte>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsGreaterThanOrEqualTo(
		this IThat<short> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<short>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IThat<ushort>> IsGreaterThanOrEqualTo(
		this IThat<ushort> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<ushort>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsGreaterThanOrEqualTo(
		this IThat<int> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<int>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IThat<uint>> IsGreaterThanOrEqualTo(
		this IThat<uint> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<uint>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsGreaterThanOrEqualTo(
		this IThat<long> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<long>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IThat<ulong>> IsGreaterThanOrEqualTo(
		this IThat<ulong> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<ulong>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsGreaterThanOrEqualTo(
		this IThat<float> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<float>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsGreaterThanOrEqualTo(
		this IThat<double> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<double>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsGreaterThanOrEqualTo(
		this IThat<decimal> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new GenericConstraint<decimal>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IThat<byte?>> IsGreaterThanOrEqualTo(
		this IThat<byte?> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<byte>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsGreaterThanOrEqualTo(
		this IThat<sbyte?> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<sbyte>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsGreaterThanOrEqualTo(
		this IThat<short?> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<short>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IThat<ushort?>> IsGreaterThanOrEqualTo(
		this IThat<ushort?> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<ushort>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsGreaterThanOrEqualTo(
		this IThat<int?> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<int>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IThat<uint?>> IsGreaterThanOrEqualTo(
		this IThat<uint?> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<uint>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsGreaterThanOrEqualTo(
		this IThat<long?> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<long>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IThat<ulong?>> IsGreaterThanOrEqualTo(
		this IThat<ulong?> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<ulong>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsGreaterThanOrEqualTo(
		this IThat<float?> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<float>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsGreaterThanOrEqualTo(
		this IThat<double?> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<double>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsGreaterThanOrEqualTo(
		this IThat<decimal?> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new NullableGenericConstraint<decimal>(
					it,
					expected,
					e => $"is greater than or equal to {Formatter.Format(e)}",
					(a, e) => a >= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
