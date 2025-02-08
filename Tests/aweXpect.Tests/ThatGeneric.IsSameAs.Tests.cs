﻿namespace aweXpect.Tests;

public sealed partial class ThatGeneric
{
	public sealed class IsSameAs
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenComparingTheSameObjectReference_ShouldSucceed()
			{
				Other subject = new()
				{
					Value = 1
				};
				Other expected = subject;

				async Task Act()
					=> await That(subject).IsSameAs(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenComparingTwoIndividualObjectsWithSameValues_ShouldFail()
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
					=> await That(subject).IsSameAs(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             refer to expected Other {
					               Value = 1
					             },
					             but it was Other {
					               Value = 1
					             }
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				Other subject = new()
				{
					Value = 1
				};
				Other? expected = null;

				async Task Act()
					=> await That(subject).IsSameAs(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             refer to expected <null>,
					             but it was Other {
					               Value = 1
					             }
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldSucceed()
			{
				Other? subject = null;
				Other? expected = null;

				async Task Act()
					=> await That(subject).IsSameAs(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Other? subject = null;
				Other expected = new()
				{
					Value = 1
				};

				async Task Act()
					=> await That(subject).IsSameAs(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             refer to expected Other {
					               Value = 1
					             },
					             but it was <null>
					             """);
			}
		}
	}
}
