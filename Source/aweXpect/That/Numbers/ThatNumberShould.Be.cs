using System;
using Testably.Expectations.Core;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNumberShould
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<byte, IThat<byte>> Be(
		this IThat<byte> source,
		byte? expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericConstraint<byte>(
						it,
						expected,
						e => $"be {Formatter.Format(e)}{options}",
						(a, e) => options.IsWithinTolerance(a, e),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<sbyte, IThat<sbyte>> Be(
		this IThat<sbyte> source,
		sbyte? expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<sbyte>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<short, IThat<short>> Be(
		this IThat<short> source,
		short? expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<short>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<ushort, IThat<ushort>> Be(
		this IThat<ushort> source,
		ushort? expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<ushort>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<int, IThat<int>> Be(
		this IThat<int> source,
		int? expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<int>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<uint, IThat<uint>> Be(
		this IThat<uint> source,
		uint? expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericConstraint<uint>(
						it,
						expected,
						e => $"be {Formatter.Format(e)}{options}",
						(a, e) => options.IsWithinTolerance(a, e),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<long, IThat<long>> Be(
		this IThat<long> source,
		long? expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericConstraint<long>(
						it,
						expected,
						e => $"be {Formatter.Format(e)}{options}",
						(a, e) => options.IsWithinTolerance(a, e),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<ulong, IThat<ulong>> Be(
		this IThat<ulong> source,
		ulong? expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<ulong>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<float, IThat<float>> Be(
		this IThat<float> source,
		float? expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<float>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<double, IThat<double>> Be(
		this IThat<double> source,
		double? expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<double>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NumberToleranceResult<decimal, IThat<decimal>> Be(
		this IThat<decimal> source,
		decimal? expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<decimal>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IThat<byte?>> Be(
		this IThat<byte?> source,
		byte? expected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<byte>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> Be(
		this IThat<sbyte?> source,
		sbyte? expected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<sbyte>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<short, IThat<short?>> Be(
		this IThat<short?> source,
		short? expected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<short>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> Be(
		this IThat<ushort?> source,
		ushort? expected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<ushort>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<int, IThat<int?>> Be(
		this IThat<int?> source,
		int? expected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<int>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IThat<uint?>> Be(
		this IThat<uint?> source,
		uint? expected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<uint>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<long, IThat<long?>> Be(
		this IThat<long?> source,
		long? expected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<long>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> Be(
		this IThat<ulong?> source,
		ulong? expected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<ulong>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<float, IThat<float?>> Be(
		this IThat<float?> source,
		float? expected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<float>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<double, IThat<double?>> Be(
		this IThat<double?> source,
		double? expected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<double>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> Be(
		this IThat<decimal?> source,
		decimal? expected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<decimal>(
					it,
					expected,
					e => $"be {Formatter.Format(e)}{options}",
					(a, e) => options.IsWithinTolerance(a, e),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<byte, IThat<byte>> NotBe(
		this IThat<byte> source,
		byte? unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<byte, IThat<byte>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericConstraint<byte>(
						it,
						unexpected,
						u => $"not be {Formatter.Format(u)}{options}",
						(a, u) => !options.IsWithinTolerance(a, u),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<sbyte, IThat<sbyte>> NotBe(
		this IThat<sbyte> source,
		sbyte? unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<sbyte, IThat<sbyte>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<sbyte>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<short, IThat<short>> NotBe(
		this IThat<short> source,
		short? unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<short, IThat<short>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<short>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<ushort, IThat<ushort>> NotBe(
		this IThat<ushort> source,
		ushort? unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<ushort, IThat<ushort>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<ushort>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<int, IThat<int>> NotBe(
		this IThat<int> source,
		int? unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<int, IThat<int>>(source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<int>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<uint, IThat<uint>> NotBe(
		this IThat<uint> source,
		uint? unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<uint, IThat<uint>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericConstraint<uint>(
						it,
						unexpected,
						u => $"not be {Formatter.Format(u)}{options}",
						(a, u) => !options.IsWithinTolerance(a, u),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<long, IThat<long>> NotBe(
		this IThat<long> source,
		long? unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<long, IThat<long>>(source.ExpectationBuilder.AddConstraint(
				it
					=> new GenericConstraint<long>(
						it,
						unexpected,
						u => $"not be {Formatter.Format(u)}{options}",
						(a, u) => !options.IsWithinTolerance(a, u),
						(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<ulong, IThat<ulong>> NotBe(
		this IThat<ulong> source,
		ulong? unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NumberToleranceResult<ulong, IThat<ulong>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<ulong>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<float, IThat<float>> NotBe(
		this IThat<float> source,
		float? unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<float, IThat<float>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<float>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<double, IThat<double>> NotBe(
		this IThat<double> source,
		double? unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<double, IThat<double>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<double>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NumberToleranceResult<decimal, IThat<decimal>> NotBe(
		this IThat<decimal> source,
		decimal? unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NumberToleranceResult<decimal, IThat<decimal>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new GenericConstraint<decimal>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<byte, IThat<byte?>> NotBe(
		this IThat<byte?> source,
		byte? unexpected)
	{
		NumberTolerance<byte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<byte, IThat<byte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<byte>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<sbyte, IThat<sbyte?>> NotBe(
		this IThat<sbyte?> source,
		sbyte? unexpected)
	{
		NumberTolerance<sbyte> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<sbyte, IThat<sbyte?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<sbyte>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<short, IThat<short?>> NotBe(
		this IThat<short?> source,
		short? unexpected)
	{
		NumberTolerance<short> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<short, IThat<short?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<short>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ushort, IThat<ushort?>> NotBe(
		this IThat<ushort?> source,
		ushort? unexpected)
	{
		NumberTolerance<ushort> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<ushort, IThat<ushort?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<ushort>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<int, IThat<int?>> NotBe(
		this IThat<int?> source,
		int? unexpected)
	{
		NumberTolerance<int> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<int, IThat<int?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<int>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<uint, IThat<uint?>> NotBe(
		this IThat<uint?> source,
		uint? unexpected)
	{
		NumberTolerance<uint> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<uint, IThat<uint?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<uint>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<long, IThat<long?>> NotBe(
		this IThat<long?> source,
		long? unexpected)
	{
		NumberTolerance<long> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<long, IThat<long?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<long>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<ulong, IThat<ulong?>> NotBe(
		this IThat<ulong?> source,
		ulong? unexpected)
	{
		NumberTolerance<ulong> options = new(
			(a, e, t) => (a > e ? a - e : e - a) <= (t ?? 0));
		return new NullableNumberToleranceResult<ulong, IThat<ulong?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<ulong>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<float, IThat<float?>> NotBe(
		this IThat<float?> source,
		float? unexpected)
	{
		NumberTolerance<float> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<float, IThat<float?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<float>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<double, IThat<double?>> NotBe(
		this IThat<double?> source,
		double? unexpected)
	{
		NumberTolerance<double> options = new(
			(a, e, t) => a.Equals(e) || Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<double, IThat<double?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<double>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static NullableNumberToleranceResult<decimal, IThat<decimal?>> NotBe(
		this IThat<decimal?> source,
		decimal? unexpected)
	{
		NumberTolerance<decimal> options = new(
			(a, e, t) => Math.Abs(a - e) <= (t ?? 0));
		return new NullableNumberToleranceResult<decimal, IThat<decimal?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new NullableGenericConstraint<decimal>(
					it,
					unexpected,
					u => $"not be {Formatter.Format(u)}{options}",
					(a, u) => !options.IsWithinTolerance(a, u),
					(a, _, i) => $"{i} was {Formatter.Format(a)}")),
			source,
			options);
	}
}
