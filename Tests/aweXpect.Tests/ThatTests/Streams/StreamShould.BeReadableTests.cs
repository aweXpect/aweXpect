using System.IO;

namespace Testably.Expectations.Tests.ThatTests.Streams;

public sealed partial class StreamShould
{
	public sealed class BeReadableTests
	{
		[Fact]
		public async Task WhenSubjectIsNotReadable_ShouldFail()
		{
			Stream subject = new MyStream(canRead: false);

			async Task Act()
				=> await That(subject).Should().BeReadable();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be readable,
				             but it was not
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Stream? subject = null;

			async Task Act()
				=> await That(subject).Should().BeReadable();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be readable,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsReadable_ShouldSucceed()
		{
			Stream subject = new MyStream(canRead: true);

			async Task Act()
				=> await That(subject).Should().BeReadable();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeReadableTests
	{
		[Fact]
		public async Task WhenSubjectIsNotReadable_ShouldSucceed()
		{
			Stream subject = new MyStream(canRead: false);

			async Task Act()
				=> await That(subject).Should().NotBeReadable();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Stream? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeReadable();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be readable,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsReadable_ShouldFail()
		{
			Stream subject = new MyStream(canRead: true);

			async Task Act()
				=> await That(subject).Should().NotBeReadable();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be readable,
				             but it was
				             """);
		}
	}
}
