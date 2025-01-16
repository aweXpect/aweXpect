using System.IO;

namespace aweXpect.Tests.Streams;

public sealed partial class StreamShould
{
	public sealed class BeReadable
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNotReadable_ShouldFail()
			{
				Stream subject = new MyStream(canRead: false);

				async Task Act()
					=> await That(subject).Should().BeReadable();

				await That(Act).Does().Throw<XunitException>()
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

				await That(Act).Does().Throw<XunitException>()
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

				await That(Act).Does().NotThrow();
			}
		}
	}
}
