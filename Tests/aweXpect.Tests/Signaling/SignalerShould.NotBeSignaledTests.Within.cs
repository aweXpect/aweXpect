using System.Threading;
using aweXpect.Signaling;

// ReSharper disable MethodHasAsyncOverload

namespace aweXpect.Tests.Signaling;

public sealed partial class SignalerShould
{
	public sealed partial class NotBeSignaled
	{
		public sealed class WithinTests
		{
			[Fact]
			public async Task WhenNotTriggeredWithinTheGivenTimeout_ShouldSucceed()
			{
				Signaler signaler = new();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(5.Seconds(), token)
					.ContinueWith(_ => signaler.Signal(), token);

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled().Within(40.Milliseconds());

				await That(Act).Should().NotThrow();
				cts.Cancel();
			}

			[Fact]
			public async Task WhenNotTriggeredWithParameterWithinTheGivenTimeout_ShouldSucceed()
			{
				Signaler<string> signaler = new();
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;

				_ = Task.Delay(5.Seconds(), token)
					.ContinueWith(_ => signaler.Signal("foo"), token);

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled().Within(40.Milliseconds());

				await That(Act).Should().NotThrow();
				cts.Cancel();
			}

			[Fact]
			public async Task WhenTriggeredWithinTheGivenTimeout_ShouldFail()
			{
				Signaler signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => signaler.Signal());

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled().Within(10.Seconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             not have recorded the callback within 0:10,
					             but it was recorded once
					             """);
			}

			[Fact]
			public async Task WhenTriggeredWithParameterWithinTheGivenTimeout_ShouldFail()
			{
				Signaler<string> signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => signaler.Signal("foo"));

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled().Within(10.Seconds());

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             not have recorded the callback within 0:10,
					             but it was recorded once in [
					               "foo"
					             ]
					             """);
			}
		}
	}
}
