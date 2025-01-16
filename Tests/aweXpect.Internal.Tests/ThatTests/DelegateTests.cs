﻿using System.Runtime.CompilerServices;
using System.Threading;

namespace aweXpect.Internal.Tests.ThatTests;

public sealed class DelegateTests
{
	[Theory]
	[AutoData]
	public async Task ShouldReturnValue_FuncTaskValue(int value)
	{
		Task<int> Delegate() => Task.FromResult(value);

		int result = await That(Delegate).Does().NotThrow();

		await That(result).Should().Be(value);
	}

	[Theory]
	[AutoData]
	public async Task ShouldReturnValue_FuncValue(int value)
	{
		int Delegate() => value;

		int result = await That(Delegate).Does().NotThrow();

		await That(result).Should().Be(value);
	}

#if NET8_0_OR_GREATER
	[Theory]
	[AutoData]
	public async Task ShouldReturnValue_FuncValueTaskValue(int value)
	{
		ValueTask<int> Delegate() => ValueTask.FromResult(value);

		int result = await That(Delegate).Does().NotThrow();

		await That(result).Should().Be(value);
	}
#endif

#if NET8_0_OR_GREATER
	[Theory]
	[AutoData]
	public async Task ShouldReturnValue_FuncValueTaskValue_WithCancellationToken(int value)
	{
		ValueTask<int> Delegate(CancellationToken _) => ValueTask.FromResult(value);

		int result = await That(Delegate).Does().NotThrow();

		await That(result).Should().Be(value);
	}
#endif

	[Fact]
	public async Task ShouldSupportDelegate_Action_WhenSuccess()
	{
		Action @delegate = () => { };

		async Task Act()
			=> await That(@delegate).Does().ThrowException();

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected @delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_Action_WhenThrown()
	{
		Action @delegate = () => throw new MyException();

		async Task Act()
			=> await That(@delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected @delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_Action_WhenThrown)}
			              """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_Action_WithCancellationToken_Cancelled()
	{
		using CancellationTokenSource cts = new();
		// ReSharper disable once MethodHasAsyncOverload
		cts.Cancel();
		CancellationToken token = cts.Token;

		void Delegate(CancellationToken t)
			=> t.ThrowIfCancellationRequested();

		await That(Delegate).Does().Throw<OperationCanceledException>()
			.WithCancellation(token);
	}

	[Fact]
	public async Task ShouldSupportDelegate_Action_WithCancellationToken_WhenSuccess()
	{
		Action<CancellationToken> @delegate = _ => { };

		async Task Act()
			=> await That(@delegate).Does().ThrowException();

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected @delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_Action_WithCancellationToken_WhenThrown()
	{
		Action<CancellationToken> @delegate = _ => throw new MyException();

		async Task Act()
			=> await That(@delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected @delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_Action_WithCancellationToken_WhenThrown)}
			              """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncTask_WhenSuccess()
	{
		Func<Task> @delegate = () => Task.CompletedTask;

		async Task Act()
			=> await That(@delegate).Does().ThrowException();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage("""
			             Expected @delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncTask_WhenThrown()
	{
		Func<Task> @delegate = () => Task.FromException(new MyException());

		async Task Act()
			=> await That(@delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected @delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_FuncTask_WhenThrown)}
			              """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncTask_WithCancellationToken_Cancelled()
	{
		using CancellationTokenSource cts = new();
		// ReSharper disable once MethodHasAsyncOverload
		cts.Cancel();
		CancellationToken token = cts.Token;

		Task Delegate(CancellationToken t)
			=> Task.FromCanceled(t);

		await That(Delegate).Does().Throw<OperationCanceledException>()
			.WithCancellation(token);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncTask_WithCancellationToken_WhenSuccess()
	{
		Func<CancellationToken, Task> @delegate = _ => Task.CompletedTask;

		async Task Act()
			=> await That(@delegate).Does().ThrowException();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage("""
			             Expected @delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncTask_WithCancellationToken_WhenThrown()
	{
		Func<CancellationToken, Task> @delegate = _ => Task.FromException(new MyException());

		async Task Act()
			=> await That(@delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected @delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_FuncTask_WithCancellationToken_WhenThrown)}
			              """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncTaskValue_WhenSuccess()
	{
		Func<Task<int>> @delegate = () => Task.FromResult(1);

		async Task Act()
			=> await That(@delegate).Does().ThrowException();

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected @delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncTaskValue_WhenThrown()
	{
		Func<Task<int>> @delegate = () => Task.FromException<int>(new MyException());

		async Task Act()
			=> await That(@delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected @delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_FuncTaskValue_WhenThrown)}
			              """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncTaskValue_WithCancellationToken_Cancelled()
	{
		using CancellationTokenSource cts = new();
		// ReSharper disable once MethodHasAsyncOverload
		cts.Cancel();
		CancellationToken token = cts.Token;

		Task<int> Delegate(CancellationToken t)
			=> Task.FromCanceled<int>(t);

		await That(Delegate).Does().Throw<OperationCanceledException>()
			.WithCancellation(token);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncTaskValue_WithCancellationToken_WhenSuccess()
	{
		Func<CancellationToken, Task<int>> @delegate = _ => Task.FromResult(1);

		async Task Act()
			=> await That(@delegate).Does().ThrowException();

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected @delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncTaskValue_WithCancellationToken_WhenThrown()
	{
		Func<CancellationToken, Task<int>> @delegate = _
			=> Task.FromException<int>(new MyException());

		async Task Act()
			=> await That(@delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected @delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_FuncTaskValue_WithCancellationToken_WhenThrown)}
			              """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncValue_WhenSuccess()
	{
		Func<int> @delegate = () => 1;

		async Task Act()
			=> await That(@delegate).Does().ThrowException();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage("""
			             Expected @delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncValue_WhenThrown()
	{
		Func<int> @delegate = () => throw new MyException();

		async Task Act()
			=> await That(@delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected @delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_FuncValue_WhenThrown)}
			              """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncValue_WithCancellationToken_Cancelled()
	{
		using CancellationTokenSource cts = new();
		// ReSharper disable once MethodHasAsyncOverload
		cts.Cancel();
		CancellationToken token = cts.Token;

		int Delegate(CancellationToken t)
		{
			t.ThrowIfCancellationRequested();
			return 0;
		}

		await That(Delegate).Does().Throw<OperationCanceledException>()
			.WithCancellation(token);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncValue_WithCancellationToken_WhenSuccess()
	{
		Func<CancellationToken, int> @delegate = _ => 1;

		async Task Act()
			=> await That(@delegate).Does().ThrowException();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage("""
			             Expected @delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}

	[Fact]
	public async Task ShouldSupportDelegate_FuncValue_WithCancellationToken_WhenThrown()
	{
		Func<CancellationToken, int> @delegate = _ => throw new MyException();

		async Task Act()
			=> await That(@delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected @delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_FuncValue_WithCancellationToken_WhenThrown)}
			              """);
	}

#if NET8_0_OR_GREATER
	[Fact]
	public async Task ShouldSupportDelegate_FuncValueTask_WhenSuccess()
	{
		ValueTask Delegate() => ValueTask.CompletedTask;

		async Task Act()
			=> await That(Delegate).Does().ThrowException();

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected Delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}
#endif


#if NET8_0_OR_GREATER
	[Fact]
	public async Task ShouldSupportDelegate_FuncValueTask_WithCancellationToken_WhenSuccess()
	{
		ValueTask Delegate(CancellationToken _) => ValueTask.CompletedTask;

		async Task Act()
			=> await That(Delegate).Does().ThrowException();

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected Delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}
#endif

#if NET8_0_OR_GREATER
	[Fact]
	public async Task ShouldSupportDelegate_ValueTask_WhenThrown()
	{
		ValueTask Delegate() => ValueTask.FromException(new MyException());

		async Task Act()
			=> await That(Delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected Delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_ValueTask_WhenThrown)}
			              """);
	}
#endif

#if NET8_0_OR_GREATER
	[Fact]
	public async Task ShouldSupportDelegate_ValueTask_WithCancellationToken_WhenThrown()
	{
		ValueTask Delegate(CancellationToken _) => ValueTask.FromException(new MyException());

		async Task Act()
			=> await That(Delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected Delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_ValueTask_WithCancellationToken_WhenThrown)}
			              """);
	}
#endif

#if NET8_0_OR_GREATER
	[Fact]
	public async Task ShouldSupportDelegate_ValueTaskValue_WhenSuccess()
	{
		ValueTask<int> Delegate() => ValueTask.FromResult(1);

		async Task Act()
			=> await That(Delegate).Does().ThrowException();

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected Delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}
#endif

#if NET8_0_OR_GREATER
	[Fact]
	public async Task ShouldSupportDelegate_ValueTaskValue_WhenThrown()
	{
		ValueTask<int> Delegate() => ValueTask.FromException<int>(new MyException());

		async Task Act()
			=> await That(Delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected Delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_ValueTaskValue_WhenThrown)}
			              """);
	}
#endif

#if NET8_0_OR_GREATER
	[Fact]
	public async Task ShouldSupportDelegate_ValueTaskValue_WithCancellationToken_WhenSuccess()
	{
		ValueTask<int> Delegate(CancellationToken _) => ValueTask.FromResult(1);

		async Task Act()
			=> await That(Delegate).Does().ThrowException();

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected Delegate to
			             throw an exception,
			             but it did not throw any exception
			             """);
	}
#endif

#if NET8_0_OR_GREATER
	[Fact]
	public async Task ShouldSupportDelegate_ValueTaskValue_WithCancellationToken_WhenThrown()
	{
		ValueTask<int> Delegate(CancellationToken _) => ValueTask.FromException<int>(new MyException());

		async Task Act()
			=> await That(Delegate).Does().NotThrow();

		await That(Act).Does().Throw<XunitException>()
			.WithMessage($"""
			              Expected Delegate to
			              not throw any exception,
			              but it did throw a MyException:
			                {nameof(ShouldSupportDelegate_ValueTaskValue_WithCancellationToken_WhenThrown)}
			              """);
	}
#endif
	[Theory]
	[AutoData]
	public async Task ShouldSupportNestedChecks(
		string innermostMessage, string innerMessage, string outerMessage)
	{
		Exception exception = new CustomException(outerMessage,
			new SubCustomException(innerMessage,
				// ReSharper disable once NotResolvedInText
				new ArgumentException(innermostMessage, nameof(innermostMessage))));
		void Act() => throw exception;

		CustomException result = await That(Act).Does().Throw<CustomException>()
			.WithInnerException(e1 => e1
				.HaveMessage(innerMessage).And
				.HaveInner<ArgumentException>(e2 => e2
					.HaveParamName(nameof(innermostMessage)).And.HaveMessage($"{innermostMessage}*")
					.AsWildcard()));

		await That(result).IsSameAs(exception);
	}

	public class MyException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: Exception(message, innerException);

	public class CustomException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: Exception(message, innerException);

	public class SubCustomException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: CustomException(message, innerException);
}
