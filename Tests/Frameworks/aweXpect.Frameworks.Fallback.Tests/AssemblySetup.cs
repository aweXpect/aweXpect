using aweXpect.Customization;
using NUnit.Framework;

namespace aweXpect.Frameworks.Fallback.Tests;

[SetUpFixture]
public class AssemblySetup
{
	private CustomizationLifetime? _customizer;

	[OneTimeSetUp]
	public void RunBeforeAnyTests() => _customizer = Customize.aweXpect.Reflection().ExcludedAssemblyPrefixes.Set([
		..Customize.aweXpect.Reflection().ExcludedAssemblyPrefixes.Get(),
		"aweXpect.Core"
	]);

	[OneTimeTearDown]
	public void RunAfterAnyTests() => _customizer?.Dispose();
}
