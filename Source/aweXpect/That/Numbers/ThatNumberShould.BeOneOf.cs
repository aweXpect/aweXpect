using System;
using System.Linq;
using aweXpect.Core;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumberShould
{
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<byte, IThatShould<byte>> BeOneOf(
		this IThatShould<byte> source,
		params byte?[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThatShould<byte>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraintWithNullableValues<byte>(
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
	public static NumberToleranceResult<sbyte, IThatShould<sbyte>> BeOneOf(
		this IThatShould<sbyte> source,
		params sbyte?[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThatShould<sbyte>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<sbyte>(
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
	public static NumberToleranceResult<short, IThatShould<short>> BeOneOf(
		this IThatShould<short> source,
		params short?[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThatShould<short>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<short>(
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
	public static NumberToleranceResult<ushort, IThatShould<ushort>> BeOneOf(
		this IThatShould<ushort> source,
		params ushort?[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThatShould<ushort>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<ushort>(
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
	public static NumberToleranceResult<int, IThatShould<int>> BeOneOf(
		this IThatShould<int> source,
		params int?[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThatShould<int>>(source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<int>(
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
	public static NumberToleranceResult<uint, IThatShould<uint>> BeOneOf(
		this IThatShould<uint> source,
		params uint?[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThatShould<uint>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraintWithNullableValues<uint>(
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
	public static NumberToleranceResult<long, IThatShould<long>> BeOneOf(
		this IThatShould<long> source,
		params long?[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThatShould<long>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraintWithNullableValues<long>(
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
	public static NumberToleranceResult<ulong, IThatShould<ulong>> BeOneOf(
		this IThatShould<ulong> source,
		params ulong?[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThatShould<ulong>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<ulong>(
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
	public static NumberToleranceResult<float, IThatShould<float>> BeOneOf(
		this IThatShould<float> source,
		params float?[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThatShould<float>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<float>(
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
	public static NumberToleranceResult<double, IThatShould<double>> BeOneOf(
		this IThatShould<double> source,
		params double?[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThatShould<double>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<double>(
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
	public static NumberToleranceResult<decimal, IThatShould<decimal>> BeOneOf(
		this IThatShould<decimal> source,
		params decimal?[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThatShould<decimal>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<decimal>(
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
	public static NullableNumberToleranceResult<byte, IThatShould<byte?>> BeOneOf(
		this IThatShould<byte?> source,
		params byte?[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThatShould<byte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<byte>(
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
	public static NullableNumberToleranceResult<sbyte, IThatShould<sbyte?>> BeOneOf(
		this IThatShould<sbyte?> source,
		params sbyte?[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThatShould<sbyte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<sbyte>(
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
	public static NullableNumberToleranceResult<short, IThatShould<short?>> BeOneOf(
		this IThatShould<short?> source,
		params short?[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThatShould<short?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<short>(
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
	public static NullableNumberToleranceResult<ushort, IThatShould<ushort?>> BeOneOf(
		this IThatShould<ushort?> source,
		params ushort?[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThatShould<ushort?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<ushort>(
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
	public static NullableNumberToleranceResult<int, IThatShould<int?>> BeOneOf(
		this IThatShould<int?> source,
		params int?[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThatShould<int?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<int>(
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
	public static NullableNumberToleranceResult<uint, IThatShould<uint?>> BeOneOf(
		this IThatShould<uint?> source,
		params uint?[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThatShould<uint?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<uint>(
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
	public static NullableNumberToleranceResult<long, IThatShould<long?>> BeOneOf(
		this IThatShould<long?> source,
		params long?[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThatShould<long?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<long>(
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
	public static NullableNumberToleranceResult<ulong, IThatShould<ulong?>> BeOneOf(
		this IThatShould<ulong?> source,
		params ulong?[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThatShould<ulong?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<ulong>(
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
	public static NullableNumberToleranceResult<float, IThatShould<float?>> BeOneOf(
		this IThatShould<float?> source,
		params float?[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThatShould<float?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<float>(
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
	public static NullableNumberToleranceResult<double, IThatShould<double?>> BeOneOf(
		this IThatShould<double?> source,
		params double?[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThatShould<double?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<double>(
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
	public static NullableNumberToleranceResult<decimal, IThatShould<decimal?>> BeOneOf(
		this IThatShould<decimal?> source,
		params decimal?[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThatShould<decimal?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<decimal>(
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
	public static NumberToleranceResult<byte, IThatShould<byte>> BeOneOf(
		this IThatShould<byte> source,
		params byte[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThatShould<byte>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraint<byte>(
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
	public static NumberToleranceResult<sbyte, IThatShould<sbyte>> BeOneOf(
		this IThatShould<sbyte> source,
		params sbyte[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThatShould<sbyte>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<sbyte>(
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
	public static NumberToleranceResult<short, IThatShould<short>> BeOneOf(
		this IThatShould<short> source,
		params short[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThatShould<short>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<short>(
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
	public static NumberToleranceResult<ushort, IThatShould<ushort>> BeOneOf(
		this IThatShould<ushort> source,
		params ushort[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThatShould<ushort>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<ushort>(
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
	public static NumberToleranceResult<int, IThatShould<int>> BeOneOf(
		this IThatShould<int> source,
		params int[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThatShould<int>>(source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<int>(
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
	public static NumberToleranceResult<uint, IThatShould<uint>> BeOneOf(
		this IThatShould<uint> source,
		params uint[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThatShould<uint>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraint<uint>(
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
	public static NumberToleranceResult<long, IThatShould<long>> BeOneOf(
		this IThatShould<long> source,
		params long[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThatShould<long>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraint<long>(
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
	public static NumberToleranceResult<ulong, IThatShould<ulong>> BeOneOf(
		this IThatShould<ulong> source,
		params ulong[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThatShould<ulong>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<ulong>(
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
	public static NumberToleranceResult<float, IThatShould<float>> BeOneOf(
		this IThatShould<float> source,
		params float[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThatShould<float>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<float>(
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
	public static NumberToleranceResult<double, IThatShould<double>> BeOneOf(
		this IThatShould<double> source,
		params double[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThatShould<double>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<double>(
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
	public static NumberToleranceResult<decimal, IThatShould<decimal>> BeOneOf(
		this IThatShould<decimal> source,
		params decimal[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThatShould<decimal>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<decimal>(
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
	public static NullableNumberToleranceResult<byte, IThatShould<byte?>> BeOneOf(
		this IThatShould<byte?> source,
		params byte[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThatShould<byte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<byte>(
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
	public static NullableNumberToleranceResult<sbyte, IThatShould<sbyte?>> BeOneOf(
		this IThatShould<sbyte?> source,
		params sbyte[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThatShould<sbyte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<sbyte>(
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
	public static NullableNumberToleranceResult<short, IThatShould<short?>> BeOneOf(
		this IThatShould<short?> source,
		params short[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThatShould<short?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<short>(
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
	public static NullableNumberToleranceResult<ushort, IThatShould<ushort?>> BeOneOf(
		this IThatShould<ushort?> source,
		params ushort[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThatShould<ushort?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<ushort>(
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
	public static NullableNumberToleranceResult<int, IThatShould<int?>> BeOneOf(
		this IThatShould<int?> source,
		params int[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThatShould<int?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<int>(
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
	public static NullableNumberToleranceResult<uint, IThatShould<uint?>> BeOneOf(
		this IThatShould<uint?> source,
		params uint[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThatShould<uint?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<uint>(
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
	public static NullableNumberToleranceResult<long, IThatShould<long?>> BeOneOf(
		this IThatShould<long?> source,
		params long[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThatShould<long?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<long>(
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
	public static NullableNumberToleranceResult<ulong, IThatShould<ulong?>> BeOneOf(
		this IThatShould<ulong?> source,
		params ulong[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThatShould<ulong?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<ulong>(
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
	public static NullableNumberToleranceResult<float, IThatShould<float?>> BeOneOf(
		this IThatShould<float?> source,
		params float[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThatShould<float?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<float>(
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
	public static NullableNumberToleranceResult<double, IThatShould<double?>> BeOneOf(
		this IThatShould<double?> source,
		params double[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThatShould<double?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<double>(
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
	public static NullableNumberToleranceResult<decimal, IThatShould<decimal?>> BeOneOf(
		this IThatShould<decimal?> source,
		params decimal[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThatShould<decimal?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<decimal>(
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
	public static NumberToleranceResult<byte, IThatShould<byte>> NotBeOneOf(
		this IThatShould<byte> source,
		params byte?[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThatShould<byte>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraintWithNullableValues<byte>(
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
	public static NumberToleranceResult<sbyte, IThatShould<sbyte>> NotBeOneOf(
		this IThatShould<sbyte> source,
		params sbyte?[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThatShould<sbyte>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<sbyte>(
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
	public static NumberToleranceResult<short, IThatShould<short>> NotBeOneOf(
		this IThatShould<short> source,
		params short?[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThatShould<short>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<short>(
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
	public static NumberToleranceResult<ushort, IThatShould<ushort>> NotBeOneOf(
		this IThatShould<ushort> source,
		params ushort?[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThatShould<ushort>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<ushort>(
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
	public static NumberToleranceResult<int, IThatShould<int>> NotBeOneOf(
		this IThatShould<int> source,
		params int?[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThatShould<int>>(source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<int>(
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
	public static NumberToleranceResult<uint, IThatShould<uint>> NotBeOneOf(
		this IThatShould<uint> source,
		params uint?[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThatShould<uint>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraintWithNullableValues<uint>(
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
	public static NumberToleranceResult<long, IThatShould<long>> NotBeOneOf(
		this IThatShould<long> source,
		params long?[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThatShould<long>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraintWithNullableValues<long>(
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
	public static NumberToleranceResult<ulong, IThatShould<ulong>> NotBeOneOf(
		this IThatShould<ulong> source,
		params ulong?[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThatShould<ulong>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<ulong>(
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
	public static NumberToleranceResult<float, IThatShould<float>> NotBeOneOf(
		this IThatShould<float> source,
		params float?[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThatShould<float>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<float>(
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
	public static NumberToleranceResult<double, IThatShould<double>> NotBeOneOf(
		this IThatShould<double> source,
		params double?[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThatShould<double>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<double>(
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
	public static NumberToleranceResult<decimal, IThatShould<decimal>> NotBeOneOf(
		this IThatShould<decimal> source,
		params decimal?[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThatShould<decimal>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraintWithNullableValues<decimal>(
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
	public static NullableNumberToleranceResult<byte, IThatShould<byte?>> NotBeOneOf(
		this IThatShould<byte?> source,
		params byte?[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThatShould<byte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<byte>(
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
	public static NullableNumberToleranceResult<sbyte, IThatShould<sbyte?>> NotBeOneOf(
		this IThatShould<sbyte?> source,
		params sbyte?[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThatShould<sbyte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<sbyte>(
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
	public static NullableNumberToleranceResult<short, IThatShould<short?>> NotBeOneOf(
		this IThatShould<short?> source,
		params short?[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThatShould<short?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<short>(
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
	public static NullableNumberToleranceResult<ushort, IThatShould<ushort?>> NotBeOneOf(
		this IThatShould<ushort?> source,
		params ushort?[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThatShould<ushort?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<ushort>(
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
	public static NullableNumberToleranceResult<int, IThatShould<int?>> NotBeOneOf(
		this IThatShould<int?> source,
		params int?[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThatShould<int?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<int>(
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
	public static NullableNumberToleranceResult<uint, IThatShould<uint?>> NotBeOneOf(
		this IThatShould<uint?> source,
		params uint?[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThatShould<uint?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<uint>(
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
	public static NullableNumberToleranceResult<long, IThatShould<long?>> NotBeOneOf(
		this IThatShould<long?> source,
		params long?[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThatShould<long?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<long>(
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
	public static NullableNumberToleranceResult<ulong, IThatShould<ulong?>> NotBeOneOf(
		this IThatShould<ulong?> source,
		params ulong?[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThatShould<ulong?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<ulong>(
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
	public static NullableNumberToleranceResult<float, IThatShould<float?>> NotBeOneOf(
		this IThatShould<float?> source,
		params float?[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThatShould<float?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<float>(
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
	public static NullableNumberToleranceResult<double, IThatShould<double?>> NotBeOneOf(
		this IThatShould<double?> source,
		params double?[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThatShould<double?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<double>(
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
	public static NullableNumberToleranceResult<decimal, IThatShould<decimal?>> NotBeOneOf(
		this IThatShould<decimal?> source,
		params decimal?[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThatShould<decimal?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraintWithNullableValues<decimal>(
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
	public static NumberToleranceResult<byte, IThatShould<byte>> NotBeOneOf(
		this IThatShould<byte> source,
		params byte[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThatShould<byte>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraint<byte>(
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
	public static NumberToleranceResult<sbyte, IThatShould<sbyte>> NotBeOneOf(
		this IThatShould<sbyte> source,
		params sbyte[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThatShould<sbyte>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<sbyte>(
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
	public static NumberToleranceResult<short, IThatShould<short>> NotBeOneOf(
		this IThatShould<short> source,
		params short[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThatShould<short>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<short>(
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
	public static NumberToleranceResult<ushort, IThatShould<ushort>> NotBeOneOf(
		this IThatShould<ushort> source,
		params ushort[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThatShould<ushort>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<ushort>(
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
	public static NumberToleranceResult<int, IThatShould<int>> NotBeOneOf(
		this IThatShould<int> source,
		params int[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThatShould<int>>(source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<int>(
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
	public static NumberToleranceResult<uint, IThatShould<uint>> NotBeOneOf(
		this IThatShould<uint> source,
		params uint[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThatShould<uint>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraint<uint>(
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
	public static NumberToleranceResult<long, IThatShould<long>> NotBeOneOf(
		this IThatShould<long> source,
		params long[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThatShould<long>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericArrayConstraint<long>(
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
	public static NumberToleranceResult<ulong, IThatShould<ulong>> NotBeOneOf(
		this IThatShould<ulong> source,
		params ulong[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThatShould<ulong>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<ulong>(
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
	public static NumberToleranceResult<float, IThatShould<float>> NotBeOneOf(
		this IThatShould<float> source,
		params float[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThatShould<float>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<float>(
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
	public static NumberToleranceResult<double, IThatShould<double>> NotBeOneOf(
		this IThatShould<double> source,
		params double[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThatShould<double>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<double>(
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
	public static NumberToleranceResult<decimal, IThatShould<decimal>> NotBeOneOf(
		this IThatShould<decimal> source,
		params decimal[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThatShould<decimal>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericArrayConstraint<decimal>(
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
	public static NullableNumberToleranceResult<byte, IThatShould<byte?>> NotBeOneOf(
		this IThatShould<byte?> source,
		params byte[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThatShould<byte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<byte>(
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
	public static NullableNumberToleranceResult<sbyte, IThatShould<sbyte?>> NotBeOneOf(
		this IThatShould<sbyte?> source,
		params sbyte[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThatShould<sbyte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<sbyte>(
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
	public static NullableNumberToleranceResult<short, IThatShould<short?>> NotBeOneOf(
		this IThatShould<short?> source,
		params short[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThatShould<short?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<short>(
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
	public static NullableNumberToleranceResult<ushort, IThatShould<ushort?>> NotBeOneOf(
		this IThatShould<ushort?> source,
		params ushort[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThatShould<ushort?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<ushort>(
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
	public static NullableNumberToleranceResult<int, IThatShould<int?>> NotBeOneOf(
		this IThatShould<int?> source,
		params int[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThatShould<int?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<int>(
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
	public static NullableNumberToleranceResult<uint, IThatShould<uint?>> NotBeOneOf(
		this IThatShould<uint?> source,
		params uint[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThatShould<uint?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<uint>(
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
	public static NullableNumberToleranceResult<long, IThatShould<long?>> NotBeOneOf(
		this IThatShould<long?> source,
		params long[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThatShould<long?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<long>(
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
	public static NullableNumberToleranceResult<ulong, IThatShould<ulong?>> NotBeOneOf(
		this IThatShould<ulong?> source,
		params ulong[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThatShould<ulong?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<ulong>(
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
	public static NullableNumberToleranceResult<float, IThatShould<float?>> NotBeOneOf(
		this IThatShould<float?> source,
		params float[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThatShould<float?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<float>(
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
	public static NullableNumberToleranceResult<double, IThatShould<double?>> NotBeOneOf(
		this IThatShould<double?> source,
		params double[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThatShould<double?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<double>(
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
	public static NullableNumberToleranceResult<decimal, IThatShould<decimal?>> NotBeOneOf(
		this IThatShould<decimal?> source,
		params decimal[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThatShould<decimal?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericArrayConstraint<decimal>(
					it,
					unexpected,
					u => $"not be one of {Formatter.Format(u)}{options}",
					(a, u) => u.All(v => !options.IsWithinTolerance(a, v)),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}
}
