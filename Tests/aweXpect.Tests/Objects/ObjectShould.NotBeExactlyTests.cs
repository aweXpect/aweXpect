namespace aweXpect.Tests.Objects;

public sealed partial class ObjectShould
{
	public sealed class NotBeExactly
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

				object? result = await That(subject).Should().NotBeExactly<OtherClass>();

				await That(result).Should().BeSameAs(subject);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBeExactly<MyClass>();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenTypeDoesNotMatch_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly<OtherClass>();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenTypeIsSubtype_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly<MyBaseClass>();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenTypeIsSupertype_ShouldSucceed()
			{
				object subject = new MyBaseClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly<MyClass>();

				await That(Act).Does().NotThrow();
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
					=> await That(subject).Should().NotBeExactly<MyClass>()
						.Because(reason);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               not be exactly type MyClass, because {{reason}},
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
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

				object? result = await That(subject).Should().NotBeExactly(typeof(OtherClass));

				await That(result).Should().BeSameAs(subject);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBeExactly(typeof(MyClass));

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenTypeDoesNotMatch_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly(typeof(OtherClass));

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenTypeIsSubtype_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly(typeof(MyBaseClass));

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenTypeIsSupertype_ShouldSucceed()
			{
				object subject = new MyBaseClass();

				async Task Act()
					=> await That(subject).Should().NotBeExactly(typeof(MyClass));

				await That(Act).Does().NotThrow();
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
					=> await That(subject).Should().NotBeExactly(typeof(MyClass))
						.Because(reason);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               not be exactly type MyClass, because {{reason}},
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}
		}
	}
}
