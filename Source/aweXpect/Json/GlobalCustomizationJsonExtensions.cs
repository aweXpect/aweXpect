#if NET8_0_OR_GREATER
using System;
using System.Text.Json;
using aweXpect.Customization;

namespace aweXpect.Json;

/// <summary>
///     Extension methods on <see cref="GlobalCustomization" /> for JSON.
/// </summary>
public static class GlobalCustomizationJsonExtensions
{
	/// <summary>
	///     Customize the JSON settings.
	/// </summary>
	public static JsonCustomization Json(this GlobalCustomization globalCustomization)
		=> new(globalCustomization);

	private class CustomizationValue<TValue>(
		Func<TValue> getter,
		Func<TValue, CustomizationLifetime> setter)
		: ICustomizationValue<TValue>
	{
		/// <inheritdoc cref="ICustomizationValue{TValue}.Get()" />
		public TValue Get() => getter();

		/// <inheritdoc cref="ICustomizationValue{TValue}.Set(TValue)" />
		public CustomizationLifetime Set(TValue value) => setter(value);
	}

	/// <summary>
	///     Customize the JSON settings.
	/// </summary>
	public class JsonCustomization : IUpdateableCustomizationValue<JsonCustomizationValue>
	{
		private readonly IGlobalCustomization _globalCustomization;

		internal JsonCustomization(IGlobalCustomization globalCustomization)
		{
			_globalCustomization = globalCustomization;
			DefaultJsonDocumentOptions = new CustomizationValue<JsonDocumentOptions>(
				() => Get().DefaultJsonDocumentOptions,
				// ReSharper disable once WithExpressionModifiesAllMembers
				v => Update(p => p with
				{
					DefaultJsonDocumentOptions = v
				}));
		}

		/// <inheritdoc cref="JsonCustomizationValue.DefaultJsonDocumentOptions" />
		public ICustomizationValue<JsonDocumentOptions> DefaultJsonDocumentOptions { get; }

		/// <inheritdoc cref="IUpdateableCustomizationValue{JsonCustomizationValue}.Get()" />
		public JsonCustomizationValue Get()
			=> _globalCustomization.Get(nameof(Json), new JsonCustomizationValue());

		/// <inheritdoc
		///     cref="IUpdateableCustomizationValue{JsonCustomizationValue}.Update(Func{JsonCustomizationValue,JsonCustomizationValue})" />
		public CustomizationLifetime Update(Func<JsonCustomizationValue, JsonCustomizationValue> update)
			=> _globalCustomization.Set(nameof(Json), update(Get()));
	}

	/// <summary>
	///     Customize the JSON settings.
	/// </summary>
	public record JsonCustomizationValue
	{
		/// <summary>
		///     The default <see cref="JsonDocumentOptions" />.
		/// </summary>
		public JsonDocumentOptions DefaultJsonDocumentOptions { get; set; } = new()
		{
			AllowTrailingCommas = true
		};
	}
}
#endif
