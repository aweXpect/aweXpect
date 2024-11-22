namespace Testably.Expectations.Tests.ThatTests.Numbers;

public sealed partial class NumberShould
{
	public sealed class BeLessThanOrEqualToTests
	{
		[Theory]
		[AutoData]
		public async Task ForByte_WhenExpectedIsNull_ShouldFail(
			byte subject)
		{
			byte? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((byte)2, (byte)1)]
		public async Task ForByte_WhenValueIsGreaterThanExpected_ShouldFail(byte subject,
			byte? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((byte)1, (byte)2)]
		[InlineData((byte)0, (byte)0)]
		public async Task ForByte_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(byte subject,
			byte? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForDecimal_WhenExpectedIsNull_ShouldFail(decimal subject)
		{
			decimal? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(2.0, 1.1)]
		[InlineData(3.03, -5.8)]
		public async Task ForDecimal_WhenValueIsGreaterThanExpected_ShouldFail(
			double subjectValue, double expectedValue)
		{
			decimal subject = new(subjectValue);
			decimal expected = new(expectedValue);

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 2.1)]
		[InlineData(0.0, 0.0)]
		public async Task ForDecimal_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			double subjectValue, double expectedValue)
		{
			decimal subject = new(subjectValue);
			decimal? expected = new(expectedValue);

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.0, 2.0F)]
		public async Task ForDouble_WhenExpectedIsLargerFloat_ShouldSucceed(double subject,
			float expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForDouble_WhenExpectedIsNull_ShouldFail(double subject)
		{
			double? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(double.NaN, 0.0)]
		[InlineData(0.0, double.NaN)]
		public async Task ForDouble_WhenSubjectOrExpectedIsNaN_ShouldFail(
			double subject, double expected)
		{
			async Task Act() => await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(2.0, 1.1)]
		[InlineData(3.03, -5.8)]
		public async Task ForDouble_WhenValueIsGreaterThanExpected_ShouldFail(
			double subject, double? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 2.1)]
		[InlineData(0.0, 0.0)]
		public async Task ForDouble_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			double subject, double? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForFloat_WhenExpectedIsNull_ShouldFail(float subject)
		{
			float? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(float.NaN, 0.0)]
		[InlineData(0.0, float.NaN)]
		public async Task ForFloat_WhenSubjectOrExpectedIsNaN_ShouldFail(
			float subject, float expected)
		{
			async Task Act() => await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)2.0, (float)1.1)]
		[InlineData((float)3.03, (float)-5.8)]
		public async Task ForFloat_WhenValueIsGreaterThanExpected_ShouldFail(
			float subject, float? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)1.1, (float)2.1)]
		[InlineData((float)0.0, (float)0.0)]
		public async Task ForFloat_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			float subject, float? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForInt_WhenExpectedIsNull_ShouldFail(
			int subject)
		{
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(-1, -2)]
		public async Task ForInt_WhenValueIsGreaterThanExpected_ShouldFail(int subject,
			int? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(0, 0)]
		public async Task ForInt_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(int subject,
			int? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1L, 2)]
		public async Task ForLong_WhenExpectedIsLargerInt_ShouldSucceed(long subject, int expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForLong_WhenExpectedIsNull_ShouldFail(
			long subject)
		{
			long? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((long)-1, (long)-2)]
		public async Task ForLong_WhenValueIsGreaterThanExpected_ShouldFail(long subject,
			long? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((long)1, (long)2)]
		[InlineData((long)0, (long)0)]
		public async Task ForLong_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(long subject,
			long? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((byte)2, (byte)1)]
		public async Task ForNullableByte_WhenValueIsGreaterThanExpected_ShouldFail(
			byte? subject, byte? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((byte)1, (byte)2)]
		[InlineData((byte)0, (byte)0)]
		public async Task ForNullableByte_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			byte? subject, byte? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableByte_WhenValueIsNull_ShouldFail(
			byte? expected)
		{
			byte? subject = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableDecimal_WhenExpectedIsNull_ShouldFail(decimal? subject)
		{
			decimal? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(2.1, 1.1)]
		[InlineData(3.03, -5.8)]
		public async Task ForNullableDecimal_WhenValueIsGreaterThanExpected_ShouldFail(
			double? subjectValue, double? expectedValue)
		{
			decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
			decimal? expected = expectedValue == null ? null : new decimal(expectedValue.Value);

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 2.1)]
		[InlineData(0.0, 0.0)]
		public async Task ForNullableDecimal_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			double? subjectValue, double? expectedValue)
		{
			decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
			decimal? expected = expectedValue == null ? null : new decimal(expectedValue.Value);

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableDouble_WhenExpectedIsNull_ShouldFail(double? subject)
		{
			double? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(2.1, 1.1)]
		[InlineData(3.03, -5.8)]
		public async Task ForNullableDouble_WhenValueIsGreaterThanExpected_ShouldFail(
			double? subject, double? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 2.1)]
		[InlineData(0.0, 0.0)]
		public async Task ForNullableDouble_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			double? subject, double? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableFloat_WhenExpectedIsNull_ShouldFail(float? subject)
		{
			float? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)2.1, (float)1.1)]
		[InlineData((float)3.03, (float)-5.8)]
		public async Task ForNullableFloat_WhenValueIsGreaterThanExpected_ShouldFail(
			float? subject, float? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)1.1, (float)2.1)]
		[InlineData((float)0.0, (float)0.0)]
		public async Task ForNullableFloat_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			float? subject, float? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(-1, -2)]
		public async Task ForNullableInt_WhenValueIsGreaterThanExpected_ShouldFail(
			int? subject, int? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(0, 0)]
		public async Task ForNullableInt_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			int? subject, int? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableInt_WhenValueIsNull_ShouldFail(
			int? expected)
		{
			int? subject = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((long)-1, (long)-2)]
		public async Task ForNullableLong_WhenValueIsGreaterThanExpected_ShouldFail(
			long? subject, long? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((long)1, (long)2)]
		[InlineData((long)0, (long)0)]
		public async Task ForNullableLong_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			long? subject, long? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableLong_WhenValueIsNull_ShouldFail(
			long? expected)
		{
			long? subject = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((sbyte)-1, (sbyte)-2)]
		public async Task ForNullableSbyte_WhenValueIsGreaterThanExpected_ShouldFail(
			sbyte? subject, sbyte? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)2)]
		[InlineData((sbyte)0, (sbyte)0)]
		public async Task ForNullableSbyte_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			sbyte? subject, sbyte? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableSbyte_WhenValueIsNull_ShouldFail(
			sbyte? expected)
		{
			sbyte? subject = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((short)-1, (short)-2)]
		public async Task ForNullableShort_WhenValueIsGreaterThanExpected_ShouldFail(
			short? subject, short? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((short)1, (short)2)]
		[InlineData((short)0, (short)0)]
		public async Task ForNullableShort_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			short? subject, short? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableShort_WhenValueIsNull_ShouldFail(
			short? expected)
		{
			short? subject = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((uint)2, (uint)1)]
		public async Task ForNullableUint_WhenValueIsGreaterThanExpected_ShouldFail(
			uint? subject, uint? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((uint)1, (uint)2)]
		[InlineData((uint)0, (uint)0)]
		public async Task ForNullableUint_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			uint? subject, uint? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUint_WhenValueIsNull_ShouldFail(
			uint? expected)
		{
			uint? subject = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((ulong)2, (ulong)1)]
		public async Task ForNullableUlong_WhenValueIsGreaterThanExpected_ShouldFail(
			ulong? subject, ulong? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ulong)1, (ulong)2)]
		[InlineData((ulong)0, (ulong)0)]
		public async Task ForNullableUlong_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			ulong? subject, ulong? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUlong_WhenValueIsNull_ShouldFail(
			ulong? expected)
		{
			ulong? subject = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((ushort)2, (ushort)1)]
		public async Task ForNullableUshort_WhenValueIsGreaterThanExpected_ShouldFail(
			ushort? subject, ushort? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ushort)1, (ushort)2)]
		[InlineData((ushort)0, (ushort)0)]
		public async Task ForNullableUshort_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			ushort? subject, ushort? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUshort_WhenValueIsNull_ShouldFail(
			ushort? expected)
		{
			ushort? subject = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
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
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((sbyte)-1, (sbyte)-2)]
		public async Task ForSbyte_WhenValueIsGreaterThanExpected_ShouldFail(sbyte subject,
			sbyte? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)2)]
		[InlineData((sbyte)0, (sbyte)0)]
		public async Task ForSbyte_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(sbyte subject,
			sbyte? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForShort_WhenExpectedIsNull_ShouldFail(
			short subject)
		{
			short? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((short)-1, (short)-2)]
		public async Task ForShort_WhenValueIsGreaterThanExpected_ShouldFail(short subject,
			short? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((short)1, (short)2)]
		[InlineData((short)0, (short)0)]
		public async Task ForShort_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(short subject,
			short? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForUint_WhenExpectedIsNull_ShouldFail(
			uint subject)
		{
			uint? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((uint)2, (uint)1)]
		public async Task ForUint_WhenValueIsGreaterThanExpected_ShouldFail(uint subject,
			uint? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((uint)1, (uint)2)]
		[InlineData((uint)0, (uint)0)]
		public async Task ForUint_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(uint subject,
			uint? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForUlong_WhenExpectedIsNull_ShouldFail(
			ulong subject)
		{
			ulong? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ulong)2, (ulong)1)]
		public async Task ForUlong_WhenValueIsGreaterThanExpected_ShouldFail(ulong subject,
			ulong? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ulong)1, (ulong)2)]
		[InlineData((ulong)0, (ulong)0)]
		public async Task ForUlong_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(ulong subject,
			ulong? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForUshort_WhenExpectedIsNull_ShouldFail(
			ushort subject)
		{
			ushort? expected = null;

			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ushort)2, (ushort)1)]
		public async Task ForUshort_WhenValueIsGreaterThanExpected_ShouldFail(ushort subject,
			ushort? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be less than or equal to {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ushort)1, (ushort)2)]
		[InlineData((ushort)0, (ushort)0)]
		public async Task ForUshort_WhenValueIsLessThanOrEqualToExpected_ShouldSucceed(
			ushort subject,
			ushort? expected)
		{
			async Task Act()
				=> await That(subject).Should().BeLessThanOrEqualTo(expected);

			await That(Act).Should().NotThrow();
		}
	}
}
