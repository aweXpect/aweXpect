---
sidebar_position: 2
---

# Customization

You can add you own customizations on top of the `AwexpectCustomization` class by adding extension methods.

## Add a simple customization value

You can add a simple customizable value (e.g. an `int`):
```csharp
public static class MyCustomizationExtensions
{
    public static ICustomizationValue<int> MyCustomization(this AwexpectCustomization awexpectCustomization)
        => new CustomizationValue<int>(awexpectCustomization, nameof(MyCustomization), 42);

    internal class CustomizationValue<TValue>(IAwexpectCustomization awexpectCustomization, string key, TValue defaultValue)
        : ICustomizationValue<TValue>
    {
        public TValue Get()
            => awexpectCustomization.Get(key, defaultValue);

        public CustomizationLifetime Set(TValue value)
            => awexpectCustomization.Set(key, value);
    }
}
```

This allows expectations to access the value:
```csharp
 // will return the default value of 42
int myCustomization = Customize.aweXpect.MyCustomization().Get();
```

And users can customize the value:
```csharp
using (Customize.aweXpect.MyCustomization().Set(43))
{
    // will now return 43
    int myCustomization = Customize.aweXpect.MyCustomization().Get();
}
// will now return again the default value of 42, because the customization lifetime was disposed
_ = Customize.aweXpect.MyCustomization().Get();
```

*Note: you can also use this mechanism for complex objects like classes, but they can only be changed as a whole (and not individual properties)*


## Add a customization group

You can also add a group of customization values, that can be changed individually or as a whole
```csharp
public static class JsonAwexpectCustomizationExtensions
{
    public static JsonCustomization Json(this AwexpectCustomization awexpectCustomization)
        => new(awexpectCustomization);

    public class JsonCustomization : IUpdateableCustomizationValue<JsonCustomizationValue>
    {
        private readonly IAwexpectCustomization _awexpectCustomization;

        internal JsonCustomization(IAwexpectCustomization awexpectCustomization)
        {
            _awexpectCustomization = awexpectCustomization;
            DefaultJsonDocumentOptions = new CustomizationValue<JsonDocumentOptions>(
                () => Get().DefaultJsonDocumentOptions,
                v => Update(p => p with { DefaultJsonDocumentOptions = v }));
            DefaultJsonSerializerOptions = new CustomizationValue<JsonSerializerOptions>(
                () => Get().DefaultJsonSerializerOptions,
                v => Update(p => p with { DefaultJsonSerializerOptions = v }));
        }

        public ICustomizationValue<JsonDocumentOptions> DefaultJsonDocumentOptions { get; }
        public ICustomizationValue<JsonSerializerOptions> DefaultJsonSerializerOptions { get; }

        public JsonCustomizationValue Get()
            => _awexpectCustomization.Get(nameof(Json), new JsonCustomizationValue());

        public CustomizationLifetime Update(Func<JsonCustomizationValue, JsonCustomizationValue> update)
            => _awexpectCustomization.Set(nameof(Json), update(Get()));
    }

    public record JsonCustomizationValue
    {
        public JsonDocumentOptions DefaultJsonDocumentOptions { get; set; } = new()
        {
            AllowTrailingCommas = true
        };
        public JsonSerializerOptions DefaultJsonSerializerOptions { get; set; } = new()
        {
            AllowTrailingCommas = true
        };
    }

    private class CustomizationValue<TValue>(
        Func<TValue> getter,
        Func<TValue, CustomizationLifetime> setter)
        : ICustomizationValue<TValue>
    {
        public TValue Get() => getter();
        public CustomizationLifetime Set(TValue value) => setter(value);
    }
}
```

This allows expectations to access values either individually or for the whole group:
```csharp
 // both will return the default value 'true'
int myCustomization1 = Customize.aweXpect.Json().Get().DefaultJsonDocumentOptions.AllowTrailingCommas;
int myCustomization2 = Customize.aweXpect.Json().DefaultJsonDocumentOptions.Get().AllowTrailingCommas;
```

And users can customize either individual values or the whole group:
```csharp
// update a single value (keeping the other values)
JsonSerializerOptions mySerializerOptions = new();
using (Customize.aweXpect.Json().DefaultJsonSerializerOptions.Set(mySerializerOptions))
{
    // will use `mySerializerOptions` for the `JsonSerializerOptions`
	// but keep any configured `JsonDocumentOptions`
}

// ...or update the whole group
JsonCustomizationValue myCustomization = new();
using (Customize.aweXpect.Json().Update(_ => myCustomization))
{
    // will use the all set properties from the `myCustomization`
}
```
