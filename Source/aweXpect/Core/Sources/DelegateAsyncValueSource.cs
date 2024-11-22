using System;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core.TimeSystem;

namespace Testably.Expectations.Core.Sources;

internal class DelegateAsyncValueSource<TValue>(Func<CancellationToken, Task<TValue>> action)
	: IValueSource<DelegateValue<TValue>>
{
	#region IValueSource<DelegateValue<TValue>> Members

	public async Task<DelegateValue<TValue>?> GetValue(ITimeSystem timeSystem,
		CancellationToken cancellationToken)
	{
		IStopwatch sw = timeSystem.Stopwatch.New();
		try
		{
			sw.Start();
			TValue value = await action(cancellationToken);
			sw.Stop();
			return new DelegateValue<TValue>(value, null, sw.Elapsed);
		}
		catch (Exception ex)
		{
			return new DelegateValue<TValue>(default, ex, sw.Elapsed);
		}
	}

	#endregion
}
