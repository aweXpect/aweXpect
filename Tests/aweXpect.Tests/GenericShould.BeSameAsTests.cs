namespace aweXpect.Tests;

public sealed partial class GenericShould
{
	public sealed class BeSameAsTests
	{
		[Fact]
		public async Task WhenComparingTheSameObjectReference_ShouldSucceed()
		{
			Other subject = new() { Value = 1 };
			Other expected = subject;

			async Task Act()
				=> await That(subject).IsSameAs(expected);

			await That(Act).Does().NotThrow();
		}

		[Fact]
		public async Task WhenComparingTwoIndividualObjectsWithSameValues_ShouldFail()
		{
			Other subject = new() { Value = 1 };
			Other expected = new() { Value = 1 };

			async Task Act()
				=> await That(subject).IsSameAs(expected);

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
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
			Other? subject = new() { Value = 1 };
			Other? expected = null;

			async Task Act()
				=> await That(subject).IsSameAs(expected);

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
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

			await That(Act).Does().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldFail()
		{
			Other? subject = null;
			Other expected = new() { Value = 1 };

			async Task Act()
				=> await That(subject).IsSameAs(expected);

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             refer to expected Other {
				               Value = 1
				             },
				             but it was <null>
				             """);
		}
	}

	public sealed class NotBeSameAsTests
	{
		[Fact]
		public async Task WhenComparingTheSameObjectReference_ShouldFail()
		{
			Other subject = new() { Value = 1 };
			Other expected = subject;

			async Task Act()
				=> await That(subject).IsNotSameAs(expected);

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not refer to expected Other {
				               Value = 1
				             },
				             but it did
				             """);
		}

		[Fact]
		public async Task WhenComparingTwoIndividualObjectsWithSameValues_ShouldSucceed()
		{
			Other subject = new() { Value = 1 };
			Other expected = new() { Value = 1 };

			async Task Act()
				=> await That(subject).IsNotSameAs(expected);

			await That(Act).Does().NotThrow();
		}

		[Fact]
		public async Task WhenExpectedIsNull_ShouldSucceed()
		{
			Other? subject = new() { Value = 1 };
			Other? expected = null;

			async Task Act()
				=> await That(subject).IsNotSameAs(expected);

			await That(Act).Does().NotThrow();
		}

		[Fact]
		public async Task WhenSubjectAndExpectedIsNull_ShouldFail()
		{
			Other? subject = null;
			Other? expected = null;

			async Task Act()
				=> await That(subject).IsNotSameAs(expected);

			await That(Act).Does().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not refer to expected <null>,
				             but it did
				             """);
		}

		[Fact]
		public async Task WhenSubjectIsNull_ShouldSucceed()
		{
			Other? subject = null;
			Other expected = new() { Value = 1 };

			async Task Act()
				=> await That(subject).IsNotSameAs(expected);

			await That(Act).Does().NotThrow();
		}
	}
}
