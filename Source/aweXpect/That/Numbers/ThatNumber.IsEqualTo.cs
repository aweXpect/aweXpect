using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<byte, IThat<byte>> IsEqualTo(
		this IThat<byte> source,
		byte? expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<byte>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<sbyte, IThat<sbyte>> IsEqualTo(
		this IThat<sbyte> source,
		sbyte? expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<sbyte>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<short, IThat<short>> IsEqualTo(
		this IThat<short> source,
		short? expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<short>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<ushort, IThat<ushort>> IsEqualTo(
		this IThat<ushort> source,
		ushort? expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<ushort>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<int, IThat<int>> IsEqualTo(
		this IThat<int> source,
		int? expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<int>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<uint, IThat<uint>> IsEqualTo(
		this IThat<uint> source,
		uint? expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<uint>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<long, IThat<long>> IsEqualTo(
		this IThat<long> source,
		long? expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<long>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<ulong, IThat<ulong>> IsEqualTo(
		this IThat<ulong> source,
		ulong? expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<ulong>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<float, IThat<float>> IsEqualTo(
		this IThat<float> source,
		float? expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<float>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<double, IThat<double>> IsEqualTo(
		this IThat<double> source,
		double? expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<double>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<decimal, IThat<decimal>> IsEqualTo(
		this IThat<decimal> source,
		decimal? expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<decimal>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IThat<byte?>> IsEqualTo(
		this IThat<byte?> source,
		byte? expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<byte>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> IsEqualTo(
		this IThat<sbyte?> source,
		sbyte? expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<sbyte>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<short, IThat<short?>> IsEqualTo(
		this IThat<short?> source,
		short? expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<short>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> IsEqualTo(
		this IThat<ushort?> source,
		ushort? expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<ushort>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<int, IThat<int?>> IsEqualTo(
		this IThat<int?> source,
		int? expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<int>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IThat<uint?>> IsEqualTo(
		this IThat<uint?> source,
		uint? expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<uint>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<long, IThat<long?>> IsEqualTo(
		this IThat<long?> source,
		long? expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<long>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> IsEqualTo(
		this IThat<ulong?> source,
		ulong? expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<ulong>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<float, IThat<float?>> IsEqualTo(
		this IThat<float?> source,
		float? expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<float>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<double, IThat<double?>> IsEqualTo(
		this IThat<double?> source,
		double? expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<double>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> IsEqualTo(
		this IThat<decimal?> source,
		decimal? expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<decimal>(
					it,
					expected,
					e => $"is equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<byte, IThat<byte>> IsNotEqualTo(
		this IThat<byte> source,
		byte? unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<byte>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<sbyte, IThat<sbyte>> IsNotEqualTo(
		this IThat<sbyte> source,
		sbyte? unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<sbyte>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<short, IThat<short>> IsNotEqualTo(
		this IThat<short> source,
		short? unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<short>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<ushort, IThat<ushort>> IsNotEqualTo(
		this IThat<ushort> source,
		ushort? unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<ushort>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<int, IThat<int>> IsNotEqualTo(
		this IThat<int> source,
		int? unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<int>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<uint, IThat<uint>> IsNotEqualTo(
		this IThat<uint> source,
		uint? unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<uint>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<long, IThat<long>> IsNotEqualTo(
		this IThat<long> source,
		long? unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<long>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<ulong, IThat<ulong>> IsNotEqualTo(
		this IThat<ulong> source,
		ulong? unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<ulong>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<float, IThat<float>> IsNotEqualTo(
		this IThat<float> source,
		float? unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<float>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<double, IThat<double>> IsNotEqualTo(
		this IThat<double> source,
		double? unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<double>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<decimal, IThat<decimal>> IsNotEqualTo(
		this IThat<decimal> source,
		decimal? unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericConstraint<decimal>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IThat<byte?>> IsNotEqualTo(
		this IThat<byte?> source,
		byte? unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<byte>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> IsNotEqualTo(
		this IThat<sbyte?> source,
		sbyte? unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<sbyte>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<short, IThat<short?>> IsNotEqualTo(
		this IThat<short?> source,
		short? unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<short>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> IsNotEqualTo(
		this IThat<ushort?> source,
		ushort? unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<ushort>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<int, IThat<int?>> IsNotEqualTo(
		this IThat<int?> source,
		int? unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<int>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IThat<uint?>> IsNotEqualTo(
		this IThat<uint?> source,
		uint? unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<uint>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<long, IThat<long?>> IsNotEqualTo(
		this IThat<long?> source,
		long? unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<long>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> IsNotEqualTo(
		this IThat<ulong?> source,
		ulong? unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<ulong>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<float, IThat<float?>> IsNotEqualTo(
		this IThat<float?> source,
		float? unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<float>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<double, IThat<double?>> IsNotEqualTo(
		this IThat<double?> source,
		double? unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<double>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> IsNotEqualTo(
		this IThat<decimal?> source,
		decimal? unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericConstraint<decimal>(
					it,
					unexpected,
					u => $"is not equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}
}
