using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatStream
{
	public sealed class IsWriteOnly
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(false, false)]
			[InlineData(true, false)]
			[InlineData(true, true)]
			public async Task WhenSubjectIsNotWriteOnly_ShouldFail(bool canRead, bool canWrite)
			{
				Stream subject = new MyStream(canRead: canRead, canWrite: canWrite);

				async Task Act()
					=> await That(subject).IsWriteOnly();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsWriteOnly();

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).IsWriteOnly();

				await That(Act).Does().NotThrow();
			}
		}
	}
}
