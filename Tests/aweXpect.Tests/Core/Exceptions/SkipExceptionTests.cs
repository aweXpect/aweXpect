namespace Testably.Expectations.Tests.Core.Exceptions;

public sealed class SkipExceptionTests
{
	[Theory]
	[AutoData]
	public async Task Message_ShouldBeSet(string message)
	{
		SkipException subject = new(message);

		await That(subject.Message).Should().Be(message);
	}
}
