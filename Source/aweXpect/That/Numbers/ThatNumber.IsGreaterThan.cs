using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IThat<byte>> IsGreaterThan(
		this IThat<byte> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<byte>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsGreaterThan(
		this IThat<sbyte> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<sbyte>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsGreaterThan(
		this IThat<short> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<short>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IThat<ushort>> IsGreaterThan(
		this IThat<ushort> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<ushort>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsGreaterThan(
		this IThat<int> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<int>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IThat<uint>> IsGreaterThan(
		this IThat<uint> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<uint>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsGreaterThan(
		this IThat<long> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<long>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IThat<ulong>> IsGreaterThan(
		this IThat<ulong> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<ulong>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsGreaterThan(
		this IThat<float> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<float>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsGreaterThan(
		this IThat<double> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<double>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsGreaterThan(
		this IThat<decimal> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<decimal>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IThat<byte?>> IsGreaterThan(
		this IThat<byte?> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<byte>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsGreaterThan(
		this IThat<sbyte?> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<sbyte>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsGreaterThan(
		this IThat<short?> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<short>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IThat<ushort?>> IsGreaterThan(
		this IThat<ushort?> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<ushort>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsGreaterThan(
		this IThat<int?> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<int>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IThat<uint?>> IsGreaterThan(
		this IThat<uint?> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<uint>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsGreaterThan(
		this IThat<long?> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<long>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IThat<ulong?>> IsGreaterThan(
		this IThat<ulong?> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<ulong>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsGreaterThan(
		this IThat<float?> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<float>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsGreaterThan(
		this IThat<double?> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<double>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsGreaterThan(
		this IThat<decimal?> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<decimal>(
					it,
					grammars,
					expected,
					e => $"is greater than {Formatter.Format(e)}",
					(a, e) => a > e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
