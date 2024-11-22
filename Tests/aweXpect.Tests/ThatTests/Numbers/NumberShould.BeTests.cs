namespace Testably.Expectations.Tests.ThatTests.Numbers;

public sealed partial class NumberShould
{
	public sealed partial class BeTests
	{
		[Theory]
		[AutoData]
		public async Task ForByte_WhenExpectedIsNull_ShouldFail(
			byte subject)
		{
			byte? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((byte)1, (byte)2)]
		[InlineData((byte)1, (byte)0)]
		public async Task ForByte_WhenValueIsDifferentToExpected_ShouldFail(byte subject,
			byte? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((byte)1, (byte)1)]
		public async Task ForByte_WhenValueIsEqualToExpected_ShouldSucceed(byte subject,
			byte? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForDecimal_WhenExpectedIsNull_ShouldFail(decimal subject)
		{
			decimal? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 2.1)]
		[InlineData(1.1, 0.1)]
		public async Task ForDecimal_WhenValueIsDifferentToExpected_ShouldFail(
			double subjectValue, double expectedValue)
		{
			decimal subject = new(subjectValue);
			decimal expected = new(expectedValue);

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
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
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(2.0, 2.0F)]
		public async Task ForDouble_WhenExpectedIsEqualFloat_ShouldSucceed(
			double subject, float expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForDouble_WhenExpectedIsNull_ShouldFail(double subject)
		{
			double? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task ForDouble_WhenSubjectAndExpectedAreNaN_ShouldSucceed()
		{
			double subject = double.NaN;
			double expected = double.NaN;

			async Task Act() => await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(double.NaN, 0.0)]
		[InlineData(0.0, double.NaN)]
		public async Task ForDouble_WhenSubjectOrExpectedIsNaN_ShouldFail(double subject,
			double expected)
		{
			async Task Act() => await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 2.1)]
		[InlineData(1.1, 0.1)]
		public async Task ForDouble_WhenValueIsDifferentToExpected_ShouldFail(
			double subject, double expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 1.1)]
		public async Task ForDouble_WhenValueIsEqualToExpected_ShouldSucceed(
			double subject, double? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForFloat_WhenExpectedIsNull_ShouldFail(float subject)
		{
			float? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task ForFloat_WhenSubjectAndExpectedAreNaN_ShouldSucceed()
		{
			float subject = float.NaN;
			float expected = float.NaN;

			async Task Act() => await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(float.NaN, 0.0)]
		[InlineData(0.0, float.NaN)]
		public async Task ForFloat_WhenSubjectOrExpectedIsNaN_ShouldFail(float subject,
			float expected)
		{
			async Task Act() => await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)1.1, (float)2.1)]
		[InlineData((float)1.1, (float)0.1)]
		public async Task ForFloat_WhenValueIsDifferentToExpected_ShouldFail(
			float subject, float expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)1.1, (float)1.1)]
		public async Task ForFloat_WhenValueIsEqualToExpected_ShouldSucceed(
			float subject, float? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForInt_WhenExpectedIsNull_ShouldFail(
			int subject)
		{
			int? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 1)]
		public async Task ForInt_WhenValueIsDifferentToExpected_ShouldFail(int subject,
			int? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1, 1)]
		public async Task ForInt_WhenValueIsEqualToExpected_ShouldSucceed(int subject,
			int? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1L, 1)]
		public async Task ForLong_WhenExpectedIsEqualInt_ShouldSucceed(long subject, int expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForLong_WhenExpectedIsNull_ShouldFail(
			long subject)
		{
			long? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((long)1, (long)2)]
		[InlineData((long)1, (long)0)]
		public async Task ForLong_WhenValueIsDifferentToExpected_ShouldFail(long subject,
			long? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((long)1, (long)1)]
		public async Task ForLong_WhenValueIsEqualToExpected_ShouldSucceed(long subject,
			long? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((byte)1, (byte)2)]
		[InlineData((byte)1, (byte)0)]
		public async Task ForNullableByte_WhenValueIsDifferentToExpected_ShouldFail(
			byte? subject, byte? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((byte)1, (byte)1)]
		public async Task ForNullableByte_WhenValueIsEqualToExpected_ShouldSucceed(
			byte? subject, byte? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableByte_WhenValueIsNull_ShouldFail(
			byte? expected)
		{
			byte? subject = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableDecimal_WhenExpectedIsNull_ShouldFail(decimal? subject)
		{
			decimal? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 2.1)]
		[InlineData(1.1, 0.1)]
		public async Task ForNullableDecimal_WhenValueIsDifferentToExpected_ShouldFail(
			double? subjectValue, double? expectedValue)
		{
			decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
			decimal? expected = expectedValue == null ? null : new decimal(expectedValue.Value);

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
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
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableDouble_WhenExpectedIsNull_ShouldFail(double? subject)
		{
			double? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 2.1)]
		[InlineData(1.1, 0.1)]
		public async Task ForNullableDouble_WhenValueIsDifferentToExpected_ShouldFail(
			double? subject, double? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 1.1)]
		public async Task ForNullableDouble_WhenValueIsEqualToExpected_ShouldSucceed(
			double? subject, double? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableFloat_WhenExpectedIsNull_ShouldFail(float? subject)
		{
			float? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)1.1, (float)2.1)]
		[InlineData((float)1.1, (float)0.1)]
		public async Task ForNullableFloat_WhenValueIsDifferentToExpected_ShouldFail(
			float? subject, float? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)1.1, (float)1.1)]
		public async Task ForNullableFloat_WhenValueIsEqualToExpected_ShouldSucceed(
			float? subject, float? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(1, 0)]
		public async Task ForNullableInt_WhenValueIsDifferentToExpected_ShouldFail(
			int? subject, int? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1, 1)]
		public async Task ForNullableInt_WhenValueIsEqualToExpected_ShouldSucceed(
			int? subject, int? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableInt_WhenValueIsNull_ShouldFail(
			int? expected)
		{
			int? subject = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((long)1, (long)2)]
		[InlineData((long)1, (long)0)]
		public async Task ForNullableLong_WhenValueIsDifferentToExpected_ShouldFail(
			long? subject, long? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((long)1, (long)1)]
		public async Task ForNullableLong_WhenValueIsEqualToExpected_ShouldSucceed(
			long? subject, long? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableLong_WhenValueIsNull_ShouldFail(
			long? expected)
		{
			long? subject = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)2)]
		[InlineData((sbyte)1, (sbyte)0)]
		public async Task ForNullableSbyte_WhenValueIsDifferentToExpected_ShouldFail(
			sbyte? subject, sbyte? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)1)]
		public async Task ForNullableSbyte_WhenValueIsEqualToExpected_ShouldSucceed(
			sbyte? subject, sbyte? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableSbyte_WhenValueIsNull_ShouldFail(
			sbyte? expected)
		{
			sbyte? subject = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((short)1, (short)2)]
		[InlineData((short)1, (short)0)]
		public async Task ForNullableShort_WhenValueIsDifferentToExpected_ShouldFail(
			short? subject, short? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((short)1, (short)1)]
		public async Task ForNullableShort_WhenValueIsEqualToExpected_ShouldSucceed(
			short? subject, short? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableShort_WhenValueIsNull_ShouldFail(
			short? expected)
		{
			short? subject = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((uint)1, (uint)2)]
		[InlineData((uint)1, (uint)0)]
		public async Task ForNullableUint_WhenValueIsDifferentToExpected_ShouldFail(
			uint? subject, uint? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((uint)1, (uint)1)]
		public async Task ForNullableUint_WhenValueIsEqualToExpected_ShouldSucceed(
			uint? subject, uint? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUint_WhenValueIsNull_ShouldFail(
			uint? expected)
		{
			uint? subject = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((ulong)1, (ulong)2)]
		[InlineData((ulong)1, (ulong)0)]
		public async Task ForNullableUlong_WhenValueIsDifferentToExpected_ShouldFail(
			ulong? subject, ulong? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ulong)1, (ulong)1)]
		public async Task ForNullableUlong_WhenValueIsEqualToExpected_ShouldSucceed(
			ulong? subject, ulong? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUlong_WhenValueIsNull_ShouldFail(
			ulong? expected)
		{
			ulong? subject = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((ushort)1, (ushort)2)]
		[InlineData((ushort)1, (ushort)0)]
		public async Task ForNullableUshort_WhenValueIsDifferentToExpected_ShouldFail(
			ushort? subject, ushort? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ushort)1, (ushort)1)]
		public async Task ForNullableUshort_WhenValueIsEqualToExpected_ShouldSucceed(
			ushort? subject, ushort? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUshort_WhenValueIsNull_ShouldFail(
			ushort? expected)
		{
			ushort? subject = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
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
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)2)]
		[InlineData((sbyte)1, (sbyte)0)]
		public async Task ForSbyte_WhenValueIsDifferentToExpected_ShouldFail(sbyte subject,
			sbyte? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)1)]
		public async Task ForSbyte_WhenValueIsEqualToExpected_ShouldSucceed(sbyte subject,
			sbyte? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForShort_WhenExpectedIsNull_ShouldFail(
			short subject)
		{
			short? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((short)1, (short)2)]
		[InlineData((short)1, (short)0)]
		public async Task ForShort_WhenValueIsDifferentToExpected_ShouldFail(short subject,
			short? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((short)1, (short)1)]
		public async Task ForShort_WhenValueIsEqualToExpected_ShouldSucceed(short subject,
			short? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForUint_WhenExpectedIsNull_ShouldFail(
			uint subject)
		{
			uint? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((uint)1, (uint)2)]
		[InlineData((uint)1, (uint)0)]
		public async Task ForUint_WhenValueIsDifferentToExpected_ShouldFail(uint subject,
			uint? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((uint)1, (uint)1)]
		public async Task ForUint_WhenValueIsEqualToExpected_ShouldSucceed(uint subject,
			uint? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForUlong_WhenExpectedIsNull_ShouldFail(
			ulong subject)
		{
			ulong? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ulong)1, (ulong)2)]
		[InlineData((ulong)1, (ulong)0)]
		public async Task ForUlong_WhenValueIsDifferentToExpected_ShouldFail(ulong subject,
			ulong? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ulong)1, (ulong)1)]
		public async Task ForUlong_WhenValueIsEqualToExpected_ShouldSucceed(ulong subject,
			ulong? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForUshort_WhenExpectedIsNull_ShouldFail(
			ushort subject)
		{
			ushort? expected = null;

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be <null>,
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ushort)1, (ushort)2)]
		[InlineData((ushort)1, (ushort)0)]
		public async Task ForUshort_WhenValueIsDifferentToExpected_ShouldFail(ushort subject,
			ushort? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ushort)1, (ushort)1)]
		public async Task ForUshort_WhenValueIsEqualToExpected_ShouldSucceed(ushort subject,
			ushort? expected)
		{
			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed partial class NotBeTests
	{
		[Theory]
		[AutoData]
		public async Task ForByte_WhenUnexpectedIsNull_ShouldSucceed(
			byte subject)
		{
			byte? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((byte)1, (byte)2)]
		[InlineData((byte)1, (byte)0)]
		public async Task ForByte_WhenValueIsDifferentToUnexpected_ShouldSucceed(byte subject,
			byte? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((byte)1, (byte)1)]
		public async Task ForByte_WhenValueIsEqualToUnexpected_ShouldFail(byte subject,
			byte? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForDecimal_WhenUnexpectedIsNull_ShouldSucceed(decimal subject)
		{
			decimal? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 1.1)]
		public async Task ForDecimal_WhenValueIsEqualToUnexpected_ShouldFail(
			double subjectValue, double unexpectedValue)
		{
			decimal subject = new(subjectValue);
			decimal unexpected = new(unexpectedValue);

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task ForDouble_WhenSubjectAndExpectedAreNaN_ShouldFail()
		{
			double subject = double.NaN;
			double expected = double.NaN;

			async Task Act() => await That(subject).Should().NotBe(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be NaN,
				             but it was NaN
				             """);
		}

		[Theory]
		[InlineData(double.NaN, 0.0)]
		[InlineData(0.0, double.NaN)]
		public async Task ForDouble_WhenSubjectOrExpectedIsNaN_ShouldSucceed(
			double subject, double expected)
		{
			async Task Act() => await That(subject).Should().NotBe(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(2.0, 2.0F)]
		public async Task ForDouble_WhenUnexpectedIsEqualFloat_ShouldFail(
			double subject, float unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForDouble_WhenUnexpectedIsNull_ShouldSucceed(double subject)
		{
			double? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 2.1)]
		[InlineData(1.1, 0.1)]
		public async Task ForDouble_WhenValueIsDifferentToUnexpected_ShouldSucceed(
			double subject, double unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 1.1)]
		public async Task ForDouble_WhenValueIsEqualToUnexpected_ShouldFail(
			double subject, double? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task ForFloat_WhenSubjectAndExpectedAreNaN_ShouldFail()
		{
			float subject = float.NaN;
			float expected = float.NaN;

			async Task Act() => await That(subject).Should().NotBe(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be NaN,
				             but it was NaN
				             """);
		}

		[Theory]
		[InlineData(float.NaN, 0.0)]
		[InlineData(0.0, float.NaN)]
		public async Task ForFloat_WhenSubjectOrExpectedIsNaN_ShouldSucceed(
			float subject, float expected)
		{
			async Task Act() => await That(subject).Should().NotBe(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForFloat_WhenUnexpectedIsNull_ShouldSucceed(float subject)
		{
			float? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((float)1.1, (float)2.1)]
		[InlineData((float)1.1, (float)0.1)]
		public async Task ForFloat_WhenValueIsDifferentToUnexpected_ShouldSucceed(
			float subject, float unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((float)1.1, (float)1.1)]
		public async Task ForFloat_WhenValueIsEqualToUnexpected_ShouldFail(
			float subject, float? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2, 1)]
		public async Task ForInt_WhenValueIsDifferentToUnexpected_ShouldSucceed(int subject,
			int? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, 1)]
		public async Task ForInt_WhenValueIsEqualToUnexpected_ShouldFail(int subject,
			int? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1L, 1)]
		public async Task ForLong_WhenUnexpectedIsEqualToInt_ShouldFail(
			long subject, int unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((long)1, (long)2)]
		[InlineData((long)1, (long)0)]
		public async Task ForLong_WhenValueIsDifferentToUnexpected_ShouldSucceed(long subject,
			long? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((long)1, (long)1)]
		public async Task ForLong_WhenValueIsEqualToUnexpected_ShouldFail(long subject,
			long? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((byte)1, (byte)1)]
		public async Task ForNullableByte_WhenValueIsEqualToUnexpected_ShouldFail(
			byte? subject, byte? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableDecimal_WhenUnexpectedIsNull_ShouldSucceed(decimal? subject)
		{
			decimal? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableDouble_WhenUnexpectedIsNull_ShouldSucceed(double? subject)
		{
			double? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 2.1)]
		[InlineData(1.1, 0.1)]
		public async Task ForNullableDouble_WhenValueIsDifferentToUnexpected_ShouldSucceed(
			double? subject, double? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 1.1)]
		public async Task ForNullableDouble_WhenValueIsEqualToUnexpected_ShouldFail(
			double? subject, double? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableFloat_WhenUnexpectedIsNull_ShouldSucceed(float? subject)
		{
			float? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((float)1.1, (float)2.1)]
		[InlineData((float)1.1, (float)0.1)]
		public async Task ForNullableFloat_WhenValueIsDifferentToUnexpected_ShouldSucceed(
			float? subject, float? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((float)1.1, (float)1.1)]
		public async Task ForNullableFloat_WhenValueIsEqualToUnexpected_ShouldFail(
			float? subject, float? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, 1)]
		public async Task ForNullableInt_WhenValueIsEqualToUnexpected_ShouldFail(
			int? subject, int? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((long)1, (long)2)]
		[InlineData((long)1, (long)0)]
		public async Task ForNullableLong_WhenValueIsDifferentToUnexpected_ShouldSucceed(
			long? subject, long? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((long)1, (long)1)]
		public async Task ForNullableLong_WhenValueIsEqualToUnexpected_ShouldFail(
			long? subject, long? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)2)]
		[InlineData((sbyte)1, (sbyte)0)]
		public async Task ForNullableSbyte_WhenValueIsDifferentToUnexpected_ShouldSucceed(
			sbyte? subject, sbyte? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)1)]
		public async Task ForNullableSbyte_WhenValueIsEqualToUnexpected_ShouldFail(
			sbyte? subject, sbyte? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((short)1, (short)2)]
		[InlineData((short)1, (short)0)]
		public async Task ForNullableShort_WhenValueIsDifferentToUnexpected_ShouldSucceed(
			short? subject, short? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((short)1, (short)1)]
		public async Task ForNullableShort_WhenValueIsEqualToUnexpected_ShouldFail(
			short? subject, short? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((uint)1, (uint)2)]
		[InlineData((uint)1, (uint)0)]
		public async Task ForNullableUint_WhenValueIsDifferentToUnexpected_ShouldSucceed(
			uint? subject, uint? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((uint)1, (uint)1)]
		public async Task ForNullableUint_WhenValueIsEqualToUnexpected_ShouldFail(
			uint? subject, uint? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ulong)1, (ulong)2)]
		[InlineData((ulong)1, (ulong)0)]
		public async Task ForNullableUlong_WhenValueIsDifferentToUnexpected_ShouldSucceed(
			ulong? subject, ulong? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ulong)1, (ulong)1)]
		public async Task ForNullableUlong_WhenValueIsEqualToUnexpected_ShouldFail(
			ulong? subject, ulong? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ushort)1, (ushort)2)]
		[InlineData((ushort)1, (ushort)0)]
		public async Task ForNullableUshort_WhenValueIsDifferentToUnexpected_ShouldSucceed(
			ushort? subject, ushort? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ushort)1, (ushort)1)]
		public async Task ForNullableUshort_WhenValueIsEqualToUnexpected_ShouldFail(
			ushort? subject, ushort? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForSbyte_WhenUnexpectedIsNull_ShouldSucceed(
			sbyte subject)
		{
			sbyte? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)2)]
		[InlineData((sbyte)1, (sbyte)0)]
		public async Task ForSbyte_WhenValueIsDifferentToUnexpected_ShouldSucceed(sbyte subject,
			sbyte? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)1)]
		public async Task ForSbyte_WhenValueIsEqualToUnexpected_ShouldFail(sbyte subject,
			sbyte? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((short)1, (short)2)]
		[InlineData((short)1, (short)0)]
		public async Task ForShort_WhenValueIsDifferentToUnexpected_ShouldSucceed(short subject,
			short? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((short)1, (short)1)]
		public async Task ForShort_WhenValueIsEqualToUnexpected_ShouldFail(short subject,
			short? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((uint)1, (uint)2)]
		[InlineData((uint)1, (uint)0)]
		public async Task ForUint_WhenValueIsDifferentToUnexpected_ShouldSucceed(uint subject,
			uint? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((uint)1, (uint)1)]
		public async Task ForUint_WhenValueIsEqualToUnexpected_ShouldFail(uint subject,
			uint? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ulong)1, (ulong)2)]
		[InlineData((ulong)1, (ulong)0)]
		public async Task ForUlong_WhenValueIsDifferentToUnexpected_ShouldSucceed(ulong subject,
			ulong? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ulong)1, (ulong)1)]
		public async Task ForUlong_WhenValueIsEqualToUnexpected_ShouldFail(ulong subject,
			ulong? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ushort)1, (ushort)2)]
		[InlineData((ushort)1, (ushort)0)]
		public async Task ForUshort_WhenValueIsDifferentToUnexpected_ShouldSucceed(ushort subject,
			ushort? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ushort)1, (ushort)1)]
		public async Task ForUshort_WhenValueIsEqualToUnexpected_ShouldFail(ushort subject,
			ushort? unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBe(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}
}
