using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatStream
{
	public sealed class DoesNotHaveLength
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
					=> await That(subject).DoesNotHaveLength(length);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSameLength_ShouldFail(long length)
			{
				Stream subject = new MyStream(length: length);

				async Task Act()
					=> await That(subject).DoesNotHaveLength(length);

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).DoesNotHaveLength(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have length 0,
					             but it was <null>
					             """);
			}
		}
	}
}
