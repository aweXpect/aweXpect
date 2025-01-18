#if NET8_0_OR_GREATER
using System.IO;

// ReSharper disable AccessToDisposedClosure

namespace aweXpect.Tests;

public sealed partial class ThatBufferedStream
{
	public sealed class DoesNotHaveBufferSize
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentBufferSize_ShouldSucceed(int bufferSize)
			{
				int actualBufferSize = bufferSize > 10000 ? bufferSize - 1 : bufferSize + 1;
				using BufferedStream subject = GetBufferedStream(actualBufferSize);

				async Task Act()
					=> await That(subject).DoesNotHaveBufferSize(bufferSize);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSameBufferSize_ShouldFail(int bufferSize)
			{
				using BufferedStream subject = GetBufferedStream(bufferSize);

				async Task Act()
					=> await That(subject).DoesNotHaveBufferSize(bufferSize);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have buffer size {bufferSize},
					              but it had
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				using BufferedStream? subject = null;

				async Task Act()
					=> await That(subject).DoesNotHaveBufferSize(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have buffer size 0,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
