using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Text;

namespace aweXpect.Analyzers.CodeFixers;

/// <summary>
///     A code fix provider that makes all <c>Expect.That</c> methods that are neither async nor verified async.
/// </summary>
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AwaitExpectationCodeFixProvider))]
[Shared]
public class AwaitExpectationCodeFixProvider : CodeFixProvider
{
	/// <inheritdoc />
	public sealed override ImmutableArray<string> FixableDiagnosticIds { get; } = [Rules.AwaitExpectation.Id];

	/// <inheritdoc />
	public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

	/// <inheritdoc />
	public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
	{
		foreach (Diagnostic? diagnostic in context.Diagnostics)
		{
			TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;

			SyntaxNode? root =
				await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

			SyntaxNode? diagnosticNode = root?.FindNode(diagnosticSpan);

			if (diagnosticNode is not ExpressionSyntax expressionSyntax)
			{
				return;
			}

			ExpressionSyntax? upperMostExpression =
				expressionSyntax.AncestorsAndSelf().OfType<ExpressionSyntax>().Last();

			context.RegisterCodeFix(
				CodeAction.Create(
					Resources.aweXpect0001CodeFixTitle,
					c => AwaitAssertionAsync(context.Document, upperMostExpression, c),
					nameof(Resources.aweXpect0001CodeFixTitle)),
				diagnostic);
		}
	}

	/// <summary>
	///     Executed on the quick fix action raised by the user.
	/// </summary>
	/// <param name="document">Affected source file.</param>
	/// <param name="expressionSyntax">Highlighted class declaration Syntax Node.</param>
	/// <param name="cancellationToken">Any fix is cancellable by the user, so we should support the cancellation token.</param>
	private static async Task<Document> AwaitAssertionAsync(Document document, ExpressionSyntax expressionSyntax,
		CancellationToken cancellationToken)
	{
		DocumentEditor? editor = await DocumentEditor.CreateAsync(document, cancellationToken).ConfigureAwait(false);

		// Add await to the invocation expression
		AwaitExpressionSyntax? awaitExpression = SyntaxFactory
			.AwaitExpression(expressionSyntax.WithLeadingTrivia(SyntaxFactory.Space))
			.WithLeadingTrivia(expressionSyntax.GetLeadingTrivia());

		// Find the containing method
		MethodDeclarationSyntax? methodDeclaration = expressionSyntax.AncestorsAndSelf()
			.OfType<MethodDeclarationSyntax>()
			.FirstOrDefault();

		if (methodDeclaration == null)
		{
			return editor.GetChangedDocument();
		}

		SyntaxTokenList modifiers = methodDeclaration.Modifiers;

		TypeSyntax? returnType = methodDeclaration.ReturnType;
		TypeSyntax? newReturnType = returnType;

		// Check if the method is already async
		if (!methodDeclaration.Modifiers.Any(SyntaxKind.AsyncKeyword))
		{
			// Add async modifier
			SyntaxToken asyncModifier = SyntaxFactory.Token(SyntaxKind.AsyncKeyword);
			modifiers = methodDeclaration.Modifiers.Add(asyncModifier
				.WithTrailingTrivia(SyntaxFactory.Space));

			// Update the return type to Task or Task<T>
			if (returnType is PredefinedTypeSyntax predefinedType &&
			    predefinedType.Keyword.IsKind(SyntaxKind.VoidKeyword))
			{
				newReturnType = SyntaxFactory.IdentifierName("Task")
					.WithTrailingTrivia(SyntaxFactory.Space);
			}
			else if (returnType is not GenericNameSyntax genericName || genericName.Identifier.Text != "Task")
			{
				newReturnType = SyntaxFactory.ParseTypeName($"Task<{returnType}>")
					.WithTrailingTrivia(SyntaxFactory.Space);
			}
		}

		MethodDeclarationSyntax? newMethodDeclaration = methodDeclaration
			.ReplaceNode(expressionSyntax, awaitExpression)
			.WithModifiers(modifiers)
			.WithReturnType(newReturnType)
			.WithAdditionalAnnotations(Formatter.Annotation);

		editor.ReplaceNode(methodDeclaration, newMethodDeclaration);

		return editor.GetChangedDocument();
	}
}
