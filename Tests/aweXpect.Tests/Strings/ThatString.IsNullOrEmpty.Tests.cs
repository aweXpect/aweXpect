namespace aweXpect.Tests;

public sealed partial class ThatString
{
	public class IsNullOrEmpty
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActualIsEmpty_ShouldSucceed()
			{
				string subject = "";

				async Task Act()
					=> await That(subject).IsNullOrEmpty();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenActualIsNotEmpty_ShouldFail(string? subject)
			{
				async Task Act()
					=> await That(subject).IsNullOrEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is null or empty,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenActualIsNotEmpty_ShouldLimitDisplayedStringTo100Characters()
			{
				string subject = StringWithMoreThan100Characters;

				async Task Act()
					=> await That(subject).IsNullOrEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is null or empty,
					              but it was "{StringWith100Characters}…"
					              """);
			}

			[Fact]
			public async Task WhenActualIsNull_ShouldSucceed()
			{
				string? subject = null;

				async Task Act()
					=> await That(subject).IsNullOrEmpty();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenActualIsWhitespace_ShouldFail()
			{
				string subject = " \t ";

				async Task Act()
					=> await That(subject).IsNullOrEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is null or empty,
					             but it was " \t "
					             """);
			}
		}
	}
}
