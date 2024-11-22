namespace Testably.Expectations.Tests.ThatTests.Collections;

public partial class StringEnumerableShould
{
	public class ContainTests
	{
		[Fact]
		public async Task ShouldCompareCaseSensitive()
		{
			string[] sut = ["green", "blue", "yellow"];

			async Task Act()
				=> await That(sut).Should().Contain("GREEN");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             contain "GREEN" at least once,
				             but it contained it 0 times in ["green", "blue", "yellow"]
				             """);
		}

		[Fact]
		public async Task WhenExpectedIsNotPartOfStringEnumerable_ShouldFail()
		{
			string[] sut = ["green", "blue", "yellow"];

			async Task Act()
				=> await That(sut).Should().Contain("red");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             contain "red" at least once,
				             but it contained it 0 times in ["green", "blue", "yellow"]
				             """);
		}

		[Fact]
		public async Task WhenExpectedIsPartOfStringEnumerable_ShouldSucceed()
		{
			string[] sut = ["green", "blue", "yellow"];

			async Task Act()
				=> await That(sut).Should().Contain("blue");

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenIgnoringCase_ShouldSucceedForCaseSensitiveDifference()
		{
			string[] sut = ["green", "blue", "yellow"];

			async Task Act()
				=> await That(sut).Should().Contain("GREEN").IgnoringCase();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WithAtLeast_ShouldVerifyCorrectNumberOfTimes()
		{
			string[] sut = ["green", "green", "blue", "yellow"];

			async Task Act()
				=> await That(sut).Should().Contain("green").AtLeast(3);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             contain "green" at least 3 times,
				             but it contained it 2 times in ["green", "green", "blue", "yellow"]
				             """);
		}

		[Fact]
		public async Task WithAtMost_ShouldVerifyCorrectNumberOfTimes()
		{
			string[] sut = ["green", "green", "green", "green", "blue", "yellow"];

			async Task Act()
				=> await That(sut).Should().Contain("green").AtMost(2);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected sut to
				             contain "green" at most 2 times,
				             but it contained it at least 3 times in ["green", "green", "green", "green", "blue", "yellow"]
				             """);
		}
	}
}
