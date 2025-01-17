using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatStream
{
	public sealed class HasLength
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentLength_ShouldFail(long length)
			{
				long actualLength = length > 10000 ? length - 1 : length + 1;
				Stream subject = new MyStream(length: actualLength);

				async Task Act()
					=> await That(subject).HasLength(length);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have length {length},
					              but it had length {actualLength}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSameLength_ShouldSucceed(long length)
			{
				Stream subject = new MyStream(length: length);

				async Task Act()
					=> await That(subject).HasLength(length);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).HasLength(0);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have length 0,
					             but it was <null>
					             """);
			}
		}
	}
}
