﻿namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class Is
	{
		public sealed class WhoseTests
		{
			[Fact]
			public async Task WhenPropertyDoesNotMatch_ShouldSucceed()
			{
				object subject = new MyClass
				{
					Value = 42,
				};

				async Task Act()
					=> await That(subject).Is<MyClass>()
						.Whose(it => it.Value, value => value.IsLessThan(42));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is type ThatObject.MyClass whose .Value is less than 42,
					             but .Value was 42
					             """);
			}

			[Fact]
			public async Task WhenPropertyMatches_ShouldSucceed()
			{
				object subject = new MyClass
				{
					Value = 42,
				};

				async Task Act()
					=> await That(subject).Is<MyClass>().Whose(it => it.Value, value => value.IsEqualTo(42));

				await That(Act).DoesNotThrow();
			}
		}
	}
}
