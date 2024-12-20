﻿using aweXpect.Extensions;
using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed class ExecuteWithin
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActionThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Action @delegate = () => throw new MyException();

				async Task Act()
					=> await That(@delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected @delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenActionThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenFuncTaskThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Func<Task> @delegate = () => Task.FromException(new MyException());

				async Task Act()
					=> await That(@delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected @delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenFuncTaskThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

			[Fact]
			public async Task WhenFuncTaskValueThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Func<Task<int>> @delegate = () => Task.FromException<int>(new MyException());

				async Task Act()
					=> await That(@delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected @delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenFuncTaskValueThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

#if NET8_0_OR_GREATER
			[Fact]
			public async Task WhenFuncValueTaskThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				ValueTask Delegate() => new(Task.FromException(new MyException()));

				async Task Act()
					=> await That(Delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected Delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenFuncValueTaskThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}

#endif

#if NET8_0_OR_GREATER
			[Fact]
			public async Task WhenFuncValueTaskValueThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				ValueTask<int> Delegate() => new(Task.FromException<int>(new MyException()));

				async Task Act()
					=> await That(Delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected Delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenFuncValueTaskValueThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}
#endif

			[Fact]
			public async Task WhenFuncValueThrowsAnException_ShouldFailWithDescriptiveMessage()
			{
				Func<int> @delegate = () => throw new MyException();

				async Task Act()
					=> await That(@delegate).Should().ExecuteWithin(500.Milliseconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage($"""
					              Expected @delegate to
					              execute within 0:00.500,
					              but it did throw a MyException:
					                {nameof(WhenFuncValueThrowsAnException_ShouldFailWithDescriptiveMessage)}
					              """);
			}
		}
	}
}
