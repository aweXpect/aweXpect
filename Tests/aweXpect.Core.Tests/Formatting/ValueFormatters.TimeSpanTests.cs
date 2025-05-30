﻿using System.Text;
using aweXpect.Chronology;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class TimeSpanTests
	{
		[Fact]
		public async Task Nullable_ShouldIncludeSingleDigitMinuteEvenWhenOnlySecondsAreSpecified()
		{
			TimeSpan? value = 12.Seconds();
			string expectedResult = "0:12";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task Nullable_ShouldSupportDoubleDigitDays()
		{
			TimeSpan? value = 13.Days(4.Hours(5.Minutes(6.Seconds())));
			string expectedResult = "13.04:05:06";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task Nullable_ShouldSupportDoubleDigitHours()
		{
			TimeSpan? value = 14.Hours(5.Minutes(6.Seconds()));
			string expectedResult = "14:05:06";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task Nullable_ShouldSupportDoubleDigitsMinutes()
		{
			TimeSpan? value = 13.Minutes(4.Seconds());
			string expectedResult = "13:04";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task Nullable_ShouldSupportMilliseconds()
		{
			TimeSpan? value = 13.Days(14.Hours(15.Minutes(16.Seconds(1.Milliseconds()))));
			string expectedResult = "13.14:15:16.001";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task Nullable_ShouldSupportSingleDigitDays()
		{
			TimeSpan? value = 25.Hours(5.Minutes(6.Seconds()));
			string expectedResult = "1.01:05:06";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task Nullable_ShouldSupportSingleDigitHours()
		{
			TimeSpan? value = 73.Minutes(4.Seconds());
			string expectedResult = "1:13:04";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task Nullable_WithType_ShouldIncludeTypeInformation()
		{
			TimeSpan? value = 12.Seconds();
			string expectedResult = "TimeSpan 0:12";
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.WithType);
			string objectResult = Formatter.Format((object?)value, FormattingOptions.WithType);
			Formatter.Format(sb, value, FormattingOptions.WithType);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task ShouldIncludeSingleDigitMinuteEvenWhenOnlySecondsAreSpecified()
		{
			TimeSpan value = 12.Seconds();
			string expectedResult = "0:12";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task ShouldSupportDoubleDigitDays()
		{
			TimeSpan value = 13.Days(14.Hours(15.Minutes(16.Seconds())));
			string expectedResult = "13.14:15:16";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task ShouldSupportDoubleDigitHours()
		{
			TimeSpan value = 14.Hours(15.Minutes(16.Seconds()));
			string expectedResult = "14:15:16";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task ShouldSupportDoubleDigitsMinutes()
		{
			TimeSpan value = 13.Minutes(14.Seconds());
			string expectedResult = "13:14";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task ShouldSupportMilliseconds()
		{
			TimeSpan value = 13.Days(14.Hours(15.Minutes(16.Seconds(14.Milliseconds()))));
			string expectedResult = "13.14:15:16.014";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task ShouldSupportSingleDigitDays()
		{
			TimeSpan value = 25.Hours(15.Minutes(16.Seconds()));
			string expectedResult = "1.01:15:16";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task ShouldSupportSingleDigitHours()
		{
			TimeSpan value = 73.Minutes(14.Seconds());
			string expectedResult = "1:13:14";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task ShouldSupportTenAsHoursOrMinutes()
		{
			TimeSpan value = 10.Days(10.Hours(10.Minutes(10.Seconds())));
			string expectedResult = "10.10:10:10";
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task WhenNull_ShouldUseDefaultNullString()
		{
			TimeSpan? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(ValueFormatter.NullString);
			await That(objectResult).IsEqualTo(ValueFormatter.NullString);
			await That(sb.ToString()).IsEqualTo(ValueFormatter.NullString);
		}

		[Fact]
		public async Task WithType_ShouldIncludeTypeInformation()
		{
			TimeSpan value = 3.Minutes(20.Seconds());
			string expectedResult = "TimeSpan 3:20";
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.WithType);
			string objectResult = Formatter.Format((object?)value, FormattingOptions.WithType);
			Formatter.Format(sb, value, FormattingOptions.WithType);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}
	}
}
