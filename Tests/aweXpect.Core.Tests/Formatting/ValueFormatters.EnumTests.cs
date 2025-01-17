using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class EnumTests
	{
		[Theory]
		[InlineData(Dummy.Foo, "Foo")]
		[InlineData(Dummy.Bar, "Bar")]
		[InlineData(null, "<null>")]
		public async Task NullableShouldUseStringRepresentation(Dummy? value, string expectedResult)
		{
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Is(expectedResult);
			await That(objectResult).Is(expectedResult);
			await That(sb.ToString()).Is(expectedResult);
		}

		[Theory]
		[InlineData(Dummy.Foo, "Foo")]
		[InlineData(Dummy.Bar, "Bar")]
		public async Task ShouldUseStringRepresentation(Dummy value, string expectedResult)
		{
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Is(expectedResult);
			await That(objectResult).Is(expectedResult);
			await That(sb.ToString()).Is(expectedResult);
		}

		[Fact]
		public async Task WhenNull_ShouldUseDefaultNullString()
		{
			Dummy? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Is(ValueFormatter.NullString);
			await That(objectResult).Is(ValueFormatter.NullString);
			await That(sb.ToString()).Is(ValueFormatter.NullString);
		}

		public enum Dummy
		{
			Foo,
			Bar
		}
	}
}
