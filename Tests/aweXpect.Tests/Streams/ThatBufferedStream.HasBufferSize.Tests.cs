#if NET8_0_OR_GREATER
using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatBufferedStream
{
	public sealed class HasBufferSize
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentBufferSize_ShouldFail(int bufferSize)
			{
				int actualBufferSize = bufferSize > 10000 ? bufferSize - 1 : bufferSize + 1;
				using BufferedStream subject = GetBufferedStream(actualBufferSize);

				async Task Act()
					=> await That(subject).HasBufferSize(bufferSize);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have buffer size {bufferSize},
					              but it had buffer size {actualBufferSize}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSameBufferSize_ShouldSucceed(int bufferSize)
			{
				using BufferedStream subject = GetBufferedStream(bufferSize);

				async Task Act()
					=> await That(subject).HasBufferSize(bufferSize);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				using BufferedStream? subject = null;

				async Task Act()
					=> await That(subject).HasBufferSize(0);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have buffer size 0,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
