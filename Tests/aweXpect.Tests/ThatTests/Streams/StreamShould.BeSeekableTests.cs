using System.IO;

namespace Testably.Expectations.Tests.ThatTests.Streams;

public sealed partial class StreamShould
{
	public sealed class BeSeekableTests
	{
		[Fact]
		public async Task WhenSubjectIsNotSeekable_ShouldFail()
		{
			Stream subject = new MyStream(canSeek: false);

			async Task Act()
				=> await That(subject).Should().BeSeekable();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be seekable,
				             but it was not
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Stream? subject = null;

			async Task Act()
				=> await That(subject).Should().BeSeekable();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be seekable,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsSeekable_ShouldSucceed()
		{
			Stream subject = new MyStream(canSeek: true);

			async Task Act()
				=> await That(subject).Should().BeSeekable();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeSeekableTests
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
