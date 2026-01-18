using System;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Helpers;

internal struct AsyncBecauseReason(Task<string> reason) : IBecauseReason
{
	private string? _message;

	private static string CreateMessage(string reason)
	{
		const string prefix = "because";
		string message = reason.Trim();

		return !message.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
			? $", {prefix} {message}"
			: $", {message}";
	}

#if NET8_0_OR_GREATER
	public async ValueTask<ConstraintResult>
#else
	public async Task<ConstraintResult>
#endif
		ApplyTo(ConstraintResult result)
	{
		if (_message is null)
		{
			_message = CreateMessage(await reason.ConfigureAwait(false));
		}

		string message = _message;
		return result.AppendExpectationText(e => e.Append(message));
	}
}
