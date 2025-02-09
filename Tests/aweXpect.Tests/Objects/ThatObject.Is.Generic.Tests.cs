namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class Is
	{
		public sealed class Generic
		{
			public sealed class Tests
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

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is type MyClass,
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

					await That(Act).Throws<XunitException>()
						.WithMessage($$"""
						               Expected that subject
						               is type OtherClass, because we want to test the failure,
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

					await That(Act).DoesNotThrow();
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

					await That(Act).Throws<XunitException>()
						.WithMessage($$"""
						               Expected that subject
						               is type MyClass, because {{reason}},
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

					await That(Act).DoesNotThrow();
				}
			}

			public sealed class WhoseTests
			{
				[Fact]
				public async Task WhenPropertyDoesNotMatch_ShouldSucceed()
				{
					object subject = new MyClass
					{
						Value = 42
					};

					async Task Act()
						=> await That(subject).Is<MyClass>()
							.Whose(x => x.Value, x => x.IsLessThan(42));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is type MyClass whose .Value is less than 42,
						             but .Value was 42
						             """);
				}

				[Fact]
				public async Task WhenPropertyMatches_ShouldSucceed()
				{
					object subject = new MyClass
					{
						Value = 42
					};

					async Task Act()
						=> await That(subject).Is<MyClass>().Whose(x => x.Value, x => x.IsEqualTo(42));

					await That(Act).DoesNotThrow();
				}
			}
		}
	}
}
