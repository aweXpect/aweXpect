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
///     A sample code fix provider that renames classes with the company name in their definition.
///     All code fixes must  be linked to specific analyzers.
/// </summary>
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AwaitAssertionCodeFixProvider))]
[Shared]
public class AwaitAssertionCodeFixProvider : CodeFixProvider
{
	/// <inheritdoc />
	public sealed override ImmutableArray<string> FixableDiagnosticIds { get; } =
		[Rules.AwaitExpectation.Id];

	/// <inheritdoc />
	public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

	/// <inheritdoc />
	public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
	{
		Diagnostic? diagnostic = context.Diagnostics.Single();

		TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;

		SyntaxNode? root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

		SyntaxNode? diagnosticNode = root?.FindNode(diagnosticSpan);

		if (diagnosticNode is not ExpressionStatementSyntax expressionStatementSyntax)
		{
			return;
		}

		context.RegisterCodeFix(
			CodeAction.Create(
				Resources.aweXpect0001CodeFixTitle,
				c => AwaitAssertionAsync(context.Document, expressionStatementSyntax, c),
				nameof(Resources.aweXpect0001CodeFixTitle)),
			diagnostic);
	}

	/// <summary>
	///     Executed on the quick fix action raised by the user.
	/// </summary>
	/// <param name="document">Affected source file.</param>
	/// <param name="expressionStatementSyntax">Highlighted class declaration Syntax Node.</param>
	/// <param name="cancellationToken">Any fix is cancellable by the user, so we should support the cancellation token.</param>
	/// <returns>Clone of the solution with updates: renamed class.</returns>
	private async Task<Document> AwaitAssertionAsync(Document document,
		ExpressionStatementSyntax expressionStatementSyntax, CancellationToken cancellationToken)
	{
		DocumentEditor? editor = await DocumentEditor.CreateAsync(document, cancellationToken);

		AwaitExpressionSyntax? awaitExpressionSyntax =
			SyntaxFactory.AwaitExpression(expressionStatementSyntax.Expression);

		editor.ReplaceNode(expressionStatementSyntax.Expression, awaitExpressionSyntax);

		return editor.GetChangedDocument();
	}
}
