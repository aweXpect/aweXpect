using System.Collections.Generic;
using aweXpect.Customization;

namespace aweXpect.Core.Tests.TestHelpers;

internal class TestTraceWriter : ITraceWriter
{
	public List<string> Messages { get; } = [];
	public List<Exception> Exceptions { get; } = [];

	public void WriteMessage(string message)
		=> Messages.Add(message);

	public void WriteException(Exception exception)
		=> Exceptions.Add(exception);

	public IDisposable Register() => Customize.aweXpect.EnableTracing(this);
}
