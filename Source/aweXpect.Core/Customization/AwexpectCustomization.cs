using System.Collections.Generic;
using System.Threading;

namespace aweXpect.Customization;

/// <summary>
///     Customize the global behaviour of aweXpect.
/// </summary>
public class AwexpectCustomization : IAwexpectCustomization
{
	private readonly AsyncLocal<CustomizationStore> _store = new();

	TValue IAwexpectCustomization.Get<TValue>(string key, TValue defaultValue)
	{
		if (_store.Value == null)
		{
			return defaultValue;
		}

		return _store.Value.Get(key, defaultValue);
	}

	CustomizationLifetime IAwexpectCustomization.Set<TValue>(string key, TValue value)
	{
		if (_store.Value == null)
		{
			_store.Value = new CustomizationStore();
		}

		return _store.Value.Set(key, value);
	}

	private sealed class CustomizationStore
	{
		private readonly Dictionary<string, object?> _store = new();

		public TValue Get<TValue>(string key, TValue defaultValue)
		{
			if (_store.TryGetValue(key, out object? v) && v is TValue typedValue)
			{
				return typedValue;
			}

			return defaultValue;
		}

		public CustomizationLifetime Set<TValue>(string key, TValue value)
		{
			CustomizationLifetime lifetime;
			if (_store.TryGetValue(key, out object? v) && v is TValue previousValue)
			{
				lifetime = new CustomizationLifetime(() => _store[key] = previousValue);
			}
			else
			{
				lifetime = new CustomizationLifetime(() => _store.Remove(key));
			}

			_store[key] = value;
			return lifetime;
		}
	}
}