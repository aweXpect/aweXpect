﻿using System.IO;

namespace aweXpect.Tests.Streams;

public sealed partial class StreamShould
{
	public sealed class HavePosition
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
					=> await That(subject).Should().HavePosition(position);

				await That(Act).Does().Throw<XunitException>()
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

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).Should().HavePosition(0);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have position 0,
					             but it was <null>
					             """);
			}
		}
	}
}
