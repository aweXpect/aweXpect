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
		Diagnostic? diagnostic = context.Diagnostics.Single();

		TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;

		SyntaxNode? root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

		SyntaxNode? diagnosticNode = root?.FindNode(diagnosticSpan);

		if (diagnosticNode is not InvocationExpressionSyntax invocationExpressionSyntax)
		{
			return;
		}

		context.RegisterCodeFix(
			CodeAction.Create(
				Resources.aweXpect0001CodeFixTitle,
				c => AwaitAssertionAsync(context.Document, invocationExpressionSyntax, c),
				nameof(Resources.aweXpect0001CodeFixTitle)),
			diagnostic);
	}

	/// <summary>
	///     Executed on the quick fix action raised by the user.
	/// </summary>
	/// <param name="document">Affected source file.</param>
	/// <param name="invocationExpressionSyntax">Highlighted class declaration Syntax Node.</param>
	/// <param name="cancellationToken">Any fix is cancellable by the user, so we should support the cancellation token.</param>
	private async Task<Document> AwaitAssertionAsync(Document document,
		InvocationExpressionSyntax invocationExpressionSyntax, CancellationToken cancellationToken)
	{
		DocumentEditor? editor = await DocumentEditor.CreateAsync(document, cancellationToken);

		SyntaxNode? parent = invocationExpressionSyntax;
		while (parent != null)
		{
			if (parent is ExpressionStatementSyntax expressionStatement)
			{
				AwaitExpressionSyntax? awaitExpressionSyntax =
					SyntaxFactory.AwaitExpression(expressionStatement.Expression);

				editor.ReplaceNode(expressionStatement.Expression, awaitExpressionSyntax);

				return editor.GetChangedDocument();
			}

			parent = parent.Parent;
		}
		return document;
	}
}
