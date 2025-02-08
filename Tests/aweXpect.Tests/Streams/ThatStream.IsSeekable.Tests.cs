﻿using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatStream
{
	public sealed class IsSeekable
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsNotSeekable_ShouldFail()
			{
				Stream subject = new MyStream(canSeek: false);

				async Task Act()
					=> await That(subject).IsSeekable();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             be seekable,
					             but it was not
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Stream? subject = null;

				async Task Act()
					=> await That(subject).IsSeekable();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             be seekable,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsSeekable_ShouldSucceed()
			{
				Stream subject = new MyStream(canSeek: true);

				async Task Act()
					=> await That(subject).IsSeekable();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
