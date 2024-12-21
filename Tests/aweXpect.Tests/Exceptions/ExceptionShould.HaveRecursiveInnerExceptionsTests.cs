﻿namespace aweXpect.Tests.Exceptions;

public sealed partial class ExceptionShould
{
	public class HaveRecursiveInnerExceptions
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenAllInnerExceptionsMatchTheCondition_ShouldSucceed()
			{
				Exception subject = new("outer",
					new Exception("inner1",
						new AggregateException("inner2",
							new Exception("inner3A"),
							new Exception("inner3B"))));

				async Task Act()
					=> await That(subject).Should().HaveRecursiveInnerExceptions(c
						=> c.HaveAll(x => x.Satisfy(e => e.Message.StartsWith("inner"))));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenInnerExceptionsDoNotMatchTheCondition_ForAll_ShouldFail()
			{
				Exception subject = new("outer",
					new Exception("inner1",
						new AggregateException("inner2",
							new Exception("inner3A"),
							new Exception("inner3B"))));

				async Task Act()
					=> await That(subject).Should().HaveRecursiveInnerExceptions(
						c => c.HaveAll(x => x.Satisfy(e => e.Message != "inner3A")));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have recursive inner exceptions which have all items satisfy e => e.Message != "inner3A",
					             but not all were
					             """);
			}

			[Fact]
			public async Task WhenInnerExceptionsDoNotMatchTheCondition_ForNone_ShouldFail()
			{
				Exception subject = new("outer",
					new Exception("inner1",
						new AggregateException("inner2",
							new Exception("inner3A"),
							new Exception("inner3B"))));

				async Task Act()
					=> await That(subject).Should().HaveRecursiveInnerExceptions(
						c => c.HaveNone(x => x.Satisfy(e => e.Message != "inner3A")));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have recursive inner exceptions which have no items satisfy e => e.Message != "inner3A",
					             but at least one was
					             """);
			}
		}
	}
}
