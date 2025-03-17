﻿namespace aweXpect.Tests;

public sealed partial class ThatBool
{
	public sealed partial class Nullable
	{
		public sealed class IsNotTrue
		{
			public sealed class Tests
			{
				[Theory]
				[InlineData(false)]
				[InlineData(null)]
				public async Task WhenFalseOrNull_ShouldFail(bool? subject)
				{
					async Task Act()
						=> await That(subject).IsNotTrue();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenTrue_ShouldFail()
				{
					bool? subject = true;

					async Task Act()
						=> await That(subject).IsNotTrue().Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is not True, because we want to test the failure,
						             but it was
						             """);
				}
			}
		}
	}
}
