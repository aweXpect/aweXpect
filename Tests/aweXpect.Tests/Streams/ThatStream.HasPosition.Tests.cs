using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatStream
{
	public sealed class HasPosition
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentPosition_ShouldFail(long position)
			{
				long actualPosition = position > 10000 ? position - 1 : position + 1;
				Stream subject = new MyStream(position: actualPosition);

				async Task Act()
					=> await That(subject).HasPosition(position);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have position {position},
					              but it had position {actualPosition}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSamePosition_ShouldSucceed(long position)
			{
				Stream subject = new MyStream(position: position);

				async Task Act()
					=> await That(subject).HasPosition(position);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).HasPosition(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have position 0,
					             but it was <null>
					             """);
			}
		}
	}
}
