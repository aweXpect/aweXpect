using System.IO;

namespace Testably.Expectations.Tests.ThatTests.Streams;

public sealed partial class StreamShould
{
	public sealed class HavePositionTests
	{
		[Theory]
		[AutoData]
		public async Task WhenSubjectHasDifferentPosition_ShouldFail(long position)
		{
			long actualPosition = position > 10000 ? position - 1 : position + 1;
			Stream subject = new MyStream(position: actualPosition);

			async Task Act()
				=> await That(subject).Should().HavePosition(position);

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().HavePosition(position);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Stream? subject = null;

			async Task Act()
				=> await That(subject).Should().HavePosition(0);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have position 0,
				             but it was <null>
				             """);
		}
	}

	public sealed class NotHavePositionTests
	{
		[Theory]
		[AutoData]
		public async Task WhenSubjectHasDifferentPosition_ShouldSucceed(long position)
		{
			long actualPosition = position > 10000 ? position - 1 : position + 1;
			Stream subject = new MyStream(position: actualPosition);

			async Task Act()
				=> await That(subject).Should().NotHavePosition(position);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task WhenSubjectHasSamePosition_ShouldFail(long position)
		{
			Stream subject = new MyStream(position: position);

			async Task Act()
				=> await That(subject).Should().NotHavePosition(position);

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().NotHavePosition(0);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not have position 0,
				             but it was <null>
				             """);
		}
	}
}
