namespace aweXpect.Tests;

public sealed partial class ThatNullableBool
{
	public sealed class BeTrue
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(false)]
			[InlineData(null)]
			public async Task WhenFalseOrNull_ShouldFail(bool? subject)
			{
				async Task Act()
					=> await That(subject).IsTrue().Because("we want to test the failure");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be True, because we want to test the failure,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenTrue_ShouldSucceed()
			{
				bool? subject = true;

				async Task Act()
					=> await That(subject).IsTrue();

				await That(Act).Does().NotThrow();
			}
		}
	}
}
