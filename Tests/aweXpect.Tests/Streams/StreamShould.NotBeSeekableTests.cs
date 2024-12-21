using System.IO;

namespace aweXpect.Tests.Streams;

public sealed partial class StreamShould
{
	public sealed class NotBeSeekable
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNotSeekable_ShouldSucceed()
			{
				Stream subject = new MyStream(canSeek: false);

				async Task Act()
					=> await That(subject).Should().NotBeSeekable();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBeSeekable();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be seekable,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsSeekable_ShouldFail()
			{
				Stream subject = new MyStream(canSeek: true);

				async Task Act()
					=> await That(subject).Should().NotBeSeekable();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be seekable,
					             but it was
					             """);
			}
		}
	}
}
