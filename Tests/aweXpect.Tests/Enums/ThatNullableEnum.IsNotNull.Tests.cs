﻿namespace aweXpect.Tests;

public sealed partial class ThatNullableEnum
{
	public sealed class IsNotNull
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData((MyColors)42)]
			public async Task WhenSubjectIsNotNull_ShouldSucceed(MyColors? subject)
			{
				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				MyColors? subject = null;

				async Task Act()
					=> await That(subject).IsNotNull();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be null,
					             but it was <null>
					             """);
			}
		}
	}
}