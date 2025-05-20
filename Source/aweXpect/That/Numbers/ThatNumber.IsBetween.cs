using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;
#if !NET8_0_OR_GREATER
#endif

namespace aweXpect;

public static partial class ThatNumber
{
#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the subject is between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<TNumber, IThat<TNumber>>, TNumber?> IsBetween<TNumber>(
		this IThat<TNumber> source, TNumber? minimum)
		where TNumber : struct, INumber<TNumber>
		=> new(maximum => new AndOrResult<TNumber, IThat<TNumber>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<TNumber>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<TNumber?, IThat<TNumber?>>, TNumber?> IsBetween<TNumber>(
		this IThat<TNumber?> source, TNumber? minimum)
		where TNumber : struct, INumber<TNumber>
		=> new(maximum => new AndOrResult<TNumber?, IThat<TNumber?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<TNumber>(it, grammars, minimum, maximum)),
			source));
	
	/// <summary>
	///     Verifies that the subject is not between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<TNumber, IThat<TNumber>>, TNumber?> IsNotBetween<TNumber>(
		this IThat<TNumber> source, TNumber? minimum)
		where TNumber : struct, INumber<TNumber>
		=> new(maximum => new AndOrResult<TNumber, IThat<TNumber>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new IsInRangeConstraint<TNumber>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<TNumber?, IThat<TNumber?>>, TNumber?> IsNotBetween<TNumber>(
		this IThat<TNumber?> source, TNumber? minimum)
		where TNumber : struct, INumber<TNumber>
		=> new(maximum => new AndOrResult<TNumber?, IThat<TNumber?>>(
			source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
				new NullableIsInRangeConstraint<TNumber>(it, grammars, minimum, maximum).Invert()),
			source));

	private sealed class IsInRangeConstraint<TNumber> : ConstraintResult.WithValue<TNumber>,
			IValueConstraint<TNumber>
		where TNumber : struct, INumber<TNumber>
	{
		private readonly string _it;
		private readonly TNumber? _minimum;
		private readonly TNumber? _maximum;

		public IsInRangeConstraint(string it,
			ExpectationGrammars grammars,
			TNumber? minimum,
			TNumber? maximum) : base(grammars)
		{
			if (maximum < minimum)
			{
				// ReSharper disable once LocalizableElement
				throw new ArgumentOutOfRangeException(nameof(maximum),
					"The maximum must be greater than or equal to the minimum.");
			}

			_it = it;
			_minimum = minimum;
			_maximum = maximum;
		}

		public ConstraintResult IsMetBy(TNumber actual)
		{
			Actual = actual;
			Outcome = IsFinite(_minimum) && IsFinite(_maximum) && IsFinite(actual) &&
			          _minimum <= actual && actual <= _maximum
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is between ");
			Formatter.Format(stringBuilder, _minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, _maximum);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(_it).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not between ");
			Formatter.Format(stringBuilder, _minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, _maximum);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class NullableIsInRangeConstraint<TNumber> : ConstraintResult.WithEqualToValue<TNumber?>,
			IValueConstraint<TNumber?>
		where TNumber : struct, INumber<TNumber>
	{
		private readonly TNumber? _minimum;
		private readonly TNumber? _maximum;

		public NullableIsInRangeConstraint(string it,
			ExpectationGrammars grammars,
			TNumber? minimum,
			TNumber? maximum) : base(it, grammars, minimum is null)
		{
			if (maximum < minimum)
			{
				// ReSharper disable once LocalizableElement
				throw new ArgumentOutOfRangeException(nameof(maximum),
					"The maximum must be greater than or equal to the minimum.");
			}

			_minimum = minimum;
			_maximum = maximum;
		}

		public ConstraintResult IsMetBy(TNumber? actual)
		{
			Actual = actual;
			Outcome = IsFinite(_minimum) && IsFinite(_maximum) && IsFinite(actual) &&
			          _minimum <= actual && actual <= _maximum
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is between ");
			Formatter.Format(stringBuilder, _minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, _maximum);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(It).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not between ");
			Formatter.Format(stringBuilder, _minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, _maximum);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
#else
	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<byte, IThat<byte>>, byte?> IsBetween(
		this IThat<byte> source,
		byte? minimum)
		=> new(maximum => new AndOrResult<byte, IThat<byte>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<byte>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<sbyte, IThat<sbyte>>, sbyte?> IsBetween(
		this IThat<sbyte> source,
		sbyte? minimum)
		=> new(maximum => new AndOrResult<sbyte, IThat<sbyte>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<sbyte>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<short, IThat<short>>, short?> IsBetween(
		this IThat<short> source,
		short? minimum)
		=> new(maximum => new AndOrResult<short, IThat<short>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<short>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<ushort, IThat<ushort>>, ushort?> IsBetween(
		this IThat<ushort> source,
		ushort? minimum)
		=> new(maximum => new AndOrResult<ushort, IThat<ushort>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<ushort>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<int, IThat<int>>, int?> IsBetween(
		this IThat<int> source,
		int? minimum)
		=> new(maximum => new AndOrResult<int, IThat<int>>(source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=>
				new IsInRangeConstraint<int>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<uint, IThat<uint>>, uint?> IsBetween(
		this IThat<uint> source,
		uint? minimum)
		=> new(maximum => new AndOrResult<uint, IThat<uint>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<uint>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<long, IThat<long>>, long?> IsBetween(
		this IThat<long> source,
		long? minimum)
		=> new(maximum => new AndOrResult<long, IThat<long>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<long>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<ulong, IThat<ulong>>, ulong?> IsBetween(
		this IThat<ulong> source,
		ulong? minimum)
		=> new(maximum => new AndOrResult<ulong, IThat<ulong>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<ulong>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<float, IThat<float>>, float?> IsBetween(
		this IThat<float> source,
		float? minimum)
		=> new(maximum => new AndOrResult<float, IThat<float>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<float>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<double, IThat<double>>, double?> IsBetween(
		this IThat<double> source,
		double? minimum)
		=> new(maximum => new AndOrResult<double, IThat<double>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<double>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<decimal, IThat<decimal>>, decimal?> IsBetween(
		this IThat<decimal> source,
		decimal? minimum)
		=> new(maximum => new AndOrResult<decimal, IThat<decimal>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<decimal>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<byte?, IThat<byte?>>, byte?> IsBetween(
		this IThat<byte?> source,
		byte? minimum)
		=> new(maximum => new AndOrResult<byte?, IThat<byte?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<byte>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<sbyte?, IThat<sbyte?>>, sbyte?> IsBetween(
		this IThat<sbyte?> source,
		sbyte? minimum)
		=> new(maximum => new AndOrResult<sbyte?, IThat<sbyte?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<sbyte>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<short?, IThat<short?>>, short?> IsBetween(
		this IThat<short?> source,
		short? minimum)
		=> new(maximum => new AndOrResult<short?, IThat<short?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<short>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<ushort?, IThat<ushort?>>, ushort?> IsBetween(
		this IThat<ushort?> source,
		ushort? minimum)
		=> new(maximum => new AndOrResult<ushort?, IThat<ushort?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<ushort>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<int?, IThat<int?>>, int?> IsBetween(
		this IThat<int?> source,
		int? minimum)
		=> new(maximum => new AndOrResult<int?, IThat<int?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<int>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<uint?, IThat<uint?>>, uint?> IsBetween(
		this IThat<uint?> source,
		uint? minimum)
		=> new(maximum => new AndOrResult<uint?, IThat<uint?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<uint>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<long?, IThat<long?>>, long?> IsBetween(
		this IThat<long?> source,
		long? minimum)
		=> new(maximum => new AndOrResult<long?, IThat<long?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<long>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<ulong?, IThat<ulong?>>, ulong?> IsBetween(
		this IThat<ulong?> source,
		ulong? minimum)
		=> new(maximum => new AndOrResult<ulong?, IThat<ulong?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<ulong>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<float?, IThat<float?>>, float?> IsBetween(
		this IThat<float?> source,
		float? minimum)
		=> new(maximum => new AndOrResult<float?, IThat<float?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<float>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<double?, IThat<double?>>, double?> IsBetween(
		this IThat<double?> source,
		double? minimum)
		=> new(maximum => new AndOrResult<double?, IThat<double?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<double>(it, grammars, minimum, maximum)),
			source));

	/// <summary>
	///     Verifies that the subject is in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<decimal?, IThat<decimal?>>, decimal?> IsBetween(
		this IThat<decimal?> source,
		decimal? minimum)
		=> new(maximum => new AndOrResult<decimal?, IThat<decimal?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<decimal>(it, grammars, minimum, maximum)),
			source));

	
	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<byte, IThat<byte>>, byte?> IsNotBetween(
		this IThat<byte> source,
		byte? minimum)
		=> new(maximum => new AndOrResult<byte, IThat<byte>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<byte>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<sbyte, IThat<sbyte>>, sbyte?> IsNotBetween(
		this IThat<sbyte> source,
		sbyte? minimum)
		=> new(maximum => new AndOrResult<sbyte, IThat<sbyte>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<sbyte>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<short, IThat<short>>, short?> IsNotBetween(
		this IThat<short> source,
		short? minimum)
		=> new(maximum => new AndOrResult<short, IThat<short>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<short>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<ushort, IThat<ushort>>, ushort?> IsNotBetween(
		this IThat<ushort> source,
		ushort? minimum)
		=> new(maximum => new AndOrResult<ushort, IThat<ushort>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<ushort>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<int, IThat<int>>, int?> IsNotBetween(
		this IThat<int> source,
		int? minimum)
		=> new(maximum => new AndOrResult<int, IThat<int>>(source.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=>
				new IsInRangeConstraint<int>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<uint, IThat<uint>>, uint?> IsNotBetween(
		this IThat<uint> source,
		uint? minimum)
		=> new(maximum => new AndOrResult<uint, IThat<uint>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<uint>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<long, IThat<long>>, long?> IsNotBetween(
		this IThat<long> source,
		long? minimum)
		=> new(maximum => new AndOrResult<long, IThat<long>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<long>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<ulong, IThat<ulong>>, ulong?> IsNotBetween(
		this IThat<ulong> source,
		ulong? minimum)
		=> new(maximum => new AndOrResult<ulong, IThat<ulong>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<ulong>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<float, IThat<float>>, float?> IsNotBetween(
		this IThat<float> source,
		float? minimum)
		=> new(maximum => new AndOrResult<float, IThat<float>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<float>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<double, IThat<double>>, double?> IsNotBetween(
		this IThat<double> source,
		double? minimum)
		=> new(maximum => new AndOrResult<double, IThat<double>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<double>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<decimal, IThat<decimal>>, decimal?> IsNotBetween(
		this IThat<decimal> source,
		decimal? minimum)
		=> new(maximum => new AndOrResult<decimal, IThat<decimal>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new IsInRangeConstraint<decimal>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<byte?, IThat<byte?>>, byte?> IsNotBetween(
		this IThat<byte?> source,
		byte? minimum)
		=> new(maximum => new AndOrResult<byte?, IThat<byte?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<byte>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<sbyte?, IThat<sbyte?>>, sbyte?> IsNotBetween(
		this IThat<sbyte?> source,
		sbyte? minimum)
		=> new(maximum => new AndOrResult<sbyte?, IThat<sbyte?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<sbyte>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<short?, IThat<short?>>, short?> IsNotBetween(
		this IThat<short?> source,
		short? minimum)
		=> new(maximum => new AndOrResult<short?, IThat<short?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<short>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<ushort?, IThat<ushort?>>, ushort?> IsNotBetween(
		this IThat<ushort?> source,
		ushort? minimum)
		=> new(maximum => new AndOrResult<ushort?, IThat<ushort?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<ushort>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<int?, IThat<int?>>, int?> IsNotBetween(
		this IThat<int?> source,
		int? minimum)
		=> new(maximum => new AndOrResult<int?, IThat<int?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<int>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<uint?, IThat<uint?>>, uint?> IsNotBetween(
		this IThat<uint?> source,
		uint? minimum)
		=> new(maximum => new AndOrResult<uint?, IThat<uint?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<uint>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<long?, IThat<long?>>, long?> IsNotBetween(
		this IThat<long?> source,
		long? minimum)
		=> new(maximum => new AndOrResult<long?, IThat<long?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<long>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<ulong?, IThat<ulong?>>, ulong?> IsNotBetween(
		this IThat<ulong?> source,
		ulong? minimum)
		=> new(maximum => new AndOrResult<ulong?, IThat<ulong?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<ulong>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<float?, IThat<float?>>, float?> IsNotBetween(
		this IThat<float?> source,
		float? minimum)
		=> new(maximum => new AndOrResult<float?, IThat<float?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<float>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<double?, IThat<double?>>, double?> IsNotBetween(
		this IThat<double?> source,
		double? minimum)
		=> new(maximum => new AndOrResult<double?, IThat<double?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<double>(it, grammars, minimum, maximum).Invert()),
			source));

	/// <summary>
	///     Verifies that the subject is not in the range between the <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<AndOrResult<decimal?, IThat<decimal?>>, decimal?> IsNotBetween(
		this IThat<decimal?> source,
		decimal? minimum)
		=> new(maximum => new AndOrResult<decimal?, IThat<decimal?>>(source.Get().ExpectationBuilder
				.AddConstraint((it, grammars) =>
					new NullableIsInRangeConstraint<decimal>(it, grammars, minimum, maximum).Invert()),
			source));

	private sealed class IsInRangeConstraint<TNumber> : ConstraintResult.WithValue<TNumber>,
		IValueConstraint<TNumber>
		where TNumber : struct, IComparable<TNumber>
	{
		private readonly string _it;
		private readonly TNumber? _maximum;
		private readonly TNumber? _minimum;

		public IsInRangeConstraint(string it, ExpectationGrammars grammars, TNumber? minimum, TNumber? maximum) :
			base(grammars)
		{
			if (maximum != null && minimum != null &&
			    maximum.Value.CompareTo(minimum.Value) < 0)
			{
				// ReSharper disable once LocalizableElement
				throw new ArgumentOutOfRangeException(nameof(maximum),
					"The maximum must be greater than or equal to the minimum.");
			}

			_it = it;
			_minimum = minimum;
			_maximum = maximum;
		}

		public ConstraintResult IsMetBy(TNumber actual)
		{
			Actual = actual;
			Outcome = IsFinite(_minimum) && IsFinite(_maximum) && IsFinite(actual) &&
			          actual.CompareTo(_minimum.Value) >= 0 && actual.CompareTo(_maximum.Value) <= 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is between ");
			Formatter.Format(stringBuilder, _minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, _maximum);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(_it).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not between ");
			Formatter.Format(stringBuilder, _minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, _maximum);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}

	private sealed class NullableIsInRangeConstraint<TNumber> : ConstraintResult.WithValue<TNumber?>,
		IValueConstraint<TNumber?>
		where TNumber : struct, IComparable<TNumber>
	{
		private readonly string _it;
		private readonly TNumber? _maximum;
		private readonly TNumber? _minimum;

		public NullableIsInRangeConstraint(string it,
			ExpectationGrammars grammars,
			TNumber? minimum,
			TNumber? maximum) : base(grammars)
		{
			if (maximum != null && minimum != null &&
			    maximum.Value.CompareTo(minimum.Value) < 0)
			{
				// ReSharper disable once LocalizableElement
				throw new ArgumentOutOfRangeException(nameof(maximum),
					"The maximum must be greater than or equal to the minimum.");
			}

			_it = it;
			_minimum = minimum;
			_maximum = maximum;
		}

		public ConstraintResult IsMetBy(TNumber? actual)
		{
			Actual = actual;
			Outcome = IsFinite(_minimum) && IsFinite(_maximum) && IsFinite(actual) &&
			          actual.Value.CompareTo(_minimum.Value) >= 0 && actual.Value.CompareTo(_maximum.Value) <= 0
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is between ");
			Formatter.Format(stringBuilder, _minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, _maximum);
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(_it).Append(" was ");
			Formatter.Format(stringBuilder, Actual);
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append("is not between ");
			Formatter.Format(stringBuilder, _minimum);
			stringBuilder.Append(" and ");
			Formatter.Format(stringBuilder, _maximum);
		}

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> AppendNormalResult(stringBuilder, indentation);
	}
#endif
}
