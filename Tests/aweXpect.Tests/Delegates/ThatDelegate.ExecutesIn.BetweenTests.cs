﻿using System.Threading;

namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ExecutesIn
	{
		public sealed class BetweenTests
		{
			[Fact]
			public async Task WhenDelegateIsTooFast_ShouldFail()
			{
				Action @delegate = () => { Thread.Sleep(5); };

				async Task Act()
					=> await That(@delegate).ExecutesIn().Between(5123.Milliseconds()).And(6000.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that @delegate
					             executes in between 0:05.123 and 0:06,
					             but it took only 0:*
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateTakesLongEnough_ShouldSucceed()
			{
				Action @delegate = () => { Thread.Sleep(50); };

				async Task Act()
					=> await That(@delegate).ExecutesIn().Between(10.Milliseconds()).And(5000.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDelegateTakesTooLong_ShouldFail()
			{
				Action @delegate = () => { Thread.Sleep(50); };

				async Task Act()
					=> await That(@delegate).ExecutesIn().Between(5.Milliseconds()).And(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that @delegate
					             executes in between 0:00.005 and 0:00.010,
					             but it took 0:*
					             """).AsWildcard();
			}
		}
	}
}
