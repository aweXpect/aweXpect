using Microsoft.CodeAnalysis;

namespace aweXpect.SourceGenerators;

internal readonly record struct ExpectationToGenerate
{
	public ExpectationToGenerate(string @namespace,
		string className,
		INamedTypeSymbol targetType,
		string name,
		string outcomeMethod,
		string expectationText,
		string[] usings,
		string? remarks)
	{
		Namespace = @namespace;
		ClassName = className;
		TargetType = targetType;
		Name = name.Replace("{Not}", "");
		NegatedName = name.Replace("{Not}", "Not");
		IncludeNegated = name.Contains("{Not}");
		OutcomeMethod = outcomeMethod;
		ExpectationText = expectationText.Replace("{not}", "").Replace("  ", " ");
		NegatedExpectationText = expectationText.Replace("{not}", " not ").Replace("  ", " ");
		Remarks = remarks;
		Usings = usings;
		FileName = $"{ClassName}.{Name}.g.cs";
	}

	public string[] Usings { get; }
	public string FileName { get; }
	public bool IncludeNegated { get; }
	public string NegatedName { get; }
	public string Namespace { get; }
	public string ClassName { get; }
	public INamedTypeSymbol TargetType { get; }
	public string Name { get; }
	public string OutcomeMethod { get; }
	public string ExpectationText { get; }
	public string NegatedExpectationText { get; }
	public string? Remarks { get; }

	public string AppendRemarks()
	{
		if (string.IsNullOrEmpty(Remarks))
		{
			return "";
		}

		return $$"""

		         /// <remarks>
		         ///     {{Remarks!.Replace("\n", "\n///     ")}}
		         /// </remarks>
		         """.Replace("\n", "\n\t");
	}
}
