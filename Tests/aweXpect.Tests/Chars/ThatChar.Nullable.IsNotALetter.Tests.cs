namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed partial class Nullable
	{
		public sealed class IsNotALetter
		{
			public sealed class Tests
			{
				[Theory]
				[InlineData('\t')]
				[InlineData('5')]
				[InlineData('@')]
				[InlineData('[')]
				[InlineData(']')]
				[InlineData('{')]
				[InlineData('}')]
				public async Task WhenSubjectIsNoLetter_ShouldSucceed(char? subject)
				{
					async Task Act()
						=> await That(subject).IsNotALetter();

					await That(Act).DoesNotThrow();
				}

				[Theory]
				[InlineData('a')]
				[InlineData('d')]
				[InlineData('z')]
				[InlineData('A')]
				[InlineData('M')]
				[InlineData('Z')]
				[InlineData('\u4E50')]
				public async Task WhenSubjectIsNotALetter_ShouldFail(char? subject)
				{
					async Task Act()
						=> await That(subject).IsNotALetter();

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not a letter,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					char? subject = null;

					async Task Act()
						=> await That(subject).IsNotALetter();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is not a letter,
						             but it was <null>
						             """);
				}
			}

			public sealed class NegatedTests
			{
				[Theory]
				[InlineData('\t')]
				[InlineData('5')]
				[InlineData('@')]
				[InlineData('[')]
				[InlineData(']')]
				[InlineData('{')]
				[InlineData('}')]
				public async Task WhenSubjectIsNoLetter_ShouldFail(char? subject)
				{
					async Task Act()
						=> await That(subject).DoesNotComplyWith(it => it.IsNotALetter());

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is a letter,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Theory]
				[InlineData('a')]
				[InlineData('d')]
				[InlineData('z')]
				[InlineData('A')]
				[InlineData('M')]
				[InlineData('Z')]
				[InlineData('\u4E50')]
				public async Task WhenSubjectIsNotALetter_ShouldSucceed(char? subject)
				{
					async Task Act()
						=> await That(subject).DoesNotComplyWith(it => it.IsNotALetter());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					char? subject = null;

					async Task Act()
						=> await That(subject).DoesNotComplyWith(it => it.IsNotALetter());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is a letter,
						             but it was <null>
						             """);
				}
			}
		}
	}
}
