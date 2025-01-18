using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatStream
{
	public sealed class IsNotReadable
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNotReadable_ShouldSucceed()
			{
				Stream subject = new MyStream(canRead: false);

				async Task Act()
					=> await That(subject).IsNotReadable();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).IsNotReadable();

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
					=> await That(subject).IsNotReadable();

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
