using System;

namespace aweXpect.Customization;

public static partial class AwexpectCustomizationExtensions
{
	/// <summary>
	///     Customize the reflection settings.
	/// </summary>
	public static ReflectionCustomization Reflection(this AwexpectCustomization awexpectCustomization)
		=> new(awexpectCustomization);

	/// <summary>
	///     Customize the reflection settings.
	/// </summary>
	public class ReflectionCustomization : ICustomizationValueUpdater<ReflectionCustomizationValue>
	{
		private readonly IAwexpectCustomization _awexpectCustomization;

		internal ReflectionCustomization(IAwexpectCustomization awexpectCustomization)
		{
			_awexpectCustomization = awexpectCustomization;
			ExcludedAssemblyPrefixes = new CustomizationValue<string[]>(
				() => Get().ExcludedAssemblyPrefixes,
				// ReSharper disable once WithExpressionModifiesAllMembers
				v => Update(p => p with
				{
					ExcludedAssemblyPrefixes = v
				}));
		}

		/// <inheritdoc cref="ReflectionCustomizationValue.ExcludedAssemblyPrefixes" />
		public ICustomizationValueSetter<string[]> ExcludedAssemblyPrefixes { get; }

		/// <inheritdoc cref="ICustomizationValueUpdater{ReflectionCustomizationValue}.Get()" />
		public ReflectionCustomizationValue Get()
			=> _awexpectCustomization.Get(nameof(Reflection), new ReflectionCustomizationValue());

		/// <inheritdoc
		///     cref="ICustomizationValueUpdater{ReflectionCustomizationValue}.Update(Func{ReflectionCustomizationValue,ReflectionCustomizationValue})" />
		public CustomizationLifetime Update(Func<ReflectionCustomizationValue, ReflectionCustomizationValue> update)
			=> _awexpectCustomization.Set(nameof(Reflection), update(Get()));
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
			"DynamicProxyGenAssembly2"
		];
	}
}