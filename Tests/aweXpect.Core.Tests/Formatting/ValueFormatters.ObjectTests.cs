using System.Text;

namespace aweXpect.Core.Tests.Formatting;

public partial class ValueFormatters
{
	public sealed class FormatObjectTests
	{
		[Fact]
		public async Task ShouldDisplayNestedObjects()
		{
			Dummy value = new()
			{
				Inner = new InnerDummy
				{
					InnerValue = "foo"
				},
				Value = 2
			};
			string expectedResult = """
			                        Dummy { Inner = InnerDummy { InnerValue = "foo" }, Value = 2 }
			                        """;
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.SingleLine);
			Formatter.Format(sb, value, FormattingOptions.SingleLine);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task ShouldDisplayRecursiveObjects()
		{
			RecursiveDummy value = new()
			{
				Value = 1
			};
			value.Inner = value;
			string expectedResult = """
			                        RecursiveDummy { Inner = RecursiveDummy { *recursive* }, Value = 1 }
			                        """;
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.SingleLine);
			Formatter.Format(sb, value, FormattingOptions.SingleLine);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task ShouldUseMultipleLinesPerDefault()
		{
			Dummy value = new()
			{
				Inner = new InnerDummy
				{
					InnerValue = "foo"
				},
				Value = 2
			};
			string expectedResult = """
			                        Dummy {
			                          Inner = InnerDummy {
			                            InnerValue = "foo"
			                          },
			                          Value = 2
			                        }
			                        """;
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.MultipleLines);
			Formatter.Format(sb, value, FormattingOptions.MultipleLines);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Theory]
		[AutoData]
		public async Task ShouldUseToStringWhenImplemented_Default(string[] values)
		{
			string value = string.Join(Environment.NewLine, values);
			string expectedResult = string.Join($"{Environment.NewLine}  ", values) + Environment.NewLine;
			ClassWithToString subject = new(value);
			StringBuilder sb = new();

			string result = Formatter.Format(subject, FormattingOptions.MultipleLines);
			Formatter.Format(sb, subject, FormattingOptions.MultipleLines);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Theory]
		[AutoData]
		public async Task ShouldUseToStringWhenImplemented_WithSingleLine(string value)
		{
			ClassWithToString subject = new(value);
			string expectedResult = value;
			StringBuilder sb = new();

			string result = Formatter.Format(subject, FormattingOptions.SingleLine);
			Formatter.Format(sb, subject, FormattingOptions.SingleLine);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task WhenClassContainsField_ShouldDisplayFieldValue()
		{
			object value = new ClassWithField
			{
				Value = 42
			};
			string expectedResult = "ClassWithField { Value = 42 }";
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.SingleLine);
			Formatter.Format(sb, value, FormattingOptions.SingleLine);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task WhenClassIsEmpty_ShouldDisplayClassName()
		{
			object value = new EmptyClass();
			string expectedResult = "EmptyClass { }";
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.SingleLine);
			Formatter.Format(sb, value, FormattingOptions.SingleLine);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task WhenClassMemberThrowsException_ShouldDisplayException()
		{
			Exception exception = new("foo");
			object value = new ClassWithExceptionProperty(exception);
			string expectedResult = "ClassWithExceptionProperty { Value = [Member 'Value' threw an exception: 'foo'] }";
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.SingleLine);
			Formatter.Format(sb, value, FormattingOptions.SingleLine);

			await That(result).Should().Be(expectedResult);
			await That(sb.ToString()).Should().Be(expectedResult);
		}

		[Fact]
		public async Task WhenNull_ShouldUseDefaultNullString()
		{
			object? value = null;
			StringBuilder sb = new();

			string result = Formatter.Format(value);
			string objectResult = Formatter.Format(value);
			Formatter.Format(sb, value);

			await That(result).Should().Be(ValueFormatter.NullString);
			await That(sb.ToString()).Should().Be(ValueFormatter.NullString);
		}

		[Fact]
		public async Task WhenObject_ShouldDisplayHashCode()
		{
			object value = new();
			string expectedResult = "System.Object (HashCode=*)";
			StringBuilder sb = new();

			string result = Formatter.Format(value, FormattingOptions.SingleLine);
			Formatter.Format(sb, value, FormattingOptions.SingleLine);

			await That(result).Should().Be(expectedResult).AsWildcard();
			await That(sb.ToString()).Should().Be(expectedResult).AsWildcard();
		}

		private sealed class ClassWithExceptionProperty(Exception exception)
		{
			// ReSharper disable once UnusedMember.Local
			public int Value => throw exception;
		}

		private sealed class ClassWithField
		{
			// ReSharper disable once NotAccessedField.Local
			public int Value = 2;
		}

		private sealed class ClassWithToString(string value)
		{
			/// <inheritdoc />
			public override string ToString()
				=> value;
		}

		private sealed class Dummy
		{
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public InnerDummy? Inner { get; set; }

			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public int Value { get; set; }
		}

		private sealed class RecursiveDummy
		{
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public RecursiveDummy? Inner { get; set; }

			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public int Value { get; set; }
		}

		private sealed class EmptyClass;

		private sealed class InnerDummy
		{
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public string? InnerValue { get; set; }
		}
	}
}
