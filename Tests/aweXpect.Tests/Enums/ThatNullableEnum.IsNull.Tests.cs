namespace aweXpect.Tests;

public sealed partial class ThatNullableEnum
{
	public sealed class IsNull
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData((MyColors)42)]
			public async Task WhenSubjectIsNotNull_ShouldFail(MyColors? subject)
			{
				async Task Act()
					=> await That(subject).IsNull();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              be null,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				MyColors? subject = null;

				async Task Act()
					=> await That(subject).IsNull();

				await That(Act).Does().NotThrow();
			}
		}
	}
}
