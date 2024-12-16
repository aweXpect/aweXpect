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
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Theory]
		[InlineData(true, "True")]
		[InlineData(false, "False")]
		[InlineData(null, "<null>")]
		public async Task NullableBooleans_ShouldHaveCapitalizedFirstLetter(bool? value, string expectedResult)
		{
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}
	}
}
