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
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<TNumber, IThat<TNumber>> IsInRange<TNumber>(
		this IThat<TNumber> source, TNumber? minimum, TNumber? maximum)
		where TNumber : struct, INumber<TNumber>
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<TNumber>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<TNumber?, IThat<TNumber?>> IsInRange<TNumber>(
		this IThat<TNumber?> source, TNumber? minimum, TNumber? maximum)
		where TNumber : struct, INumber<TNumber>
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<TNumber>(it, grammars, minimum, maximum)),
			source);

	private sealed class IsInRangeConstraint<TNumber>(
		string it,
		ExpectationGrammars grammars,
		TNumber? minimum,
		TNumber? maximum)
		: ConstraintResult.WithValue<TNumber>(grammars),
			IValueConstraint<TNumber>
		where TNumber : struct, INumber<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber actual)
		{
			Actual = actual;
			Outcome = IsFinite(minimum) && IsFinite(maximum) && IsFinite(actual) &&
			          minimum <= actual && actual <= maximum
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is in range between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not in range between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class NullableIsInRangeConstraint<TNumber>(
		string it,
		ExpectationGrammars grammars,
		TNumber? minimum,
		TNumber? maximum)
		: ConstraintResult.WithEqualToValue<TNumber?>(it, grammars, minimum is null),
			IValueConstraint<TNumber?>
		where TNumber : struct, INumber<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber? actual)
		{
			Actual = actual;
			Outcome = IsFinite(minimum) && IsFinite(maximum) && IsFinite(actual) &&
			          minimum <= actual && actual <= maximum
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is in range between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not in range between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
#else
	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<byte, IThat<byte>> IsInRange(
		this IThat<byte> source,
		byte? minimum,
		byte? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<byte>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsInRange(
		this IThat<sbyte> source,
		sbyte? minimum,
		sbyte? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<sbyte>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsInRange(
		this IThat<short> source,
		short? minimum,
		short? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<short>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<ushort, IThat<ushort>> IsInRange(
		this IThat<ushort> source,
		ushort? minimum,
		ushort? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<ushort>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsInRange(
		this IThat<int> source,
		int? minimum,
		int? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<int>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<uint, IThat<uint>> IsInRange(
		this IThat<uint> source,
		uint? minimum,
		uint? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<uint>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsInRange(
		this IThat<long> source,
		long? minimum,
		long? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<long>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<ulong, IThat<ulong>> IsInRange(
		this IThat<ulong> source,
		ulong? minimum,
		ulong? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<ulong>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsInRange(
		this IThat<float> source,
		float? minimum,
		float? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<float>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsInRange(
		this IThat<double> source,
		double? minimum,
		double? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<double>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsInRange(
		this IThat<decimal> source,
		decimal? minimum,
		decimal? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<decimal>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<byte?, IThat<byte?>> IsInRange(
		this IThat<byte?> source,
		byte? minimum,
		byte? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<byte>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsInRange(
		this IThat<sbyte?> source,
		sbyte? minimum,
		sbyte? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<sbyte>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsInRange(
		this IThat<short?> source,
		short? minimum,
		short? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<short>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IThat<ushort?>> IsInRange(
		this IThat<ushort?> source,
		ushort? minimum,
		ushort? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<ushort>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsInRange(
		this IThat<int?> source,
		int? minimum,
		int? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<int>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<uint?, IThat<uint?>> IsInRange(
		this IThat<uint?> source,
		uint? minimum,
		uint? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<uint>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsInRange(
		this IThat<long?> source,
		long? minimum,
		long? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<long>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IThat<ulong?>> IsInRange(
		this IThat<ulong?> source,
		ulong? minimum,
		ulong? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<ulong>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsInRange(
		this IThat<float?> source,
		float? minimum,
		float? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<float>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsInRange(
		this IThat<double?> source,
		double? minimum,
		double? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<double>(it, grammars, minimum, maximum)),
			source);

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />
	///     and <paramref name="maximum" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsInRange(
		this IThat<decimal?> source,
		decimal? minimum,
		decimal? maximum)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<decimal>(it, grammars, minimum, maximum)),
			source);

	private sealed class IsInRangeConstraint<TNumber>(string it, ExpectationGrammars grammars, TNumber? minimum, TNumber? maximum)
		: ConstraintResult.WithValue<TNumber>(grammars),
			IValueConstraint<TNumber>
		where TNumber : struct, IComparable<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber actual)
		{
			Actual = actual;
			Outcome = IsFinite(minimum) && IsFinite(maximum) && IsFinite(actual) &&
			          actual.CompareTo(minimum.Value) <= 0 && actual.CompareTo(maximum.Value) >= 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is in range between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not in range between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class NullableIsInRangeConstraint<TNumber>(
		string it,
		ExpectationGrammars grammars,
		TNumber? minimum,
		TNumber? maximum)
		: ConstraintResult.WithValue<TNumber?>(grammars),
			IValueConstraint<TNumber?>
		where TNumber : struct, IComparable<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber? actual)
		{
			Actual = actual;
			Outcome = IsFinite(minimum) && IsFinite(maximum) && IsFinite(actual) &&
			          actual.Value.CompareTo(minimum.Value) <= 0 && actual.Value.CompareTo(maximum.Value) >= 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is in range between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(it).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not in range between ");
			Formatter.Format(stringBuilder, minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, maximum);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
#endif
}
