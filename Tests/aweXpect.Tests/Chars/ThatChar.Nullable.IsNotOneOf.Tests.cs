﻿using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed partial class Nullable
	{
		public sealed class IsNotOneOf
		{
			public sealed class Tests
			{
				[Theory]
				[InlineData('a')]
				[InlineData('X')]
				[InlineData('5')]
				[InlineData('\t')]
				public async Task WhenExpectedOnlyContainsNull_ShouldSucceed(char subject)
				{
					IEnumerable<char?> expected = [null,];

					async Task Act()
						=> await That(subject).IsNotOneOf(expected);

					await That(Act).DoesNotThrow();
				}

				[Theory]
				[InlineData('a')]
				[InlineData('B', 'b', 'A')]
				public async Task WhenSubjectIsContained_ShouldFail(char subject,
					params char[] otherValues)
				{
					IEnumerable<char> expected = [..otherValues, subject,];

					async Task Act()
						=> await That(subject).IsNotOneOf(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not one of {Formatter.Format(expected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Theory]
				[InlineData('B', 'b', 'A')]
				public async Task WhenSubjectIsDifferent_ShouldSucceed(char subject,
					params char[] expected)
				{
					async Task Act()
						=> await That(subject).IsNotOneOf(expected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNullAndUnexpectedContainsNull_ShouldFail()
				{
					char? subject = null;
					IEnumerable<char?> expected = ['a', null,];

					async Task Act()
						=> await That(subject).IsNotOneOf(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is not one of {Formatter.Format(expected)},
						              but it was <null>
						              """);
				}
			}
		}
	}
}
