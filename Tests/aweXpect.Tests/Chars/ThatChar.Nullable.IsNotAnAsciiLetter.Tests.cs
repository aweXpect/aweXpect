namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed partial class Nullable
	{
		public sealed class IsNotAnAsciiLetter
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
				[InlineData('\u4E50')]
				public async Task WhenSubjectIsNoAsciiLetter_ShouldSucceed(char? subject)
				{
					async Task Act()
						=> await That(subject).IsNotAnAsciiLetter();

					await That(Act).DoesNotThrow();
				}

				[Theory]
				[InlineData('a')]
				[InlineData('d')]
				[InlineData('z')]
				[InlineData('A')]
				[InlineData('M')]
				[InlineData('Z')]
				public async Task WhenSubjectIsNotAnAsciiLetter_ShouldFail(char? subject)
				{
					async Task Act()
						=> await That(subject).IsNotAnAsciiLetter();

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not an ASCII letter,
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					char? subject = null;

					async Task Act()
						=> await That(subject).IsNotAnAsciiLetter();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is not an ASCII letter,
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
				[InlineData('\u4E50')]
				public async Task WhenSubjectIsNoAsciiLetter_ShouldFail(char? subject)
				{
					async Task Act()
						=> await That(subject).DoesNotComplyWith(it => it.IsNotAnAsciiLetter());

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is an ASCII letter,
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
				public async Task WhenSubjectIsNotAnAsciiLetter_ShouldSucceed(char? subject)
				{
					async Task Act()
						=> await That(subject).DoesNotComplyWith(it => it.IsNotAnAsciiLetter());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					char? subject = null;

					async Task Act()
						=> await That(subject).DoesNotComplyWith(it => it.IsNotAnAsciiLetter());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is an ASCII letter,
						             but it was <null>
						             """);
				}
			}
		}
	}
}
