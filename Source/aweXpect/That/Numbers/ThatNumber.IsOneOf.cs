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
	public static NumberToleranceResult<byte, IThat<byte>> IsOneOf(
		this IThat<byte> source,
		params byte?[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<byte>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<sbyte, IThat<sbyte>> IsOneOf(
		this IThat<sbyte> source,
		params sbyte?[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<sbyte>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<short, IThat<short>> IsOneOf(
		this IThat<short> source,
		params short?[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<short>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<ushort, IThat<ushort>> IsOneOf(
		this IThat<ushort> source,
		params ushort?[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<ushort>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<int, IThat<int>> IsOneOf(
		this IThat<int> source,
		params int?[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<int>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<uint, IThat<uint>> IsOneOf(
		this IThat<uint> source,
		params uint?[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<uint>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<long, IThat<long>> IsOneOf(
		this IThat<long> source,
		params long?[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<long>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<ulong, IThat<ulong>> IsOneOf(
		this IThat<ulong> source,
		params ulong?[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<ulong>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<float, IThat<float>> IsOneOf(
		this IThat<float> source,
		params float?[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<float>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<double, IThat<double>> IsOneOf(
		this IThat<double> source,
		params double?[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<double>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<decimal, IThat<decimal>> IsOneOf(
		this IThat<decimal> source,
		params decimal?[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<decimal>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IThat<byte?>> IsOneOf(
		this IThat<byte?> source,
		params byte?[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<byte>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> IsOneOf(
		this IThat<sbyte?> source,
		params sbyte?[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<sbyte>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<short, IThat<short?>> IsOneOf(
		this IThat<short?> source,
		params short?[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<short>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> IsOneOf(
		this IThat<ushort?> source,
		params ushort?[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<ushort>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<int, IThat<int?>> IsOneOf(
		this IThat<int?> source,
		params int?[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<int>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IThat<uint?>> IsOneOf(
		this IThat<uint?> source,
		params uint?[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<uint>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<long, IThat<long?>> IsOneOf(
		this IThat<long?> source,
		params long?[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<long>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> IsOneOf(
		this IThat<ulong?> source,
		params ulong?[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<ulong>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<float, IThat<float?>> IsOneOf(
		this IThat<float?> source,
		params float?[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<float>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<double, IThat<double?>> IsOneOf(
		this IThat<double?> source,
		params double?[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<double>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> IsOneOf(
		this IThat<decimal?> source,
		params decimal?[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<decimal>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<byte, IThat<byte>> IsOneOf(
		this IThat<byte> source,
		params byte[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<byte>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<sbyte, IThat<sbyte>> IsOneOf(
		this IThat<sbyte> source,
		params sbyte[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<sbyte>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<short, IThat<short>> IsOneOf(
		this IThat<short> source,
		params short[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<short>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<ushort, IThat<ushort>> IsOneOf(
		this IThat<ushort> source,
		params ushort[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<ushort>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<int, IThat<int>> IsOneOf(
		this IThat<int> source,
		params int[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<int>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<uint, IThat<uint>> IsOneOf(
		this IThat<uint> source,
		params uint[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<uint>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<long, IThat<long>> IsOneOf(
		this IThat<long> source,
		params long[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<long>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<ulong, IThat<ulong>> IsOneOf(
		this IThat<ulong> source,
		params ulong[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<ulong>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<float, IThat<float>> IsOneOf(
		this IThat<float> source,
		params float[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<float>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<double, IThat<double>> IsOneOf(
		this IThat<double> source,
		params double[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<double>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<decimal, IThat<decimal>> IsOneOf(
		this IThat<decimal> source,
		params decimal[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<decimal>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IThat<byte?>> IsOneOf(
		this IThat<byte?> source,
		params byte[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<byte>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> IsOneOf(
		this IThat<sbyte?> source,
		params sbyte[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<sbyte>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<short, IThat<short?>> IsOneOf(
		this IThat<short?> source,
		params short[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<short>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> IsOneOf(
		this IThat<ushort?> source,
		params ushort[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<ushort>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<int, IThat<int?>> IsOneOf(
		this IThat<int?> source,
		params int[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<int>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IThat<uint?>> IsOneOf(
		this IThat<uint?> source,
		params uint[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<uint>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<long, IThat<long?>> IsOneOf(
		this IThat<long?> source,
		params long[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<long>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> IsOneOf(
		this IThat<ulong?> source,
		params ulong[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<ulong>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<float, IThat<float?>> IsOneOf(
		this IThat<float?> source,
		params float[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<float>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<double, IThat<double?>> IsOneOf(
		this IThat<double?> source,
		params double[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<double>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> IsOneOf(
		this IThat<decimal?> source,
		params decimal[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<decimal>(
					it,
					grammars,
					expected,
					e => $"is one of {Formatter.Format(e)}{options}",
					(a, e) => e.Any(v => options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<byte, IThat<byte>> IsNotOneOf(
		this IThat<byte> source,
		params byte?[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<byte>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<sbyte, IThat<sbyte>> IsNotOneOf(
		this IThat<sbyte> source,
		params sbyte?[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<sbyte>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<short, IThat<short>> IsNotOneOf(
		this IThat<short> source,
		params short?[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<short>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<ushort, IThat<ushort>> IsNotOneOf(
		this IThat<ushort> source,
		params ushort?[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<ushort>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<int, IThat<int>> IsNotOneOf(
		this IThat<int> source,
		params int?[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<int>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<uint, IThat<uint>> IsNotOneOf(
		this IThat<uint> source,
		params uint?[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<uint>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<long, IThat<long>> IsNotOneOf(
		this IThat<long> source,
		params long?[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<long>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<ulong, IThat<ulong>> IsNotOneOf(
		this IThat<ulong> source,
		params ulong?[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<ulong>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<float, IThat<float>> IsNotOneOf(
		this IThat<float> source,
		params float?[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<float>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<double, IThat<double>> IsNotOneOf(
		this IThat<double> source,
		params double?[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<double>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<decimal, IThat<decimal>> IsNotOneOf(
		this IThat<decimal> source,
		params decimal?[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraintWithNullableValues<decimal>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IThat<byte?>> IsNotOneOf(
		this IThat<byte?> source,
		params byte?[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<byte>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> IsNotOneOf(
		this IThat<sbyte?> source,
		params sbyte?[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<sbyte>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<short, IThat<short?>> IsNotOneOf(
		this IThat<short?> source,
		params short?[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<short>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> IsNotOneOf(
		this IThat<ushort?> source,
		params ushort?[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<ushort>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<int, IThat<int?>> IsNotOneOf(
		this IThat<int?> source,
		params int?[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<int>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IThat<uint?>> IsNotOneOf(
		this IThat<uint?> source,
		params uint?[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<uint>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<long, IThat<long?>> IsNotOneOf(
		this IThat<long?> source,
		params long?[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<long>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> IsNotOneOf(
		this IThat<ulong?> source,
		params ulong?[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<ulong>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<float, IThat<float?>> IsNotOneOf(
		this IThat<float?> source,
		params float?[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<float>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<double, IThat<double?>> IsNotOneOf(
		this IThat<double?> source,
		params double?[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<double>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> IsNotOneOf(
		this IThat<decimal?> source,
		params decimal?[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraintWithNullableValues<decimal>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<byte, IThat<byte>> IsNotOneOf(
		this IThat<byte> source,
		params byte[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<byte>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<sbyte, IThat<sbyte>> IsNotOneOf(
		this IThat<sbyte> source,
		params sbyte[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<sbyte>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<short, IThat<short>> IsNotOneOf(
		this IThat<short> source,
		params short[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<short>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<ushort, IThat<ushort>> IsNotOneOf(
		this IThat<ushort> source,
		params ushort[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<ushort>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<int, IThat<int>> IsNotOneOf(
		this IThat<int> source,
		params int[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<int>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<uint, IThat<uint>> IsNotOneOf(
		this IThat<uint> source,
		params uint[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<uint>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<long, IThat<long>> IsNotOneOf(
		this IThat<long> source,
		params long[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<long>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<ulong, IThat<ulong>> IsNotOneOf(
		this IThat<ulong> source,
		params ulong[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<ulong>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<float, IThat<float>> IsNotOneOf(
		this IThat<float> source,
		params float[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<float>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<double, IThat<double>> IsNotOneOf(
		this IThat<double> source,
		params double[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<double>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NumberToleranceResult<decimal, IThat<decimal>> IsNotOneOf(
		this IThat<decimal> source,
		params decimal[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new GenericArrayConstraint<decimal>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IThat<byte?>> IsNotOneOf(
		this IThat<byte?> source,
		params byte[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<byte>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> IsNotOneOf(
		this IThat<sbyte?> source,
		params sbyte[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<sbyte>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<short, IThat<short?>> IsNotOneOf(
		this IThat<short?> source,
		params short[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<short>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> IsNotOneOf(
		this IThat<ushort?> source,
		params ushort[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<ushort>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<int, IThat<int?>> IsNotOneOf(
		this IThat<int?> source,
		params int[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<int>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IThat<uint?>> IsNotOneOf(
		this IThat<uint?> source,
		params uint[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<uint>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<long, IThat<long?>> IsNotOneOf(
		this IThat<long?> source,
		params long[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<long>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> IsNotOneOf(
		this IThat<ulong?> source,
		params ulong[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<ulong>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<float, IThat<float?>> IsNotOneOf(
		this IThat<float?> source,
		params float[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<float>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<double, IThat<double?>> IsNotOneOf(
		this IThat<double?> source,
		params double[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<double>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not one of the <paramref name="unexpected" /> values.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> IsNotOneOf(
		this IThat<decimal?> source,
		params decimal[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableGenericArrayConstraint<decimal>(
					it,
					grammars,
					unexpected,
					u => $"is not one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}
}
