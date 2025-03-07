﻿using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.TimeSystem;

namespace aweXpect.Core.Sources;

internal class ValueSource<TValue>(TValue value) : IValueSource<TValue>
{
	#region IValueSource<TValue> Members

	public Task<TValue> GetValue(ITimeSystem timeSystem, CancellationToken cancellationToken)
		=> Task.FromResult(value);

	#endregion
}
