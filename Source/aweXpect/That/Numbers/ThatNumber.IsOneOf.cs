using System;
using System.Linq;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<byte, IExpectSubject<byte>> IsOneOf(
		this IExpectSubject<byte> source,
		params byte?[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IExpectSubject<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<byte>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<sbyte, IExpectSubject<sbyte>> IsOneOf(
		this IExpectSubject<sbyte> source,
		params sbyte?[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IExpectSubject<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<sbyte>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<short, IExpectSubject<short>> IsOneOf(
		this IExpectSubject<short> source,
		params short?[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IExpectSubject<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<short>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<ushort, IExpectSubject<ushort>> IsOneOf(
		this IExpectSubject<ushort> source,
		params ushort?[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IExpectSubject<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<ushort>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<int, IExpectSubject<int>> IsOneOf(
		this IExpectSubject<int> source,
		params int?[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IExpectSubject<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<int>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<uint, IExpectSubject<uint>> IsOneOf(
		this IExpectSubject<uint> source,
		params uint?[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IExpectSubject<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<uint>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<long, IExpectSubject<long>> IsOneOf(
		this IExpectSubject<long> source,
		params long?[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IExpectSubject<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<long>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<ulong, IExpectSubject<ulong>> IsOneOf(
		this IExpectSubject<ulong> source,
		params ulong?[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IExpectSubject<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<ulong>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<float, IExpectSubject<float>> IsOneOf(
		this IExpectSubject<float> source,
		params float?[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IExpectSubject<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<float>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<double, IExpectSubject<double>> IsOneOf(
		this IExpectSubject<double> source,
		params double?[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IExpectSubject<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<double>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<decimal, IExpectSubject<decimal>> IsOneOf(
		this IExpectSubject<decimal> source,
		params decimal?[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IExpectSubject<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<decimal>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IExpectSubject<byte?>> IsOneOf(
		this IExpectSubject<byte?> source,
		params byte?[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IExpectSubject<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<byte>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>> IsOneOf(
		this IExpectSubject<sbyte?> source,
		params sbyte?[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<sbyte>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<short, IExpectSubject<short?>> IsOneOf(
		this IExpectSubject<short?> source,
		params short?[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IExpectSubject<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<short>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>> IsOneOf(
		this IExpectSubject<ushort?> source,
		params ushort?[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<ushort>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<int, IExpectSubject<int?>> IsOneOf(
		this IExpectSubject<int?> source,
		params int?[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IExpectSubject<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<int>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IExpectSubject<uint?>> IsOneOf(
		this IExpectSubject<uint?> source,
		params uint?[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IExpectSubject<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<uint>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<long, IExpectSubject<long?>> IsOneOf(
		this IExpectSubject<long?> source,
		params long?[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IExpectSubject<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<long>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>> IsOneOf(
		this IExpectSubject<ulong?> source,
		params ulong?[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<ulong>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<float, IExpectSubject<float?>> IsOneOf(
		this IExpectSubject<float?> source,
		params float?[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IExpectSubject<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<float>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<double, IExpectSubject<double?>> IsOneOf(
		this IExpectSubject<double?> source,
		params double?[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IExpectSubject<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<double>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>> IsOneOf(
		this IExpectSubject<decimal?> source,
		params decimal?[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<decimal>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<byte, IExpectSubject<byte>> IsOneOf(
		this IExpectSubject<byte> source,
		params byte[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IExpectSubject<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<byte>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<sbyte, IExpectSubject<sbyte>> IsOneOf(
		this IExpectSubject<sbyte> source,
		params sbyte[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IExpectSubject<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<sbyte>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<short, IExpectSubject<short>> IsOneOf(
		this IExpectSubject<short> source,
		params short[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IExpectSubject<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<short>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<ushort, IExpectSubject<ushort>> IsOneOf(
		this IExpectSubject<ushort> source,
		params ushort[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IExpectSubject<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<ushort>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<int, IExpectSubject<int>> IsOneOf(
		this IExpectSubject<int> source,
		params int[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IExpectSubject<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<int>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<uint, IExpectSubject<uint>> IsOneOf(
		this IExpectSubject<uint> source,
		params uint[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IExpectSubject<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<uint>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<long, IExpectSubject<long>> IsOneOf(
		this IExpectSubject<long> source,
		params long[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IExpectSubject<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<long>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<ulong, IExpectSubject<ulong>> IsOneOf(
		this IExpectSubject<ulong> source,
		params ulong[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IExpectSubject<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<ulong>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<float, IExpectSubject<float>> IsOneOf(
		this IExpectSubject<float> source,
		params float[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IExpectSubject<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<float>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<double, IExpectSubject<double>> IsOneOf(
		this IExpectSubject<double> source,
		params double[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IExpectSubject<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<double>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<decimal, IExpectSubject<decimal>> IsOneOf(
		this IExpectSubject<decimal> source,
		params decimal[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IExpectSubject<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<decimal>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IExpectSubject<byte?>> IsOneOf(
		this IExpectSubject<byte?> source,
		params byte[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IExpectSubject<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<byte>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>> IsOneOf(
		this IExpectSubject<sbyte?> source,
		params sbyte[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<sbyte>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<short, IExpectSubject<short?>> IsOneOf(
		this IExpectSubject<short?> source,
		params short[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IExpectSubject<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<short>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>> IsOneOf(
		this IExpectSubject<ushort?> source,
		params ushort[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<ushort>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<int, IExpectSubject<int?>> IsOneOf(
		this IExpectSubject<int?> source,
		params int[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IExpectSubject<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<int>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IExpectSubject<uint?>> IsOneOf(
		this IExpectSubject<uint?> source,
		params uint[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IExpectSubject<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<uint>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<long, IExpectSubject<long?>> IsOneOf(
		this IExpectSubject<long?> source,
		params long[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IExpectSubject<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<long>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>> IsOneOf(
		this IExpectSubject<ulong?> source,
		params ulong[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<ulong>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<float, IExpectSubject<float?>> IsOneOf(
		this IExpectSubject<float?> source,
		params float[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IExpectSubject<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<float>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<double, IExpectSubject<double?>> IsOneOf(
		this IExpectSubject<double?> source,
		params double[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IExpectSubject<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<double>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>> IsOneOf(
		this IExpectSubject<decimal?> source,
		params decimal[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<decimal>(
					it,
					expected,
					e => $"be one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<byte, IExpectSubject<byte>> IsNotOneOf(
		this IExpectSubject<byte> source,
		params byte?[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IExpectSubject<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<byte>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<sbyte, IExpectSubject<sbyte>> IsNotOneOf(
		this IExpectSubject<sbyte> source,
		params sbyte?[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IExpectSubject<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<sbyte>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<short, IExpectSubject<short>> IsNotOneOf(
		this IExpectSubject<short> source,
		params short?[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IExpectSubject<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<short>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<ushort, IExpectSubject<ushort>> IsNotOneOf(
		this IExpectSubject<ushort> source,
		params ushort?[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IExpectSubject<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<ushort>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<int, IExpectSubject<int>> IsNotOneOf(
		this IExpectSubject<int> source,
		params int?[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IExpectSubject<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<int>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<uint, IExpectSubject<uint>> IsNotOneOf(
		this IExpectSubject<uint> source,
		params uint?[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IExpectSubject<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<uint>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<long, IExpectSubject<long>> IsNotOneOf(
		this IExpectSubject<long> source,
		params long?[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IExpectSubject<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<long>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<ulong, IExpectSubject<ulong>> IsNotOneOf(
		this IExpectSubject<ulong> source,
		params ulong?[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IExpectSubject<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<ulong>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<float, IExpectSubject<float>> IsNotOneOf(
		this IExpectSubject<float> source,
		params float?[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IExpectSubject<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<float>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<double, IExpectSubject<double>> IsNotOneOf(
		this IExpectSubject<double> source,
		params double?[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IExpectSubject<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<double>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<decimal, IExpectSubject<decimal>> IsNotOneOf(
		this IExpectSubject<decimal> source,
		params decimal?[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IExpectSubject<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraintWithNullableValues<decimal>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IExpectSubject<byte?>> IsNotOneOf(
		this IExpectSubject<byte?> source,
		params byte?[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IExpectSubject<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<byte>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>> IsNotOneOf(
		this IExpectSubject<sbyte?> source,
		params sbyte?[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<sbyte>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<short, IExpectSubject<short?>> IsNotOneOf(
		this IExpectSubject<short?> source,
		params short?[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IExpectSubject<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<short>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>> IsNotOneOf(
		this IExpectSubject<ushort?> source,
		params ushort?[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<ushort>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<int, IExpectSubject<int?>> IsNotOneOf(
		this IExpectSubject<int?> source,
		params int?[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IExpectSubject<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<int>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IExpectSubject<uint?>> IsNotOneOf(
		this IExpectSubject<uint?> source,
		params uint?[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IExpectSubject<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<uint>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<long, IExpectSubject<long?>> IsNotOneOf(
		this IExpectSubject<long?> source,
		params long?[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IExpectSubject<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<long>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>> IsNotOneOf(
		this IExpectSubject<ulong?> source,
		params ulong?[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<ulong>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<float, IExpectSubject<float?>> IsNotOneOf(
		this IExpectSubject<float?> source,
		params float?[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IExpectSubject<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<float>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<double, IExpectSubject<double?>> IsNotOneOf(
		this IExpectSubject<double?> source,
		params double?[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IExpectSubject<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<double>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>> IsNotOneOf(
		this IExpectSubject<decimal?> source,
		params decimal?[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraintWithNullableValues<decimal>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<byte, IExpectSubject<byte>> IsNotOneOf(
		this IExpectSubject<byte> source,
		params byte[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IExpectSubject<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<byte>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<sbyte, IExpectSubject<sbyte>> IsNotOneOf(
		this IExpectSubject<sbyte> source,
		params sbyte[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IExpectSubject<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<sbyte>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<short, IExpectSubject<short>> IsNotOneOf(
		this IExpectSubject<short> source,
		params short[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IExpectSubject<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<short>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<ushort, IExpectSubject<ushort>> IsNotOneOf(
		this IExpectSubject<ushort> source,
		params ushort[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IExpectSubject<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<ushort>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<int, IExpectSubject<int>> IsNotOneOf(
		this IExpectSubject<int> source,
		params int[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IExpectSubject<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<int>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<uint, IExpectSubject<uint>> IsNotOneOf(
		this IExpectSubject<uint> source,
		params uint[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IExpectSubject<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<uint>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<long, IExpectSubject<long>> IsNotOneOf(
		this IExpectSubject<long> source,
		params long[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IExpectSubject<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<long>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<ulong, IExpectSubject<ulong>> IsNotOneOf(
		this IExpectSubject<ulong> source,
		params ulong[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IExpectSubject<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<ulong>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<float, IExpectSubject<float>> IsNotOneOf(
		this IExpectSubject<float> source,
		params float[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IExpectSubject<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<float>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<double, IExpectSubject<double>> IsNotOneOf(
		this IExpectSubject<double> source,
		params double[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IExpectSubject<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<double>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<decimal, IExpectSubject<decimal>> IsNotOneOf(
		this IExpectSubject<decimal> source,
		params decimal[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IExpectSubject<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new GenericArrayConstraint<decimal>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IExpectSubject<byte?>> IsNotOneOf(
		this IExpectSubject<byte?> source,
		params byte[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IExpectSubject<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<byte>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>> IsNotOneOf(
		this IExpectSubject<sbyte?> source,
		params sbyte[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IExpectSubject<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<sbyte>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<short, IExpectSubject<short?>> IsNotOneOf(
		this IExpectSubject<short?> source,
		params short[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IExpectSubject<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<short>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>> IsNotOneOf(
		this IExpectSubject<ushort?> source,
		params ushort[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IExpectSubject<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<ushort>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<int, IExpectSubject<int?>> IsNotOneOf(
		this IExpectSubject<int?> source,
		params int[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IExpectSubject<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<int>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IExpectSubject<uint?>> IsNotOneOf(
		this IExpectSubject<uint?> source,
		params uint[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IExpectSubject<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<uint>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<long, IExpectSubject<long?>> IsNotOneOf(
		this IExpectSubject<long?> source,
		params long[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IExpectSubject<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<long>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>> IsNotOneOf(
		this IExpectSubject<ulong?> source,
		params ulong[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IExpectSubject<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<ulong>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<float, IExpectSubject<float?>> IsNotOneOf(
		this IExpectSubject<float?> source,
		params float[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IExpectSubject<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<float>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<double, IExpectSubject<double?>> IsNotOneOf(
		this IExpectSubject<double?> source,
		params double[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IExpectSubject<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<double>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>> IsNotOneOf(
		this IExpectSubject<decimal?> source,
		params decimal[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IExpectSubject<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NullableGenericArrayConstraint<decimal>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}
}
