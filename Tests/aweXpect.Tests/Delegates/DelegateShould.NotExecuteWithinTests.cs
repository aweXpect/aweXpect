using aweXpect.Extensions;
using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed class NotExecuteWithin
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenActionThrowsAnException_ShouldSucceed()
			{
				Action @delegate = () => throw new MyException();

				async Task Act()
					=> await That(@delegate).Should().NotExecuteWithin(500.Milliseconds());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenFuncTaskThrowsAnException_ShouldSucceed()
			{
				Func<Task> @delegate = () => Task.FromException(new MyException());

				async Task Act()
					=> await That(@delegate).Should().NotExecuteWithin(500.Milliseconds());

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenFuncTaskValueThrowsAnException_ShouldSucceed()
			{
				Func<Task<int>> @delegate = () => Task.FromException<int>(new MyException());

				async Task Act()
					=> await That(@delegate).Should().NotExecuteWithin(500.Milliseconds());

				await That(Act).Should().NotThrow();
			}

#if NET8_0_OR_GREATER
			[Fact]
			public async Task WhenFuncValueTaskThrowsAnException_ShouldSucceed()
			{
				ValueTask Delegate() => new(Task.FromException(new MyException()));

				async Task Act()
					=> await That(Delegate).Should().NotExecuteWithin(500.Milliseconds());

				await That(Act).Should().NotThrow();
			}

#endif

#if NET8_0_OR_GREATER
			[Fact]
			public async Task WhenFuncValueTaskValueThrowsAnException_ShouldSucceed()
			{
				ValueTask<int> Delegate() => new(Task.FromException<int>(new MyException()));

				async Task Act()
					=> await That(Delegate).Should().NotExecuteWithin(500.Milliseconds());

				await That(Act).Should().NotThrow();
			}
#endif

			[Fact]
			public async Task WhenFuncValueThrowsAnException_ShouldSucceed()
			{
				Func<int> @delegate = () => throw new MyException();

				async Task Act()
					=> await That(@delegate).Should().NotExecuteWithin(500.Milliseconds());

				await That(Act).Should().NotThrow();
			}
		}
	}
}
