namespace aweXpect.Tests.Objects;

public sealed partial class ObjectShould
{
	public sealed partial class Be
	{
		public sealed class GenericTests
		{
			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnTypedResult(int value)
			{
				object subject = new MyClass
				{
					Value = value
				};

				MyClass result = await That(subject).Is<MyClass>();

				await That(result).IsSameAs(subject);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).Is<MyClass>();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be type MyClass,
					             but it was <null>
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeDoesNotMatch_ShouldFail(int value)
			{
				object subject = new MyClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Is<OtherClass>()
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               be type OtherClass, because we want to test the failure,
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Fact]
			public async Task WhenTypeIsSubtype_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Is<MyBaseClass>();

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeIsSupertype_ShouldFail(int value, string reason)
			{
				object subject = new MyBaseClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Is<MyClass>()
						.Because(reason);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               be type MyClass, because {{reason}},
					               but it was MyBaseClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Fact]
			public async Task WhenTypeMatches_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Is<MyClass>();

				await That(Act).Does().NotThrow();
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

				object? result = await That(subject).Is(typeof(MyClass));

				await That(result).IsSameAs(subject);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).Is(typeof(MyClass));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be type MyClass,
					             but it was <null>
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeDoesNotMatch_ShouldFail(int value)
			{
				object subject = new MyClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Is(typeof(OtherClass))
						.Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               be type OtherClass, because we want to test the failure,
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Fact]
			public async Task WhenTypeIsSubtype_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Is(typeof(MyBaseClass));

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeIsSupertype_ShouldFail(int value, string reason)
			{
				object subject = new MyBaseClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Is(typeof(MyClass))
						.Because(reason);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               be type MyClass, because {{reason}},
					               but it was MyBaseClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Fact]
			public async Task WhenTypeMatches_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Is(typeof(MyClass));

				await That(Act).Does().NotThrow();
			}
		}
	}
}
