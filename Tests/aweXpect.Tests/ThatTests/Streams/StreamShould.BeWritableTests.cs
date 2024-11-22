using System.IO;

namespace Testably.Expectations.Tests.ThatTests.Streams;

public sealed partial class StreamShould
{
	public sealed class BeWritableTests
	{
		[Fact]
		public async Task WhenSubjectIsNotWritable_ShouldFail()
		{
			Stream subject = new MyStream(canWrite: false);

			async Task Act()
				=> await That(subject).Should().BeWritable();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be writable,
				             but it was not
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Stream? subject = null;

			async Task Act()
				=> await That(subject).Should().BeWritable();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be writable,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsWritable_ShouldSucceed()
		{
			Stream subject = new MyStream(canWrite: true);

			async Task Act()
				=> await That(subject).Should().BeWritable();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeWritableTests
	{
		[Fact]
		public async Task WhenSubjectIsNotWritable_ShouldSucceed()
		{
			Stream subject = new MyStream(canWrite: false);

			async Task Act()
				=> await That(subject).Should().NotBeWritable();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Stream? subject = null;

			async Task Act()
				=> await That(subject).Should().NotBeWritable();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be writable,
				             but it was <null>
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsWritable_ShouldFail()
		{
			Stream subject = new MyStream(canWrite: true);

			async Task Act()
				=> await That(subject).Should().NotBeWritable();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be writable,
				             but it was
				             """);
		}
	}
}
