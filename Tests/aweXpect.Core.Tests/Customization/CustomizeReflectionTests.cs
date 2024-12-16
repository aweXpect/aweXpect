using aweXpect.Customization;

namespace aweXpect.Core.Tests.Customization;

public sealed class CustomizeReflectionTests
{
	[Fact]
	public async Task ExcludeAssemblies_ShouldChangeTheExcludedAssemblyPrefixes()
	{
		string additionalExcludedAssemblyNamespace = "foo";

		await That(Customize.Reflection.ExcludedAssemblyPrefixes).Should()
			.NotContain(additionalExcludedAssemblyNamespace);

		using (IDisposable _ = Customize.Reflection.ExcludeAssemblies(l => l.Add(additionalExcludedAssemblyNamespace)))
		{
			await That(Customize.Reflection.ExcludedAssemblyPrefixes).Should()
				.Contain(additionalExcludedAssemblyNamespace);
		}

		await That(Customize.Reflection.ExcludedAssemblyPrefixes).Should()
			.NotContain(additionalExcludedAssemblyNamespace);
	}
}
