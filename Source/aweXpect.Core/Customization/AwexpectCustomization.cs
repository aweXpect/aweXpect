using System;
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

	private class CustomizationStore
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

/// <summary>
///     A customization value of type <typeparamref name="TValue" /> that can be set.
/// </summary>
/// <remarks>
///     This is primarily intended for primitive types
/// </remarks>
public interface ICustomizationValue<TValue>
{
	/// <summary>
	///     Get the stored <typeparamref name="TValue" />.
	/// </summary>
	TValue Get();

	/// <summary>
	///     Set the stored <typeparamref name="TValue" />.
	/// </summary>
	CustomizationLifetime Set(TValue value);
}

/// <summary>
///     A customization value of type <typeparamref name="TValue" /> that can be updated.
/// </summary>
/// <remarks>
///     This is primarily intended for record types.
/// </remarks>
public interface IUpdateableCustomizationValue<TValue>
{
	/// <summary>
	///     Get the stored <typeparamref name="TValue" />.
	/// </summary>
	TValue Get();

	/// <summary>
	///     Update the stored <typeparamref name="TValue" />.
	/// </summary>
	CustomizationLifetime Update(Func<TValue, TValue> update);
}

/// <summary>
///     The lifetime of a customization setting.
/// </summary>
public sealed class CustomizationLifetime(Action callback) : IDisposable
{
	/// <inheritdoc cref="IDisposable.Dispose()" />
	public void Dispose() => callback();
}

/// <summary>
///     Customize the global behaviour of aweXpect.
/// </summary>
public interface IAwexpectCustomization
{
	/// <summary>
	///     Get the customization <typeparamref name="TValue" /> stored under the given <paramref name="key" />.
	/// </summary>
	/// <remarks>
	///     If no customization was stored, use the given <paramref name="defaultValue" />.
	/// </remarks>
	TValue Get<TValue>(string key, TValue defaultValue);

	/// <summary>
	///     Set the customization <typeparamref name="TValue" /> stored under the given <paramref name="key" />.
	/// </summary>
	/// <remarks>
	///     When the returned <see cref="CustomizationLifetime" /> is disposed, the value will be reset to the previous value.
	/// </remarks>
	CustomizationLifetime Set<TValue>(string key, TValue value);
}
