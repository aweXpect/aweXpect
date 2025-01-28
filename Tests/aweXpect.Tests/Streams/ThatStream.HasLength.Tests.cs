using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatStream
{
	public sealed class HasLength
	{
		public sealed class EqualToTests
		{
			[Fact]
			public async Task WhenExpectedLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				Stream subject = new MyStream();

				async Task Act()
					=> await That(subject).HasLength().EqualTo(-1);

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.WithMessage("*The expected length must be greater than or equal to zero*")
					.AsWildcard().And
					.WithParamName("expected");
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentLength_ShouldFail(long length)
			{
				long actualLength = length > 10000 ? length - 1 : length + 1;
				Stream subject = new MyStream(length: actualLength);

				async Task Act()
					=> await That(subject).HasLength().EqualTo(length);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have length equal to {length},
					              but it had length {actualLength}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSameLength_ShouldSucceed(long length)
			{
				Stream subject = new MyStream(length: length);

				async Task Act()
					=> await That(subject).HasLength().EqualTo(length);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).HasLength().EqualTo(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have length equal to 0,
					             but it was <null>
					             """);
			}
		}

		public sealed class NotEqualToTests
		{
			[Theory]
			[AutoData]
			public async Task WhenSubjectHasDifferentLength_ShouldSucceed(long length)
			{
				long actualLength = length > 10000 ? length - 1 : length + 1;
				Stream subject = new MyStream(length: actualLength);

				async Task Act()
					=> await That(subject).HasLength().NotEqualTo(length);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenSubjectHasSameLength_ShouldFail(long length)
			{
				Stream subject = new MyStream(length: length);

				async Task Act()
					=> await That(subject).HasLength().NotEqualTo(length);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected subject to
					              have length not equal to {length},
					              but it had length {length}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).HasLength().NotEqualTo(0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenUnexpectedLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
			{
				Stream subject = new MyStream();

				async Task Act()
					=> await That(subject).HasLength().NotEqualTo(-1);

				await That(Act).Throws<ArgumentOutOfRangeException>()
					.WithMessage("*The unexpected length must be greater than or equal to zero*")
					.AsWildcard().And
					.WithParamName("unexpected");
			}
		}
	}
}
