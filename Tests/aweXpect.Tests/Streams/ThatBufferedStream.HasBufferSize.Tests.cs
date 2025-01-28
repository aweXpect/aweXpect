#if NET8_0_OR_GREATER
using System.IO;

// ReSharper disable AccessToDisposedClosure

namespace aweXpect.Tests;

public sealed partial class ThatBufferedStream
{
	public sealed class HasBufferSize
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenExpectedBufferSizeIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				using BufferedStream subject = GetBufferedStream(1);

				async Task Act()
					=> await That(subject).HasBufferSize().EqualTo(-1);

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.WithMessage("*The expected buffer size must be greater than or equal to zero*")
					.AsWildcard().And
					.WithParamName("expected");
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentBufferSize_ShouldFail(int bufferSize)
			{
				int actualBufferSize = bufferSize > 10000 ? bufferSize - 1 : bufferSize + 1;
				using BufferedStream subject = GetBufferedStream(actualBufferSize);

				async Task Act()
					=> await That(subject).HasBufferSize().EqualTo(bufferSize);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have buffer size equal to {bufferSize},
					              but it had buffer size {actualBufferSize}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSameBufferSize_ShouldSucceed(int bufferSize)
			{
				using BufferedStream subject = GetBufferedStream(bufferSize);

				async Task Act()
					=> await That(subject).HasBufferSize().EqualTo(bufferSize);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				using BufferedStream? subject = null;

				async Task Act()
					=> await That(subject).HasBufferSize().EqualTo(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have buffer size equal to 0,
					             but it was <null>
					             """);
			}
		}

		public sealed class NotEqualToTests
		{
			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentBufferSize_ShouldSucceed(int bufferSize)
			{
				int actualBufferSize = bufferSize > 10000 ? bufferSize - 1 : bufferSize + 1;
				using BufferedStream subject = GetBufferedStream(actualBufferSize);

				async Task Act()
					=> await That(subject).HasBufferSize().NotEqualTo(bufferSize);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSameBufferSize_ShouldFail(int bufferSize)
			{
				using BufferedStream subject = GetBufferedStream(bufferSize);

				async Task Act()
					=> await That(subject).HasBufferSize().NotEqualTo(bufferSize);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have buffer size not equal to {bufferSize},
					              but it had buffer size {bufferSize}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				using BufferedStream? subject = null;

				async Task Act()
					=> await That(subject).HasBufferSize().NotEqualTo(0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedBufferSizeIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				using BufferedStream subject = GetBufferedStream(1);

				async Task Act()
					=> await That(subject).HasBufferSize().NotEqualTo(-1);

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.WithMessage("*The unexpected buffer size must be greater than or equal to zero*")
					.AsWildcard().And
					.WithParamName("unexpected");
			}
		}
	}
}
#endif
