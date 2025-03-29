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
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TNumber, IThat<TNumber>> IsGreaterThanOrEqualTo<TNumber>(
		this IThat<TNumber> source, TNumber? expected)
		where TNumber : struct, INumber<TNumber>
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<TNumber>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TNumber?, IThat<TNumber?>> IsGreaterThanOrEqualTo<TNumber>(
		this IThat<TNumber?> source, TNumber? expected)
		where TNumber : struct, INumber<TNumber>
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<TNumber>(it, grammars, expected)),
			source);

	private sealed class IsGreaterThanOrEqualToConstraint<TNumber>(
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
			Outcome = IsFinite(expected) && IsFinite(actual) && actual >= expected
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is greater than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not greater than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class NullableIsGreaterThanOrEqualToConstraint<TNumber>(
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
			Outcome = IsFinite(expected) && IsFinite(actual) && actual >= expected
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is greater than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not greater than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
#else
	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IThat<byte>> IsGreaterThanOrEqualTo(
		this IThat<byte> source,
		byte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<byte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsGreaterThanOrEqualTo(
		this IThat<sbyte> source,
		sbyte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<sbyte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsGreaterThanOrEqualTo(
		this IThat<short> source,
		short? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<short>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IThat<ushort>> IsGreaterThanOrEqualTo(
		this IThat<ushort> source,
		ushort? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<ushort>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsGreaterThanOrEqualTo(
		this IThat<int> source,
		int? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<int>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IThat<uint>> IsGreaterThanOrEqualTo(
		this IThat<uint> source,
		uint? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<uint>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsGreaterThanOrEqualTo(
		this IThat<long> source,
		long? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<long>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IThat<ulong>> IsGreaterThanOrEqualTo(
		this IThat<ulong> source,
		ulong? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<ulong>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsGreaterThanOrEqualTo(
		this IThat<float> source,
		float? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<float>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsGreaterThanOrEqualTo(
		this IThat<double> source,
		double? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<double>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsGreaterThanOrEqualTo(
		this IThat<decimal> source,
		decimal? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanOrEqualToConstraint<decimal>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IThat<byte?>> IsGreaterThanOrEqualTo(
		this IThat<byte?> source,
		byte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<byte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsGreaterThanOrEqualTo(
		this IThat<sbyte?> source,
		sbyte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<sbyte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsGreaterThanOrEqualTo(
		this IThat<short?> source,
		short? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<short>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IThat<ushort?>> IsGreaterThanOrEqualTo(
		this IThat<ushort?> source,
		ushort? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<ushort>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsGreaterThanOrEqualTo(
		this IThat<int?> source,
		int? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<int>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IThat<uint?>> IsGreaterThanOrEqualTo(
		this IThat<uint?> source,
		uint? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<uint>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsGreaterThanOrEqualTo(
		this IThat<long?> source,
		long? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<long>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IThat<ulong?>> IsGreaterThanOrEqualTo(
		this IThat<ulong?> source,
		ulong? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<ulong>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsGreaterThanOrEqualTo(
		this IThat<float?> source,
		float? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<float>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsGreaterThanOrEqualTo(
		this IThat<double?> source,
		double? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<double>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than or equal to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsGreaterThanOrEqualTo(
		this IThat<decimal?> source,
		decimal? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanOrEqualToConstraint<decimal>(it, grammars, expected)),
			source);

	private sealed class IsGreaterThanOrEqualToConstraint<TNumber>(string it, ExpectationGrammars grammars, TNumber? expected)
		: ConstraintResult.WithEqualToValue<TNumber>(it, grammars, expected is null),
			IValueConstraint<TNumber>
		where TNumber : struct, IComparable<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber actual)
		{
			Actual = actual;
			Outcome = IsFinite(expected) && IsFinite(actual) && actual.CompareTo(expected.Value) >= 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is greater than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not greater than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class NullableIsGreaterThanOrEqualToConstraint<TNumber>(
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
			Outcome = IsFinite(expected) && IsFinite(actual) && actual.Value.CompareTo(expected.Value) >= 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is greater than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not greater than or equal to ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
#endif
}
