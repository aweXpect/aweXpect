using System;
using System.Linq;
using Testably.Expectations.Core;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNumberShould
{
	/// <summary>
	///     Verifies that the subject is one of the <paramref name="expected" /> values.
	/// </summary>
	public static NumberToleranceResult<byte, IThat<byte>> BeOneOf(
		this IThat<byte> source,
		params byte?[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<sbyte, IThat<sbyte>> BeOneOf(
		this IThat<sbyte> source,
		params sbyte?[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
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
	public static NumberToleranceResult<short, IThat<short>> BeOneOf(
		this IThat<short> source,
		params short?[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
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
	public static NumberToleranceResult<ushort, IThat<ushort>> BeOneOf(
		this IThat<ushort> source,
		params ushort?[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
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
	public static NumberToleranceResult<int, IThat<int>> BeOneOf(
		this IThat<int> source,
		params int?[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(source.ExpectationBuilder.AddConstraint(it
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
	public static NumberToleranceResult<uint, IThat<uint>> BeOneOf(
		this IThat<uint> source,
		params uint?[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<long, IThat<long>> BeOneOf(
		this IThat<long> source,
		params long?[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<ulong, IThat<ulong>> BeOneOf(
		this IThat<ulong> source,
		params ulong?[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
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
	public static NumberToleranceResult<float, IThat<float>> BeOneOf(
		this IThat<float> source,
		params float?[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
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
	public static NumberToleranceResult<double, IThat<double>> BeOneOf(
		this IThat<double> source,
		params double?[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
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
	public static NumberToleranceResult<decimal, IThat<decimal>> BeOneOf(
		this IThat<decimal> source,
		params decimal?[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
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
	public static NullableNumberToleranceResult<byte, IThat<byte?>> BeOneOf(
		this IThat<byte?> source,
		params byte?[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
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
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> BeOneOf(
		this IThat<sbyte?> source,
		params sbyte?[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
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
	public static NullableNumberToleranceResult<short, IThat<short?>> BeOneOf(
		this IThat<short?> source,
		params short?[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
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
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> BeOneOf(
		this IThat<ushort?> source,
		params ushort?[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
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
	public static NullableNumberToleranceResult<int, IThat<int?>> BeOneOf(
		this IThat<int?> source,
		params int?[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
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
	public static NullableNumberToleranceResult<uint, IThat<uint?>> BeOneOf(
		this IThat<uint?> source,
		params uint?[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
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
	public static NullableNumberToleranceResult<long, IThat<long?>> BeOneOf(
		this IThat<long?> source,
		params long?[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
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
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> BeOneOf(
		this IThat<ulong?> source,
		params ulong?[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
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
	public static NullableNumberToleranceResult<float, IThat<float?>> BeOneOf(
		this IThat<float?> source,
		params float?[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
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
	public static NullableNumberToleranceResult<double, IThat<double?>> BeOneOf(
		this IThat<double?> source,
		params double?[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
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
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> BeOneOf(
		this IThat<decimal?> source,
		params decimal?[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
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
	public static NumberToleranceResult<byte, IThat<byte>> BeOneOf(
		this IThat<byte> source,
		params byte[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<sbyte, IThat<sbyte>> BeOneOf(
		this IThat<sbyte> source,
		params sbyte[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
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
	public static NumberToleranceResult<short, IThat<short>> BeOneOf(
		this IThat<short> source,
		params short[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
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
	public static NumberToleranceResult<ushort, IThat<ushort>> BeOneOf(
		this IThat<ushort> source,
		params ushort[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
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
	public static NumberToleranceResult<int, IThat<int>> BeOneOf(
		this IThat<int> source,
		params int[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(source.ExpectationBuilder.AddConstraint(it
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
	public static NumberToleranceResult<uint, IThat<uint>> BeOneOf(
		this IThat<uint> source,
		params uint[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<long, IThat<long>> BeOneOf(
		this IThat<long> source,
		params long[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<ulong, IThat<ulong>> BeOneOf(
		this IThat<ulong> source,
		params ulong[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
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
	public static NumberToleranceResult<float, IThat<float>> BeOneOf(
		this IThat<float> source,
		params float[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
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
	public static NumberToleranceResult<double, IThat<double>> BeOneOf(
		this IThat<double> source,
		params double[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
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
	public static NumberToleranceResult<decimal, IThat<decimal>> BeOneOf(
		this IThat<decimal> source,
		params decimal[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
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
	public static NullableNumberToleranceResult<byte, IThat<byte?>> BeOneOf(
		this IThat<byte?> source,
		params byte[] expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
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
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> BeOneOf(
		this IThat<sbyte?> source,
		params sbyte[] expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
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
	public static NullableNumberToleranceResult<short, IThat<short?>> BeOneOf(
		this IThat<short?> source,
		params short[] expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
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
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> BeOneOf(
		this IThat<ushort?> source,
		params ushort[] expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
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
	public static NullableNumberToleranceResult<int, IThat<int?>> BeOneOf(
		this IThat<int?> source,
		params int[] expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
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
	public static NullableNumberToleranceResult<uint, IThat<uint?>> BeOneOf(
		this IThat<uint?> source,
		params uint[] expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
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
	public static NullableNumberToleranceResult<long, IThat<long?>> BeOneOf(
		this IThat<long?> source,
		params long[] expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
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
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> BeOneOf(
		this IThat<ulong?> source,
		params ulong[] expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
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
	public static NullableNumberToleranceResult<float, IThat<float?>> BeOneOf(
		this IThat<float?> source,
		params float[] expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
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
	public static NullableNumberToleranceResult<double, IThat<double?>> BeOneOf(
		this IThat<double?> source,
		params double[] expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
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
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> BeOneOf(
		this IThat<decimal?> source,
		params decimal[] expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
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
	public static NumberToleranceResult<byte, IThat<byte>> NotBeOneOf(
		this IThat<byte> source,
		params byte?[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<sbyte, IThat<sbyte>> NotBeOneOf(
		this IThat<sbyte> source,
		params sbyte?[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
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
	public static NumberToleranceResult<short, IThat<short>> NotBeOneOf(
		this IThat<short> source,
		params short?[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
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
	public static NumberToleranceResult<ushort, IThat<ushort>> NotBeOneOf(
		this IThat<ushort> source,
		params ushort?[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
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
	public static NumberToleranceResult<int, IThat<int>> NotBeOneOf(
		this IThat<int> source,
		params int?[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(source.ExpectationBuilder.AddConstraint(it
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
	public static NumberToleranceResult<uint, IThat<uint>> NotBeOneOf(
		this IThat<uint> source,
		params uint?[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<long, IThat<long>> NotBeOneOf(
		this IThat<long> source,
		params long?[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<ulong, IThat<ulong>> NotBeOneOf(
		this IThat<ulong> source,
		params ulong?[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
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
	public static NumberToleranceResult<float, IThat<float>> NotBeOneOf(
		this IThat<float> source,
		params float?[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
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
	public static NumberToleranceResult<double, IThat<double>> NotBeOneOf(
		this IThat<double> source,
		params double?[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
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
	public static NumberToleranceResult<decimal, IThat<decimal>> NotBeOneOf(
		this IThat<decimal> source,
		params decimal?[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
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
	public static NullableNumberToleranceResult<byte, IThat<byte?>> NotBeOneOf(
		this IThat<byte?> source,
		params byte?[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
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
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> NotBeOneOf(
		this IThat<sbyte?> source,
		params sbyte?[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
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
	public static NullableNumberToleranceResult<short, IThat<short?>> NotBeOneOf(
		this IThat<short?> source,
		params short?[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
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
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> NotBeOneOf(
		this IThat<ushort?> source,
		params ushort?[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
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
	public static NullableNumberToleranceResult<int, IThat<int?>> NotBeOneOf(
		this IThat<int?> source,
		params int?[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
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
	public static NullableNumberToleranceResult<uint, IThat<uint?>> NotBeOneOf(
		this IThat<uint?> source,
		params uint?[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
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
	public static NullableNumberToleranceResult<long, IThat<long?>> NotBeOneOf(
		this IThat<long?> source,
		params long?[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
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
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> NotBeOneOf(
		this IThat<ulong?> source,
		params ulong?[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
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
	public static NullableNumberToleranceResult<float, IThat<float?>> NotBeOneOf(
		this IThat<float?> source,
		params float?[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
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
	public static NullableNumberToleranceResult<double, IThat<double?>> NotBeOneOf(
		this IThat<double?> source,
		params double?[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
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
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> NotBeOneOf(
		this IThat<decimal?> source,
		params decimal?[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
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
	public static NumberToleranceResult<byte, IThat<byte>> NotBeOneOf(
		this IThat<byte> source,
		params byte[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<sbyte, IThat<sbyte>> NotBeOneOf(
		this IThat<sbyte> source,
		params sbyte[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
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
	public static NumberToleranceResult<short, IThat<short>> NotBeOneOf(
		this IThat<short> source,
		params short[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
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
	public static NumberToleranceResult<ushort, IThat<ushort>> NotBeOneOf(
		this IThat<ushort> source,
		params ushort[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
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
	public static NumberToleranceResult<int, IThat<int>> NotBeOneOf(
		this IThat<int> source,
		params int[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(source.ExpectationBuilder.AddConstraint(it
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
	public static NumberToleranceResult<uint, IThat<uint>> NotBeOneOf(
		this IThat<uint> source,
		params uint[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<long, IThat<long>> NotBeOneOf(
		this IThat<long> source,
		params long[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(source.ExpectationBuilder.AddConstraint(
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
	public static NumberToleranceResult<ulong, IThat<ulong>> NotBeOneOf(
		this IThat<ulong> source,
		params ulong[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
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
	public static NumberToleranceResult<float, IThat<float>> NotBeOneOf(
		this IThat<float> source,
		params float[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
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
	public static NumberToleranceResult<double, IThat<double>> NotBeOneOf(
		this IThat<double> source,
		params double[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
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
	public static NumberToleranceResult<decimal, IThat<decimal>> NotBeOneOf(
		this IThat<decimal> source,
		params decimal[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
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
	public static NullableNumberToleranceResult<byte, IThat<byte?>> NotBeOneOf(
		this IThat<byte?> source,
		params byte[] unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
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
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> NotBeOneOf(
		this IThat<sbyte?> source,
		params sbyte[] unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
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
	public static NullableNumberToleranceResult<short, IThat<short?>> NotBeOneOf(
		this IThat<short?> source,
		params short[] unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
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
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> NotBeOneOf(
		this IThat<ushort?> source,
		params ushort[] unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
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
	public static NullableNumberToleranceResult<int, IThat<int?>> NotBeOneOf(
		this IThat<int?> source,
		params int[] unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
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
	public static NullableNumberToleranceResult<uint, IThat<uint?>> NotBeOneOf(
		this IThat<uint?> source,
		params uint[] unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
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
	public static NullableNumberToleranceResult<long, IThat<long?>> NotBeOneOf(
		this IThat<long?> source,
		params long[] unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
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
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> NotBeOneOf(
		this IThat<ulong?> source,
		params ulong[] unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
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
	public static NullableNumberToleranceResult<float, IThat<float?>> NotBeOneOf(
		this IThat<float?> source,
		params float[] unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => (float.IsNaN(a) && float.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
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
	public static NullableNumberToleranceResult<double, IThat<double?>> NotBeOneOf(
		this IThat<double?> source,
		params double[] unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => (double.IsNaN(a) && double.IsNaN(e)) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
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
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> NotBeOneOf(
		this IThat<decimal?> source,
		params decimal[] unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
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
