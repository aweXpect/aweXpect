using Microsoft.CodeAnalysis;

namespace aweXpect.SourceGenerators.Helpers;

internal readonly record struct ExpectationToGenerate
{
	public ExpectationToGenerate(string @namespace,
		string className,
		INamedTypeSymbol targetType,
		string name,
		string outcomeMethod,
		AttributeData attributeData)
	{
		Namespace = @namespace;
		ClassName = className;
		TargetType = targetType.ToDisplayString();
		Name = name.Replace("{Not}", "");
		NegatedName = name.Replace("{Not}", "Not");
		IncludeNegated = name.Contains("{Not}");
		OutcomeMethod = outcomeMethod;

		string expectationText = outcomeMethod;
		foreach (KeyValuePair<string, TypedConstant> namedArgument in attributeData.NamedArguments)
		{
			switch (namedArgument.Key)
			{
				case "ExpectationText":
					expectationText = namedArgument.Value.Value?.ToString() ?? expectationText;
					break;
				case "Remarks":
					Remarks = namedArgument.Value.Value?.ToString();
					break;
				case "Using":
					Usings =
						namedArgument.Value.Values.Select(x => x.Value?.ToString()).Where(x => x != null).ToArray()!;
					break;
			}
		}

		IsNullable = attributeData.AttributeClass!.Name ==
		             nameof(SourceGenerationHelper.CreateExpectationOnNullableAttribute);
		if (IsNullable)
		{
			TargetType += "?";
		}

		ExpectationText = expectationText.Replace("{not}", "").Replace("  ", " ");
		NegatedExpectationText = expectationText.Replace("{not}", " not ").Replace("  ", " ");
		FileName = $"{ClassName}.{Name}.g.cs";
	}

	public string[] Usings { get; } = [];
	public string FileName { get; }
	public bool IncludeNegated { get; }
	public bool IsNullable { get; }
	public string NegatedName { get; }
	public string Namespace { get; }
	public string ClassName { get; }
	public string TargetType { get; }
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
