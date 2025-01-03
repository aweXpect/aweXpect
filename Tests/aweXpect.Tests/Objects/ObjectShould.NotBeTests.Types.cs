namespace aweXpect.Tests.Objects;

public sealed partial class ObjectShould
{
	public sealed partial class NotBe
	{
		public sealed class GenericTests
		{
			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnObjectResult(int value)
			{
				object subject = new MyClass
				{
					Value = value
				};

				object? result = await That(subject).Should().NotBe<OtherClass>();

				await That(result).Should().BeSameAs(subject);
			}

			[Fact]
			public async Task WhenTypeDoesNotMatch_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBe<OtherClass>();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeIsSubtype_ShouldFail(int value)
			{
				object subject = new MyClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Should().NotBe<MyBaseClass>()
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               not be type MyBaseClass, because we want to test the failure,
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Fact]
			public async Task WhenTypeIsSupertype_ShouldSucceed()
			{
				object subject = new MyBaseClass();

				async Task Act()
					=> await That(subject).Should().NotBe<MyClass>();

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeMatches_ShouldFail(int value, string reason)
			{
				object subject = new MyClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Should().NotBe<MyClass>()
						.Because(reason);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               not be type MyClass, because {{reason}},
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBe<MyClass>();

				await That(Act).Should().NotThrow();
			}
		}

		public sealed class TypeTests
		{
			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnTypedResult(int value)
			{
				object subject = new MyClass
				{
					Value = value
				};

				object? result = await That(subject).Should().NotBe(typeof(OtherClass));

				await That(result).Should().BeSameAs(subject);
			}

			[Fact]
			public async Task WhenTypeDoesNotMatch_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBe(typeof(OtherClass));

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeIsSubtype_ShouldFail(int value)
			{
				object subject = new MyClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Should().NotBe(typeof(MyBaseClass))
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               not be type MyBaseClass, because we want to test the failure,
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Fact]
			public async Task WhenTypeIsSupertype_ShouldSucceed()
			{
				object subject = new MyBaseClass();

				async Task Act()
					=> await That(subject).Should().NotBe(typeof(MyClass));

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeMatches_ShouldFail(int value, string reason)
			{
				object subject = new MyClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Should().NotBe(typeof(MyClass))
						.Because(reason);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               not be type MyClass, because {{reason}},
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBe(typeof(MyClass));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
