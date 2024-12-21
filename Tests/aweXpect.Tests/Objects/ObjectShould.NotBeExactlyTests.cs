namespace aweXpect.Tests.Objects;

public sealed partial class ObjectShould
{
	public sealed class NotBeExactly
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task ForGeneric_WhenAwaited_ShouldReturnObjectResult(int value)
			{
				object subject = new MyClass
				{
					Value = value
				};

				object? result = await That(subject).Should().NotBeExactly<OtherClass>();

				await That(result).Should().BeSameAs(subject);
			}

			[Fact]
			public async Task ForGeneric_WhenTypeDoesNotMatch_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly<OtherClass>();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task ForGeneric_WhenTypeIsSubtype_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly<MyBaseClass>();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForGeneric_WhenTypeMatches_ShouldFail(int value, string reason)
			{
				object subject = new MyClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Should().NotBeExactly<MyClass>()
						.Because(reason);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               not be exactly type MyClass, because {{reason}},
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Fact]
			public async Task ForGeneric_WhenTypeIsSupertype_ShouldSucceed()
			{
				object subject = new MyBaseClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly<MyClass>();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForType_WhenAwaited_ShouldReturnTypedResult(int value)
			{
				object subject = new MyClass
				{
					Value = value
				};

				object? result = await That(subject).Should().NotBeExactly(typeof(OtherClass));

				await That(result).Should().BeSameAs(subject);
			}

			[Fact]
			public async Task ForType_WhenTypeIsSubtype_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly(typeof(MyBaseClass));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task ForType_WhenTypeDoesNotMatch_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly(typeof(OtherClass));

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task ForType_WhenTypeMatches_ShouldFail(int value, string reason)
			{
				object subject = new MyClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Should().NotBeExactly(typeof(MyClass))
						.Because(reason);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               not be exactly type MyClass, because {{reason}},
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Fact]
			public async Task ForType_WhenTypeIsSupertype_ShouldSucceed()
			{
				object subject = new MyBaseClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly(typeof(MyClass));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
