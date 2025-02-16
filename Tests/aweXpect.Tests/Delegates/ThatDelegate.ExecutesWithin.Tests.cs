﻿using System.Diagnostics;
using System.Threading;

namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed class ExecutesWithin
	{
		public sealed class ActionTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldSucceed()
			{
				Action @delegate = () => { };

				async Task Act()
					=> await That(@delegate).ExecutesWithin(5000.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDelegateTakesLonger_ShouldFail()
			{
				Action @delegate = () => { Thread.Sleep(50); };

				async Task Act()
					=> await That(@delegate).ExecutesWithin(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that @delegate
					             executes within 0:00.010,
					             but it took 0:*
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Action @delegate = () => throw new MyException();

				async Task Act()
					=> await That(@delegate).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that @delegate
					              executes within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             executes within 0:00.500,
					             but it was <null>
					             """);
			}
		}

		public sealed class FuncTaskTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldSucceed()
			{
				Func<Task> @delegate = () => Task.CompletedTask;

				async Task Act()
					=> await That(@delegate).ExecutesWithin(5000.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDelegateTakesLonger_ShouldFail()
			{
				Func<Task> @delegate = () => Task.Delay(50.Milliseconds());

				async Task Act()
					=> await That(@delegate).ExecutesWithin(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that @delegate
					             executes within 0:00.010,
					             but it took 0:*
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Func<Task> @delegate = () => Task.FromException(new MyException());

				async Task Act()
					=> await That(@delegate).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that @delegate
					              executes within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<Task>? subject = null;

				async Task Act()
					=> await That(subject!).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             executes within 0:00.500,
					             but it was <null>
					             """);
			}
		}

		public sealed class FuncTaskValueTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldSucceed()
			{
				Func<Task<int>> @delegate = () => Task.FromResult(1);

				async Task Act()
					=> await That(@delegate).ExecutesWithin(5000.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDelegateTakesLonger_ShouldFail()
			{
				Func<Task<int>> @delegate = () => Task.Delay(50.Milliseconds()).ContinueWith(_ => 1);

				async Task Act()
					=> await That(@delegate).ExecutesWithin(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that @delegate
					             executes within 0:00.010,
					             but it took 0:*
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Func<Task<int>> @delegate = () => Task.FromException<int>(new MyException());

				async Task Act()
					=> await That(@delegate).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that @delegate
					              executes within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<Task<int>>? subject = null;

				async Task Act()
					=> await That(subject!).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             executes within 0:00.500,
					             but it was <null>
					             """);
			}
		}

#if NET8_0_OR_GREATER
		public sealed class FuncValueTaskTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldSucceed()
			{
				ValueTask Delegate() => new(Task.CompletedTask);

				async Task Act()
					=> await That(Delegate).ExecutesWithin(5000.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDelegateTakesLonger_ShouldFail()
			{
				ValueTask Delegate() => new(Task.Delay(50.Milliseconds()));

				async Task Act()
					=> await That(Delegate).ExecutesWithin(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that Delegate
					             executes within 0:00.010,
					             but it took 0:*
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				ValueTask Delegate() => new(Task.FromException(new MyException()));

				async Task Act()
					=> await That(Delegate).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that Delegate
					              executes within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<ValueTask>? subject = null;

				async Task Act()
					=> await That(subject!).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             executes within 0:00.500,
					             but it was <null>
					             """);
			}
		}
#endif

#if NET8_0_OR_GREATER
		public sealed class FuncCancellationTokenValueTaskTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldSucceed()
			{
				ValueTask Delegate(CancellationToken _)
					=> new(Task.CompletedTask);

				async Task Act()
					=> await That(Delegate).ExecutesWithin(5000.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDelegateTakesLonger_ShouldFail()
			{
				ValueTask Delegate(CancellationToken token) => new(Task.Delay(50.Milliseconds(), token));

				async Task Act()
					=> await That(Delegate).ExecutesWithin(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that Delegate
					             executes within 0:00.010,
					             but it took 0:*
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				ValueTask Delegate(CancellationToken _)
					=> new(Task.FromException(new MyException()));

				async Task Act()
					=> await That(Delegate).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that Delegate
					              executes within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<CancellationToken, ValueTask>? subject = null;

				async Task Act()
					=> await That(subject!).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             executes within 0:00.500,
					             but it was <null>
					             """);
			}
		}
#endif

#if NET8_0_OR_GREATER
		public sealed class FuncValueTaskValueTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldSucceed()
			{
				ValueTask<int> Delegate() => new(Task.FromResult(1));

				async Task Act()
					=> await That(Delegate).ExecutesWithin(5000.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDelegateTakesLonger_ShouldFail()
			{
				ValueTask<int> Delegate() => new(Task.Delay(50.Milliseconds()).ContinueWith(_ => 1));

				async Task Act()
					=> await That(Delegate).ExecutesWithin(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that Delegate
					             executes within 0:00.010,
					             but it took 0:*
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				ValueTask<int> Delegate() => new(Task.FromException<int>(new MyException()));

				async Task Act()
					=> await That(Delegate).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that Delegate
					              executes within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<ValueTask<int>>? subject = null;

				async Task Act()
					=> await That(subject!).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             executes within 0:00.500,
					             but it was <null>
					             """);
			}
		}
#endif

#if NET8_0_OR_GREATER
		public sealed class FuncCancellationTokenValueTaskValueTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldSucceed()
			{
				ValueTask<int> Delegate(CancellationToken _)
					=> new(Task.FromResult(1));

				async Task Act()
					=> await That(Delegate).ExecutesWithin(5000.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDelegateTakesLonger_ShouldFail()
			{
				ValueTask<int> Delegate(CancellationToken token)
					=> new(Task.Delay(50.Milliseconds(), token).ContinueWith(_ => 1, token));

				async Task Act()
					=> await That(Delegate).ExecutesWithin(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that Delegate
					             executes within 0:00.010,
					             but it took 0:*
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				ValueTask<int> Delegate(CancellationToken _)
					=> new(Task.FromException<int>(new MyException()));

				async Task Act()
					=> await That(Delegate).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that Delegate
					              executes within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<CancellationToken, ValueTask<int>>? subject = null;

				async Task Act()
					=> await That(subject!).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             executes within 0:00.500,
					             but it was <null>
					             """);
			}
		}
#endif

		public sealed class FuncValueTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldSucceed()
			{
				Func<int> @delegate = () => 0;

				async Task Act()
					=> await That(@delegate).ExecutesWithin(5000.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDelegateTakesLonger_ShouldFail()
			{
				Func<int> @delegate = () =>
				{
					Thread.Sleep(50);
					return 0;
				};

				async Task Act()
					=> await That(@delegate).ExecutesWithin(10.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that @delegate
					             executes within 0:00.010,
					             but it took 0:*
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Func<int> @delegate = () => throw new MyException();

				async Task Act()
					=> await That(@delegate).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that @delegate
					              executes within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<int>? subject = null;

				async Task Act()
					=> await That(subject!).ExecutesWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             executes within 0:00.500,
					             but it was <null>
					             """);
			}
		}

		public sealed class WithTimeoutTests
		{
			[Fact]
			public async Task WithoutReturnValue_WhenTimeoutIsApplied_ShouldCancelTheCancellationToken()
			{
				Func<CancellationToken, Task> @delegate = token => Task.Delay(6.Seconds(), token);
				Stopwatch sw = new();

				async Task Act()
					=> await That(@delegate).ExecutesWithin(50.Milliseconds()).WithTimeout(50.Milliseconds());

				sw.Start();
				await That(Act).Throws<XunitException>();
				sw.Stop();

				await That(sw.Elapsed).IsLessThan(5.Seconds());
			}

			[Fact]
			public async Task WithReturnValue_WhenTimeoutIsApplied_ShouldCancelTheCancellationToken()
			{
				Func<CancellationToken, Task<int>> @delegate = async token =>
				{
					await Task.Delay(6.Seconds(), token);
					return 1;
				};
				Stopwatch sw = new();

				async Task Act()
					=> await That(@delegate).ExecutesWithin(50.Milliseconds()).WithTimeout(50.Milliseconds());

				sw.Start();
				await That(Act).Throws<XunitException>();
				sw.Stop();

				await That(sw.Elapsed).IsLessThan(5.Seconds());
			}
		}

		public sealed class WithCancellationTests
		{
			[Fact]
			public async Task WithoutReturnValue_WhenTimeoutIsApplied_ShouldCancelTheCancellationToken()
			{
				Func<CancellationToken, Task> @delegate = token => Task.Delay(6.Seconds(), token);
				CancellationToken cancelledToken = new(true);
				Stopwatch sw = new();

				async Task Act()
					=> await That(@delegate).ExecutesWithin(50.Milliseconds()).WithCancellation(cancelledToken);

				sw.Start();
				await That(Act).Throws<XunitException>();
				sw.Stop();

				await That(sw.Elapsed).IsLessThan(5.Seconds());
			}

			[Fact]
			public async Task WithReturnValue_WhenTimeoutIsApplied_ShouldCancelTheCancellationToken()
			{
				Func<CancellationToken, Task<int>> @delegate = async token =>
				{
					await Task.Delay(6.Seconds(), token);
					return 1;
				};
				CancellationToken cancelledToken = new(true);
				Stopwatch sw = new();

				async Task Act()
					=> await That(@delegate).ExecutesWithin(50.Milliseconds()).WithCancellation(cancelledToken);

				sw.Start();
				await That(Act).Throws<XunitException>();
				sw.Stop();

				await That(sw.Elapsed).IsLessThan(5.Seconds());
			}
		}
	}
}
