using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumberShould
{
	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IThatShould<byte>> BeLessThanOrEqualTo(
		this IThatShould<byte> source,
		byte? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<byte>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IThatShould<sbyte>> BeLessThanOrEqualTo(
		this IThatShould<sbyte> source,
		sbyte? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<sbyte>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IThatShould<short>> BeLessThanOrEqualTo(
		this IThatShould<short> source,
		short? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<short>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IThatShould<ushort>> BeLessThanOrEqualTo(
		this IThatShould<ushort> source,
		ushort? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<ushort>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IThatShould<int>> BeLessThanOrEqualTo(
		this IThatShould<int> source,
		int? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<int>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IThatShould<uint>> BeLessThanOrEqualTo(
		this IThatShould<uint> source,
		uint? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<uint>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IThatShould<long>> BeLessThanOrEqualTo(
		this IThatShould<long> source,
		long? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<long>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IThatShould<ulong>> BeLessThanOrEqualTo(
		this IThatShould<ulong> source,
		ulong? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<ulong>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IThatShould<float>> BeLessThanOrEqualTo(
		this IThatShould<float> source,
		float? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<float>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IThatShould<double>> BeLessThanOrEqualTo(
		this IThatShould<double> source,
		double? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<double>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IThatShould<decimal>> BeLessThanOrEqualTo(
		this IThatShould<decimal> source,
		decimal? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<decimal>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IThatShould<byte?>> BeLessThanOrEqualTo(
		this IThatShould<byte?> source,
		byte? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<byte>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IThatShould<sbyte?>> BeLessThanOrEqualTo(
		this IThatShould<sbyte?> source,
		sbyte? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<sbyte>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IThatShould<short?>> BeLessThanOrEqualTo(
		this IThatShould<short?> source,
		short? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<short>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IThatShould<ushort?>> BeLessThanOrEqualTo(
		this IThatShould<ushort?> source,
		ushort? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<ushort>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IThatShould<int?>> BeLessThanOrEqualTo(
		this IThatShould<int?> source,
		int? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<int>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IThatShould<uint?>> BeLessThanOrEqualTo(
		this IThatShould<uint?> source,
		uint? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<uint>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IThatShould<long?>> BeLessThanOrEqualTo(
		this IThatShould<long?> source,
		long? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<long>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IThatShould<ulong?>> BeLessThanOrEqualTo(
		this IThatShould<ulong?> source,
		ulong? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<ulong>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IThatShould<float?>> BeLessThanOrEqualTo(
		this IThatShould<float?> source,
		float? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<float>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IThatShould<double?>> BeLessThanOrEqualTo(
		this IThatShould<double?> source,
		double? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<double>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IThatShould<decimal?>> BeLessThanOrEqualTo(
		this IThatShould<decimal?> source,
		decimal? expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<decimal>(
					it,
					expected,
					e => $"be less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
