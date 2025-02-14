using System.Text;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.TestHelpers;

public static class ConstraintResultExtension
{
	public static string GetResultText(this ConstraintResult result)
	{
		StringBuilder sb = new();
		result.AppendResult(sb);
		return sb.ToString();
	}

	public static string GetExpectationText(this ConstraintResult result)
	{
		StringBuilder sb = new();
		result.AppendExpectation(sb);
		return sb.ToString();
	}
}
