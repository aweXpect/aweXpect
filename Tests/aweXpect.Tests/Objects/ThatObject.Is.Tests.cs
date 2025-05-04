﻿using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class Is
	{
		public sealed class GenericTests
		{
			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnTypedResult(int value)
			{
				object subject = new MyClass
				{
					Value = value,
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
					             is type ThatObject.MyClass,
					             but it was <null>
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeDoesNotMatch_ShouldFail(int value)
			{
				object subject = new MyClass
				{
					Value = value,
				};

				async Task Act()
					=> await That(subject).Is<OtherClass>()
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($$"""
					               Expected that subject
					               is type ThatObject.OtherClass, because we want to test the failure,
					               but it was ThatObject.MyClass {
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
					Value = value,
				};

				async Task Act()
					=> await That(subject).Is<MyClass>()
						.Because(reason);

				await That(Act).Throws<XunitException>()
					.WithMessage($$"""
					               Expected that subject
					               is type ThatObject.MyClass, because {{reason}},
					               but it was ThatObject.MyBaseClass {
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

		public sealed class TypeTests
		{
			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnTypedResult(int value)
			{
				object subject = new MyClass
				{
					Value = value,
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

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is type ThatObject.MyClass,
					             but it was <null>
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeDoesNotMatch_ShouldFail(int value)
			{
				object subject = new MyClass
				{
					Value = value,
				};

				async Task Act()
					=> await That(subject).Is(typeof(OtherClass))
						.Because("we want to test the failure");

				await That(Act).Throws<XunitException>()
					.WithMessage($$"""
					               Expected that subject
					               is type ThatObject.OtherClass, because we want to test the failure,
					               but it was ThatObject.MyClass {
					                   Value = {{value}}
					                 }
					               """);
			}

			[Fact]
			public async Task WhenTypeIsNull_ShouldThrowArgumentNullException()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Is(null!);

				await That(Act).Throws<ArgumentNullException>()
					.WithParamName("type").And
					.WithMessage("The type cannot be null.").AsPrefix();
			}

			[Fact]
			public async Task WhenTypeIsSubtype_ShouldSucceed()
			{
				object subject = new MyClass();

				async Task Act()
					=> await That(subject).Is(typeof(MyBaseClass));

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenTypeIsSupertype_ShouldFail(int value, string reason)
			{
				object subject = new MyBaseClass
				{
					Value = value,
				};

				async Task Act()
					=> await That(subject).Is(typeof(MyClass))
						.Because(reason);

				await That(Act).Throws<XunitException>()
					.WithMessage($$"""
					               Expected that subject
					               is type ThatObject.MyClass, because {{reason}},
					               but it was ThatObject.MyBaseClass {
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

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMatchingOpenGenericInterfaceType_ShouldSucceed()
			{
				List<string> subject = new();

				async Task Act()
					=> await That(subject).Is(typeof(IList<>));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMatchingOpenGenericType_ShouldSucceed()
			{
				List<string> subject = new();

				async Task Act()
					=> await That(subject).Is(typeof(List<>));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithNotMatchingOpenGenericInterfaceType_ShouldFail()
			{
				List<string> subject = new();

				async Task Act()
					=> await That(subject).Is(typeof(IDictionary<,>));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is type IDictionary<, >,
					             but it was List<string> []
					             """);
			}
		}
	}
}
