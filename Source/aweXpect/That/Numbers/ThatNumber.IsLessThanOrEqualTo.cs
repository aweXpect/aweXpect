using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IThat<byte>> IsLessThanOrEqualTo(
		this IThat<byte> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<byte>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsLessThanOrEqualTo(
		this IThat<sbyte> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<sbyte>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsLessThanOrEqualTo(
		this IThat<short> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<short>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IThat<ushort>> IsLessThanOrEqualTo(
		this IThat<ushort> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<ushort>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsLessThanOrEqualTo(
		this IThat<int> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<int>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IThat<uint>> IsLessThanOrEqualTo(
		this IThat<uint> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<uint>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsLessThanOrEqualTo(
		this IThat<long> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<long>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IThat<ulong>> IsLessThanOrEqualTo(
		this IThat<ulong> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<ulong>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsLessThanOrEqualTo(
		this IThat<float> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<float>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsLessThanOrEqualTo(
		this IThat<double> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<double>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsLessThanOrEqualTo(
		this IThat<decimal> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<decimal>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IThat<byte?>> IsLessThanOrEqualTo(
		this IThat<byte?> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<byte>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsLessThanOrEqualTo(
		this IThat<sbyte?> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<sbyte>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsLessThanOrEqualTo(
		this IThat<short?> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<short>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IThat<ushort?>> IsLessThanOrEqualTo(
		this IThat<ushort?> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<ushort>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsLessThanOrEqualTo(
		this IThat<int?> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<int>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IThat<uint?>> IsLessThanOrEqualTo(
		this IThat<uint?> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<uint>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsLessThanOrEqualTo(
		this IThat<long?> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<long>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IThat<ulong?>> IsLessThanOrEqualTo(
		this IThat<ulong?> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<ulong>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsLessThanOrEqualTo(
		this IThat<float?> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<float>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsLessThanOrEqualTo(
		this IThat<double?> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<double>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsLessThanOrEqualTo(
		this IThat<decimal?> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<decimal>(
					it,
					grammars,
					expected,
					e => $"is less than or equal to {Formatter.Format(e)}",
					(a, e) => a <= e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
