namespace aweXpect.Tests.Enums;

public sealed partial class NullableEnumShould
{
	public sealed class BeNull
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(MyColors.Blue)]
			[InlineData((MyColors)42)]
			public async Task WhenSubjectIsNotNull_ShouldFail(MyColors? subject)
			{
				async Task Act()
					=> await That(subject).Should().BeNull();

				await That(Act).Should().Throw<XunitException>()
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
					=> await That(subject).Should().BeNull();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
