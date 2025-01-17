using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatStream
{
	public sealed class IsNotWritable
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNotWritable_ShouldSucceed()
			{
				Stream subject = new MyStream(canWrite: false);

				async Task Act()
					=> await That(subject).IsNotWritable();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).IsNotWritable();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsNotWritable();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be writable,
					             but it was
					             """);
			}
		}
	}
}
