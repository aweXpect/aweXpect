﻿namespace aweXpect.Tests.Objects;

public sealed partial class ObjectShould
{
	public sealed class BeExactly
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

				MyClass result = await That(subject).Should().BeExactly<MyClass>();

				await That(result).Should().BeSameAs(subject);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).Should().BeExactly<MyClass>();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be exactly type MyClass,
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
					=> await That(subject).Should().BeExactly<OtherClass>()
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               be exactly type OtherClass, because we want to test the failure,
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
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
					=> await That(subject).Should().BeExactly<MyBaseClass>()
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               be exactly type MyBaseClass, because we want to test the failure,
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
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
					=> await That(subject).Should().BeExactly<MyClass>()
						.Because(reason);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               be exactly type MyClass, because {{reason}},
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
					=> await That(subject).Should().BeExactly<MyClass>();

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

				object? result = await That(subject).Should().BeExactly(typeof(MyClass));

				await That(result).Should().BeSameAs(subject);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				object? subject = null;

				async Task Act()
					=> await That(subject).Should().BeExactly(typeof(MyClass));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be exactly type MyClass,
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
					=> await That(subject).Should().BeExactly(typeof(OtherClass))
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               be exactly type OtherClass, because we want to test the failure,
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeIsSubtype_ShouldSucceed(int value)
			{
				object subject = new MyClass
				{
					Value = value
				};

				async Task Act()
					=> await That(subject).Should().BeExactly(typeof(MyBaseClass))
						.Because("we want to test the failure");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               be exactly type MyBaseClass, because we want to test the failure,
					               but it was MyClass {
					                 Value = {{value}}
					               }
					               """);
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
					=> await That(subject).Should().BeExactly(typeof(MyClass))
						.Because(reason);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($$"""
					               Expected subject to
					               be exactly type MyClass, because {{reason}},
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
					=> await That(subject).Should().BeExactly(typeof(MyClass));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
