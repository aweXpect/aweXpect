using Microsoft.CodeAnalysis;

namespace aweXpect.Analyzers.Helpers;

internal static class MethodSymbolExtensions
{
	public static bool MatchesFullName(this IMethodSymbol methodSymbol,
		string @namespace,
		string typeName,
		string name)
		=> methodSymbol.ContainingNamespace?.ContainingNamespace?.IsGlobalNamespace == true &&
		   methodSymbol.ContainingNamespace?.Name == @namespace &&
		   methodSymbol.ContainingType?.Name == typeName &&
		   methodSymbol.Name == name;

	public static bool MatchesFullName(this IMethodSymbol methodSymbol,
		string namespace1, string namespace2,
		string typeName,
		string name)
		=> methodSymbol.ContainingNamespace?.ContainingNamespace?.ContainingNamespace?.IsGlobalNamespace == true &&
		   methodSymbol.ContainingNamespace?.ContainingNamespace?.Name == namespace1 &&
		   methodSymbol.ContainingNamespace?.Name == namespace2 &&
		   methodSymbol.ContainingType?.Name == typeName &&
		   methodSymbol.Name == name;
}
