namespace Testably.Expectations.Tests.ThatTests.Objects;

public sealed partial class ObjectShould
{
	public sealed class BeTests
	{
		[Theory]
		[AutoData]
		public async Task ForGeneric_WhenAwaited_ShouldReturnTypedResult(int value)
		{
			object subject = new MyClass { Value = value };

			MyClass result = await That(subject).Should().Be<MyClass>();
			await That(result.Value).Should().Be(value);
		}

		[Theory]
		[AutoData]
		public async Task ForGeneric_WhenTypeDoesNotMatch_ShouldFail(int value)
		{
			object subject = new MyClass { Value = value };

			async Task Act()
				=> await That(subject).Should().Be<OtherClass>()
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($$"""
				               Expected subject to
				               be type OtherClass, because we want to test the failure,
				               but it was MyClass {
				                 Value = {{value}}
				               }
				               """);
		}

		[Fact]
		public async Task ForGeneric_WhenTypeIsSubtype_ShouldSucceed()
		{
			object subject = new MyClass();

			async Task Act()
				=> await That(subject).Should().Be<MyBaseClass>();

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForGeneric_WhenTypeIsSupertype_ShouldFail(int value, string reason)
		{
			object subject = new MyBaseClass { Value = value };

			async Task Act()
				=> await That(subject).Should().Be<MyClass>()
					.Because(reason);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($$"""
				               Expected subject to
				               be type MyClass, because {{reason}},
				               but it was MyBaseClass {
				                 Value = {{value}}
				               }
				               """);
		}

		[Fact]
		public async Task ForGeneric_WhenTypeMatches_ShouldSucceed()
		{
			object subject = new MyClass();

			async Task Act()
				=> await That(subject).Should().Be<MyClass>();

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForType_WhenAwaited_ShouldReturnTypedResult(int value)
		{
			object subject = new MyClass { Value = value };

			object? result = await That(subject).Should().Be(typeof(MyClass));

			await That(result).Should().BeSameAs(subject);
		}

		[Theory]
		[AutoData]
		public async Task ForType_WhenTypeDoesNotMatch_ShouldFail(int value)
		{
			object subject = new MyClass { Value = value };

			async Task Act()
				=> await That(subject).Should().Be(typeof(OtherClass))
					.Because("we want to test the failure");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($$"""
				               Expected subject to
				               be type OtherClass, because we want to test the failure,
				               but it was MyClass {
				                 Value = {{value}}
				               }
				               """);
		}

		[Fact]
		public async Task ForType_WhenTypeIsSubtype_ShouldSucceed()
		{
			object subject = new MyClass();

			async Task Act()
				=> await That(subject).Should().Be(typeof(MyBaseClass));

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task ForType_WhenTypeIsSupertype_ShouldFail(int value, string reason)
		{
			object subject = new MyBaseClass { Value = value };

			async Task Act()
				=> await That(subject).Should().Be(typeof(MyClass))
					.Because(reason);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($$"""
				               Expected subject to
				               be type MyClass, because {{reason}},
				               but it was MyBaseClass {
				                 Value = {{value}}
				               }
				               """);
		}

		[Fact]
		public async Task ForType_WhenTypeMatches_ShouldSucceed()
		{
			object subject = new MyClass();

			async Task Act()
				=> await That(subject).Should().Be(typeof(MyClass));

			await That(Act).Should().NotThrow();
		}
	}
}
