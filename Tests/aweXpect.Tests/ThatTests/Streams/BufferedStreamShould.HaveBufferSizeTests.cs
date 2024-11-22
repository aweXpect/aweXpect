#if NET6_0_OR_GREATER
using System.IO;

namespace Testably.Expectations.Tests.ThatTests.Streams;

public sealed partial class BufferedStreamShould
{
	public sealed class HaveBufferSizeTests
	{
		[Theory]
		[AutoData]
		public async Task WhenSubjectHasDifferentBufferSize_ShouldFail(int bufferSize)
		{
			int actualBufferSize = bufferSize > 10000 ? bufferSize - 1 : bufferSize + 1;
			using BufferedStream subject = GetBufferedStream(actualBufferSize);

			async Task Act()
				=> await That(subject).Should().HaveBufferSize(bufferSize);

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().HaveBufferSize(bufferSize);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			using BufferedStream? subject = null;

			async Task Act()
				=> await That(subject).Should().HaveBufferSize(0);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have buffer size 0,
				             but it was <null>
				             """);
		}
	}

	public sealed class NotHaveBufferSizeTests
	{
		[Theory]
		[AutoData]
		public async Task WhenSubjectHasDifferentBufferSize_ShouldSucceed(int bufferSize)
		{
			int actualBufferSize = bufferSize > 10000 ? bufferSize - 1 : bufferSize + 1;
			using BufferedStream subject = GetBufferedStream(actualBufferSize);

			async Task Act()
				=> await That(subject).Should().NotHaveBufferSize(bufferSize);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task WhenSubjectHasSameBufferSize_ShouldFail(int bufferSize)
		{
			using BufferedStream subject = GetBufferedStream(bufferSize);

			async Task Act()
				=> await That(subject).Should().NotHaveBufferSize(bufferSize);

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().NotHaveBufferSize(0);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not have buffer size 0,
				             but it was <null>
				             """);
		}
	}
}
#endif
