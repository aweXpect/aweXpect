﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace aweXpect.Tests;

public sealed partial class ThatReadOnlyDictionary
{
	public sealed class DoesNotContainValues
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAllValuesDoNotExist_ShouldSucceed()
			{
				IReadOnlyDictionary<int, int> subject = ToDictionary([1, 2, 3,], [41, 42, 43,]);

				async Task Act()
					=> await That(subject).DoesNotContainValues(0, 2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllValuesDoNotExist_WithNull_ShouldSucceed()
			{
				IReadOnlyDictionary<int, int?> subject = ToDictionary<int, int?>([1, 2, 3,], [41, 42, 43,]);

				async Task Act()
					=> await That(subject).DoesNotContainValues(2, null);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAtLeastOneValueExists_ShouldFail()
			{
				IReadOnlyDictionary<int, int> subject = ToDictionary([1, 2, 3,], [41, 42, 43,]);

				async Task Act()
					=> await That(subject).DoesNotContainValues(42, 2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain values [42, 2],
					             but it did contain [
					               42
					             ]

					             Dictionary:
					             {[1] = 41, [2] = 42, [3] = 43}
					             """);
			}

			[Fact]
			public async Task WhenAtLeastOneValueExists_WithNull_ShouldFail()
			{
				IReadOnlyDictionary<int, int?> subject = ToDictionary<int, int?>([1, 2, 3,], [null, 42, 43,]);

				async Task Act()
					=> await That(subject).DoesNotContainValues(2, null);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain values [2, <null>],
					             but it did contain [
					               <null>
					             ]

					             Dictionary:
					             {[1] = <null>, [2] = 42, [3] = 43}
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				ReadOnlyDictionary<int, string>? subject = null;

				async Task Act()
					=> await That(subject).DoesNotContainValues("foo", "bar");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain values ["foo", "bar"],
					             but it was <null>
					             """);
			}
		}
	}
}
