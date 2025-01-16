namespace aweXpect.Core.Tests.Core.Nodes;

public sealed class PrecedenceTests
{
	public sealed class OrOverAnd
	{
		[Fact]
		public async Task F_and_T_or_F_ShouldFail()
		{
			async Task Act()
				=> await That(true).IsFalse().And.IsTrue().Or.IsFalse();

			await That(Act).Does().ThrowException()
				.WithMessage("""
				             Expected true to
				             be False and be True or be False,
				             but it was True
				             """);
		}

		[Fact]
		public async Task F_and_T_or_T_and_F_ShouldFail()
		{
			async Task Act()
				=> await That(true).IsFalse().And.IsTrue().Or.IsTrue().And.IsFalse();

			await That(Act).Does().ThrowException()
				.WithMessage("""
				             Expected true to
				             be False and be True or be True and be False,
				             but it was True
				             """);
		}

		[Fact]
		public async Task F_and_T_or_T_ShouldSucceed()
		{
			async Task Act()
				=> await That(true).IsFalse().And.IsTrue().Or.IsTrue();

			await That(Act).Does().NotThrow();
		}

		[Fact]
		public async Task F_or_T_and_F_ShouldFail()
		{
			async Task Act()
				=> await That(true).IsFalse().Or.IsTrue().And.IsFalse();

			await That(Act).Does().ThrowException()
				.WithMessage("""
				             Expected true to
				             be False or be True and be False,
				             but it was True
				             """);
		}

		[Fact]
		public async Task T_and_F_or_F_ShouldFail()
		{
			async Task Act()
				=> await That(true).IsTrue().And.IsFalse().Or.IsFalse();

			await That(Act).Does().ThrowException()
				.WithMessage("""
				             Expected true to
				             be True and be False or be False,
				             but it was True
				             """);
		}

		[Fact]
		public async Task T_and_F_or_T_ShouldSucceed()
		{
			async Task Act()
				=> await That(true).IsTrue().And.IsFalse().Or.IsTrue();

			await That(Act).Does().NotThrow();
		}

		[Fact]
		public async Task T_or_T_and_F_ShouldSucceed()
		{
			async Task Act()
				=> await That(true).IsTrue().Or.IsTrue().And.IsFalse();

			await That(Act).Does().NotThrow();
		}
	}
}
