using aweXpect.Customization;

namespace aweXpect.Core.Tests.Customization;

public class AwexpectCustomizationTests
{
	[Fact]
	public async Task Formatting_ShouldReturnTenAsDefaultMaximumNumberOfCollectionItems()
	{
		int defaultValue1 = Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get();
		int defaultValue2 = Customize.aweXpect.Formatting().Get().MaximumNumberOfCollectionItems;

		await That(defaultValue1).IsEqualTo(10);
		await That(defaultValue2).IsEqualTo(10);
	}

	[Fact]
	public async Task Formatting_ShouldUpdate()
	{
		int defaultValue = 10;
		int value = 42;
		using (Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Set(value))
		{
			await That(Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get()).IsEqualTo(value);
			await That(Customize.aweXpect.Formatting().Get().MaximumNumberOfCollectionItems).IsEqualTo(value);
		}

		await That(Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get()).IsEqualTo(defaultValue);
		await That(Customize.aweXpect.Formatting().Get().MaximumNumberOfCollectionItems).IsEqualTo(defaultValue);
	}

	[Fact]
	public async Task Formatting_ShouldUpdate2()
	{
		int defaultValue = 10;
		int value = 42;
		// ReSharper disable once WithExpressionModifiesAllMembers
		using (Customize.aweXpect.Formatting().Update(p => p with
		       {
			       MaximumNumberOfCollectionItems = value,
		       }))
		{
			await That(Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get()).IsEqualTo(value);
			await That(Customize.aweXpect.Formatting().Get().MaximumNumberOfCollectionItems).IsEqualTo(value);
		}

		await That(Customize.aweXpect.Formatting().MaximumNumberOfCollectionItems.Get()).IsEqualTo(defaultValue);
		await That(Customize.aweXpect.Formatting().Get().MaximumNumberOfCollectionItems).IsEqualTo(defaultValue);
	}

	[Fact]
	public async Task NestedLifetimes_ShouldSetPreviousValue()
	{
		string valueInLifetime1;
		string valueInLifetime2;
		string valueInLifetime1AfterLifetime2;
		string valueBeforeLifetime1 = Customize.aweXpect.MyConfiguration().Get();
		using (Customize.aweXpect.MyConfiguration().Set("l1"))
		{
			valueInLifetime1 = Customize.aweXpect.MyConfiguration().Get();
			using (Customize.aweXpect.MyConfiguration().Set("l2"))
			{
				valueInLifetime2 = Customize.aweXpect.MyConfiguration().Get();
			}

			valueInLifetime1AfterLifetime2 = Customize.aweXpect.MyConfiguration().Get();
		}

		string valueAfterLifetime1 = Customize.aweXpect.MyConfiguration().Get();

		await ThatAll(
			That(valueBeforeLifetime1).IsEqualTo("foo"),
			That(valueInLifetime1).IsEqualTo("l1"),
			That(valueInLifetime2).IsEqualTo("l2"),
			That(valueInLifetime1AfterLifetime2).IsEqualTo("l1"),
			That(valueAfterLifetime1).IsEqualTo("foo")
		);
	}
}

public static class DummyExtensions
{
	private static readonly string MyKey = Guid.NewGuid().ToString();

	public static ICustomizationValueSetter<string> MyConfiguration(this AwexpectCustomization awexpectCustomization)
		=> new CustomizationValue<string>(awexpectCustomization, MyKey, "foo");

	private sealed class CustomizationValue<TValue>(
		IAwexpectCustomization customization,
		string key,
		TValue defaultValue)
		: ICustomizationValueSetter<TValue>
	{
		public TValue Get()
			=> customization.Get(key, defaultValue);

		public CustomizationLifetime Set(TValue value)
			=> customization.Set(key, value);
	}
}
