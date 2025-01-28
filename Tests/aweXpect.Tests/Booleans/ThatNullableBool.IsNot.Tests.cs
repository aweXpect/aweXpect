namespace aweXpect.Tests;

public sealed partial class ThatNullableBool
{
	public sealed class IsNot
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(true, false)]
			[InlineData(true, null)]
			[InlineData(false, true)]
			[InlineData(false, null)]
			[InlineData(null, true)]
			[InlineData(null, false)]
			public async Task WhenSubjectIsDifferent_ShouldSucceed(bool? subject, bool? unexpected)
			{
				async Task Act()
					=> await That(subject).IsNotEqualTo(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			[InlineData(null)]
			public async Task WhenSubjectIsTheSame_ShouldFail(bool? subject)
			{
				bool? unexpected = subject;

				async Task Act()
					=> await That(subject).IsNotEqualTo(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not be {Formatter.Format(unexpected)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
