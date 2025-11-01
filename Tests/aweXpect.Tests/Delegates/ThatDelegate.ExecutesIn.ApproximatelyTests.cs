using System.Threading;

namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ExecutesIn
	{
		public sealed class ApproximatelyTests
		{
			[Fact]
			public async Task WhenDelegateIsTooFast_ShouldFail()
			{
				Action @delegate = () => { Thread.Sleep(5); };

				async Task Act()
					=> await That(@delegate).ExecutesIn().Approximately(5000.Milliseconds(), 1123.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that @delegate
					             executes in approximately 0:05 ± 0:01.123,
					             but it took only 0:*
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateTakesLongEnough_ShouldSucceed()
			{
				Action @delegate = () => { Thread.Sleep(50); };

				async Task Act()
					=> await That(@delegate).ExecutesIn().Approximately(50.Milliseconds(), 500.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDelegateTakesTooLong_ShouldFail()
			{
				Action @delegate = () => { Thread.Sleep(50); };

				async Task Act()
					=> await That(@delegate).ExecutesIn().Approximately(10.Milliseconds(), 5.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that @delegate
					             executes in approximately 0:00.010 ± 0:00.005,
					             but it took 0:*
					             """).AsWildcard();
			}
		}
	}
}
