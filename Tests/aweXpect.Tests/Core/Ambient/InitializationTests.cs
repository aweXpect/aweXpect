#if NET6_0_OR_GREATER
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Testably.Expectations.Tests.Core.Ambient;

public sealed class InitializationTests
{
	[Fact]
	public async Task AssemblyWithoutReferenceOnSupportedFramework_ShouldThrowExpectationException()
	{
		if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			// *.exe files only work on windows!
			return;
		}

		const string fallbackTestPath = @"..\..\TestFramework.FallbackTest.exe";

		Process fallbackTestProcess = new();
		fallbackTestProcess.StartInfo.CreateNoWindow = true;
		fallbackTestProcess.StartInfo.FileName = fallbackTestPath;
		fallbackTestProcess.Start();
		fallbackTestProcess.WaitForExit(30000);

		await That(fallbackTestProcess.ExitCode).Should().Be(1);
	}
}
#endif
