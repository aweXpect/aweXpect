namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class IsNotExactly
	{
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

				object? result = await That(subject).IsNotExactly(typeof(OtherClass));

				await That(result).IsSameAs(subject);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).IsNotExactly(typeof(MyClass));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenTypeDoesNotMatch_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).IsNotExactly(typeof(OtherClass));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenTypeIsSubtype_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).IsNotExactly(typeof(MyBaseClass));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenTypeIsSupertype_ShouldSucceed()
			{
				object subject = new MyBaseClass();

				async Task Act()
					=> await That(subject).IsNotExactly(typeof(MyClass));

				await That(Act).DoesNotThrow();
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
					=> await That(subject).IsNotExactly(typeof(MyClass))
						.Because(reason);

				await That(Act).Throws<XunitException>()
					.WithMessage($$"""
					               Expected that subject
					               is not exactly type MyClass, because {{reason}},
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}
		}
	}
}
