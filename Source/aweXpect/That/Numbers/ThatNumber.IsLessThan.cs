﻿using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IThat<byte>> IsLessThan(
		this IThat<byte> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<byte>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsLessThan(
		this IThat<sbyte> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<sbyte>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsLessThan(
		this IThat<short> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<short>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IThat<ushort>> IsLessThan(
		this IThat<ushort> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<ushort>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsLessThan(
		this IThat<int> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<int>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IThat<uint>> IsLessThan(
		this IThat<uint> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<uint>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsLessThan(
		this IThat<long> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<long>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IThat<ulong>> IsLessThan(
		this IThat<ulong> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<ulong>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsLessThan(
		this IThat<float> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<float>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsLessThan(
		this IThat<double> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<double>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsLessThan(
		this IThat<decimal> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new GenericConstraint<decimal>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IThat<byte?>> IsLessThan(
		this IThat<byte?> source,
		byte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<byte>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsLessThan(
		this IThat<sbyte?> source,
		sbyte? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<sbyte>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsLessThan(
		this IThat<short?> source,
		short? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<short>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IThat<ushort?>> IsLessThan(
		this IThat<ushort?> source,
		ushort? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<ushort>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsLessThan(
		this IThat<int?> source,
		int? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<int>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IThat<uint?>> IsLessThan(
		this IThat<uint?> source,
		uint? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<uint>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsLessThan(
		this IThat<long?> source,
		long? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<long>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IThat<ulong?>> IsLessThan(
		this IThat<ulong?> source,
		ulong? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<ulong>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsLessThan(
		this IThat<float?> source,
		float? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<float>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsLessThan(
		this IThat<double?> source,
		double? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<double>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsLessThan(
		this IThat<decimal?> source,
		decimal? expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new NullableGenericConstraint<decimal>(
					it,
					expected,
					e => $"is less than {Formatter.Format(e)}",
					(a, e) => a < e,
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source);
}
