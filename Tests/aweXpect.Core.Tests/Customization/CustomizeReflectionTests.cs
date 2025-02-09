using aweXpect.Customization;

namespace aweXpect.Core.Tests.Customization;

public sealed class CustomizeReflectionTests
{
	[Fact]
	public async Task ExcludeAssemblies_ShouldChangeTheExcludedAssemblyPrefixes()
	{
		string additionalExcludedAssemblyNamespace = "foo";

		await That(Customize.aweXpect.Reflection().ExcludedAssemblyPrefixes.Get())
			.DoesNotContain(additionalExcludedAssemblyNamespace);

		using (IDisposable _ = Customize.aweXpect.Reflection().Update(p => p with
		       {
			       ExcludedAssemblyPrefixes = [..p.ExcludedAssemblyPrefixes, additionalExcludedAssemblyNamespace],
		       }))
		{
			await That(Customize.aweXpect.Reflection().ExcludedAssemblyPrefixes.Get())
				.Contains(additionalExcludedAssemblyNamespace);
		}

		await That(Customize.aweXpect.Reflection().ExcludedAssemblyPrefixes.Get())
			.DoesNotContain(additionalExcludedAssemblyNamespace);
	}
}
