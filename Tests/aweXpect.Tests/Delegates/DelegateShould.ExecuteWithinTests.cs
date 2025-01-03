﻿using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed class ExecuteWithin
	{
		public sealed class ActionTests
		{
			[Fact]
			public async Task WhenDelegateIsFastEnough_ShouldSucceed()
			{
				Action @delegate = () => { };

				async Task Act()
					=> await That(@delegate).Should().ExecuteWithin(5000.Milliseconds());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Action @delegate = () => throw new MyException();

				async Task Act()
					=> await That(@delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected @delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Action? subject = null;

				async Task Act()
					=> await That(subject!).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             execute within 0:00.500,
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
					=> await That(@delegate).Should().ExecuteWithin(5000.Milliseconds());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Func<Task> @delegate = () => Task.FromException(new MyException());

				async Task Act()
					=> await That(@delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected @delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<Task>? subject = null;

				async Task Act()
					=> await That(subject!).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             execute within 0:00.500,
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
					=> await That(@delegate).Should().ExecuteWithin(5000.Milliseconds());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Func<Task<int>> @delegate = () => Task.FromException<int>(new MyException());

				async Task Act()
					=> await That(@delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected @delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<Task<int>>? subject = null;

				async Task Act()
					=> await That(subject!).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             execute within 0:00.500,
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
					=> await That(Delegate).Should().ExecuteWithin(5000.Milliseconds());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				ValueTask Delegate() => new(Task.FromException(new MyException()));

				async Task Act()
					=> await That(Delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected Delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<ValueTask>? subject = null;

				async Task Act()
					=> await That(subject!).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             execute within 0:00.500,
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
					=> await That(Delegate).Should().ExecuteWithin(5000.Milliseconds());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				ValueTask<int> Delegate() => new(Task.FromException<int>(new MyException()));

				async Task Act()
					=> await That(Delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected Delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<ValueTask<int>>? subject = null;

				async Task Act()
					=> await That(subject!).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             execute within 0:00.500,
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
					=> await That(@delegate).Should().ExecuteWithin(5000.Milliseconds());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Func<int> @delegate = () => throw new MyException();

				async Task Act()
					=> await That(@delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected @delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenDelegateThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Func<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             execute within 0:00.500,
					             but it was <null>
					             """);
			}
		}
	}
}
