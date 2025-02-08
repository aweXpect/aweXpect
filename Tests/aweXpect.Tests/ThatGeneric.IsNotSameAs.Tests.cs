﻿namespace aweXpect.Tests;

public sealed partial class ThatGeneric
{
	public sealed class IsNotSameAs
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenComparingTheSameObjectReference_ShouldFail()
			{
				Other subject = new()
				{
					Value = 1
				};
				Other expected = subject;

				async Task Act()
					=> await That(subject).IsNotSameAs(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             not refer to expected Other {
					               Value = 1
					             },
					             but it did
					             """);
			}

			[Fact]
			public async Task WhenComparingTwoIndividualObjectsWithSameValues_ShouldSucceed()
			{
				Other subject = new()
				{
					Value = 1
				};
				Other expected = new()
				{
					Value = 1
				};

				async Task Act()
					=> await That(subject).IsNotSameAs(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldSucceed()
			{
				Other subject = new()
				{
					Value = 1
				};
				Other? expected = null;

				async Task Act()
					=> await That(subject).IsNotSameAs(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
			{
				Other? subject = null;
				Other? expected = null;

				async Task Act()
					=> await That(subject).IsNotSameAs(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             not refer to expected <null>,
					             but it did
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldSucceed()
			{
				Other? subject = null;
				Other expected = new()
				{
					Value = 1
				};

				async Task Act()
					=> await That(subject).IsNotSameAs(expected);

				await That(Act).DoesNotThrow();
			}
		}
	}
}
