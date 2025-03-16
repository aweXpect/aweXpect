using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNumber
{
	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte, IThat<byte>> IsGreaterThan(
		this IThat<byte> source,
		byte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<byte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte, IThat<sbyte>> IsGreaterThan(
		this IThat<sbyte> source,
		sbyte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<sbyte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short, IThat<short>> IsGreaterThan(
		this IThat<short> source,
		short? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<short>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort, IThat<ushort>> IsGreaterThan(
		this IThat<ushort> source,
		ushort? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<ushort>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int, IThat<int>> IsGreaterThan(
		this IThat<int> source,
		int? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<int>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint, IThat<uint>> IsGreaterThan(
		this IThat<uint> source,
		uint? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<uint>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long, IThat<long>> IsGreaterThan(
		this IThat<long> source,
		long? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<long>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong, IThat<ulong>> IsGreaterThan(
		this IThat<ulong> source,
		ulong? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<ulong>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float, IThat<float>> IsGreaterThan(
		this IThat<float> source,
		float? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<float>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double, IThat<double>> IsGreaterThan(
		this IThat<double> source,
		double? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<double>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal, IThat<decimal>> IsGreaterThan(
		this IThat<decimal> source,
		decimal? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsGreaterThanConstraint<decimal>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<byte?, IThat<byte?>> IsGreaterThan(
		this IThat<byte?> source,
		byte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<byte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<sbyte?, IThat<sbyte?>> IsGreaterThan(
		this IThat<sbyte?> source,
		sbyte? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<sbyte>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<short?, IThat<short?>> IsGreaterThan(
		this IThat<short?> source,
		short? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<short>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ushort?, IThat<ushort?>> IsGreaterThan(
		this IThat<ushort?> source,
		ushort? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<ushort>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<int?, IThat<int?>> IsGreaterThan(
		this IThat<int?> source,
		int? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<int>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<uint?, IThat<uint?>> IsGreaterThan(
		this IThat<uint?> source,
		uint? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<uint>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<long?, IThat<long?>> IsGreaterThan(
		this IThat<long?> source,
		long? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<long>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<ulong?, IThat<ulong?>> IsGreaterThan(
		this IThat<ulong?> source,
		ulong? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<ulong>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<float?, IThat<float?>> IsGreaterThan(
		this IThat<float?> source,
		float? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<float>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<double?, IThat<double?>> IsGreaterThan(
		this IThat<double?> source,
		double? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<double>(it, grammars, expected)),
			source);

	/// <summary>
	///     Verifies that the subject is greater than the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<decimal?, IThat<decimal?>> IsGreaterThan(
		this IThat<decimal?> source,
		decimal? expected)
		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsGreaterThanConstraint<decimal>(it, grammars, expected)),
			source);

	private sealed class IsGreaterThanConstraint<TNumber>(string it, ExpectationGrammars grammars, TNumber? expected)
		: ConstraintResult.WithEqualToValue<TNumber>(it, grammars, expected is null),
			IValueConstraint<TNumber>
		where TNumber : struct, IComparable<TNumber>
	{
		public ConstraintResult IsMetBy(TNumber actual)
		{
			Actual = actual;
			Outcome = IsFinite(expected) && IsFinite(actual) && actual.CompareTo(expected.Value) > 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is greater than ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not greater than ");
			Formatter.Format(stringBuilder, expected);
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}
	}

	private sealed class NullableIsGreaterThanConstraint<TNumber>(
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
			Outcome = IsFinite(expected) && IsFinite(actual) && actual.Value.CompareTo(expected.Value) > 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is greater than ");
			Formatter.Format(stringBuilder, expected);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not greater than ");
			Formatter.Format(stringBuilder, expected);
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}
	}
}
