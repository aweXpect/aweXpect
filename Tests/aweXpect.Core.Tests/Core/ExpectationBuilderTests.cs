using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core;

public class ExpectationBuilderTests
{
	[Fact]
	public async Task ForAsyncMember_WithFailingExpectation_ShouldReturnFailureConstraintResult()
	{
		ManualExpectationBuilder<string> sut = new(null);

		sut.ForAsyncMember(MemberAccessor<string, Task<int>>.FromFunc(x => Task.FromResult(x.Length), "length "))
			.AddExpectations(expectationBuilder => expectationBuilder.AddConstraint((_, _)
				=> new DummyConstraint<int>(v => v == 2, "equal to 2")));

		ConstraintResult constraintResult = await sut.IsMetBy("bar", null!, CancellationToken.None);

		await That(constraintResult.Outcome).IsEqualTo(Outcome.Failure);
		await That(constraintResult.GetExpectationText()).IsEqualTo("length equal to 2");
	}

	[Fact]
	public async Task ForAsyncMember_WithSucceedingExpectation_ShouldReturnSuccessConstraintResult()
	{
		ManualExpectationBuilder<string> sut = new(null);

		sut.ForAsyncMember(MemberAccessor<string, Task<int>>.FromFunc(x => Task.FromResult(x.Length), "length "))
			.AddExpectations(expectationBuilder => expectationBuilder.AddConstraint((_, _)
				=> new DummyConstraint<int>(v => v == 3, "equal to 3")));

		ConstraintResult constraintResult = await sut.IsMetBy("bar", null!, CancellationToken.None);

		await That(constraintResult.Outcome).IsEqualTo(Outcome.Success);
		await That(constraintResult.GetExpectationText()).IsEqualTo("length equal to 3");
	}

	[Fact]
	public async Task ForMember_WithFailingExpectation_ShouldReturnFailureConstraintResult()
	{
		ManualExpectationBuilder<string> sut = new(null);

		sut.ForMember(MemberAccessor<string, int>.FromFunc(x => x.Length, "length "))
			.AddExpectations(expectationBuilder => expectationBuilder.AddConstraint((_, _)
				=> new DummyConstraint<int>(v => v == 2, "equal to 2")));

		ConstraintResult constraintResult = await sut.IsMetBy("bar", null!, CancellationToken.None);

		await That(constraintResult.Outcome).IsEqualTo(Outcome.Failure);
		await That(constraintResult.GetExpectationText()).IsEqualTo("length equal to 2");
	}

	[Fact]
	public async Task ForMember_WithSucceedingExpectation_ShouldReturnSuccessConstraintResult()
	{
		ManualExpectationBuilder<string> sut = new(null);

		sut.ForMember(MemberAccessor<string, int>.FromFunc(x => x.Length, "length "))
			.AddExpectations(expectationBuilder => expectationBuilder.AddConstraint((_, _)
				=> new DummyConstraint<int>(v => v == 3, "equal to 3")));

		ConstraintResult constraintResult = await sut.IsMetBy("bar", null!, CancellationToken.None);

		await That(constraintResult.Outcome).IsEqualTo(Outcome.Success);
		await That(constraintResult.GetExpectationText()).IsEqualTo("length equal to 3");
	}

	[Fact]
	public async Task WhenSubjectHasMultipleLines_ShouldTrimCommonWhiteSpace()
	{
		async Task Act() => await That(new[]
		{
			1, 2, 3,
		}).IsEmpty();

		await That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected that new[]
			             {
			             	1, 2, 3,
			             }
			             is empty,
			             but it was [
			               1,
			               2,
			               3
			             ]
			             """);
	}

	[Fact]
	public async Task WhenTypeImplementsIDescribableSubject_ShouldUseToStringFromIt()
	{
		MyDescribableSubject subject = new("this long description for the subject");

		async Task Act() => await That(subject).IsNull();

		await That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected that this long description for the subject
			             is null,
			             but it was ExpectationBuilderTests.MyDescribableSubject { }
			             """);
	}

	private sealed class MyDescribableSubject(string subject) : IDescribableSubject
	{
		public string GetDescription()
			=> subject;
	}
}
