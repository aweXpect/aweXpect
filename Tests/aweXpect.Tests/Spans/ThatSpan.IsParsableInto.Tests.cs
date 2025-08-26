﻿#if NET8_0_OR_GREATER
using System.Globalization;

namespace aweXpect.Tests;

public sealed partial class ThatSpan
{
	public sealed partial class IsParsableInto
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSpanIsNotParsable_ShouldFail()
			{
				async Task Act()
					=> await That("abc".AsSpan()).IsParsableInto<int>();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that "abc".AsSpan()
					             is parsable into int,
					             but it was not because the input string 'abc' was not in a correct format
					             """);
			}

			[Fact]
			public async Task WhenSpanIsParsable_ShouldSucceed()
			{
				async Task Act()
					=> await That("42".AsSpan()).IsParsableInto<int>();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData("-12.34", "de-AT")]
			[InlineData("-12,34", "en-US")]
			public async Task WithFormatProvider_WhenFormatDoesNotMatch_ShouldFail(string subject, string cultureName)
			{
				IFormatProvider formatProvider = new CultureInfo(cultureName);

				async Task Act()
					=> await That(subject.AsSpan()).IsParsableInto<uint>(formatProvider);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject.AsSpan()
					              is parsable into uint using {cultureName},
					              but it was not because the input string '{subject}' was not in a correct format
					              """);
			}

			[Theory]
			[InlineData("12,34", "de-AT")]
			[InlineData("12.34", "en-US")]
			public async Task WithFormatProvider_WhenFormatMatches_ShouldSucceed(string subject, string cultureName)
			{
				IFormatProvider formatProvider = new CultureInfo(cultureName);

				async Task Act()
					=> await That(subject.AsSpan()).IsParsableInto<decimal>(formatProvider);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class WhichTests
		{
			[Fact]
			public async Task WhenSpanIsNotParsable_ShouldFail()
			{
				async Task Act()
					=> await That("abc".AsSpan()).IsParsableInto<TimeSpan>().Which.IsLessThan(10.Seconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that "abc".AsSpan()
					             is parsable into TimeSpan which is less than 0:10,
					             but it was not because string 'abc' was not recognized as a valid TimeSpan
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
