using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed partial class Nullable
	{
		public sealed class IsOneOf
		{
			public sealed class Tests
			{
				[Theory]
				[InlineData('a')]
				[InlineData('X')]
				[InlineData('5')]
				[InlineData('\t')]
				public async Task WhenExpectedOnlyContainsNull_ShouldFail(char? subject)
				{
					IEnumerable<char?> expected = [null,];

					async Task Act()
						=> await That(subject).IsOneOf(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is one of [<null>],
						              but it was {Formatter.Format(subject)}
						              """);
				}

				[Theory]
				[InlineData('a')]
				[InlineData('B', 'b', 'A')]
				public async Task WhenSubjectIsContained_ShouldSucceed(char? subject,
					params char[] otherValues)
				{
					char?[] expected = [..otherValues, subject,];

					async Task Act()
						=> await That(subject).IsOneOf(expected);

					await That(Act).DoesNotThrow();
				}

				[Theory]
				[InlineData('B', 'b', 'A')]
				public async Task WhenSubjectIsDifferent_ShouldFail(char? subject,
					params char[] expected)
				{
					async Task Act()
						=> await That(subject).IsOneOf(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              is one of {Formatter.Format(expected)},
						              but it was {Formatter.Format(subject)}
						              """);
				}
			}
		}
	}
}
