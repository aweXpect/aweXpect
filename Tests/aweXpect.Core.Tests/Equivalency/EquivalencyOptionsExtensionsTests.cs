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

		if (ignoreCollectionOrder)
		{
			await That(result.ToString()).IsEqualTo("""
			                                         - include public fields and properties
			                                         - for EquivalencyOptionsExtensionsTests.MyClass:
			                                           - include public fields and properties
			                                           - ignore collection order
			                                        """);
		}
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
		await That(result.ToString()).IsEqualTo($"""
		                                          - include public fields and properties
		                                          - for EquivalencyOptionsExtensionsTests.MyClass:
		                                            - include public fields and properties
		                                            - ignore members: ["{memberToIgnore}"]
		                                         """);
	}

	[Theory]
	[InlineData(IncludeMembers.None)]
	[InlineData(IncludeMembers.Internal)]
	[InlineData(IncludeMembers.Private)]
	public async Task Generic_For_IncludingFields_ShouldSetOptionForType(IncludeMembers fieldsToInclude)
	{
		EquivalencyOptions options = new();
		string expectedVisibility = fieldsToInclude switch
		{
			IncludeMembers.Public => "public",
			IncludeMembers.Internal => "internal",
			IncludeMembers.Private => "private",
			_ => "no",
		};

		EquivalencyOptions result = options.For<MyClass>(o => o.IncludingFields(fieldsToInclude));

		await That(result.Fields).IsEqualTo(IncludeMembers.Public);
		await That(result.CustomOptions).ContainsKey(typeof(MyClass))
			.WhoseValue.IsEquivalentTo(new
			{
				Fields = fieldsToInclude,
			});
		await That(result.ToString()).IsEqualTo($"""
		                                          - include public fields and properties
		                                          - for EquivalencyOptionsExtensionsTests.MyClass:
		                                            - include {expectedVisibility} fields and public properties
		                                         """);
	}

	[Theory]
	[InlineData(IncludeMembers.None)]
	[InlineData(IncludeMembers.Internal)]
	[InlineData(IncludeMembers.Private)]
	public async Task Generic_For_IncludingProperties_ShouldSetOptionForType(IncludeMembers propertiesToInclude)
	{
		EquivalencyOptions options = new();
		string expectedVisibility = propertiesToInclude switch
		{
			IncludeMembers.Public => "public",
			IncludeMembers.Internal => "internal",
			IncludeMembers.Private => "private",
			_ => "no",
		};

		EquivalencyOptions result = options.For<MyClass>(o => o.IncludingProperties(propertiesToInclude));

		await That(result.Properties).IsEqualTo(IncludeMembers.Public);
		await That(result.CustomOptions).ContainsKey(typeof(MyClass))
			.WhoseValue.IsEquivalentTo(new
			{
				Properties = propertiesToInclude,
			});
		await That(result.ToString()).IsEqualTo($"""
		                                          - include public fields and properties
		                                          - for EquivalencyOptionsExtensionsTests.MyClass:
		                                            - include public fields and {expectedVisibility} properties
		                                         """);
	}

	private sealed class MyClass;
}
