using System.IO;

namespace aweXpect.Tests.Streams;

public sealed partial class StreamShould
{
	public sealed class NotBeReadable
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNotReadable_ShouldSucceed()
			{
				Stream subject = new MyStream(canRead: false);

				async Task Act()
					=> await That(subject).Should().NotBeReadable();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).Should().NotBeReadable();

				await That(Act).Does().Throw<XunitException>()
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

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be readable,
					             but it was
					             """);
			}
		}
	}
}
