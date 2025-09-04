using System.Text;
using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Helpers;
using aweXpect.Core.Nodes;

namespace aweXpect.Core.Tests.TestHelpers;

internal class DummyNode(string name, Func<ConstraintResult>? result = null) : Node
{
	public MemberAccessor? MappingMemberAccessor { get; private set; }

	public override void AddConstraint(IConstraint constraint)
		=> throw new NotSupportedException();

	public override Node AddMapping<TValue, TTarget>(
		MemberAccessor<TValue, TTarget> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null)
		where TValue : default
		where TTarget : default
	{
		MappingMemberAccessor = memberAccessor;
		return this;
	}

	public override Node AddAsyncMapping<TValue, TTarget>(
		MemberAccessor<TValue, Task<TTarget>> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null)
		where TValue : default
		where TTarget : default
	{
		MappingMemberAccessor = memberAccessor;
		return this;
	}

	public override void AddNode(Node node, string? separator = null)
		=> throw new NotSupportedException();

	public override Task<ConstraintResult> IsMetBy<TValue>(
		TValue? value,
		IEvaluationContext context,
		CancellationToken cancellationToken)
		where TValue : default
		=> result == null ? throw new NotSupportedException() : Task.FromResult(result());

	public override void SetReason(BecauseReason becauseReason)
		=> throw new NotSupportedException();

	public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		=> stringBuilder.Append(name);
}
