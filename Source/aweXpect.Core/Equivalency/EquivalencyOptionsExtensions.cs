using System;

namespace aweXpect.Equivalency;

/// <summary>
///     Extension methods for <see cref="EquivalencyOptions" />.
/// </summary>
internal static class EquivalencyOptionsExtensions
{
	/// <summary>
	///     Returns type-specific <see cref="EquivalencyTypeOptions" />.
	/// </summary>
	internal static EquivalencyTypeOptions GetTypeOptions(this EquivalencyOptions @this, Type? type,
		EquivalencyTypeOptions defaultValue)
	{
		if (type != null && @this.CustomOptions.TryGetValue(type, out EquivalencyTypeOptions? customOptions))
		{
			return customOptions;
		}

		return defaultValue;
	}
}
