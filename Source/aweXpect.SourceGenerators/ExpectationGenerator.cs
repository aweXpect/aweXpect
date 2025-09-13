using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace aweXpect.SourceGenerators;

/// <summary>
///     The <see cref="IIncrementalGenerator" /> for simple expectations.
/// </summary>
[Generator]
public class ExpectationGenerator : IIncrementalGenerator
{
	void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
	{
		// Add the marker attribute to the compilation
		context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
			"CreateExpectationOnAttribute.g.cs",
			SourceText.From(SourceGenerationHelper.CreateExpectationOnAttribute, Encoding.UTF8)));

		HashSet<string> files = new();
		IncrementalValuesProvider<ExpectationToGenerate> expectationsToGenerate = context.SyntaxProvider
			.CreateSyntaxProvider(
				static (s, _) => IsSyntaxTargetForGeneration(s),
				(ctx, _) => GetSemanticTargetForGeneration(ctx, files))
			.Where(static m => m is not null)
			.SelectMany((x, _) => x!.ToImmutableArray());

		context.RegisterSourceOutput(expectationsToGenerate,
			static (spc, source) => Execute(source, spc));
	}

	private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
		=> node is ClassDeclarationSyntax { AttributeLists.Count: > 0, };

	private static IEnumerable<ExpectationToGenerate> GetSemanticTargetForGeneration(GeneratorSyntaxContext context,
		HashSet<string> files)
	{
		// we know the node is a ClassDeclarationSyntax thanks to IsSyntaxTargetForGeneration
		ClassDeclarationSyntax classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

		SemanticModel semanticModel = context.SemanticModel;
		if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
		{
			yield break;
		}

		foreach (AttributeData? attributeData in classSymbol.GetAttributes())
		{
			INamedTypeSymbol? attributeClass = attributeData.AttributeClass;
			if (attributeClass == null || !attributeClass.IsGenericType ||
			    attributeClass.Name != "CreateExpectationOnAttribute")
			{
				continue;
			}

			// Extract the target type from the generic type argument
			INamedTypeSymbol? targetType = attributeClass.TypeArguments[0] as INamedTypeSymbol;
			if (targetType == null)
			{
				continue;
			}

			ExpectationToGenerate? expectationToGenerate = GetExpectationToGenerate(classSymbol,
				attributeData);
			if (expectationToGenerate != null &&
			    files.Add(expectationToGenerate.Value.FileName))
			{
				yield return expectationToGenerate.Value;
			}
		}
	}

	private static void Execute(ExpectationToGenerate expectationToGenerate, SourceProductionContext context)
	{
		string result = SourceGenerationHelper.GenerateExtensionClass(expectationToGenerate);
		// Create a separate partial class file for each enum
		context.AddSource(expectationToGenerate.FileName, SourceText.From(result, Encoding.UTF8));
	}

	private static ExpectationToGenerate? GetExpectationToGenerate(INamedTypeSymbol classSymbol,
		AttributeData attributeData)
	{
		string containingNamespace = classSymbol.ContainingNamespace.ToString();
		if (containingNamespace is null)
		{
			return null;
		}

		INamedTypeSymbol? targetType = attributeData.AttributeClass?.TypeArguments[0] as INamedTypeSymbol;
		if (targetType == null)
		{
			return null;
		}

		string? outcomeMethod = null;
		string? name = null;
		if (attributeData.ConstructorArguments.Length == 2)
		{
			name = attributeData.ConstructorArguments[0].Value?.ToString();
			outcomeMethod = attributeData.ConstructorArguments[1].Value?.ToString();
		}

		if (outcomeMethod == null || name == null)
		{
			return null;
		}

		if (targetType.TypeKind == TypeKind.Error)
		{
			return null;
		}

		string expectationText = outcomeMethod;
		string? remarks = null;
		string[] usings = [];
		foreach (KeyValuePair<string, TypedConstant> namedArgument in attributeData.NamedArguments)
		{
			switch (namedArgument.Key)
			{
				case "ExpectationText":
					expectationText = namedArgument.Value.Value?.ToString() ?? expectationText;
					break;
				case "Remarks":
					remarks = namedArgument.Value.Value?.ToString();
					break;
				case "Using":
					usings =
						namedArgument.Value.Values.Select(x => x.Value?.ToString()).Where(x => x != null).ToArray()!;
					break;
			}
		}

		return new ExpectationToGenerate(containingNamespace, classSymbol.Name, targetType, name, outcomeMethod,
			expectationText, usings, remarks);
	}
}
