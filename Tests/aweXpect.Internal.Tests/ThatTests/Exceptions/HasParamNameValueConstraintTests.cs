using System.Text;
using aweXpect.Core;

namespace aweXpect.Internal.Tests.ThatTests.Exceptions;

public class HasParamNameValueConstraintTests
{
	[Theory]
	[InlineData(ExpectationGrammars.None, "has ParamName \"foo\"")]
	[InlineData(ExpectationGrammars.Negated, "does not have ParamName \"foo\"")]
	[InlineData(ExpectationGrammars.Nested, "ParamName is \"foo\"")]
	[InlineData(ExpectationGrammars.Nested | ExpectationGrammars.Negated, "ParamName is not \"foo\"")]
	[InlineData(ExpectationGrammars.Active, "with ParamName \"foo\"")]
	[InlineData(ExpectationGrammars.Active | ExpectationGrammars.Negated, "without ParamName \"foo\"")]
	public async Task AppendExpectation_WithExpected_ShouldAppendExpectedText(ExpectationGrammars grammar,
		string expected)
	{
		ThatException.HasParamNameValueConstraint<ArgumentException> sut = new("it", grammar, "foo");
		StringBuilder sb = new();

		sut.AppendExpectation(sb, "");

		await That(sb.ToString()).IsEqualTo(expected);
	}

	[Theory]
	[InlineData(ExpectationGrammars.None, "has a ParamName")]
	[InlineData(ExpectationGrammars.Negated, "does not have a ParamName")]
	[InlineData(ExpectationGrammars.Nested, "ParamName exists")]
	[InlineData(ExpectationGrammars.Nested | ExpectationGrammars.Negated, "ParamName does not exist")]
	[InlineData(ExpectationGrammars.Active, "with a ParamName")]
	[InlineData(ExpectationGrammars.Active | ExpectationGrammars.Negated, "without a ParamName")]
	public async Task AppendExpectation_WithNullAsExpected_ShouldAppendExpectedText(ExpectationGrammars grammar,
		string expected)
	{
		ThatException.HasParamNameValueConstraint<ArgumentException> sut = new("it", grammar, null);
		StringBuilder sb = new();

		sut.AppendExpectation(sb, "");

		await That(sb.ToString()).IsEqualTo(expected);
	}
}
