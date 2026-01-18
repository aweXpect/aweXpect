using System;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Helpers;

internal readonly struct BecauseReason(string reason) : IBecauseReason
{
	private readonly Lazy<string> _message = new(() => CreateMessage(reason));

	private static string CreateMessage(string reason)
	{
		const string prefix = "because";
		string message = reason.Trim();

		return !message.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
			? $", {prefix} {message}"
			: $", {message}";
	}

	public override string ToString()
		=> _message.Value;
	
#if NET8_0_OR_GREATER
	public ValueTask<ConstraintResult>
#else
	public Task<ConstraintResult>
#endif
	ApplyTo(ConstraintResult result)
	{
		string message = _message.Value;
#if NET8_0_OR_GREATER
		return ValueTask.FromResult(result.AppendExpectationText(e => e.Append(message)));
#else
		return Task.FromResult(result.AppendExpectationText(e => e.Append(message)));
#endif
	}
}
