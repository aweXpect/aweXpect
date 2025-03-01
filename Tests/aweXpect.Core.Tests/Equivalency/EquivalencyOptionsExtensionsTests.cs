using aweXpect.Equivalency;

namespace aweXpect.Core.Tests.Equivalency;

public sealed class EquivalencyOptionsExtensionsTests
{
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Generic_For_IgnoringCollectionOrder_ShouldSetOptionForType(bool ignoreCollectionOrder)
	{
		EquivalencyOptions options = new();

		EquivalencyOptions result = options.For<MyClass>(o => o.IgnoringCollectionOrder(ignoreCollectionOrder));

		await That(result.IgnoreCollectionOrder).IsFalse();
		await That(result.CustomOptions).ContainsKey(typeof(MyClass))
			.WhoseValue.IsEquivalentTo(new
			{
				IgnoreCollectionOrder = ignoreCollectionOrder,
			});
	}

	[Theory]
	[AutoData]
	public async Task Generic_For_IgnoringMember_ShouldSetOptionForType(string memberToIgnore)
	{
		EquivalencyOptions options = new();

		EquivalencyOptions result = options.For<MyClass>(o => o.IgnoringMember(memberToIgnore));

		await That(result.MembersToIgnore).IsEmpty();
		await That(result.CustomOptions).ContainsKey(typeof(MyClass))
			.WhoseValue.IsEquivalentTo(new
			{
				MembersToIgnore = new[]
				{
					memberToIgnore,
				},
			});
	}

	[Theory]
	[InlineData(IncludeMembers.None)]
	[InlineData(IncludeMembers.Public)]
	[InlineData(IncludeMembers.Internal)]
	[InlineData(IncludeMembers.Private)]
	public async Task Generic_For_IncludingFields_ShouldSetOptionForType(IncludeMembers fieldsToInclude)
	{
		EquivalencyOptions options = new();

		EquivalencyOptions result = options.For<MyClass>(o => o.IncludingFields(fieldsToInclude));

		await That(result.Fields).IsEqualTo(IncludeMembers.Public);
		await That(result.CustomOptions).ContainsKey(typeof(MyClass))
			.WhoseValue.IsEquivalentTo(new
			{
				Fields = fieldsToInclude,
			});
	}

	[Theory]
	[InlineData(IncludeMembers.None)]
	[InlineData(IncludeMembers.Public)]
	[InlineData(IncludeMembers.Internal)]
	[InlineData(IncludeMembers.Private)]
	public async Task Generic_For_IncludingProperties_ShouldSetOptionForType(IncludeMembers propertiesToInclude)
	{
		EquivalencyOptions options = new();

		EquivalencyOptions result = options.For<MyClass>(o => o.IncludingProperties(propertiesToInclude));

		await That(result.Properties).IsEqualTo(IncludeMembers.Public);
		await That(result.CustomOptions).ContainsKey(typeof(MyClass))
			.WhoseValue.IsEquivalentTo(new
			{
				Properties = propertiesToInclude,
			});
	}

	private class MyClass;
}
