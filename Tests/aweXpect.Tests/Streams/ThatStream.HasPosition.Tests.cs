using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatStream
{
	public sealed class HasPosition
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenExpectedPositionIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				Stream subject = new MyStream();

				async Task Act()
					=> await That(subject).HasPosition().EqualTo(-1);

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.WithMessage("*The expected position must be greater than or equal to zero*")
					.AsWildcard().And
					.WithParamName("expected");
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentPosition_ShouldFail(long position)
			{
				long actualPosition = position > 10000 ? position - 1 : position + 1;
				Stream subject = new MyStream(position: actualPosition);

				async Task Act()
					=> await That(subject).HasPosition().EqualTo(position);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have position equal to {position},
					              but it had position {actualPosition}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSamePosition_ShouldSucceed(long position)
			{
				Stream subject = new MyStream(position: position);

				async Task Act()
					=> await That(subject).HasPosition().EqualTo(position);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).HasPosition().EqualTo(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have position equal to 0,
					             but it was <null>
					             """);
			}
		}

		public sealed class NotEqualToTests
		{
			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentPosition_ShouldSucceed(long position)
			{
				long actualPosition = position > 10000 ? position - 1 : position + 1;
				Stream subject = new MyStream(position: actualPosition);

				async Task Act()
					=> await That(subject).HasPosition().NotEqualTo(position);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSamePosition_ShouldFail(long position)
			{
				Stream subject = new MyStream(position: position);

				async Task Act()
					=> await That(subject).HasPosition().NotEqualTo(position);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have position not equal to {position},
					              but it had position {position}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).HasPosition().NotEqualTo(0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedPositionIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				Stream subject = new MyStream();

				async Task Act()
					=> await That(subject).HasPosition().NotEqualTo(-1);

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.WithMessage("*The unexpected position must be greater than or equal to zero*")
					.AsWildcard().And
					.WithParamName("unexpected");
			}
		}
	}
}
