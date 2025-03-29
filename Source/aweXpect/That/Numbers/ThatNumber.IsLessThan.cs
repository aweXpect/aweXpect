using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;
#if !NET8_0_OR_GREATER
using System;
#endif

namespace aweXpect;

public static partial class ThatNumber
{
#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TNumber, IThat<TNumber>> IsLessThan<TNumber>(
		this IThat<TNumber> source, TNumber? expected)
		where TNumber : struct, INumber<TNumber>
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<TNumber>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TNumber?, IThat<TNumber?>> IsLessThan<TNumber>(
		this IThat<TNumber?> source, TNumber? expected)
		where TNumber : struct, INumber<TNumber>
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<TNumber>(it, grammars, expected)),
			source);

	private sealed class IsLessThanConstraint<TNumber>(string it, ExpectationGrammars grammars, TNumber? expected)
		: ConstraintResult.WithEqualToValue<TNumber>(it, grammars, expected is null),
			IValueConstraint<TNumber>
		where TNumber : struct, INumber<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber actual)
		{
			Actual = actual;
			Outcome = IsFinite(expected) && IsFinite(actual) && actual < expected
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is less than ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not less than ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class NullableIsLessThanConstraint<TNumber>(
		string it,
		ExpectationGrammars grammars,
		TNumber? expected)
		: ConstraintResult.WithEqualToValue<TNumber?>(it, grammars, expected is null),
			IValueConstraint<TNumber?>
		where TNumber : struct, INumber<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber? actual)
		{
			Actual = actual;
			Outcome = IsFinite(expected) && IsFinite(actual) && actual < expected
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is less than ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not less than ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
#else
	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IThat<byte>> IsLessThan(
		this IThat<byte> source,
		byte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<byte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsLessThan(
		this IThat<sbyte> source,
		sbyte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<sbyte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsLessThan(
		this IThat<short> source,
		short? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<short>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IThat<ushort>> IsLessThan(
		this IThat<ushort> source,
		ushort? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<ushort>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsLessThan(
		this IThat<int> source,
		int? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<int>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IThat<uint>> IsLessThan(
		this IThat<uint> source,
		uint? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<uint>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsLessThan(
		this IThat<long> source,
		long? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<long>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IThat<ulong>> IsLessThan(
		this IThat<ulong> source,
		ulong? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<ulong>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsLessThan(
		this IThat<float> source,
		float? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<float>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsLessThan(
		this IThat<double> source,
		double? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<double>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsLessThan(
		this IThat<decimal> source,
		decimal? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanConstraint<decimal>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IThat<byte?>> IsLessThan(
		this IThat<byte?> source,
		byte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<byte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsLessThan(
		this IThat<sbyte?> source,
		sbyte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<sbyte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsLessThan(
		this IThat<short?> source,
		short? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<short>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IThat<ushort?>> IsLessThan(
		this IThat<ushort?> source,
		ushort? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<ushort>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsLessThan(
		this IThat<int?> source,
		int? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<int>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IThat<uint?>> IsLessThan(
		this IThat<uint?> source,
		uint? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<uint>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsLessThan(
		this IThat<long?> source,
		long? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<long>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IThat<ulong?>> IsLessThan(
		this IThat<ulong?> source,
		ulong? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<ulong>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsLessThan(
		this IThat<float?> source,
		float? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<float>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsLessThan(
		this IThat<double?> source,
		double? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<double>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsLessThan(
		this IThat<decimal?> source,
		decimal? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanConstraint<decimal>(it, grammars, expected)),
			source);

	private sealed class IsLessThanConstraint<TNumber>(string it, ExpectationGrammars grammars, TNumber? expected)
		: ConstraintResult.WithEqualToValue<TNumber>(it, grammars, expected is null),
			IValueConstraint<TNumber>
		where TNumber : struct, IComparable<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber actual)
		{
			Actual = actual;
			Outcome = IsFinite(expected) && IsFinite(actual) && actual.CompareTo(expected.Value) < 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is less than ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not less than ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class NullableIsLessThanConstraint<TNumber>(
		string it,
		ExpectationGrammars grammars,
		TNumber? expected)
		: ConstraintResult.WithEqualToValue<TNumber?>(it, grammars, expected is null),
			IValueConstraint<TNumber?>
		where TNumber : struct, IComparable<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber? actual)
		{
			Actual = actual;
			Outcome = IsFinite(expected) && IsFinite(actual) && actual.Value.CompareTo(expected.Value) < 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is less than ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not less than ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
#endif
}
