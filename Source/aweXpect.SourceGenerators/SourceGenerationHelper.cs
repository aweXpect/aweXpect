namespace aweXpect.SourceGenerators;

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
		    public CreateExpectationOnAttribute(string methodName, string name)
		    {
		        TargetType = typeof(TTarget);
		        MethodName = methodName;
		        Name = name;
		    }

		    public Type TargetType { get; }
		    public string MethodName { get; }
		    public string Name { get; set; }
		    public string? ExpectationText { get; set; }
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
		                      public static AndOrResult<{{expectationToGenerate.TargetType.ToDisplayString()}}, IThat<{{expectationToGenerate.TargetType.ToDisplayString()}}>> {{expectationToGenerate.Name}}(this IThat<{{expectationToGenerate.TargetType.ToDisplayString()}}> source)
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
			                public static AndOrResult<{{expectationToGenerate.TargetType.ToDisplayString()}}, IThat<{{expectationToGenerate.TargetType.ToDisplayString()}}>> {{expectationToGenerate.NegatedName}}(this IThat<{{expectationToGenerate.TargetType.ToDisplayString()}}> source)
			                	=> new(source.Get().ExpectationBuilder.AddConstraint((it, grammars) =>
			                			new {{expectationToGenerate.Name}}Constraint(it, grammars).Invert()),
			                		source);
			            
			            
			            """;
		}

		result += $$"""
		            	private sealed class {{expectationToGenerate.Name}}Constraint(string it, ExpectationGrammars grammars)
		            		: ConstraintResult.WithValue<{{expectationToGenerate.TargetType.ToDisplayString()}}>(grammars),
		            			IValueConstraint<{{expectationToGenerate.TargetType.ToDisplayString()}}>
		            	{
		            		public ConstraintResult IsMetBy({{expectationToGenerate.TargetType.ToDisplayString()}} actual)
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
		            }
		            #nullable disable
		            """;
		return result.TrimStart();
	}
}
