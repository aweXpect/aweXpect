using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.TimeSystem;

namespace aweXpect.Core.Sources;

internal class ValueSource<TValue> : IValueSource<TValue>
{
	private readonly TValue? _value;

	public ValueSource(TValue? value)
	{
		_value = value;
	}

	#region IValueSource<TValue> Members

	public Task<TValue?> GetValue(ITimeSystem timeSystem, CancellationToken cancellationToken)
	{
		return Task.FromResult(_value);
	}

	#endregion
}
