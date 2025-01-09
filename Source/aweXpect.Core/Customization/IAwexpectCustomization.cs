namespace aweXpect.Customization;

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