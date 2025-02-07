using aweXpect.Equivalency;

namespace aweXpect.Internal.Tests.Equivalency;

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
		await That(result.CustomOptions).ContainsKey(typeof(MyClass));
		await That(result.CustomOptions[typeof(MyClass)].IgnoreCollectionOrder).IsEqualTo(ignoreCollectionOrder);
	}

	[Theory]
	[AutoData]
	public async Task Generic_For_IgnoringMember_ShouldSetOptionForType(string memberToIgnore)
	{
		EquivalencyOptions options = new();

		EquivalencyOptions result = options.For<MyClass>(o => o.IgnoringMember(memberToIgnore));

		await That(result.MembersToIgnore).IsEmpty();
		await That(result.CustomOptions).ContainsKey(typeof(MyClass));
		await That(result.CustomOptions[typeof(MyClass)].MembersToIgnore).HasSingle().Which.IsEqualTo(memberToIgnore);
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
		await That(result.CustomOptions).ContainsKey(typeof(MyClass));
		await That(result.CustomOptions[typeof(MyClass)].Fields).IsEqualTo(fieldsToInclude);
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
		await That(result.CustomOptions).ContainsKey(typeof(MyClass));
		await That(result.CustomOptions[typeof(MyClass)].Properties).IsEqualTo(propertiesToInclude);
	}

	private class MyClass;
}
