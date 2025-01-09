using System;

namespace aweXpect.Customization;

public static partial class GlobalCustomizationExtensions
{
	/// <summary>
	///     Customize the reflection settings.
	/// </summary>
	public static ReflectionCustomization Reflection(this GlobalCustomization globalCustomization)
		=> new(globalCustomization);

	/// <summary>
	///     Customize the reflection settings.
	/// </summary>
	public class ReflectionCustomization : IUpdateableCustomizationValue<ReflectionCustomizationValue>
	{
		private readonly IGlobalCustomization _globalCustomization;

		internal ReflectionCustomization(IGlobalCustomization globalCustomization)
		{
			_globalCustomization = globalCustomization;
			ExcludedAssemblyPrefixes = new CustomizationValue<string[]>(
				() => Get().ExcludedAssemblyPrefixes,
				// ReSharper disable once WithExpressionModifiesAllMembers
				v => Update(p => p with
				{
					ExcludedAssemblyPrefixes = v
				}));
		}

		/// <inheritdoc cref="ReflectionCustomizationValue.ExcludedAssemblyPrefixes" />
		public ICustomizationValue<string[]> ExcludedAssemblyPrefixes { get; }

		/// <inheritdoc cref="IUpdateableCustomizationValue{ReflectionCustomizationValue}.Get()" />
		public ReflectionCustomizationValue Get()
			=> _globalCustomization.Get(nameof(Reflection), new ReflectionCustomizationValue());

		/// <inheritdoc
		///     cref="IUpdateableCustomizationValue{ReflectionCustomizationValue}.Update(Func{ReflectionCustomizationValue,ReflectionCustomizationValue})" />
		public CustomizationLifetime Update(Func<ReflectionCustomizationValue, ReflectionCustomizationValue> update)
			=> _globalCustomization.Set(nameof(Reflection), update(Get()));
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
