using System.IO;

namespace aweXpect.Tests.Streams;

public sealed partial class StreamShould
{
	public sealed class NotHaveLength
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentLength_ShouldSucceed(long length)
			{
				long actualLength = length > 10000 ? length - 1 : length + 1;
				Stream subject = new MyStream(length: actualLength);

				async Task Act()
					=> await That(subject).Should().NotHaveLength(length);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSameLength_ShouldFail(long length)
			{
				Stream subject = new MyStream(length: length);

				async Task Act()
					=> await That(subject).Should().NotHaveLength(length);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have length {length},
					              but it had
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).Should().NotHaveLength(0);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have length 0,
					             but it was <null>
					             """);
			}
		}
	}
}
