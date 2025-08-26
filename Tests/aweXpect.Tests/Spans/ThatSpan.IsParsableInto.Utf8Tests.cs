﻿#if NET8_0_OR_GREATER
using System.Globalization;
using System.Text;

namespace aweXpect.Tests;

public sealed partial class ThatSpan
{
	public sealed partial class IsParsableInto
	{
		public sealed class Utf8Tests
		{
			[Fact]
			public async Task WhenSpanIsNotParsable_ShouldFail()
			{
				byte[] subject = "abc"u8.ToArray();

				async Task Act()
					=> await That(subject.AsSpan()).IsParsableInto<int>();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject.AsSpan()
					             is parsable into int,
					             but it was not because the input string 'System.ReadOnlySpan<Byte>[3]' was not in a correct format
					             """);
			}

			[Fact]
			public async Task WhenSpanIsParsable_ShouldSucceed()
			{
				byte[] subject = "42"u8.ToArray();

				async Task Act()
					=> await That(subject.AsSpan()).IsParsableInto<int>();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData("-12.34", "de-AT")]
			[InlineData("-12,34", "en-US")]
			public async Task WithFormatProvider_WhenFormatDoesNotMatch_ShouldFail(string subjectString,
				string cultureName)
			{
				byte[] subject = Encoding.UTF8.GetBytes(subjectString);
				IFormatProvider formatProvider = new CultureInfo(cultureName);

				async Task Act()
					=> await That(subject.AsSpan()).IsParsableInto<uint>(formatProvider);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject.AsSpan()
					              is parsable into uint using {cultureName},
					              but it was not because the input string 'System.ReadOnlySpan<Byte>[6]' was not in a correct format
					              """);
			}

			[Theory]
			[InlineData("12,34", "de-AT")]
			[InlineData("12.34", "en-US")]
			public async Task WithFormatProvider_WhenFormatMatches_ShouldSucceed(string subjectString,
				string cultureName)
			{
				byte[] subject = Encoding.UTF8.GetBytes(subjectString);
				IFormatProvider formatProvider = new CultureInfo(cultureName);

				async Task Act()
					=> await That(subject.AsSpan()).IsParsableInto<decimal>(formatProvider);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class Utf8WhichTests
		{
			[Fact]
			public async Task WhenSpanIsNotParsable_ShouldFail()
			{
				byte[] subject = "abc"u8.ToArray();

				async Task Act()
					=> await That(subject.AsSpan()).IsParsableInto<double>().Which.IsLessThan(10.0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject.AsSpan()
					             is parsable into double which is less than 10.0,
					             but it was not because the input string 'System.ReadOnlySpan<Byte>[3]' was not in a correct format
					             """);
			}

			[Fact]
			public async Task WhenSpanIsParsable_ShouldSucceed()
			{
				async Task Act()
					=> await That("42".AsSpan()).IsParsableInto<int>().Which.IsBetween(41).And(43);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData("12,34", "de-AT")]
			[InlineData("12.34", "en-US")]
			public async Task WithFormatProvider_ShouldBeUsed(string subject, string cultureName)
			{
				IFormatProvider formatProvider = new CultureInfo(cultureName);

				async Task Act()
					=> await That(subject.AsSpan()).IsParsableInto<decimal>(formatProvider).Which.IsEqualTo(12.34M);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
#endif
