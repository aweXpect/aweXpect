using System.Linq;

namespace Testably.Expectations.Tests.ThatTests.Numbers;

public sealed partial class NumberShould
{
	public sealed partial class BeOneOfTests
	{
		[Theory]
		[InlineData((byte)1, (byte)2, (byte)3)]
		[InlineData((byte)1, (byte)0, (byte)3)]
		public async Task ForByte_WhenValueIsDifferentToAllExpected_ShouldFail(byte subject,
			params byte?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((byte)1, (byte)0, (byte)1, (byte)3)]
		public async Task ForByte_WhenValueIsEqualToAnyExpected_ShouldSucceed(byte subject,
			params byte?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 2.1, 3.1)]
		[InlineData(1.1, 0.1, 3.1)]
		public async Task ForDecimal_WhenValueIsDifferentToAllExpected_ShouldFail(
			double subjectValue, params double[] expectedValues)
		{
			decimal subject = new(subjectValue);
			decimal[] expected = expectedValues
				.Select(expectedValue => new decimal(expectedValue))
				.ToArray();

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 0.1, 1.1, 3.1)]
		public async Task ForDecimal_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			double subjectValue, params double[] expectedValues)
		{
			decimal subject = new(subjectValue);
			decimal[] expected = expectedValues
				.Select(expectedValue => new decimal(expectedValue))
				.ToArray();

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(2.0, 2.0F)]
		public async Task ForDouble_WhenExpectedIsEqualFloat_ShouldSucceed(
			double subject, float expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForDouble_WhenSubjectAndExpectedAreNaN_ShouldSucceed()
		{
			double subject = double.NaN;
			double expected = double.NaN;

			async Task Act() => await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(double.NaN, 0.0, 1.0)]
		[InlineData(0.0, 1.0, double.NaN)]
		public async Task ForDouble_WhenSubjectOrExpectedIsNaN_ShouldFail(double subject,
			params double[] expected)
		{
			async Task Act() => await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 2.1, 3.1)]
		[InlineData(1.1, 0.1, 3.1)]
		public async Task ForDouble_WhenValueIsDifferentToAllExpected_ShouldFail(
			double subject, params double[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 0.1, 1.1, 3.1)]
		public async Task ForDouble_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			double subject, params double?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task ForFloat_WhenSubjectAndExpectedAreNaN_ShouldSucceed()
		{
			float subject = float.NaN;
			float expected = float.NaN;

			async Task Act() => await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(float.NaN, 0.0F, 1.0F)]
		[InlineData(0.0F, 1.0F, float.NaN)]
		public async Task ForFloat_WhenSubjectOrExpectedIsNaN_ShouldFail(float subject,
			params float[] expected)
		{
			async Task Act() => await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)1.1, (float)2.1, (float)3.1)]
		[InlineData((float)1.1, (float)0.1, (float)3.1)]
		public async Task ForFloat_WhenValueIsDifferentToAllExpected_ShouldFail(
			float subject, params float[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)1.1, (float)0.1, (float)1.1, (float)3.1)]
		public async Task ForFloat_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			float subject, params float?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, 2, 3)]
		[InlineData(2, 1, 3)]
		public async Task ForInt_WhenValueIsDifferentToAllExpected_ShouldFail(int subject,
			params int?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1, 0, 1, 3)]
		public async Task ForInt_WhenValueIsEqualToAnyExpected_ShouldSucceed(int subject,
			params int?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1L, 1)]
		public async Task ForLong_WhenExpectedIsEqualInt_ShouldSucceed(long subject, int expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((long)1, (long)2, (long)3)]
		[InlineData((long)1, (long)0, (long)3)]
		public async Task ForLong_WhenValueIsDifferentToAllExpected_ShouldFail(long subject,
			params long?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((long)1, (long)0, (long)1, (long)3)]
		public async Task ForLong_WhenValueIsEqualToAnyExpected_ShouldSucceed(long subject,
			params long?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((byte)1, (byte)2, (byte)3)]
		[InlineData((byte)1, (byte)0, (byte)3)]
		public async Task ForNullableByte_WhenValueIsDifferentToAllExpected_ShouldFail(
			byte? subject, params byte?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((byte)1, (byte)0, (byte)1, (byte)3)]
		public async Task ForNullableByte_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			byte? subject, params byte?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableByte_WhenValueIsNull_ShouldFail(
			params byte?[] expected)
		{
			byte? subject = null;

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData(1.1, 2.1, 3.1)]
		[InlineData(1.1, 0.1, 3.1)]
		public async Task ForNullableDecimal_WhenValueIsDifferentToAllExpected_ShouldFail(
			double? subjectValue, params double?[] expectedValues)
		{
			decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
			decimal?[] expected = expectedValues
				.Select(expectedValue => expectedValue == null
					? (decimal?)null
					: new decimal(expectedValue.Value))
				.ToArray();

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 0.1, 1.1, 3.1)]
		public async Task ForNullableDecimal_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			double? subjectValue, params double?[] expectedValues)
		{
			decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
			decimal?[] expected = expectedValues
				.Select(expectedValue => expectedValue == null
					? (decimal?)null
					: new decimal(expectedValue.Value))
				.ToArray();

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 2.1, 3.1)]
		[InlineData(1.1, 0.1, 3.1)]
		public async Task ForNullableDouble_WhenValueIsDifferentToAllExpected_ShouldFail(
			double? subject, params double?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1.1, 0.1, 1.1, 3.1)]
		public async Task ForNullableDouble_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			double? subject, params double?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((float)1.1, (float)2.1, (float)3.1)]
		[InlineData((float)1.1, (float)0.1, (float)3.1)]
		public async Task ForNullableFloat_WhenValueIsDifferentToAllExpected_ShouldFail(
			float? subject, params float?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((float)1.1, (float)0.1, (float)1.1, (float)3.1)]
		public async Task ForNullableFloat_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			float? subject, params float?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, 2, 3)]
		[InlineData(1, 0, 3)]
		public async Task ForNullableInt_WhenValueIsDifferentToAllExpected_ShouldFail(
			int? subject, params int?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1, 0, 1, 3)]
		public async Task ForNullableInt_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			int? subject, params int?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableInt_WhenValueIsNull_ShouldFail(
			params int?[] expected)
		{
			int? subject = null;

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((long)1, (long)2, (long)3)]
		[InlineData((long)1, (long)0, (long)3)]
		public async Task ForNullableLong_WhenValueIsDifferentToAllExpected_ShouldFail(
			long? subject, params long?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((long)1, (long)0, (long)1, (long)3)]
		public async Task ForNullableLong_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			long? subject, params long?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableLong_WhenValueIsNull_ShouldFail(
			params long?[] expected)
		{
			long? subject = null;

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)2, (sbyte)3)]
		[InlineData((sbyte)1, (sbyte)0, (sbyte)3)]
		public async Task ForNullableSbyte_WhenValueIsDifferentToAllExpected_ShouldFail(
			sbyte? subject, params sbyte?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)0, (sbyte)1, (sbyte)3)]
		public async Task ForNullableSbyte_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			sbyte? subject, params sbyte?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableSbyte_WhenValueIsNull_ShouldFail(
			params sbyte?[] expected)
		{
			sbyte? subject = null;

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((short)1, (short)2, (short)3)]
		[InlineData((short)1, (short)0, (short)3)]
		public async Task ForNullableShort_WhenValueIsDifferentToAllExpected_ShouldFail(
			short? subject, params short?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((short)1, (short)0, (short)1, (short)3)]
		public async Task ForNullableShort_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			short? subject, params short?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableShort_WhenValueIsNull_ShouldFail(
			params short?[] expected)
		{
			short? subject = null;

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((uint)1, (uint)2, (uint)3)]
		[InlineData((uint)1, (uint)0, (uint)3)]
		public async Task ForNullableUint_WhenValueIsDifferentToAllExpected_ShouldFail(
			uint? subject, params uint?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((uint)1, (uint)0, (uint)1, (uint)3)]
		public async Task ForNullableUint_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			uint? subject, params uint?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUint_WhenValueIsNull_ShouldFail(
			params uint?[] expected)
		{
			uint? subject = null;

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((ulong)1, (ulong)2, (ulong)3)]
		[InlineData((ulong)1, (ulong)0, (ulong)3)]
		public async Task ForNullableUlong_WhenValueIsDifferentToAllExpected_ShouldFail(
			ulong? subject, params ulong?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ulong)1, (ulong)0, (ulong)1, (ulong)3)]
		public async Task ForNullableUlong_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			ulong? subject, params ulong?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUlong_WhenValueIsNull_ShouldFail(
			params ulong?[] expected)
		{
			ulong? subject = null;

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((ushort)1, (ushort)2, (ushort)3)]
		[InlineData((ushort)1, (ushort)0, (ushort)3)]
		public async Task ForNullableUshort_WhenValueIsDifferentToAllExpected_ShouldFail(
			ushort? subject, params ushort?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ushort)1, (ushort)0, (ushort)1, (ushort)3)]
		public async Task ForNullableUshort_WhenValueIsEqualToAnyExpected_ShouldSucceed(
			ushort? subject, params ushort?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUshort_WhenValueIsNull_ShouldFail(
			params ushort?[] expected)
		{
			ushort? subject = null;

			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was <null>
				              """);
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)2, (sbyte)3)]
		[InlineData((sbyte)1, (sbyte)0, (sbyte)3)]
		public async Task ForSbyte_WhenValueIsDifferentToAllExpected_ShouldFail(sbyte subject,
			params sbyte?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)0, (sbyte)1, (sbyte)3)]
		public async Task ForSbyte_WhenValueIsEqualToAnyExpected_ShouldSucceed(sbyte subject,
			params sbyte?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((short)1, (short)2, (short)3)]
		[InlineData((short)1, (short)0, (short)3)]
		public async Task ForShort_WhenValueIsDifferentToAllExpected_ShouldFail(short subject,
			params short?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((short)1, (short)0, (short)1, (short)3)]
		public async Task ForShort_WhenValueIsEqualToAnyExpected_ShouldSucceed(short subject,
			params short?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((uint)1, (uint)2, (uint)3)]
		[InlineData((uint)1, (uint)0, (uint)3)]
		public async Task ForUint_WhenValueIsDifferentToAllExpected_ShouldFail(uint subject,
			params uint?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((uint)1, (uint)0, (uint)1, (uint)3)]
		public async Task ForUint_WhenValueIsEqualToAnyExpected_ShouldSucceed(uint subject,
			params uint?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ulong)1, (ulong)2, (ulong)3)]
		[InlineData((ulong)1, (ulong)0, (ulong)3)]
		public async Task ForUlong_WhenValueIsDifferentToAllExpected_ShouldFail(ulong subject,
			params ulong?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ulong)1, (ulong)0, (ulong)1, (ulong)3)]
		public async Task ForUlong_WhenValueIsEqualToAnyExpected_ShouldSucceed(ulong subject,
			params ulong?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ushort)1, (ushort)2, (ushort)3)]
		[InlineData((ushort)1, (ushort)0, (ushort)3)]
		public async Task ForUshort_WhenValueIsDifferentToAllExpected_ShouldFail(ushort subject,
			params ushort?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              be one of {Formatter.Format(expected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((ushort)1, (ushort)0, (ushort)1, (ushort)3)]
		public async Task ForUshort_WhenValueIsEqualToAnyExpected_ShouldSucceed(ushort subject,
			params ushort?[] expected)
		{
			async Task Act()
				=> await That(subject).Should().BeOneOf(expected);

			await That(Act).Should().NotThrow();
		}
	}

	public sealed partial class NotBeOneOfTests
	{
		[Theory]
		[AutoData]
		public async Task ForByte_WhenUnexpectedIsNull_ShouldSucceed(
			byte subject)
		{
			byte? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((byte)1, (byte)2, (byte)3)]
		[InlineData((byte)1, (byte)0, (byte)3)]
		public async Task ForByte_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(byte subject,
			params byte?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((byte)1, (byte)0, (byte)1, (byte)3)]
		public async Task ForByte_WhenValueIsEqualToAnyUnexpected_ShouldFail(byte subject,
			params byte?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForDecimal_WhenUnexpectedIsNull_ShouldSucceed(decimal subject)
		{
			decimal? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 2.1, 3.1)]
		[InlineData(1.1, 0.1, 3.1)]
		public async Task ForDecimal_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			double subjectValue, params double[] unexpectedValues)
		{
			decimal subject = new(subjectValue);
			decimal[] unexpected = unexpectedValues
				.Select(unexpectedValue => new decimal(unexpectedValue))
				.ToArray();

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 0.1, 1.1, 3.1)]
		public async Task ForDecimal_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			double subjectValue, params double[] unexpectedValues)
		{
			decimal subject = new(subjectValue);
			decimal[] unexpected = unexpectedValues
				.Select(unexpectedValue => new decimal(unexpectedValue))
				.ToArray();

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task ForDouble_WhenSubjectAndExpectedAreNaN_ShouldFail()
		{
			double subject = double.NaN;
			double[] expected = [double.NaN];

			async Task Act() => await That(subject).Should().NotBeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be one of [NaN],
				             but it was NaN
				             """);
		}

		[Theory]
		[InlineData(double.NaN, 0.0, 1.0)]
		[InlineData(0.0, 1.0, double.NaN)]
		public async Task ForDouble_WhenSubjectOrExpectedIsNaN_ShouldSucceed(
			double subject, params double[] expected)
		{
			async Task Act() => await That(subject).Should().NotBeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(2.0, 2.0F)]
		public async Task ForDouble_WhenUnexpectedIsEqualFloat_ShouldFail(
			double subject, float unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of [{Formatter.Format(unexpected)}],
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForDouble_WhenUnexpectedIsNull_ShouldSucceed(double subject)
		{
			double? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 2.1, 3.1)]
		[InlineData(1.1, 0.1, 3.1)]
		public async Task ForDouble_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			double subject, params double[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 0.1, 1.1, 3.1)]
		public async Task ForDouble_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			double subject, params double?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Fact]
		public async Task ForFloat_WhenSubjectAndExpectedAreNaN_ShouldFail()
		{
			float subject = float.NaN;
			float[] expected = [float.NaN];

			async Task Act() => await That(subject).Should().NotBeOneOf(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be one of [NaN],
				             but it was NaN
				             """);
		}

		[Theory]
		[InlineData(float.NaN, 0.0F, 1.0F)]
		[InlineData(0.0F, 1.0F, float.NaN)]
		public async Task ForFloat_WhenSubjectOrExpectedIsNaN_ShouldSucceed(
			float subject, params float[] expected)
		{
			async Task Act() => await That(subject).Should().NotBeOneOf(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForFloat_WhenUnexpectedIsNull_ShouldSucceed(float subject)
		{
			float? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((float)1.1, (float)2.1, (float)3.1)]
		[InlineData((float)1.1, (float)0.1, (float)3.1)]
		public async Task ForFloat_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			float subject, params float[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((float)1.1, (float)0.1, (float)1.1, (float)3.1)]
		public async Task ForFloat_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			float subject, params float?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, 2, 3)]
		[InlineData(2, 1, 3)]
		public async Task ForInt_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(int subject,
			params int?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, 0, 1, 3)]
		public async Task ForInt_WhenValueIsEqualToAnyUnexpected_ShouldFail(int subject,
			params int?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1L, 1)]
		public async Task ForLong_WhenUnexpectedIsEqualToInt_ShouldFail(
			long subject, int unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of [{Formatter.Format(unexpected)}],
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
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((long)1, (long)2, (long)3)]
		[InlineData((long)1, (long)0, (long)3)]
		public async Task ForLong_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(long subject,
			params long?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((long)1, (long)0, (long)1, (long)3)]
		public async Task ForLong_WhenValueIsEqualToAnyUnexpected_ShouldFail(long subject,
			params long?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData((byte)1, (byte)2, (byte)3)]
		[InlineData((byte)1, (byte)0, (byte)3)]
		public async Task ForNullableByte_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			byte? subject, params byte?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((byte)1, (byte)0, (byte)1, (byte)3)]
		public async Task ForNullableByte_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			byte? subject, params byte?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableByte_WhenValueIsNull_ShouldSucceed(
			params byte?[] unexpected)
		{
			byte? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForNullableDecimal_WhenUnexpectedIsNull_ShouldSucceed(decimal? subject)
		{
			decimal? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 2.1, 3.1)]
		[InlineData(1.1, 0.1, 3.1)]
		public async Task ForNullableDecimal_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			double? subjectValue, params double?[] unexpectedValues)
		{
			decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
			decimal?[] unexpected = unexpectedValues
				.Select(unexpectedValue => unexpectedValue == null
					? (decimal?)null
					: new decimal(unexpectedValue.Value))
				.ToArray();

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 0.1, 1.1, 3.1)]
		public async Task ForNullableDecimal_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			double? subjectValue, params double?[] unexpectedValues)
		{
			decimal? subject = subjectValue == null ? null : new decimal(subjectValue.Value);
			decimal?[] unexpected = unexpectedValues
				.Select(unexpectedValue => unexpectedValue == null
					? (decimal?)null
					: new decimal(unexpectedValue.Value))
				.ToArray();

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableDouble_WhenUnexpectedIsNull_ShouldSucceed(double? subject)
		{
			double? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 2.1, 3.1)]
		[InlineData(1.1, 0.1, 3.1)]
		public async Task ForNullableDouble_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			double? subject, params double?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1.1, 0.1, 1.1, 3.1)]
		public async Task ForNullableDouble_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			double? subject, params double?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableFloat_WhenUnexpectedIsNull_ShouldSucceed(float? subject)
		{
			float? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((float)1.1, (float)2.1, (float)3.1)]
		[InlineData((float)1.1, (float)0.1, (float)3.1)]
		public async Task ForNullableFloat_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			float? subject, params float?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((float)1.1, (float)0.1, (float)1.1, (float)3.1)]
		public async Task ForNullableFloat_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			float? subject, params float?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[InlineData(1, 2, 3)]
		[InlineData(1, 0, 3)]
		public async Task ForNullableInt_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			int? subject, params int?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, 0, 1, 3)]
		public async Task ForNullableInt_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			int? subject, params int?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableInt_WhenValueIsNull_ShouldSucceed(
			params int?[] unexpected)
		{
			int? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((long)1, (long)2, (long)3)]
		[InlineData((long)1, (long)0, (long)3)]
		public async Task ForNullableLong_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			long? subject, params long?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((long)1, (long)0, (long)1, (long)3)]
		public async Task ForNullableLong_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			long? subject, params long?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableLong_WhenValueIsNull_ShouldSucceed(
			params long?[] unexpected)
		{
			long? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)2, (sbyte)3)]
		[InlineData((sbyte)1, (sbyte)0, (sbyte)3)]
		public async Task ForNullableSbyte_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			sbyte? subject, params sbyte?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)0, (sbyte)1, (sbyte)3)]
		public async Task ForNullableSbyte_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			sbyte? subject, params sbyte?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableSbyte_WhenValueIsNull_ShouldSucceed(
			params sbyte?[] unexpected)
		{
			sbyte? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((short)1, (short)2, (short)3)]
		[InlineData((short)1, (short)0, (short)3)]
		public async Task ForNullableShort_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			short? subject, params short?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((short)1, (short)0, (short)1, (short)3)]
		public async Task ForNullableShort_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			short? subject, params short?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableShort_WhenValueIsNull_ShouldSucceed(
			params short?[] unexpected)
		{
			short? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((uint)1, (uint)2, (uint)3)]
		[InlineData((uint)1, (uint)0, (uint)3)]
		public async Task ForNullableUint_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			uint? subject, params uint?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((uint)1, (uint)0, (uint)1, (uint)3)]
		public async Task ForNullableUint_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			uint? subject, params uint?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUint_WhenValueIsNull_ShouldSucceed(
			params uint?[] unexpected)
		{
			uint? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ulong)1, (ulong)2, (ulong)3)]
		[InlineData((ulong)1, (ulong)0, (ulong)3)]
		public async Task ForNullableUlong_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			ulong? subject, params ulong?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ulong)1, (ulong)0, (ulong)1, (ulong)3)]
		public async Task ForNullableUlong_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			ulong? subject, params ulong?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUlong_WhenValueIsNull_ShouldSucceed(
			params ulong?[] unexpected)
		{
			ulong? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ushort)1, (ushort)2, (ushort)3)]
		[InlineData((ushort)1, (ushort)0, (ushort)3)]
		public async Task ForNullableUshort_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			ushort? subject, params ushort?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ushort)1, (ushort)0, (ushort)1, (ushort)3)]
		public async Task ForNullableUshort_WhenValueIsEqualToAnyUnexpected_ShouldFail(
			ushort? subject, params ushort?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}

		[Theory]
		[AutoData]
		public async Task ForNullableUshort_WhenValueIsNull_ShouldSucceed(
			params ushort?[] unexpected)
		{
			ushort? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForSbyte_WhenUnexpectedIsNull_ShouldSucceed(
			sbyte subject)
		{
			sbyte? unexpected = null;

			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)2, (sbyte)3)]
		[InlineData((sbyte)1, (sbyte)0, (sbyte)3)]
		public async Task ForSbyte_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(sbyte subject,
			params sbyte?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((sbyte)1, (sbyte)0, (sbyte)1, (sbyte)3)]
		public async Task ForSbyte_WhenValueIsEqualToAnyUnexpected_ShouldFail(sbyte subject,
			params sbyte?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((short)1, (short)2, (short)3)]
		[InlineData((short)1, (short)0, (short)3)]
		public async Task ForShort_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(short subject,
			params short?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((short)1, (short)0, (short)1, (short)3)]
		public async Task ForShort_WhenValueIsEqualToAnyUnexpected_ShouldFail(short subject,
			params short?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((uint)1, (uint)2, (uint)3)]
		[InlineData((uint)1, (uint)0, (uint)3)]
		public async Task ForUint_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(uint subject,
			params uint?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((uint)1, (uint)0, (uint)1, (uint)3)]
		public async Task ForUint_WhenValueIsEqualToAnyUnexpected_ShouldFail(uint subject,
			params uint?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ulong)1, (ulong)2, (ulong)3)]
		[InlineData((ulong)1, (ulong)0, (ulong)3)]
		public async Task ForUlong_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(ulong subject,
			params ulong?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ulong)1, (ulong)0, (ulong)1, (ulong)3)]
		public async Task ForUlong_WhenValueIsEqualToAnyUnexpected_ShouldFail(ulong subject,
			params ulong?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
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
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ushort)1, (ushort)2, (ushort)3)]
		[InlineData((ushort)1, (ushort)0, (ushort)3)]
		public async Task ForUshort_WhenValueIsDifferentToAllUnexpected_ShouldSucceed(
			ushort subject,
			params ushort?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData((ushort)1, (ushort)0, (ushort)1, (ushort)3)]
		public async Task ForUshort_WhenValueIsEqualToAnyUnexpected_ShouldFail(ushort subject,
			params ushort?[] unexpected)
		{
			async Task Act()
				=> await That(subject).Should().NotBeOneOf(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not be one of {Formatter.Format(unexpected)},
				              but it was {Formatter.Format(subject)}
				              """);
		}
	}
}
