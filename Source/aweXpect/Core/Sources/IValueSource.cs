using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core.TimeSystem;

namespace Testably.Expectations.Core.Sources;

internal interface IValueSource<TValue>
{
	Task<TValue?> GetValue(ITimeSystem timeSystem, CancellationToken cancellationToken);
}
