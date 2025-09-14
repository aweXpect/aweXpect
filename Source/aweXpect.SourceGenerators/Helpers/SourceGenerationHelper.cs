namespace aweXpect.SourceGenerators.Helpers;

internal static class SourceGenerationHelper
{
	public const string CreateExpectationOnAttribute =
		"""
		using System;

		namespace aweXpect.SourceGenerators;

		#nullable enable
		/// <summary>
		/// Create an assertion on the <typeparamref name="TTarget"/> attribute.
		/// </summary>
		/// <typeparam name="TTarget">The target type for the assertion</typeparam>
		[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
		internal class CreateExpectationOnAttribute<TTarget> : System.Attribute
		{
			public CreateExpectationOnAttribute(string name, string outcomeMethod)
			{
				TargetType = typeof(TTarget);
				PositiveName = name.Replace("{Not}", "");
				if (name.Contains("{Not}"))
				{
					NegativeName = name.Replace("{Not}", "Not");
				}
				OutcomeMethod = outcomeMethod;
			}
			
			public CreateExpectationOnAttribute(string positiveName, string negativeName, string outcomeMethod)
			{
				TargetType = typeof(TTarget);
				PositiveName = positiveName;
				NegativeName = negativeName;
				OutcomeMethod = outcomeMethod;
			}

			public Type TargetType { get; }
			public string PositiveName { get; }
			public string? NegativeName { get; }
			public string OutcomeMethod { get; set; }
			public string? ExpectationText { get; set; }
			public string? PositiveExpectationText { get; set; }
			public string? NegativeExpectationText { get; set; }
			public string? Remarks { get; set; }
			public string[] Using { get; set; } = [];
		}
		#nullable disable
		""";

	public const string CreateExpectationOnNullableAttribute =
		"""
		using System;

		namespace aweXpect.SourceGenerators;

		#nullable enable
		/// <summary>
		/// Create an assertion on the <typeparamref name="TTarget"/> attribute.
		/// </summary>
		/// <typeparam name="TTarget">The target type for the assertion</typeparam>
		[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
		internal class CreateExpectationOnNullableAttribute<TTarget> : System.Attribute
		{
			public CreateExpectationOnNullableAttribute(string name, string outcomeMethod)
			{
				TargetType = typeof(TTarget);
				PositiveName = name.Replace("{Not}", "");
				if (name.Contains("{Not}"))
				{
					NegativeName = name.Replace("{Not}", "Not");
				}
				OutcomeMethod = outcomeMethod;
			}
			
			public CreateExpectationOnNullableAttribute(string positiveName, string negativeName, string outcomeMethod)
			{
				TargetType = typeof(TTarget);
				PositiveName = positiveName;
				NegativeName = negativeName;
				OutcomeMethod = outcomeMethod;
			}
			
			public Type TargetType { get; }
			public string PositiveName { get; }
			public string? NegativeName { get; }
			public string OutcomeMethod { get; set; }
			public string? ExpectationText { get; set; }
			public string? PositiveExpectationText { get; set; }
			public string? NegativeExpectationText { get; set; }
			public string? Remarks { get; set; }
			public string[] Using { get; set; } = [];
		}
		#nullable disable
		""";

	public static string GenerateExtensionClass(ExpectationToGenerate expectationToGenerate)
	{
		string result = $$"""
		                  {{string.Join("\n", expectationToGenerate.Usings.Select(x => $"using {x};"))}}
		                  using aweXpect.Core;
		                  using aweXpect.Core.Constraints;
		                  using aweXpect.Helpers;
		                  using aweXpect.Results;

		                  namespace {{expectationToGenerate.Namespace}};

		                  #nullable enable
		                  public static partial class {{expectationToGenerate.ClassName}}
		                  {
		                  	/// <summary>
		                  	///     Verifies that the subject {{expectationToGenerate.ExpectationText}}.
		                  	/// </summary>{{expectationToGenerate.AppendRemarks()}}
		                  	public static AndOrResult<{{expectationToGenerate.TargetType}}, IThat<{{expectationToGenerate.TargetType}}>> {{expectationToGenerate.Name}}(this IThat<{{expectationToGenerate.TargetType}}> source)
		                  		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
		                  			new {{expectationToGenerate.Name}}Constraint(it, grammars)),
		                  		source);


		                  """;
		if (expectationToGenerate.IncludeNegated)
		{
			result += $$"""
			            	/// <summary>
			            	///     Verifies that the subject {{expectationToGenerate.NegatedExpectationText}}.
			            	/// </summary>{{expectationToGenerate.AppendRemarks()}}
			            	public static AndOrResult<{{expectationToGenerate.TargetType}}, IThat<{{expectationToGenerate.TargetType}}>> {{expectationToGenerate.NegatedName}}(this IThat<{{expectationToGenerate.TargetType}}> source)
			            		=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
			            			new {{expectationToGenerate.Name}}Constraint(it, grammars).Invert()),
			            		source);


			            """;
		}

		if (expectationToGenerate.IsNullable)
		{
			result += $$"""
			            	private sealed class {{expectationToGenerate.Name}}Constraint(string it, ExpectationGrammars grammars)
			            		: ConstraintResult.WithNotNullValue<{{expectationToGenerate.TargetType}}>(it, grammars),
			            			IValueConstraint<{{expectationToGenerate.TargetType}}>
			            	{
			            		public ConstraintResult IsMetBy({{expectationToGenerate.TargetType}} actual)
			            		{
			            			Actual = actual;
			            			Outcome = actual is not null && {{expectationToGenerate.OutcomeMethod.Replace("{value}", "actual")}} ? Outcome.Success : Outcome.Failure;
			            			return this;
			            		}
			            	
			            		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			            			=> stringBuilder.Append("{{expectationToGenerate.ExpectationText}}");
			            	
			            		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			            		{
			            			stringBuilder.Append(It).Append(" was ");
			            			Formatter.Format(stringBuilder, Actual);
			            		}
			            	
			            		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			            			=> stringBuilder.Append("{{expectationToGenerate.NegatedExpectationText}}");
			            	
			            		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			            			=> AppendNormalResult(stringBuilder, indentation);
			            	}
			            """;
		}
		else
		{
			result += $$"""
			            	private sealed class {{expectationToGenerate.Name}}Constraint(string it, ExpectationGrammars grammars)
			            		: ConstraintResult.WithValue<{{expectationToGenerate.TargetType}}>(grammars),
			            			IValueConstraint<{{expectationToGenerate.TargetType}}>
			            	{
			            		public ConstraintResult IsMetBy({{expectationToGenerate.TargetType}} actual)
			            		{
			            			Actual = actual;
			            			Outcome = {{expectationToGenerate.OutcomeMethod.Replace("{value}", "actual")}} ? Outcome.Success : Outcome.Failure;
			            			return this;
			            		}
			            	
			            		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			            			=> stringBuilder.Append("{{expectationToGenerate.ExpectationText}}");
			            	
			            		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			            		{
			            			stringBuilder.Append(it).Append(" was ");
			            			Formatter.Format(stringBuilder, Actual);
			            		}
			            	
			            		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			            			=> stringBuilder.Append("{{expectationToGenerate.NegatedExpectationText}}");
			            	
			            		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			            			=> AppendNormalResult(stringBuilder, indentation);
			            	}
			            """;
		}

		result += """
		          }
		          #nullable disable
		          """;
		return result.TrimStart();
	}
}
