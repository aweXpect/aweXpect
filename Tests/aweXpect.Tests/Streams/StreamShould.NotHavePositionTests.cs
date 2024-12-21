using System.IO;

namespace aweXpect.Tests.Streams;

public sealed partial class StreamShould
{
	public sealed class NotHavePosition
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentPosition_ShouldSucceed(long position)
			{
				long actualPosition = position > 10000 ? position - 1 : position + 1;
				Stream subject = new MyStream(position: actualPosition);

				async Task Act()
					=> await That(subject).Should().NotHavePosition(position);

				await That(Act).Should().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSamePosition_ShouldFail(long position)
			{
				Stream subject = new MyStream(position: position);

				async Task Act()
					=> await That(subject).Should().NotHavePosition(position);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have position {position},
					              but it had
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).Should().NotHavePosition(0);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have position 0,
					             but it was <null>
					             """);
			}
		}
	}
}
