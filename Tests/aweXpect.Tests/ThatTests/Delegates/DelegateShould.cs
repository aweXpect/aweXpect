using System.Runtime.CompilerServices;
using System.Threading;
using Testably.Expectations.Tests.TestHelpers;

namespace Testably.Expectations.Tests.ThatTests.Delegates;

public sealed partial class DelegateShould
{
	public sealed class Tests
	{
		[Theory]
		[AutoData]
		public async Task ShouldReturnValue_FuncTaskValue(int value)
		{
			Task<int> Delegate() => Task.FromResult(value);

			int result = await That(Delegate).Should().NotThrow();

			await That(result).Should().Be(value);
		}

		[Theory]
		[AutoData]
		public async Task ShouldReturnValue_FuncValue(int value)
		{
			int Delegate() => value;

			int result = await That(Delegate).Should().NotThrow();

			await That(result).Should().Be(value);
		}

		[Theory]
		[AutoData]
		public async Task ShouldReturnValue_TaskValue(int value)
		{
			Task<int> @delegate = Task.FromResult(value);

			int result = await That(@delegate).Should().NotThrow();

			await That(result).Should().Be(value);
		}

#if NET6_0_OR_GREATER
		[Theory]
		[AutoData]
		public async Task ShouldReturnValue_ValueTaskValue(int value)
		{
			ValueTask<int> @delegate = ValueTask.FromResult(value);

			int result = await That(@delegate).Should().NotThrow();

			await That(result).Should().Be(value);
		}
#endif

		[Fact]
		public async Task ShouldSupportDelegate_Action_WhenSuccess()
		{
			Action @delegate = () => { };

			async Task Act()
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_Action_WhenThrown()
		{
			Action @delegate = () => throw new MyException();

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
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

			await That(Delegate).Should().Throw<OperationCanceledException>()
				.WithCancellation(token);
		}

		[Fact]
		public async Task ShouldSupportDelegate_Action_WithCancellationToken_WhenSuccess()
		{
			Action<CancellationToken> @delegate = _ => { };

			async Task Act()
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_Action_WithCancellationToken_WhenThrown()
		{
			Action<CancellationToken> @delegate = _ => throw new MyException();

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_FuncTask_WhenThrown()
		{
			Func<Task> @delegate = () => Task.FromException(new MyException());

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
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

			await That(Delegate).Should().Throw<OperationCanceledException>()
				.WithCancellation(token);
		}

		[Fact]
		public async Task ShouldSupportDelegate_FuncTask_WithCancellationToken_WhenSuccess()
		{
			Func<CancellationToken, Task> @delegate = _ => Task.CompletedTask;

			async Task Act()
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_FuncTask_WithCancellationToken_WhenThrown()
		{
			Func<CancellationToken, Task> @delegate = _ => Task.FromException(new MyException());

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_FuncTaskValue_WhenThrown()
		{
			Func<Task<int>> @delegate = () => Task.FromException<int>(new MyException());

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
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

			await That(Delegate).Should().Throw<OperationCanceledException>()
				.WithCancellation(token);
		}

		[Fact]
		public async Task ShouldSupportDelegate_FuncTaskValue_WithCancellationToken_WhenSuccess()
		{
			Func<CancellationToken, Task<int>> @delegate = _ => Task.FromResult(1);

			async Task Act()
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_FuncTaskValue_WithCancellationToken_WhenThrown()
		{
			Func<CancellationToken, Task<int>> @delegate = _
				=> Task.FromException<int>(new MyException());

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_FuncValue_WhenThrown()
		{
			Func<int> @delegate = () => throw new MyException();

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
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

			await That(Delegate).Should().Throw<OperationCanceledException>()
				.WithCancellation(token);
		}

		[Fact]
		public async Task ShouldSupportDelegate_FuncValue_WithCancellationToken_WhenSuccess()
		{
			Func<CancellationToken, int> @delegate = _ => 1;

			async Task Act()
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_FuncValue_WithCancellationToken_WhenThrown()
		{
			Func<CancellationToken, int> @delegate = _ => throw new MyException();

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected @delegate to
				              not throw any exception,
				              but it did throw a MyException:
				                {nameof(ShouldSupportDelegate_FuncValue_WithCancellationToken_WhenThrown)}
				              """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_Task_WhenSuccess()
		{
			Task @delegate = Task.CompletedTask;

			async Task Act()
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_Task_WhenThrown()
		{
			Task @delegate = Task.FromException(new MyException());

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected @delegate to
				              not throw any exception,
				              but it did throw a MyException:
				                {nameof(ShouldSupportDelegate_Task_WhenThrown)}
				              """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_TaskValue_WhenSuccess()
		{
			Task<int> @delegate = Task.FromResult(1);

			async Task Act()
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}

		[Fact]
		public async Task ShouldSupportDelegate_TaskValue_WhenThrown()
		{
			Task<int> @delegate = Task.FromException<int>(new MyException());

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected @delegate to
				              not throw any exception,
				              but it did throw a MyException:
				                {nameof(ShouldSupportDelegate_TaskValue_WhenThrown)}
				              """);
		}

#if NET6_0_OR_GREATER
		[Fact]
		public async Task ShouldSupportDelegate_ValueTask_WhenSuccess()
		{
			ValueTask @delegate = ValueTask.CompletedTask;

			async Task Act()
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}
#endif

#if NET6_0_OR_GREATER
		[Fact]
		public async Task ShouldSupportDelegate_ValueTask_WhenThrown()
		{
			ValueTask @delegate = ValueTask.FromException(new MyException());

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected @delegate to
				              not throw any exception,
				              but it did throw a MyException:
				                {nameof(ShouldSupportDelegate_ValueTask_WhenThrown)}
				              """);
		}
#endif

#if NET6_0_OR_GREATER
		[Fact]
		public async Task ShouldSupportDelegate_ValueTaskValue_WhenSuccess()
		{
			ValueTask<int> @delegate = ValueTask.FromResult(1);

			async Task Act()
				=> await That(@delegate).Should().ThrowException();

			await That(Act).Should().ThrowException()
				.WithMessage("""
				             Expected @delegate to
				             throw an Exception,
				             but it did not throw any exception
				             """);
		}
#endif

#if NET6_0_OR_GREATER
		[Fact]
		public async Task ShouldSupportDelegate_ValueTaskValue_WhenThrown()
		{
			ValueTask<int> @delegate = ValueTask.FromException<int>(new MyException());

			async Task Act()
				=> await That(@delegate).Should().NotThrow();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected @delegate to
				              not throw any exception,
				              but it did throw a MyException:
				                {nameof(ShouldSupportDelegate_ValueTaskValue_WhenThrown)}
				              """);
		}
#endif
	}

	public class CustomException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: Exception(message, innerException)
	{
		public string? Value { get; set; }
	}

	public class SubCustomException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: CustomException(message, innerException);

	public class OtherException(
		[CallerMemberName] string message = "",
		Exception? innerException = null)
		: Exception(message, innerException);
}
