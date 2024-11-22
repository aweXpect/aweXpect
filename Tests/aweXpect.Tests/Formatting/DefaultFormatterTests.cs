namespace Testably.Expectations.Tests.Formatting;

public sealed class DefaultFormatterTests
{
	[Fact]
	public async Task ShouldDisplayNestedObjects()
	{
		Dummy value = new()
		{
			Inner = new InnerDummy { InnerValue = "foo" },
			Value = 2
		};

		string result = Formatter.Format(value, FormattingOptions.SingleLine);

		await That(result).Should().Be("""
		                               Dummy { Inner = InnerDummy { InnerValue = "foo" }, Value = 2 }
		                               """);
	}

	[Fact]
	public async Task ShouldUseMultipleLinesPerDefault()
	{
		Dummy value = new()
		{
			Inner = new InnerDummy { InnerValue = "foo" },
			Value = 2
		};

		string result = Formatter.Format(value, FormattingOptions.MultipleLines);

		await That(result).Should().Be("""
		                               Dummy {
		                                 Inner = InnerDummy {
		                                   InnerValue = "foo"
		                                 },
		                                 Value = 2
		                               }
		                               """);
	}

	[Theory]
	[AutoData]
	public async Task ShouldUseToStringWhenImplemented_Default(string[] values)
	{
		string value = string.Join(Environment.NewLine, values);
		string expected = string.Join($"{Environment.NewLine}  ", values) + Environment.NewLine;
		ClassWithToString subject = new(value);

		string result = Formatter.Format(subject, FormattingOptions.MultipleLines);

		await That(result).Should().Be(expected);
	}

	[Theory]
	[AutoData]
	public async Task ShouldUseToStringWhenImplemented_WithSingleLine(string value)
	{
		ClassWithToString subject = new(value);

		string result = Formatter.Format(subject, FormattingOptions.SingleLine);

		await That(result).Should().Be(value);
	}

	[Fact]
	public async Task WhenClassContainsField_ShouldDisplayFieldValue()
	{
		object value = new ClassWithField { Value = 42 };

		string result = Formatter.Format(value, FormattingOptions.SingleLine);

		await That(result).Should().Be("ClassWithField { Value = 42 }");
	}

	[Fact]
	public async Task WhenClassIsEmpty_ShouldDisplayClassName()
	{
		object value = new EmptyClass();

		string result = Formatter.Format(value, FormattingOptions.SingleLine);

		await That(result).Should().Be("EmptyClass { }");
	}

	[Fact]
	public async Task WhenClassMemberThrowsException_ShouldDisplayException()
	{
		Exception exception = new("foo");
		object value = new ClassWithExceptionProperty(exception);

		string result = Formatter.Format(value, FormattingOptions.SingleLine);

		await That(result).Should()
			.Be(
				"ClassWithExceptionProperty { Value = [Member 'Value' threw an exception: 'foo'] }");
	}

	[Fact]
	public async Task WhenObject_ShouldDisplayHashCode()
	{
		object value = new();

		string result = Formatter.Format(value, FormattingOptions.SingleLine);

		await That(result).Should().Be("System.Object (HashCode=*)").AsWildcard();
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

	private sealed class EmptyClass;

	private sealed class InnerDummy
	{
		// ReSharper disable once UnusedAutoPropertyAccessor.Local
		public string? InnerValue { get; set; }
	}
}
