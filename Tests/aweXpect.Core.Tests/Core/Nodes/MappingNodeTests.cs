using System.Text;
using System.Threading;
using aweXpect.Chronology;
using aweXpect.Core.Constraints;
using aweXpect.Core.Nodes;
using aweXpect.Core.Sources;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Nodes;

public class MappingNodeTests
{
	[Fact]
	public async Task IsMetBy_ShouldUseInnerConstraintWithOuterValue()
	{
		MappingNode<string, int> node = new(MemberAccessor<string, int>.FromFunc(s => s.Length, " length "));
		node.AddConstraint(new DummyValueConstraint<int>(v
			=> new DummyConstraintResult<int>(Outcome.Success, v, $"yeah: {v}")));
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy("foobar", null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo("yeah: 6");
		await That(result.Outcome).IsEqualTo(Outcome.Success);
		await That(result.TryGetValue(out string? value)).IsTrue();
		await That(value).IsEqualTo("foobar");
	}

	[Fact]
	public async Task IsMetBy_WithInvalidType_ShouldThrowInvalidOperationException()
	{
		MappingNode<string, int> node = new(MemberAccessor<string, int>.FromFunc(s => s.Length, " length "));
		node.AddConstraint(
			new DummyValueConstraint<int?>(v => new DummyConstraintResult<int?>(Outcome.Success, v, "yeah!")));
		async Task Act() => await node.IsMetBy(42, null!, CancellationToken.None);

		await That(Act).Throws<InvalidOperationException>()
			.WithMessage("""
			             The member type for the actual value in the which node did not match.
			             Expected: string,
			                Found: int
			             """);
	}

	[Fact]
	public async Task IsMetBy_WithNullDelegate_ShouldReturnNullFailure()
	{
		DelegateValue<string?> value = new("foo", null, 10.Milliseconds(), true);
		MappingNode<string?, int?> node = new(MemberAccessor<string?, int?>.FromFunc(s => s?.Length, " length "));
		node.AddConstraint(
			new DummyValueConstraint<int?>(v => new DummyConstraintResult<int?>(Outcome.Success, v, "yeah!")));
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy(value, null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(result.Outcome).IsEqualTo(Outcome.Failure);
		await That(sb.ToString()).IsEqualTo("yeah!");
		await That(result.GetResultText()).IsEqualTo("it was <null>");
	}

	[Fact]
	public async Task IsMetBy_WithNullValue_ShouldReturnNullFailure()
	{
		MappingNode<string?, int?> node = new(MemberAccessor<string?, int?>.FromFunc(s => s?.Length, " length "));
		node.AddConstraint(
			new DummyValueConstraint<int?>(v => new DummyConstraintResult<int?>(Outcome.Success, v, "yeah!")));
		StringBuilder sb = new();

		ConstraintResult result = await node.IsMetBy<string?>(null, null!, CancellationToken.None);

		result.AppendExpectation(sb);
		await That(result.Outcome).IsEqualTo(Outcome.Failure);
		await That(sb.ToString()).IsEqualTo("yeah!");
		await That(result.GetResultText()).IsEqualTo("it was <null>");
	}
}
