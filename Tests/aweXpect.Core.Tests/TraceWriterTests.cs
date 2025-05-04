using aweXpect.Chronology;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests;

public class TraceWriterTests
{
	[Fact]
	public async Task WithEnabledTracing_ForBooleanExpectations_ShouldTraceSuccessfulVerificationDetails()
	{
		bool subject = true;
		TestTraceWriter traceWriter = new();
		using (traceWriter.Register())
		{
			await That(subject).IsTrue();
		}

		await That(traceWriter.Messages).IsEqualTo([
			"Checking expectation for subject True",
			"  Successfully verified that subject is True",
		]);
	}

	[Fact]
	public async Task WithEnabledTracing_ForFailedDelegateWithoutReturnValue_ShouldTraceSuccessfulVerificationDetails()
	{
		Action callback = () => throw new Exception("foo");
		TestTraceWriter traceWriter = new();
		using (traceWriter.Register())
		{
			await That(callback).DoesNotExecuteWithin(1500.Milliseconds());
		}

		await That(traceWriter.Messages).HasCount(2);
		await That(traceWriter.Messages[0])
			.IsEqualTo("Checking expectation for callback delegate throwing System.Exception: foo")
			.AsPrefix();
		await That(traceWriter.Messages[1])
			.IsEqualTo("  Successfully verified that callback does not execute within 0:01.500");
	}

	[Fact]
	public async Task WithEnabledTracing_ForFailedDelegateWithReturnValue_ShouldTraceSuccessfulVerificationDetails()
	{
		Func<int> callback = () => throw new Exception("foo");
		TestTraceWriter traceWriter = new();
		using (traceWriter.Register())
		{
			await That(callback).DoesNotExecuteWithin(1500.Milliseconds());
		}

		await That(traceWriter.Messages).HasCount(2);
		await That(traceWriter.Messages[0])
			.IsEqualTo("Checking expectation for callback delegate returning int throwing System.Exception: foo")
			.AsPrefix();
		await That(traceWriter.Messages[1])
			.IsEqualTo("  Successfully verified that callback does not execute within 0:01.500");
	}

	[Fact]
	public async Task
		WithEnabledTracing_ForSuccessfulDelegateWithoutReturnValue_ShouldTraceSuccessfulVerificationDetails()
	{
		Action callback = () => { };
		TestTraceWriter traceWriter = new();
		using (traceWriter.Register())
		{
			await That(callback).ExecutesWithin(500.Milliseconds());
		}

		await That(traceWriter.Messages).IsEqualTo([
			"Checking expectation for callback delegate returning in 0:00",
			"  Successfully verified that callback executes within 0:00.500",
		]);
	}

	[Fact]
	public async Task WithEnabledTracing_ForSuccessfulDelegateWithReturnValue_ShouldTraceSuccessfulVerificationDetails()
	{
		Func<int> callback = () => 4;
		TestTraceWriter traceWriter = new();
		using (traceWriter.Register())
		{
			await That(callback).ExecutesWithin(500.Milliseconds());
		}

		await That(traceWriter.Messages).IsEqualTo([
			"Checking expectation for callback delegate returning int 4 in 0:00",
			"  Successfully verified that callback executes within 0:00.500",
		]);
	}
}
