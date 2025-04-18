﻿namespace aweXpect.Tests;

public sealed partial class ThatNumber
{
	public sealed partial class IsEqualTo
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task ForByte_WhenExpectedIsNull_ShouldFail(
				byte subject)
			{
				byte? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((byte)1, (byte)2)]
			[InlineData((byte)1, (byte)0)]
			public async Task ForByte_WhenValueIsDifferentFromExpected_ShouldFail(byte subject,
				byte? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((byte)1, (byte)1)]
			public async Task ForByte_WhenValueIsEqualToExpected_ShouldSucceed(byte subject,
				byte? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForDecimal_WhenExpectedIsNull_ShouldFail(decimal subject)
			{
				decimal? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1.1, 2.1)]
			[InlineData(1.1, 0.1)]
			public async Task ForDecimal_WhenValueIsDifferentFromExpected_ShouldFail(
				double subjectValue, double expectedValue)
			{
				decimal subject = new(subjectValue);
				decimal expected = new(expectedValue);

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1.1, 1.1)]
			public async Task ForDecimal_WhenValueIsEqualToExpected_ShouldSucceed(
				double subjectValue, double expectedValue)
			{
				decimal subject = new(subjectValue);
				decimal? expected = new(expectedValue);

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(2.0, 2.0F)]
			public async Task ForDouble_WhenExpectedIsEqualFloat_ShouldSucceed(
				double subject, float expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForDouble_WhenExpectedIsNull_ShouldFail(double subject)
			{
				double? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForDouble_WhenSubjectAndExpectedAreNaN_ShouldSucceed()
			{
				double subject = double.NaN;
				double expected = double.NaN;

				async Task Act() => await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(double.NaN, 0.0)]
			[InlineData(0.0, double.NaN)]
			public async Task ForDouble_WhenSubjectOrExpectedIsNaN_ShouldFail(double subject,
				double expected)
			{
				async Task Act() => await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1.1, 2.1)]
			[InlineData(1.1, 0.1)]
			public async Task ForDouble_WhenValueIsDifferentFromExpected_ShouldFail(
				double subject, double expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1.1, 1.1)]
			public async Task ForDouble_WhenValueIsEqualToExpected_ShouldSucceed(
				double subject, double? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForFloat_WhenExpectedIsNull_ShouldFail(float subject)
			{
				float? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task ForFloat_WhenSubjectAndExpectedAreNaN_ShouldSucceed()
			{
				float subject = float.NaN;
				float expected = float.NaN;

				async Task Act() => await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(float.NaN, 0.0)]
			[InlineData(0.0, float.NaN)]
			public async Task ForFloat_WhenSubjectOrExpectedIsNaN_ShouldFail(float subject,
				float expected)
			{
				async Task Act() => await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((float)1.1, (float)2.1)]
			[InlineData((float)1.1, (float)0.1)]
			public async Task ForFloat_WhenValueIsDifferentFromExpected_ShouldFail(
				float subject, float expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((float)1.1, (float)1.1)]
			public async Task ForFloat_WhenValueIsEqualToExpected_ShouldSucceed(
				float subject, float? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForInt_WhenExpectedIsNull_ShouldFail(
				int subject)
			{
				int? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1, 2)]
			[InlineData(2, 1)]
			public async Task ForInt_WhenValueIsDifferentFromExpected_ShouldFail(int subject,
				int? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1, 1)]
			public async Task ForInt_WhenValueIsEqualToExpected_ShouldSucceed(int subject,
				int? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
#if NET8_0_OR_GREATER
			[Theory]
			[AutoData]
			public async Task ForInt128_WhenExpectedIsNull_ShouldFail(int subjectValue)
			{
				Int128 subject = subjectValue;
				Int128? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}
#endif

#if NET8_0_OR_GREATER
			[Theory]
			[InlineData(1, 2)]
			[InlineData(2, 1)]
			public async Task ForInt128_WhenValueIsDifferentFromExpected_ShouldFail(
				int subjectValue, int expectedValue)
			{
				Int128 subject = subjectValue;
				Int128? expected = expectedValue;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
#endif

#if NET8_0_OR_GREATER
			[Theory]
			[InlineData(1, 1)]
			public async Task ForInt128_WhenValueIsEqualToExpected_ShouldSucceed(
				int subjectValue, int expectedValue)
			{
				Int128 subject = subjectValue;
				Int128? expected = expectedValue;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
#endif

			[Theory]
			[InlineData(1L, 1)]
			public async Task ForLong_WhenExpectedIsEqualInt_ShouldSucceed(long subject, int expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForLong_WhenExpectedIsNull_ShouldFail(
				long subject)
			{
				long? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((long)1, (long)2)]
			[InlineData((long)1, (long)0)]
			public async Task ForLong_WhenValueIsDifferentFromExpected_ShouldFail(long subject,
				long? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((long)1, (long)1)]
			public async Task ForLong_WhenValueIsEqualToExpected_ShouldSucceed(long subject,
				long? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData((byte)1, (byte)2)]
			[InlineData((byte)1, (byte)0)]
			public async Task ForNullableByte_WhenValueIsDifferentFromExpected_ShouldFail(
				byte? subject, byte? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((byte)1, (byte)1)]
			public async Task ForNullableByte_WhenValueIsEqualToExpected_ShouldSucceed(
				byte? subject, byte? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableByte_WhenValueIsNull_ShouldFail(
				byte? expected)
			{
				byte? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was <null>
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForNullableDecimal_WhenExpectedIsNull_ShouldFail(decimal? subject)
			{
				decimal? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1.1, 2.1)]
			[InlineData(1.1, 0.1)]
			public async Task ForNullableDecimal_WhenValueIsDifferentFromExpected_ShouldFail(
				double? subjectValue, double? expectedValue)
			{
				decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
				decimal? expected = expectedValue == null ? null : new decimal(expectedValue.Value);

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1.1, 1.1)]
			public async Task ForNullableDecimal_WhenValueIsEqualToExpected_ShouldSucceed(
				double? subjectValue, double? expectedValue)
			{
				decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
				decimal? expected = expectedValue == null ? null : new decimal(expectedValue.Value);

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableDouble_WhenExpectedIsNull_ShouldFail(double? subject)
			{
				double? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1.1, 2.1)]
			[InlineData(1.1, 0.1)]
			public async Task ForNullableDouble_WhenValueIsDifferentFromExpected_ShouldFail(
				double? subject, double? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1.1, 1.1)]
			public async Task ForNullableDouble_WhenValueIsEqualToExpected_ShouldSucceed(
				double? subject, double? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableFloat_WhenExpectedIsNull_ShouldFail(float? subject)
			{
				float? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((float)1.1, (float)2.1)]
			[InlineData((float)1.1, (float)0.1)]
			public async Task ForNullableFloat_WhenValueIsDifferentFromExpected_ShouldFail(
				float? subject, float? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((float)1.1, (float)1.1)]
			public async Task ForNullableFloat_WhenValueIsEqualToExpected_ShouldSucceed(
				float? subject, float? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(1, 2)]
			[InlineData(1, 0)]
			public async Task ForNullableInt_WhenValueIsDifferentFromExpected_ShouldFail(
				int? subject, int? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData(1, 1)]
			public async Task ForNullableInt_WhenValueIsEqualToExpected_ShouldSucceed(
				int? subject, int? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableInt_WhenValueIsNull_ShouldFail(
				int? expected)
			{
				int? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was <null>
					              """);
			}

#if NET8_0_OR_GREATER
			[Theory]
			[AutoData]
			public async Task ForNullableInt128_WhenExpectedIsNull_ShouldFail(int subjectValue)
			{
				Int128 subject = subjectValue;
				Int128? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}
#endif

#if NET8_0_OR_GREATER
			[Theory]
			[InlineData(1, 2)]
			[InlineData(2, 1)]
			public async Task ForNullableInt128_WhenValueIsDifferentFromExpected_ShouldFail(
				int subjectValue, int expectedValue)
			{
				Int128 subject = subjectValue;
				Int128? expected = expectedValue;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
#endif

#if NET8_0_OR_GREATER
			[Theory]
			[InlineData(1, 1)]
			public async Task ForNullableInt128_WhenValueIsEqualToExpected_ShouldSucceed(
				int subjectValue, int expectedValue)
			{
				Int128 subject = subjectValue;
				Int128? expected = expectedValue;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
#endif

			[Theory]
			[InlineData((long)1, (long)2)]
			[InlineData((long)1, (long)0)]
			public async Task ForNullableLong_WhenValueIsDifferentFromExpected_ShouldFail(
				long? subject, long? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((long)1, (long)1)]
			public async Task ForNullableLong_WhenValueIsEqualToExpected_ShouldSucceed(
				long? subject, long? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableLong_WhenValueIsNull_ShouldFail(
				long? expected)
			{
				long? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was <null>
					              """);
			}

			[Theory]
			[InlineData((sbyte)1, (sbyte)2)]
			[InlineData((sbyte)1, (sbyte)0)]
			public async Task ForNullableSbyte_WhenValueIsDifferentFromExpected_ShouldFail(
				sbyte? subject, sbyte? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((sbyte)1, (sbyte)1)]
			public async Task ForNullableSbyte_WhenValueIsEqualToExpected_ShouldSucceed(
				sbyte? subject, sbyte? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableSbyte_WhenValueIsNull_ShouldFail(
				sbyte? expected)
			{
				sbyte? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was <null>
					              """);
			}

			[Theory]
			[InlineData((short)1, (short)2)]
			[InlineData((short)1, (short)0)]
			public async Task ForNullableShort_WhenValueIsDifferentFromExpected_ShouldFail(
				short? subject, short? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((short)1, (short)1)]
			public async Task ForNullableShort_WhenValueIsEqualToExpected_ShouldSucceed(
				short? subject, short? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableShort_WhenValueIsNull_ShouldFail(
				short? expected)
			{
				short? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was <null>
					              """);
			}

			[Theory]
			[InlineData((uint)1, (uint)2)]
			[InlineData((uint)1, (uint)0)]
			public async Task ForNullableUint_WhenValueIsDifferentFromExpected_ShouldFail(
				uint? subject, uint? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((uint)1, (uint)1)]
			public async Task ForNullableUint_WhenValueIsEqualToExpected_ShouldSucceed(
				uint? subject, uint? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableUint_WhenValueIsNull_ShouldFail(
				uint? expected)
			{
				uint? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was <null>
					              """);
			}

			[Theory]
			[InlineData((ulong)1, (ulong)2)]
			[InlineData((ulong)1, (ulong)0)]
			public async Task ForNullableUlong_WhenValueIsDifferentFromExpected_ShouldFail(
				ulong? subject, ulong? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((ulong)1, (ulong)1)]
			public async Task ForNullableUlong_WhenValueIsEqualToExpected_ShouldSucceed(
				ulong? subject, ulong? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableUlong_WhenValueIsNull_ShouldFail(
				ulong? expected)
			{
				ulong? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was <null>
					              """);
			}

			[Theory]
			[InlineData((ushort)1, (ushort)2)]
			[InlineData((ushort)1, (ushort)0)]
			public async Task ForNullableUshort_WhenValueIsDifferentFromExpected_ShouldFail(
				ushort? subject, ushort? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((ushort)1, (ushort)1)]
			public async Task ForNullableUshort_WhenValueIsEqualToExpected_ShouldSucceed(
				ushort? subject, ushort? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForNullableUshort_WhenValueIsNull_ShouldFail(
				ushort? expected)
			{
				ushort? subject = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was <null>
					              """);
			}

			[Theory]
			[AutoData]
			public async Task ForSbyte_WhenExpectedIsNull_ShouldFail(
				sbyte subject)
			{
				sbyte? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((sbyte)1, (sbyte)2)]
			[InlineData((sbyte)1, (sbyte)0)]
			public async Task ForSbyte_WhenValueIsDifferentFromExpected_ShouldFail(sbyte subject,
				sbyte? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((sbyte)1, (sbyte)1)]
			public async Task ForSbyte_WhenValueIsEqualToExpected_ShouldSucceed(sbyte subject,
				sbyte? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForShort_WhenExpectedIsNull_ShouldFail(
				short subject)
			{
				short? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((short)1, (short)2)]
			[InlineData((short)1, (short)0)]
			public async Task ForShort_WhenValueIsDifferentFromExpected_ShouldFail(short subject,
				short? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((short)1, (short)1)]
			public async Task ForShort_WhenValueIsEqualToExpected_ShouldSucceed(short subject,
				short? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForUint_WhenExpectedIsNull_ShouldFail(
				uint subject)
			{
				uint? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((uint)1, (uint)2)]
			[InlineData((uint)1, (uint)0)]
			public async Task ForUint_WhenValueIsDifferentFromExpected_ShouldFail(uint subject,
				uint? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((uint)1, (uint)1)]
			public async Task ForUint_WhenValueIsEqualToExpected_ShouldSucceed(uint subject,
				uint? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForUlong_WhenExpectedIsNull_ShouldFail(
				ulong subject)
			{
				ulong? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((ulong)1, (ulong)2)]
			[InlineData((ulong)1, (ulong)0)]
			public async Task ForUlong_WhenValueIsDifferentFromExpected_ShouldFail(ulong subject,
				ulong? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((ulong)1, (ulong)1)]
			public async Task ForUlong_WhenValueIsEqualToExpected_ShouldSucceed(ulong subject,
				ulong? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForUshort_WhenExpectedIsNull_ShouldFail(
				ushort subject)
			{
				ushort? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to <null>,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((ushort)1, (ushort)2)]
			[InlineData((ushort)1, (ushort)0)]
			public async Task ForUshort_WhenValueIsDifferentFromExpected_ShouldFail(ushort subject,
				ushort? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData((ushort)1, (ushort)1)]
			public async Task ForUshort_WhenValueIsEqualToExpected_ShouldSucceed(ushort subject,
				ushort? expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
