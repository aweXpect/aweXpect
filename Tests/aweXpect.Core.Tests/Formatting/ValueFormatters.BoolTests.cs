using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class BoolTests
	{
		[Theory]
		[InlineData(true, "True")]
		[InlineData(false, "False")]
		public async Task Booleans_ShouldHaveCapitalizedFirstLetter(bool value, string expectedResult)
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
		[InlineData(true, "True")]
		[InlineData(false, "False")]
		[InlineData(null, "<null>")]
		public async Task NullableBooleans_ShouldHaveCapitalizedFirstLetter(bool? value, string expectedResult)
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
			bool? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format((object?)value);
			Formatter.Format(sb, value);

			await That(result).Is(ValueFormatter.NullString);
			await That(objectResult).Is(ValueFormatter.NullString);
			await That(sb.ToString()).Is(ValueFormatter.NullString);
		}
	}
}
