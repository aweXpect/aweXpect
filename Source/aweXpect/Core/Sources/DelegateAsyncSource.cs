using System;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core.TimeSystem;

namespace Testably.Expectations.Core.Sources;

internal class DelegateAsyncSource(Func<CancellationToken, Task> action)
	: IValueSource<DelegateValue>
{
	#region IValueSource<DelegateValue> Members

	public async Task<DelegateValue?> GetValue(ITimeSystem timeSystem,
		CancellationToken cancellationToken)
	{
		IStopwatch sw = timeSystem.Stopwatch.New();
		try
		{
			sw.Start();
			await action(cancellationToken);
			sw.Stop();
			return new DelegateValue(null, sw.Elapsed);
		}
		catch (Exception ex)
		{
			return new DelegateValue(ex, sw.Elapsed);
		}
	}

	#endregion
}
