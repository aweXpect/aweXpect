using Microsoft.CodeAnalysis;

namespace aweXpect.Analyzers;

internal static class Rules
{
	private const string UsageCategory = "Usage";

	public static readonly DiagnosticDescriptor AwaitExpectation =
		CreateDescriptor("aweXpect0001", UsageCategory, DiagnosticSeverity.Error);


	private static DiagnosticDescriptor CreateDescriptor(string diagnosticId, string category,
		DiagnosticSeverity severity) => new(
		diagnosticId,
		new LocalizableResourceString(diagnosticId + "Title",
			Resources.ResourceManager, typeof(Resources)),
		new LocalizableResourceString(diagnosticId + "MessageFormat", Resources.ResourceManager,
			typeof(Resources)),
		category,
		severity,
		true,
		new LocalizableResourceString(diagnosticId + "Description", Resources.ResourceManager,
			typeof(Resources))
	);
}
