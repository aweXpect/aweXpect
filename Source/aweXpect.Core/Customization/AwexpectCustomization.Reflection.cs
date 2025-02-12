using System;
using System.Threading;

namespace aweXpect.Customization;

public partial class AwexpectCustomization
{
	private readonly AsyncLocal<ReflectionCustomizationValue> _reflectionCustomizationValue = new();

	/// <summary>
	///     Customize the reflection settings.
	/// </summary>
	public ReflectionCustomization Reflection() => new(this);

	/// <summary>
	///     Customize the reflection settings.
	/// </summary>
	public class ReflectionCustomization : ICustomizationValueUpdater<ReflectionCustomizationValue>
	{
		private readonly AwexpectCustomization _awexpectCustomization;

		internal ReflectionCustomization(AwexpectCustomization awexpectCustomization)
		{
			_awexpectCustomization = awexpectCustomization;
			ExcludedAssemblyPrefixes = new CustomizationValue<string[]>(
				() => Get().ExcludedAssemblyPrefixes,
				// ReSharper disable once WithExpressionModifiesAllMembers
				v => Update(p => p with
				{
					ExcludedAssemblyPrefixes = v,
				}));
		}

		/// <inheritdoc cref="ReflectionCustomizationValue.ExcludedAssemblyPrefixes" />
		public ICustomizationValueSetter<string[]> ExcludedAssemblyPrefixes { get; }

		/// <inheritdoc cref="ICustomizationValueUpdater{ReflectionCustomizationValue}.Get()" />
		public ReflectionCustomizationValue Get()
			=> _awexpectCustomization._reflectionCustomizationValue.Value ?? new ReflectionCustomizationValue();

		/// <inheritdoc
		///     cref="ICustomizationValueUpdater{ReflectionCustomizationValue}.Update(Func{ReflectionCustomizationValue,ReflectionCustomizationValue})" />
		public CustomizationLifetime Update(Func<ReflectionCustomizationValue, ReflectionCustomizationValue> update)
		{
			ReflectionCustomizationValue previousValue = Get();
			CustomizationLifetime lifetime = new(() =>
				_awexpectCustomization._reflectionCustomizationValue.Value = previousValue);

			_awexpectCustomization._reflectionCustomizationValue.Value = update(previousValue);
			return lifetime;
		}
	}

	/// <summary>
	///     Customize the reflection settings.
	/// </summary>
	public record ReflectionCustomizationValue
	{
		/// <summary>
		///     The assembly namespace prefixes that are excluded during reflection.
		/// </summary>
		/// <remarks>
		///     Defaults to<br />
		///     - mscorlib<br />
		///     - System<br />
		///     - Microsoft<br />
		///     - JetBrains<br />
		///     - xunit<br />
		///     - Castle<br />
		///     - DynamicProxyGenAssembly2
		/// </remarks>
		public string[] ExcludedAssemblyPrefixes { get; set; } =
		[
			"mscorlib",
			"System",
			"Microsoft",
			"JetBrains",
			"xunit",
			"Castle",
			"DynamicProxyGenAssembly2",
		];
	}
}
