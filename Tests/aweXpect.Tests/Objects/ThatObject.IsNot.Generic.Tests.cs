namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class IsNot
	{
		public sealed class Generic
		{
			public sealed class Tests
			{
				[Theory]
				[AutoData]
				public async Task WhenAwaited_ShouldReturnObjectResult(int value)
				{
					object subject = new MyClass
					{
						Value = value
					};

					object? result = await That(subject).IsNot<OtherClass>();

					await That(result).IsSameAs(subject);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldSucceed()
				{
					object? subject = null;

					async Task Act()
						=> await That(subject).IsNot<MyClass>();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenTypeDoesNotMatch_ShouldSucceed()
				{
					object subject = new MyClass();

					async Task Act()
						=> await That(subject).IsNot<OtherClass>();

					await That(Act).DoesNotThrow();
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
						=> await That(subject).IsNot<MyBaseClass>()
							.Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage($$"""
						               Expected that subject
						               is not type MyBaseClass, because we want to test the failure,
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
						=> await That(subject).IsNot<MyClass>();

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
						=> await That(subject).IsNot<MyClass>()
							.Because(reason);

					await That(Act).Throws<XunitException>()
						.WithMessage($$"""
						               Expected that subject
						               is not type MyClass, because {{reason}},
						               but it was MyClass {
						                 Value = {{value}}
						               }
						               """);
				}
			}
		}
	}
}
