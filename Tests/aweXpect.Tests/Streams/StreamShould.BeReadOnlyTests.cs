﻿using System.IO;

namespace aweXpect.Tests.Streams;

public sealed partial class StreamShould
{
	public sealed class BeReadOnly
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(false, false)]
			[InlineData(false, true)]
			[InlineData(true, true)]
			public async Task WhenSubjectIsNotReadOnly_ShouldFail(bool canRead, bool canWrite)
			{
				Stream subject = new MyStream(canRead: canRead, canWrite: canWrite);

				async Task Act()
					=> await That(subject).Should().BeReadOnly();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be read-only,
					             but it was not
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).Should().BeReadOnly();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be read-only,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsReadOnly_ShouldSucceed()
			{
				Stream subject = new MyStream(canRead: true, canWrite: false);

				async Task Act()
					=> await That(subject).Should().BeReadOnly();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
