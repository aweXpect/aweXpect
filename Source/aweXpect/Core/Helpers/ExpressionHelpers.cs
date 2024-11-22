using System.Linq.Expressions;
using System.Text;

namespace Testably.Expectations.Core.Helpers;

internal static class ExpressionHelpers
{
	public static MemberExpression? GetMemberExpression(Expression? expression)
		=> expression switch
		{
			MemberExpression memberExpression => memberExpression,
			LambdaExpression lambdaExpression => lambdaExpression.Body switch
			{
				MemberExpression body => body,
				UnaryExpression unaryExpression => (MemberExpression)unaryExpression.Operand,
				_ => null
			},
			_ => null
		};

	public static string GetPropertyPath(Expression expression)
	{
		MemberExpression? memberExpression = GetMemberExpression(expression);
		StringBuilder path = new();
		while (memberExpression != null)
		{
			path.Insert(0, memberExpression.Member.Name);
			path.Insert(0, ".");
			memberExpression = GetMemberExpression(memberExpression.Expression);
		}

		return path.ToString();
	}
}
