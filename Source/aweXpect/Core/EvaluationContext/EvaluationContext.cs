using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Testably.Expectations.Core.EvaluationContext;

internal class EvaluationContext : IEvaluationContext
{
	private Dictionary<string, object?>? _store;

	#region IEvaluationContext Members

	/// <inheritdoc />
	public void Store<T>(string key, T value)
	{
		_store ??= new Dictionary<string, object?>();
		_store[key] = value;
	}

	/// <inheritdoc />
	public bool TryReceive<T>(string key, [NotNullWhen(true)] out T? value)
	{
		if (_store != null &&
		    _store.TryGetValue(key, out object? storedValue)
		    && storedValue is T typeMatchingValue)
		{
			value = typeMatchingValue;
			return true;
		}

		value = default;
		return false;
	}

	#endregion
}
