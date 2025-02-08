namespace aweXpect.Tests;

public sealed partial class ThatException
{
	public class HasRecursiveInnerExceptions
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
					=> await That(subject).HasRecursiveInnerExceptions(c
						=> c.All().Satisfy(e => e.Message.StartsWith("inner")));

				await That(Act).DoesNotThrow();
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
					=> await That(subject).HasRecursiveInnerExceptions(
						c => c.All().Satisfy(e => e.Message != "inner3A"));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has recursive inner exceptions which should have satisfies e => e.Message != "inner3A" for all items,
					             but not all did
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
					=> await That(subject).HasRecursiveInnerExceptions(
						c => c.None().Satisfy(e => e.Message != "inner3A"));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has recursive inner exceptions which should have no items satisfy e => e.Message != "inner3A",
					             but at least one did
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Exception? subject = null;

				async Task Act()
					=> await That(subject).HasRecursiveInnerExceptions(c => c.IsEmpty());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has recursive inner exceptions which should be empty,
					             but it was <null>
					             """);
			}
		}
	}
}
