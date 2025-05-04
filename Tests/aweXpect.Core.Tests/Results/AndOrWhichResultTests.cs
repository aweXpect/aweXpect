using aweXpect.Core.Helpers;
using aweXpect.Core.Tests.TestHelpers;
using aweXpect.Results;

namespace aweXpect.Core.Tests.Results;

public class AndOrWhichResultTests
{
	[Fact(Skip="TODO Reactivate after next update")]
	public async Task MultipleWhose_ShouldAllowChaining()
	{
		MyClass subject = new();
		AndOrWhichResult<MyClass, IThat<MyClass>> sut = CreateSut(subject);

		async Task Act()
			=> await sut
				.Which(f => f.Value1, f => f.IsTrue())
				.AndWhich(f => f.Value2, f => f.IsTrue())
				.And.IsSameAs(subject);

		await That(Act).ThrowsException()
			.WithMessage("""
			             Expected that subject
			              which .Value1 is True and which .Value2 is True and refers to AndOrWhichResultTests.MyClass {
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
	public async Task MultipleWhose_ShouldVerifyAll(bool value1, bool value2, bool expectSuccess)
	{
		MyClass subject = new()
		{
			Value1 = value1,
			Value2 = value2,
		};
		AndOrWhichResult<MyClass, IThat<MyClass>> sut = CreateSut(subject);

		async Task Act()
			=> await sut
				.Which(f => f.Value1, f => f.IsTrue())
				.AndWhich(f => f.Value2, f => f.IsTrue());

		await That(Act).ThrowsException().OnlyIf(!expectSuccess)
			.WithMessage($"""
			              Expected that subject
			               which .Value1 is True and which .Value2 is True,
			              but {(value1 ? "" : ".Value1 was False")}{(!value1 && !value2 ? " and " : "")}{(value2 ? "" : ".Value2 was False")}
			              """);
	}

	private sealed class MyClass
	{
		public bool Value1 { get; init; }
		public bool Value2 { get; init; }
	}

	private static AndOrWhichResult<T, IThat<T>> CreateSut<T>(T subject)
	{
#pragma warning disable aweXpect0001
		IThat<T> source = That(subject);
#pragma warning restore aweXpect0001
		return new AndOrWhichResult<T, IThat<T>>(source.Get().ExpectationBuilder.AddConstraint((it, _)
				=> new DummyConstraint(it)),
			source);
	}
}
