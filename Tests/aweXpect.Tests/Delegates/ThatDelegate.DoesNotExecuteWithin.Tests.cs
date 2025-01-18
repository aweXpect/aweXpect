#if NET8_0_OR_GREATER
using System.Threading;
#endif

namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed class DoesNotExecuteWithin
	{
		public sealed class ActionTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldFail()
			{
				Action @delegate = () => { };

				async Task Act()
					=> await That(@delegate).DoesNotExecuteWithin(5000.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected @delegate to
					             not execute within 0:05,
					             but it took only *
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldSucceed()
			{
				Action @delegate = () => throw new MyException();

				async Task Act()
					=> await That(@delegate).DoesNotExecuteWithin(500.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).DoesNotExecuteWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not execute within 0:00.500,
					             but it was <null>
					             """);
			}
		}

		public sealed class FuncTaskTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldFail()
			{
				Func<Task> @delegate = () => Task.CompletedTask;

				async Task Act()
					=> await That(@delegate).DoesNotExecuteWithin(5000.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected @delegate to
					             not execute within 0:05,
					             but it took only *
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldSucceed()
			{
				Func<Task> @delegate = () => Task.FromException(new MyException());

				async Task Act()
					=> await That(@delegate).DoesNotExecuteWithin(500.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<Task>? subject = null;

				async Task Act()
					=> await That(subject!).DoesNotExecuteWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not execute within 0:00.500,
					             but it was <null>
					             """);
			}
		}

		public sealed class FuncTaskValueTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldFail()
			{
				Func<Task<int>> @delegate = () => Task.FromResult(1);

				async Task Act()
					=> await That(@delegate).DoesNotExecuteWithin(5000.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected @delegate to
					             not execute within 0:05,
					             but it took only *
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldSucceed()
			{
				Func<Task<int>> @delegate = () => Task.FromException<int>(new MyException());

				async Task Act()
					=> await That(@delegate).DoesNotExecuteWithin(500.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<Task<int>>? subject = null;

				async Task Act()
					=> await That(subject!).DoesNotExecuteWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not execute within 0:00.500,
					             but it was <null>
					             """);
			}
		}

#if NET8_0_OR_GREATER
			public sealed class FuncValueTaskTests
			{
				[Fact]
				public async Task WhenDelegateIsFastEnough_ShouldFail()
				{
					ValueTask Delegate() => new(Task.CompletedTask);

					async Task Act()
						=> await That(Delegate).DoesNotExecuteWithin(5000.Milliseconds());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected Delegate to
						             not execute within 0:05,
						             but it took only *
						             """).AsWildcard();
				}

				[Fact]
				public async Task WhenDelegateThrowsAnException_ShouldSucceed()
				{
					ValueTask Delegate() => new(Task.FromException(new MyException()));

					async Task Act()
						=> await That(Delegate).DoesNotExecuteWithin(500.Milliseconds());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					Func<ValueTask>? subject = null;

					async Task Act()
						=> await That(subject!).DoesNotExecuteWithin(500.Milliseconds());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             not execute within 0:00.500,
						             but it was <null>
						             """);
				}
			}
#endif

#if NET8_0_OR_GREATER
			public sealed class FuncCancellationTokenValueTaskTests
			{
				[Fact]
				public async Task WhenDelegateIsFastEnough_ShouldFail()
				{
					ValueTask Delegate(CancellationToken _)
						=> new(Task.CompletedTask);

					async Task Act()
						=> await That(Delegate).DoesNotExecuteWithin(5000.Milliseconds());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected Delegate to
						             not execute within 0:05,
						             but it took only *
						             """).AsWildcard();
				}

				[Fact]
				public async Task WhenDelegateThrowsAnException_ShouldSucceed()
				{
					ValueTask Delegate(CancellationToken _)
						=> new(Task.FromException(new MyException()));

					async Task Act()
						=> await That(Delegate).DoesNotExecuteWithin(500.Milliseconds());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					Func<CancellationToken, ValueTask>? subject = null;

					async Task Act()
						=> await That(subject!).DoesNotExecuteWithin(500.Milliseconds());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             not execute within 0:00.500,
						             but it was <null>
						             """);
				}
			}
#endif

#if NET8_0_OR_GREATER

			public sealed class FuncCancellationTokenValueTaskValueTests
			{
				[Fact]
				public async Task WhenDelegateIsFastEnough_ShouldFail()
				{
					ValueTask<int> Delegate(CancellationToken _)
						=> new(Task.FromResult(1));

					async Task Act()
						=> await That(Delegate).DoesNotExecuteWithin(5000.Milliseconds());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected Delegate to
						             not execute within 0:05,
						             but it took only *
						             """).AsWildcard();
				}

				[Fact]
				public async Task WhenDelegateThrowsAnException_ShouldSucceed()
				{
					ValueTask<int> Delegate(CancellationToken _)
						=> new(Task.FromException<int>(new MyException()));

					async Task Act()
						=> await That(Delegate).DoesNotExecuteWithin(500.Milliseconds());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					Func<CancellationToken, ValueTask<int>>? subject = null;

					async Task Act()
						=> await That(subject!).DoesNotExecuteWithin(500.Milliseconds());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             not execute within 0:00.500,
						             but it was <null>
						             """);
				}
			}
#endif

#if NET8_0_OR_GREATER

			public sealed class FuncValueTaskValueTests
			{
				[Fact]
				public async Task WhenDelegateIsFastEnough_ShouldFail()
				{
					ValueTask<int> Delegate() => new(Task.FromResult(1));

					async Task Act()
						=> await That(Delegate).DoesNotExecuteWithin(5000.Milliseconds());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected Delegate to
						             not execute within 0:05,
						             but it took only *
						             """).AsWildcard();
				}

				[Fact]
				public async Task WhenDelegateThrowsAnException_ShouldSucceed()
				{
					ValueTask<int> Delegate() => new(Task.FromException<int>(new MyException()));

					async Task Act()
						=> await That(Delegate).DoesNotExecuteWithin(500.Milliseconds());

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					Func<ValueTask<int>>? subject = null;

					async Task Act()
						=> await That(subject!).DoesNotExecuteWithin(500.Milliseconds());

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             not execute within 0:00.500,
						             but it was <null>
						             """);
				}
			}
#endif

		public sealed class FuncValueTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldFail()
			{
				Func<int> @delegate = () => 1;

				async Task Act()
					=> await That(@delegate).DoesNotExecuteWithin(5000.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected @delegate to
					             not execute within 0:05,
					             but it took only *
					             """).AsWildcard();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldSucceed()
			{
				Func<int> @delegate = () => throw new MyException();

				async Task Act()
					=> await That(@delegate).DoesNotExecuteWithin(500.Milliseconds());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<int>? subject = null;

				async Task Act()
					=> await That(subject!).DoesNotExecuteWithin(500.Milliseconds());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not execute within 0:00.500,
					             but it was <null>
					             """);
			}
		}
	}
}
