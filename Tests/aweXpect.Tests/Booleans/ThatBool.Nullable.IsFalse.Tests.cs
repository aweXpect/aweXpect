namespace aweXpect.Tests;

public sealed partial class ThatBool
{
	public sealed partial class Nullable
	{
		public sealed class IsFalse
		{
			public sealed class Tests
			{
				[Fact]
				public async Task WhenFalse_ShouldSucceed()
				{
					bool? subject = false;

					async Task Act()
						=> await That(subject).IsFalse();

					await That(Act).DoesNotThrow();
				}

				[Theory]
				[InlineData(true)]
				[InlineData(null)]
				public async Task WhenTrueOrNull_ShouldFail(bool? subject)
				{
					async Task Act()
						=> await That(subject).IsFalse().Because("we want to test the failure");

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is False, because we want to test the failure,
						              but it was {Formatter.Format(subject)}
						              """);
				}
			}
		}
	}
}
