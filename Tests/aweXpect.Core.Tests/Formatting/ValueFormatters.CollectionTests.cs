﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class CollectionTests
	{
		[Fact]
		public async Task ShouldFormatItems()
		{
			string expectedResult = "[\"1\", \"2\", \"3\", \"4\"]";
			IEnumerable<string> value = Enumerable.Range(1, 4).Select(x => x.ToString());
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task ShouldLimitTo10Items()
		{
			string expectedResult = "[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, …]";
			IEnumerable<int> value = Enumerable.Range(1, 20);
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
			IEnumerable<int>? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value!);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value!);

			await That(result).IsEqualTo(ValueFormatter.NullString);
			await That(objectResult).IsEqualTo(ValueFormatter.NullString);
			await That(sb.ToString()).IsEqualTo(ValueFormatter.NullString);
		}

		[Fact]
		public async Task WithType_Array_ShouldIncludeTypeInformation()
		{
			string expectedResult = "int[] [1, 2, 3, 4]";
			int[] value = [..Enumerable.Range(1, 4),];
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.WithType);
			string objectResult = Formatter.Format((object?)value, FormattingOptions.WithType);
			Formatter.Format(sb, value, FormattingOptions.WithType);

			await That(result).IsEqualTo(expectedResult);
			await That(objectResult).IsEqualTo(expectedResult);
			await That(sb.ToString()).IsEqualTo(expectedResult);
		}

		[Fact]
		public async Task WithType_EnumerableShouldIncludeTypeInformation()
		{
			string expectedResult = "List<int> [1, 2, 3, 4, 5]";
			IEnumerable<int> value = Enumerable.Range(1, 5).ToList();
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
