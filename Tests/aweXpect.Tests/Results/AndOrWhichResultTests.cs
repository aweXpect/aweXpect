namespace Testably.Expectations.Tests.Results;

public class AndOrWhichResultTests
{
	[Fact]
	public async Task MultipleWhich_ShouldAllowChaining()
	{
		MyClass sut = new();

		async Task Act()
		{
			await That(sut).Should().Be<MyClass>()
				.Which(f => f.Value1).Should(f => f.BeTrue())
				.AndWhich(f => f.Value2).Should(f => f.BeTrue())
				.And.BeSameAs(sut);
		}

		await That(Act).Should().ThrowException()
			.WithMessage("""
			             Expected sut to
			             be type MyClass which .Value1 should be True and which .Value2 should be True and refer to sut MyClass {
			               Value1 = False,
			               Value2 = False
			             },
			             but .Value1 was False and .Value2 was False
			             """);
	}

	[Theory]
	[InlineData(true, true, true)]
	[InlineData(true, false, false)]
	[InlineData(false, true, false)]
	[InlineData(false, false, false)]
	public async Task MultipleWhich_ShouldVerifyAll(bool value1, bool value2, bool expectSuccess)
	{
		MyClass sut = new()
		{
			Value1 = value1,
			Value2 = value2
		};

		async Task Act()
		{
			await That(sut).Should().Be<MyClass>()
				.Which(f => f.Value1).Should(f => f.BeTrue())
				.AndWhich(f => f.Value2).Should(f => f.BeTrue());
		}

		await That(Act).Should().ThrowException().OnlyIf(!expectSuccess)
			.WithMessage($"""
			              Expected sut to
			              be type MyClass which .Value1 should be True and which .Value2 should be True,
			              but {(value1 ? "" : ".Value1 was False")}{(!value1 && !value2 ? " and " : "")}{(value2 ? "" : ".Value2 was False")}
			              """);
	}

	private sealed class MyClass
	{
		public bool Value1 { get; set; }
		public bool Value2 { get; set; }
	}
}
