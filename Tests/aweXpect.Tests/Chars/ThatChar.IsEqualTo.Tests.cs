namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed class IsEqualTo
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				char subject = 'a';

				async Task Act()
					=> await That(subject).IsEqualTo(null);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to <null>,
					             but it was 'a'
					             """);
			}

			[Theory]
			[InlineData('a', 'b')]
			[InlineData('B', 'b')]
			public async Task WhenSubjectIsDifferent_ShouldFail(char subject, char expected)
			{
				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is equal to {Formatter.Format(expected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData('a')]
			[InlineData('X')]
			[InlineData('5')]
			[InlineData('\t')]
			public async Task WhenSubjectIsTheSame_ShouldSucceed(char subject)
			{
				char expected = subject;

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
