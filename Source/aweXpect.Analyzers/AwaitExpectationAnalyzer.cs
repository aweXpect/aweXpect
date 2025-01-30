using System.Collections.Immutable;
using aweXpect.Analyzers.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace aweXpect.Analyzers;

/// <summary>
///     An analyzer that checks that all <c>Expect.That</c> expectations are awaited.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class AwaitExpectationAnalyzer : DiagnosticAnalyzer
{
	/// <inheritdoc />
	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [Rules.AwaitExpectation];

	/// <inheritdoc />
	public override void Initialize(AnalysisContext context)
	{
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.EnableConcurrentExecution();

		context.RegisterOperationAction(AnalyzeOperation, OperationKind.Invocation);
	}


	private static void AnalyzeOperation(OperationAnalysisContext context)
	{
		if (context.Operation is not IInvocationOperation invocationOperation)
		{
			return;
		}

		IMethodSymbol methodSymbol = invocationOperation.TargetMethod;
		if (methodSymbol.MatchesFullName("aweXpect", "Expect", "That"))
		{
			CheckExpectThatInvocation(context, invocationOperation);
		}
	}

	private static void CheckExpectThatInvocation(OperationAnalysisContext context,
		IInvocationOperation invocationOperation)
	{
		if (IsAwaitedOrVerifyCalled(invocationOperation))
		{
			return;
		}

		context.ReportDiagnostic(
			Diagnostic.Create(Rules.AwaitExpectation, context.Operation.Syntax.GetLocation())
		);
	}

	private static bool IsAwaitedOrVerifyCalled(IInvocationOperation invocationOperation)
	{
		IOperation? parent = invocationOperation.Parent;

		while (parent != null)
		{
			if (parent is IBlockOperation or IDelegateCreationOperation)
			{
				SemanticModel? semanticModel = parent.SemanticModel;
				if (semanticModel == null)
				{
					return false;
				}

				ExpressionSyntaxWalker walker = new(semanticModel);
				walker.Visit(parent.Syntax);
				return walker.IsVerifyCalled;
			}

			if (parent is IAwaitOperation)
			{
				return true;
			}

			parent = parent.Parent;
		}

		return false;
	}

	private sealed class ExpressionSyntaxWalker(SemanticModel semanticModel) : SyntaxWalker
	{
		public bool IsVerifyCalled { get; private set; }

		public override void Visit(SyntaxNode node)
		{
			if (IsVerifyCalled)
			{
				return;
			}

			if (IsVerifyMatch(semanticModel, node, 0) ||
			    (node is MemberAccessExpressionSyntax memberAccessExpressionSyntax &&
			     IsVerifyMatch(semanticModel, memberAccessExpressionSyntax.Name, 1)))
			{
				IsVerifyCalled = true;
			}

			base.Visit(node);

			static bool IsVerifyMatch(SemanticModel semanticModel, SyntaxNode syntaxNode, int parameterCount)
				=> syntaxNode is IdentifierNameSyntax nameSyntax &&
				   semanticModel.GetSymbolInfo(nameSyntax).Symbol is IMethodSymbol methodSymbol &&
				   methodSymbol.MatchesFullName("aweXpect", "Synchronous", "Synchronously", "Verify") &&
				   methodSymbol.Parameters.Length == parameterCount;
		}
	}
}
