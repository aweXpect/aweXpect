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
	public static NumberToleranceResult<byte, IExpectSubject<byte>> Is(
		this IExpectSubject<byte> source,
		byte? expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IExpectSubject<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<byte>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<sbyte, IExpectSubject<sbyte>> Is(
		this IExpectSubject<sbyte> source,
		sbyte? expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IExpectSubject<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<sbyte>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<short, IExpectSubject<short>> Is(
		this IExpectSubject<short> source,
		short? expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IExpectSubject<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<short>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<ushort, IExpectSubject<ushort>> Is(
		this IExpectSubject<ushort> source,
		ushort? expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IExpectSubject<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<ushort>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<int, IExpectSubject<int>> Is(
		this IExpectSubject<int> source,
		int? expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IExpectSubject<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<int>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<uint, IExpectSubject<uint>> Is(
		this IExpectSubject<uint> source,
		uint? expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IExpectSubject<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<uint>(
						it,
						expected,
						e => $"be equal to {Formatter.Format(e)}{options}",
						(a, e) => options.IsWithinTolerance(a, e),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<long, IExpectSubject<long>> Is(
		this IExpectSubject<long> source,
		long? expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IExpectSubject<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<long>(
						it,
						expected,
						e => $"be equal to {Formatter.Format(e)}{options}",
						(a, e) => options.IsWithinTolerance(a, e),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<ulong, IExpectSubject<ulong>> Is(
		this IExpectSubject<ulong> source,
		ulong? expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IExpectSubject<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<ulong>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<float, IExpectSubject<float>> Is(
		this IExpectSubject<float> source,
		float? expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IExpectSubject<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<float>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<double, IExpectSubject<double>> Is(
		this IExpectSubject<double> source,
		double? expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IExpectSubject<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<double>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<decimal, IExpectSubject<decimal>> Is(
		this IExpectSubject<decimal> source,
		decimal? expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IExpectSubject<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<decimal>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IExpectSubject<byte?>> Is(
		this IExpectSubject<byte?> source,
		byte? expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IExpectSubject<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<byte>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>> Is(
		this IExpectSubject<sbyte?> source,
		sbyte? expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<sbyte>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<short, IExpectSubject<short?>> Is(
		this IExpectSubject<short?> source,
		short? expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IExpectSubject<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<short>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>> Is(
		this IExpectSubject<ushort?> source,
		ushort? expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<ushort>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<int, IExpectSubject<int?>> Is(
		this IExpectSubject<int?> source,
		int? expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IExpectSubject<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<int>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IExpectSubject<uint?>> Is(
		this IExpectSubject<uint?> source,
		uint? expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IExpectSubject<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<uint>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<long, IExpectSubject<long?>> Is(
		this IExpectSubject<long?> source,
		long? expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IExpectSubject<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<long>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>> Is(
		this IExpectSubject<ulong?> source,
		ulong? expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<ulong>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<float, IExpectSubject<float?>> Is(
		this IExpectSubject<float?> source,
		float? expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IExpectSubject<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<float>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<double, IExpectSubject<double?>> Is(
		this IExpectSubject<double?> source,
		double? expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IExpectSubject<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<double>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>> Is(
		this IExpectSubject<decimal?> source,
		decimal? expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<decimal>(
					it,
					expected,
					e => $"be equal to {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<byte, IExpectSubject<byte>> IsNot(
		this IExpectSubject<byte> source,
		byte? unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IExpectSubject<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<byte>(
						it,
						unexpected,
						u => $"not be equal to {Formatter.Format(u)}{options}",
						(a, u) => !options.IsWithinTolerance(a, u),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<sbyte, IExpectSubject<sbyte>> IsNot(
		this IExpectSubject<sbyte> source,
		sbyte? unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IExpectSubject<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<sbyte>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<short, IExpectSubject<short>> IsNot(
		this IExpectSubject<short> source,
		short? unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IExpectSubject<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<short>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<ushort, IExpectSubject<ushort>> IsNot(
		this IExpectSubject<ushort> source,
		ushort? unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IExpectSubject<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<ushort>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<int, IExpectSubject<int>> IsNot(
		this IExpectSubject<int> source,
		int? unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IExpectSubject<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<int>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<uint, IExpectSubject<uint>> IsNot(
		this IExpectSubject<uint> source,
		uint? unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IExpectSubject<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<uint>(
						it,
						unexpected,
						u => $"not be equal to {Formatter.Format(u)}{options}",
						(a, u) => !options.IsWithinTolerance(a, u),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<long, IExpectSubject<long>> IsNot(
		this IExpectSubject<long> source,
		long? unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IExpectSubject<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<long>(
						it,
						unexpected,
						u => $"not be equal to {Formatter.Format(u)}{options}",
						(a, u) => !options.IsWithinTolerance(a, u),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<ulong, IExpectSubject<ulong>> IsNot(
		this IExpectSubject<ulong> source,
		ulong? unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IExpectSubject<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<ulong>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<float, IExpectSubject<float>> IsNot(
		this IExpectSubject<float> source,
		float? unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IExpectSubject<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<float>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<double, IExpectSubject<double>> IsNot(
		this IExpectSubject<double> source,
		double? unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IExpectSubject<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<double>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<decimal, IExpectSubject<decimal>> IsNot(
		this IExpectSubject<decimal> source,
		decimal? unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IExpectSubject<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericConstraint<decimal>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IExpectSubject<byte?>> IsNot(
		this IExpectSubject<byte?> source,
		byte? unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IExpectSubject<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<byte>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>> IsNot(
		this IExpectSubject<sbyte?> source,
		sbyte? unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<sbyte>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<short, IExpectSubject<short?>> IsNot(
		this IExpectSubject<short?> source,
		short? unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IExpectSubject<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<short>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>> IsNot(
		this IExpectSubject<ushort?> source,
		ushort? unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<ushort>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<int, IExpectSubject<int?>> IsNot(
		this IExpectSubject<int?> source,
		int? unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IExpectSubject<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<int>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IExpectSubject<uint?>> IsNot(
		this IExpectSubject<uint?> source,
		uint? unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IExpectSubject<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<uint>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<long, IExpectSubject<long?>> IsNot(
		this IExpectSubject<long?> source,
		long? unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IExpectSubject<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<long>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>> IsNot(
		this IExpectSubject<ulong?> source,
		ulong? unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<ulong>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<float, IExpectSubject<float?>> IsNot(
		this IExpectSubject<float?> source,
		float? unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IExpectSubject<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<float>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<double, IExpectSubject<double?>> IsNot(
		this IExpectSubject<double?> source,
		double? unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IExpectSubject<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<double>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>> IsNot(
		this IExpectSubject<decimal?> source,
		decimal? unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericConstraint<decimal>(
					it,
					unexpected,
					u => $"not be equal to {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}
}
