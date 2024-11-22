using System;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.TimeSystem;

namespace aweXpect.Core.Sources;

internal class DelegateSource(Action<CancellationToken> action) : IValueSource<DelegateValue>
{
	#region IValueSource<DelegateValue> Members

	public Task<DelegateValue?> GetValue(ITimeSystem timeSystem,
		CancellationToken cancellationToken)
	{
		IStopwatch sw = timeSystem.Stopwatch.New();
		try
		{
			sw.Start();
			action(cancellationToken);
			sw.Stop();
			return Task.FromResult<DelegateValue?>(
				new DelegateValue(null, sw.Elapsed));
		}
		catch (Exception ex)
		{
			return Task.FromResult<DelegateValue?>(
				new DelegateValue(ex, sw.Elapsed));
		}
	}

	#endregion
}
