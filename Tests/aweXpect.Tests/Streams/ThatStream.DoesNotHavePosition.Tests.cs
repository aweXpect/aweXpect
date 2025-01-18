using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatStream
{
	public sealed class DoesNotHavePosition
	{
		public sealed class Tests
		{
			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentPosition_ShouldSucceed(long position)
			{
				long actualPosition = position > 10000 ? position - 1 : position + 1;
				Stream subject = new MyStream(position: actualPosition);

				async Task Act()
					=> await That(subject).DoesNotHavePosition(position);

				await That(Act).Does().NotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSamePosition_ShouldFail(long position)
			{
				Stream subject = new MyStream(position: position);

				async Task Act()
					=> await That(subject).DoesNotHavePosition(position);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              not have position {position},
					              but it had
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).DoesNotHavePosition(0);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have position 0,
					             but it was <null>
					             """);
			}
		}
	}
}
