﻿namespace aweXpect.Tests;

public sealed partial class ThatEnum
{
	public sealed partial class Nullable
	{
		public sealed class IsDefined
		{
			public sealed class Tests
			{
				[Theory]
				[InlineData(MyColors.Blue)]
				[InlineData(MyColors.Green)]
				public async Task WhenSubjectIsDefined_ShouldSucceed(MyColors? subject)
				{
					async Task Act()
						=> await That(subject).IsDefined();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNotDefined_ShouldFail()
				{
					MyColors? subject = (MyColors)42;

					async Task Act()
						=> await That(subject).IsDefined();

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is defined,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					MyColors? subject = null;

					async Task Act()
						=> await That(subject).IsDefined();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is defined,
						             but it was <null>
						             """);
				}
			}
		}
	}
}
