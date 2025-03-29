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
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TNumber, IThat<TNumber>> IsLessThanOrEqualTo<TNumber>(
		this IThat<TNumber> source, TNumber? expected)
		where TNumber : struct, INumber<TNumber>
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<TNumber>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TNumber?, IThat<TNumber?>> IsLessThanOrEqualTo<TNumber>(
		this IThat<TNumber?> source, TNumber? expected)
		where TNumber : struct, INumber<TNumber>
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<TNumber>(it, grammars, expected)),
			source);

	private sealed class IsLessThanOrEqualToConstraint<TNumber>(
		string it,
		ExpectationGrammars grammars,
		TNumber? expected)
		: ConstraintResult.WithEqualToValue<TNumber>(it, grammars, expected is null),
			IValueConstraint<TNumber>
		where TNumber : struct, INumber<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber actual)
		{
			Actual = actual;
			Outcome = IsFinite(expected) && IsFinite(actual) && actual <= expected
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is less than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not less than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class NullableIsLessThanOrEqualToConstraint<TNumber>(
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
			Outcome = IsFinite(expected) && IsFinite(actual) && actual <= expected
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is less than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not less than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
#else
	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IThat<byte>> IsLessThanOrEqualTo(
		this IThat<byte> source,
		byte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<byte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsLessThanOrEqualTo(
		this IThat<sbyte> source,
		sbyte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<sbyte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsLessThanOrEqualTo(
		this IThat<short> source,
		short? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<short>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IThat<ushort>> IsLessThanOrEqualTo(
		this IThat<ushort> source,
		ushort? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<ushort>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsLessThanOrEqualTo(
		this IThat<int> source,
		int? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<int>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IThat<uint>> IsLessThanOrEqualTo(
		this IThat<uint> source,
		uint? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<uint>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsLessThanOrEqualTo(
		this IThat<long> source,
		long? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<long>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IThat<ulong>> IsLessThanOrEqualTo(
		this IThat<ulong> source,
		ulong? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<ulong>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsLessThanOrEqualTo(
		this IThat<float> source,
		float? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<float>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsLessThanOrEqualTo(
		this IThat<double> source,
		double? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<double>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsLessThanOrEqualTo(
		this IThat<decimal> source,
		decimal? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsLessThanOrEqualToConstraint<decimal>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IThat<byte?>> IsLessThanOrEqualTo(
		this IThat<byte?> source,
		byte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<byte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsLessThanOrEqualTo(
		this IThat<sbyte?> source,
		sbyte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<sbyte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsLessThanOrEqualTo(
		this IThat<short?> source,
		short? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<short>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IThat<ushort?>> IsLessThanOrEqualTo(
		this IThat<ushort?> source,
		ushort? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<ushort>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsLessThanOrEqualTo(
		this IThat<int?> source,
		int? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<int>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IThat<uint?>> IsLessThanOrEqualTo(
		this IThat<uint?> source,
		uint? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<uint>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsLessThanOrEqualTo(
		this IThat<long?> source,
		long? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<long>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IThat<ulong?>> IsLessThanOrEqualTo(
		this IThat<ulong?> source,
		ulong? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<ulong>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsLessThanOrEqualTo(
		this IThat<float?> source,
		float? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<float>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsLessThanOrEqualTo(
		this IThat<double?> source,
		double? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<double>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is less than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsLessThanOrEqualTo(
		this IThat<decimal?> source,
		decimal? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsLessThanOrEqualToConstraint<decimal>(it, grammars, expected)),
			source);

	private sealed class IsLessThanOrEqualToConstraint<TNumber>(string it, ExpectationGrammars grammars, TNumber? expected)
		: ConstraintResult.WithEqualToValue<TNumber>(it, grammars, expected is null),
			IValueConstraint<TNumber>
		where TNumber : struct, IComparable<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber actual)
		{
			Actual = actual;
			Outcome = IsFinite(expected) && IsFinite(actual) && actual.CompareTo(expected.Value) <= 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is less than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not less than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class NullableIsLessThanOrEqualToConstraint<TNumber>(
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
			Outcome = IsFinite(expected) && IsFinite(actual) && actual.Value.CompareTo(expected.Value) <= 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is less than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not less than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
#endif
}
