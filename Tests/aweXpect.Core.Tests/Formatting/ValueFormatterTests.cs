using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public class ValueFormatterTests
{
	[Fact]
	public async Task CustomFormatter_ShouldBeUsedDuringLifetime()
	{
		MyFormattableClass value = new()
		{
			Value = 42,
		};
		using (ValueFormatter.Register(new MyCustomFormatter("my-string")))
		{
			string customObjectResult = Formatter.Format(value);

			await That(customObjectResult).IsEqualTo("my-string");
		}

		string objectResult = Formatter.Format(value);

		await That(objectResult).IsEqualTo("""
		                                   ValueFormatterTests.MyFormattableClass {
		                                     Value = 42
		                                   }
		                                   """);
	}

	[Fact]
	public async Task CustomFormatter_WhenNull_ShouldUseDefaultNullString()
	{
		using IDisposable lifetime = ValueFormatter.Register(new MyCustomFormatter("my-string"));
		bool? value = null;

		string objectResult = Formatter.Format((object?)value);

		await That(objectResult).IsEqualTo(ValueFormatter.NullString);
	}

	[Fact]
	public async Task CustomFormatter_WhenTypeDoesNotMatch_ShouldDoNothing()
	{
		int value = 1;
		using (ValueFormatter.Register(new MyCustomFormatter("my-string")))
		{
			string customObjectResult = Formatter.Format((object?)value);

			await That(customObjectResult).IsEqualTo("1");
		}
	}

	private sealed class MyCustomFormatter(string formatString) : IValueFormatter
	{
		public bool TryFormat(StringBuilder stringBuilder, object value, FormattingOptions? options)
		{
			if (value is MyFormattableClass)
			{
				stringBuilder.Append(formatString);
				return true;
			}

			return false;
		}
	}

	private sealed class MyFormattableClass
	{
		public int Value { get; set; }
	}
}
