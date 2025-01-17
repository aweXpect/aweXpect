﻿namespace aweXpect.Tests;

public sealed partial class ThatNumber
{
	public sealed partial class IsNot
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task ForByte_WhenUnexpectedIsNull_ShouldSucceed(
				byte subject)
			{
				byte? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((byte)1, (byte)2)]
			[InlineData((byte)1, (byte)0)]
			public async Task ForByte_WhenValueIsDifferentToUnexpected_ShouldSucceed(byte subject,
				byte? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((byte)1, (byte)1)]
			public async Task ForByte_WhenValueIsEqualToUnexpected_ShouldFail(byte subject,
				byte? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForDecimal_WhenUnexpectedIsNull_ShouldSucceed(decimal subject)
			{
				decimal? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1.1, 2.1)]
			[InlineData(1.1, 0.1)]
			public async Task ForDecimal_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				double subjectValue, double unexpectedValue)
			{
				decimal subject = new(subjectValue);
				decimal unexpected = new(unexpectedValue);

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1.1, 1.1)]
			public async Task ForDecimal_WhenValueIsEqualToUnexpected_ShouldFail(
				double subjectValue, double unexpectedValue)
			{
				decimal subject = new(subjectValue);
				decimal unexpected = new(unexpectedValue);

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForDouble_WhenSubjectAndExpectedAreNaN_ShouldFail()
			{
				double subject = double.NaN;
				double expected = double.NaN;

				async Task Act() => await That(subject).IsNot(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be equal to NaN,
					             but it was NaN
					             """);
			}

			[Theory]
			[InlineData(double.NaN, 0.0)]
			[InlineData(0.0, double.NaN)]
			public async Task ForDouble_WhenSubjectOrExpectedIsNaN_ShouldSucceed(
				double subject, double expected)
			{
				async Task Act() => await That(subject).IsNot(expected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(2.0, 2.0F)]
			public async Task ForDouble_WhenUnexpectedIsEqualFloat_ShouldFail(
				double subject, float unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForDouble_WhenUnexpectedIsNull_ShouldSucceed(double subject)
			{
				double? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1.1, 2.1)]
			[InlineData(1.1, 0.1)]
			public async Task ForDouble_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				double subject, double unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1.1, 1.1)]
			public async Task ForDouble_WhenValueIsEqualToUnexpected_ShouldFail(
				double subject, double? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForFloat_WhenSubjectAndExpectedAreNaN_ShouldFail()
			{
				float subject = float.NaN;
				float expected = float.NaN;

				async Task Act() => await That(subject).IsNot(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be equal to NaN,
					             but it was NaN
					             """);
			}

			[Theory]
			[InlineData(float.NaN, 0.0)]
			[InlineData(0.0, float.NaN)]
			public async Task ForFloat_WhenSubjectOrExpectedIsNaN_ShouldSucceed(
				float subject, float expected)
			{
				async Task Act() => await That(subject).IsNot(expected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForFloat_WhenUnexpectedIsNull_ShouldSucceed(float subject)
			{
				float? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((float)1.1, (float)2.1)]
			[InlineData((float)1.1, (float)0.1)]
			public async Task ForFloat_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				float subject, float unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((float)1.1, (float)1.1)]
			public async Task ForFloat_WhenValueIsEqualToUnexpected_ShouldFail(
				float subject, float? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForInt_WhenUnexpectedIsNull_ShouldSucceed(
				int subject)
			{
				int? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1, 2)]
			[InlineData(2, 1)]
			public async Task ForInt_WhenValueIsDifferentToUnexpected_ShouldSucceed(int subject,
				int? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1, 1)]
			public async Task ForInt_WhenValueIsEqualToUnexpected_ShouldFail(int subject,
				int? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1L, 1)]
			public async Task ForLong_WhenUnexpectedIsEqualToInt_ShouldFail(
				long subject, int unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForLong_WhenUnexpectedIsNull_ShouldSucceed(
				long subject)
			{
				long? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((long)1, (long)2)]
			[InlineData((long)1, (long)0)]
			public async Task ForLong_WhenValueIsDifferentToUnexpected_ShouldSucceed(long subject,
				long? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((long)1, (long)1)]
			public async Task ForLong_WhenValueIsEqualToUnexpected_ShouldFail(long subject,
				long? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((byte)1, (byte)2)]
			[InlineData((byte)1, (byte)0)]
			public async Task ForNullableByte_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				byte? subject, byte? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((byte)1, (byte)1)]
			public async Task ForNullableByte_WhenValueIsEqualToUnexpected_ShouldFail(
				byte? subject, byte? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableByte_WhenValueIsNull_ShouldSucceed(
				byte? unexpected)
			{
				byte? subject = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableDecimal_WhenUnexpectedIsNull_ShouldSucceed(decimal? subject)
			{
				decimal? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1.1, 2.1)]
			[InlineData(1.1, 0.1)]
			public async Task ForNullableDecimal_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				double? subjectValue, double? unexpectedValue)
			{
				decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
				decimal? unexpected =
					unexpectedValue == null ? null : new decimal(unexpectedValue.Value);

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1.1, 1.1)]
			public async Task ForNullableDecimal_WhenValueIsEqualToUnexpected_ShouldFail(
				double? subjectValue, double? unexpectedValue)
			{
				decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
				decimal? unexpected =
					unexpectedValue == null ? null : new decimal(unexpectedValue.Value);

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableDouble_WhenUnexpectedIsNull_ShouldSucceed(double? subject)
			{
				double? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1.1, 2.1)]
			[InlineData(1.1, 0.1)]
			public async Task ForNullableDouble_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				double? subject, double? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1.1, 1.1)]
			public async Task ForNullableDouble_WhenValueIsEqualToUnexpected_ShouldFail(
				double? subject, double? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableFloat_WhenUnexpectedIsNull_ShouldSucceed(float? subject)
			{
				float? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((float)1.1, (float)2.1)]
			[InlineData((float)1.1, (float)0.1)]
			public async Task ForNullableFloat_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				float? subject, float? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((float)1.1, (float)1.1)]
			public async Task ForNullableFloat_WhenValueIsEqualToUnexpected_ShouldFail(
				float? subject, float? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1, 2)]
			[InlineData(1, 0)]
			public async Task ForNullableInt_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				int? subject, int? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData(1, 1)]
			public async Task ForNullableInt_WhenValueIsEqualToUnexpected_ShouldFail(
				int? subject, int? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableInt_WhenValueIsNull_ShouldSucceed(
				int? unexpected)
			{
				int? subject = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((long)1, (long)2)]
			[InlineData((long)1, (long)0)]
			public async Task ForNullableLong_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				long? subject, long? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((long)1, (long)1)]
			public async Task ForNullableLong_WhenValueIsEqualToUnexpected_ShouldFail(
				long? subject, long? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableLong_WhenValueIsNull_ShouldSucceed(
				long? unexpected)
			{
				long? subject = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((sbyte)1, (sbyte)2)]
			[InlineData((sbyte)1, (sbyte)0)]
			public async Task ForNullableSbyte_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				sbyte? subject, sbyte? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((sbyte)1, (sbyte)1)]
			public async Task ForNullableSbyte_WhenValueIsEqualToUnexpected_ShouldFail(
				sbyte? subject, sbyte? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableSbyte_WhenValueIsNull_ShouldSucceed(
				sbyte? unexpected)
			{
				sbyte? subject = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((short)1, (short)2)]
			[InlineData((short)1, (short)0)]
			public async Task ForNullableShort_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				short? subject, short? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((short)1, (short)1)]
			public async Task ForNullableShort_WhenValueIsEqualToUnexpected_ShouldFail(
				short? subject, short? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableShort_WhenValueIsNull_ShouldSucceed(
				short? unexpected)
			{
				short? subject = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((uint)1, (uint)2)]
			[InlineData((uint)1, (uint)0)]
			public async Task ForNullableUint_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				uint? subject, uint? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((uint)1, (uint)1)]
			public async Task ForNullableUint_WhenValueIsEqualToUnexpected_ShouldFail(
				uint? subject, uint? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableUint_WhenValueIsNull_ShouldSucceed(
				uint? unexpected)
			{
				uint? subject = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((ulong)1, (ulong)2)]
			[InlineData((ulong)1, (ulong)0)]
			public async Task ForNullableUlong_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				ulong? subject, ulong? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((ulong)1, (ulong)1)]
			public async Task ForNullableUlong_WhenValueIsEqualToUnexpected_ShouldFail(
				ulong? subject, ulong? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableUlong_WhenValueIsNull_ShouldSucceed(
				ulong? unexpected)
			{
				ulong? subject = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((ushort)1, (ushort)2)]
			[InlineData((ushort)1, (ushort)0)]
			public async Task ForNullableUshort_WhenValueIsDifferentToUnexpected_ShouldSucceed(
				ushort? subject, ushort? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((ushort)1, (ushort)1)]
			public async Task ForNullableUshort_WhenValueIsEqualToUnexpected_ShouldFail(
				ushort? subject, ushort? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableUshort_WhenValueIsNull_ShouldSucceed(
				ushort? unexpected)
			{
				ushort? subject = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForSbyte_WhenUnexpectedIsNull_ShouldSucceed(
				sbyte subject)
			{
				sbyte? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((sbyte)1, (sbyte)2)]
			[InlineData((sbyte)1, (sbyte)0)]
			public async Task ForSbyte_WhenValueIsDifferentToUnexpected_ShouldSucceed(sbyte subject,
				sbyte? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((sbyte)1, (sbyte)1)]
			public async Task ForSbyte_WhenValueIsEqualToUnexpected_ShouldFail(sbyte subject,
				sbyte? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForShort_WhenUnexpectedIsNull_ShouldSucceed(
				short subject)
			{
				short? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((short)1, (short)2)]
			[InlineData((short)1, (short)0)]
			public async Task ForShort_WhenValueIsDifferentToUnexpected_ShouldSucceed(short subject,
				short? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((short)1, (short)1)]
			public async Task ForShort_WhenValueIsEqualToUnexpected_ShouldFail(short subject,
				short? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForUint_WhenUnexpectedIsNull_ShouldSucceed(
				uint subject)
			{
				uint? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((uint)1, (uint)2)]
			[InlineData((uint)1, (uint)0)]
			public async Task ForUint_WhenValueIsDifferentToUnexpected_ShouldSucceed(uint subject,
				uint? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((uint)1, (uint)1)]
			public async Task ForUint_WhenValueIsEqualToUnexpected_ShouldFail(uint subject,
				uint? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForUlong_WhenUnexpectedIsNull_ShouldSucceed(
				ulong subject)
			{
				ulong? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((ulong)1, (ulong)2)]
			[InlineData((ulong)1, (ulong)0)]
			public async Task ForUlong_WhenValueIsDifferentToUnexpected_ShouldSucceed(ulong subject,
				ulong? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((ulong)1, (ulong)1)]
			public async Task ForUlong_WhenValueIsEqualToUnexpected_ShouldFail(ulong subject,
				ulong? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForUshort_WhenUnexpectedIsNull_ShouldSucceed(
				ushort subject)
			{
				ushort? unexpected = null;

				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((ushort)1, (ushort)2)]
			[InlineData((ushort)1, (ushort)0)]
			public async Task ForUshort_WhenValueIsDifferentToUnexpected_ShouldSucceed(ushort subject,
				ushort? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[InlineData((ushort)1, (ushort)1)]
			public async Task ForUshort_WhenValueIsEqualToUnexpected_ShouldFail(ushort subject,
				ushort? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNot(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be equal to {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}