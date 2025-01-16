using System.IO;

namespace aweXpect.Tests.Streams;

public sealed partial class StreamShould
{
	public sealed class BeSeekable
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNotSeekable_ShouldFail()
			{
				Stream subject = new MyStream(canSeek: false);

				async Task Act()
					=> await That(subject).Should().BeSeekable();

				await That(Act).Does().Throw<XunitException>()
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

				await That(Act).Does().Throw<XunitException>()
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

				await That(Act).Does().NotThrow();
			}
		}
	}
}
