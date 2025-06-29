using System;
using aweXpect.Options;

namespace aweXpect.Delegates;

public partial class ThatDelegateThrows<TException>
{
	/// <summary>
	///     Verifies that the delegate throws within the given <paramref name="duration" />.
	/// </summary>
	public ThatDelegateThrows<TException> Within(TimeSpan duration)
	{
		if (duration < TimeSpan.Zero)
		{
			throw new ArgumentOutOfRangeException(nameof(duration), "The duration must not be negative.");
		}

		TimeSpanEqualityOptions options = new();
		options.Within(duration);
		_throwOptions.ExecutionTimeOptions = options;
		return this;
	}
}
