using System.Collections.Immutable;
using aweXpect.Analyzers.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace aweXpect.Analyzers;

/// <summary>
///     An analyzer that checks that all <c>Expect.That</c> expectations are awaited.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class EqualsAnalyzer : DiagnosticAnalyzer
{
	/// <inheritdoc />
	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [Rules.EqualsRule,];

	/// <inheritdoc />
	public override void Initialize(AnalysisContext context)
	{
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.EnableConcurrentExecution();

		context.RegisterOperationAction(AnalyzeOperation, OperationKind.Invocation);
	}


	private static void AnalyzeOperation(OperationAnalysisContext context)
	{
		if (context.Operation is IInvocationOperation invocationOperation &&
		    invocationOperation.TargetMethod.Name == nameof(object.Equals))
		{
			IMethodSymbol methodSymbol = invocationOperation.TargetMethod;
			if (methodSymbol.MatchesFullName("aweXpect", "Core", "IThat", "Equals"))
			{
				context.ReportDiagnostic(
					Diagnostic.Create(Rules.EqualsRule, context.Operation.Syntax.GetLocation())
				);
			}
		}
	}
}
