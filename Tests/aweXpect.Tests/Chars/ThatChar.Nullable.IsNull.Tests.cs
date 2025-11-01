namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed partial class Nullable
	{
		public sealed class IsNull
		{
			public sealed class Tests
			{
				[Theory]
				[InlineData('a')]
				[InlineData('X')]
				[InlineData('5')]
				[InlineData('\t')]
				public async Task WhenSubjectIsNotNull_ShouldFail(char? subject)
				{
					async Task Act()
						=> await That(subject).IsNull();

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is null,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldSucceed()
				{
					char? subject = null;

					async Task Act()
						=> await That(subject).IsNull();

					await That(Act).DoesNotThrow();
				}
			}
		}
	}
}
