using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNumberShould
{
	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IThat<byte>> BeLessThanOrEqualTo(
		this IThat<byte> source,
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
	public static AndOrResult<sbyte, IThat<sbyte>> BeLessThanOrEqualTo(
		this IThat<sbyte> source,
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
	public static AndOrResult<short, IThat<short>> BeLessThanOrEqualTo(
		this IThat<short> source,
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
	public static AndOrResult<ushort, IThat<ushort>> BeLessThanOrEqualTo(
		this IThat<ushort> source,
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
	public static AndOrResult<int, IThat<int>> BeLessThanOrEqualTo(
		this IThat<int> source,
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
	public static AndOrResult<uint, IThat<uint>> BeLessThanOrEqualTo(
		this IThat<uint> source,
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
	public static AndOrResult<long, IThat<long>> BeLessThanOrEqualTo(
		this IThat<long> source,
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
	public static AndOrResult<ulong, IThat<ulong>> BeLessThanOrEqualTo(
		this IThat<ulong> source,
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
	public static AndOrResult<float, IThat<float>> BeLessThanOrEqualTo(
		this IThat<float> source,
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
	public static AndOrResult<double, IThat<double>> BeLessThanOrEqualTo(
		this IThat<double> source,
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
	public static AndOrResult<decimal, IThat<decimal>> BeLessThanOrEqualTo(
		this IThat<decimal> source,
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
	public static AndOrResult<byte?, IThat<byte?>> BeLessThanOrEqualTo(
		this IThat<byte?> source,
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
	public static AndOrResult<sbyte?, IThat<sbyte?>> BeLessThanOrEqualTo(
		this IThat<sbyte?> source,
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
	public static AndOrResult<short?, IThat<short?>> BeLessThanOrEqualTo(
		this IThat<short?> source,
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
	public static AndOrResult<ushort?, IThat<ushort?>> BeLessThanOrEqualTo(
		this IThat<ushort?> source,
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
	public static AndOrResult<int?, IThat<int?>> BeLessThanOrEqualTo(
		this IThat<int?> source,
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
	public static AndOrResult<uint?, IThat<uint?>> BeLessThanOrEqualTo(
		this IThat<uint?> source,
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
	public static AndOrResult<long?, IThat<long?>> BeLessThanOrEqualTo(
		this IThat<long?> source,
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
	public static AndOrResult<ulong?, IThat<ulong?>> BeLessThanOrEqualTo(
		this IThat<ulong?> source,
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
	public static AndOrResult<float?, IThat<float?>> BeLessThanOrEqualTo(
		this IThat<float?> source,
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
	public static AndOrResult<double?, IThat<double?>> BeLessThanOrEqualTo(
		this IThat<double?> source,
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
	public static AndOrResult<decimal?, IThat<decimal?>> BeLessThanOrEqualTo(
		this IThat<decimal?> source,
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
