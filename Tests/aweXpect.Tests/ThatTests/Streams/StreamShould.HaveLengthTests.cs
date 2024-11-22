using System.IO;

namespace Testably.Expectations.Tests.ThatTests.Streams;

public sealed partial class StreamShould
{
	public sealed class HaveLengthTests
	{
		[Theory]
		[AutoData]
		public async Task WhenSubjectHasDifferentLength_ShouldFail(long length)
		{
			long actualLength = length > 10000 ? length - 1 : length + 1;
			Stream subject = new MyStream(length: actualLength);

			async Task Act()
				=> await That(subject).Should().HaveLength(length);

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().HaveLength(length);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Stream? subject = null;

			async Task Act()
				=> await That(subject).Should().HaveLength(0);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have length 0,
				             but it was <null>
				             """);
		}
	}

	public sealed class NotHaveLengthTests
	{
		[Theory]
		[AutoData]
		public async Task WhenSubjectHasDifferentLength_ShouldSucceed(long length)
		{
			long actualLength = length > 10000 ? length - 1 : length + 1;
			Stream subject = new MyStream(length: actualLength);

			async Task Act()
				=> await That(subject).Should().NotHaveLength(length);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task WhenSubjectHasSameLength_ShouldFail(long length)
		{
			Stream subject = new MyStream(length: length);

			async Task Act()
				=> await That(subject).Should().NotHaveLength(length);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not have length {length},
				              but it had
				              """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Stream? subject = null;

			async Task Act()
				=> await That(subject).Should().NotHaveLength(0);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not have length 0,
				             but it was <null>
				             """);
		}
	}
}
