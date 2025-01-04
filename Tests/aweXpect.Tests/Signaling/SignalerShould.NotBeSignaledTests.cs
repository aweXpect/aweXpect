using System.Threading;
using aweXpect.Signaling;

namespace aweXpect.Tests.Signaling;

public sealed partial class SignalerShould
{
	public sealed partial class NotBeSignaled
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenNotTriggered_ShouldSucceed()
			{
				Signaler signaler = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled().WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Signaler? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotBeSignaled();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have recorded the callback,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenTriggered_ShouldFail()
			{
				Signaler signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => signaler.Signal());

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             not have recorded the callback,
					             but it was recorded once
					             """);
			}
		}

		public sealed class WithParameterTests
		{
			[Fact]
			public async Task WhenNotTriggered_ShouldSucceed()
			{
				Signaler<int> signaler = new();
				using CancellationTokenSource cts = new();
				cts.CancelAfter(50.Milliseconds());
				CancellationToken token = cts.Token;

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled().WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				Signaler<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotBeSignaled();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not have recorded the callback,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenTriggered_ShouldFail()
			{
				Signaler<int> signaler = new();

				_ = Task.Delay(10.Milliseconds())
					.ContinueWith(_ => signaler.Signal(42));

				async Task Act() =>
					await That(signaler).Should().NotBeSignaled();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected signaler to
					             not have recorded the callback,
					             but it was recorded once in [
					               42
					             ]
					             """);
			}
		}
	}
}
