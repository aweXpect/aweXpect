using System.IO;

namespace Testably.Expectations.Tests.ThatTests.Streams;

public sealed partial class StreamShould
{
	public sealed class BeWriteOnlyTests
	{
		[Theory]
		[InlineData(false, false)]
		[InlineData(true, false)]
		[InlineData(true, true)]
		public async Task WhenSubjectIsNotWriteOnly_ShouldFail(bool canRead, bool canWrite)
		{
			Stream subject = new MyStream(canRead: canRead, canWrite: canWrite);

			async Task Act()
				=> await That(subject).Should().BeWriteOnly();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be write-only,
				             but it was not
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Stream? subject = null;

			async Task Act()
				=> await That(subject).Should().BeWriteOnly();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be write-only,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsWriteOnly_ShouldSucceed()
		{
			Stream subject = new MyStream(canRead: false, canWrite: true);

			async Task Act()
				=> await That(subject).Should().BeWriteOnly();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeWriteOnlyTests
	{
		[Theory]
		[InlineData(false, false)]
		[InlineData(true, false)]
		[InlineData(true, true)]
		public async Task WhenSubjectIsNotWriteOnly_ShouldSucceed(bool canRead, bool canWrite)
		{
			Stream subject = new MyStream(canRead: canRead, canWrite: canWrite);

			async Task Act()
				=> await That(subject).Should().NotBeWriteOnly();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Stream? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeWriteOnly();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be write-only,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsWriteOnly_ShouldFail()
		{
			Stream subject = new MyStream(canRead: false, canWrite: true);

			async Task Act()
				=> await That(subject).Should().NotBeWriteOnly();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be write-only,
				             but it was
				             """);
		}
	}
}
