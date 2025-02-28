using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.Core.Constraints;

public class ConstraintResultTests
{
	[Fact]
	public async Task Failure_ShouldHaveFailureOutcome()
	{
		ConstraintResult.Failure<int> sut = new(1, "foo", "bar");

		await That(sut.Outcome).IsEqualTo(Outcome.Failure);
	}

	[Fact]
	public async Task Failure_TryGetValue_WhenTypeDoesNotMatch_ShouldReturnFalse()
	{
		ConstraintResult.Failure<int> sut = new(1, "foo", "bar");

		bool result = sut.TryGetValue(out string? value);

		await That(result).IsFalse();
		await That(value).IsNull();
	}

	[Fact]
	public async Task Failure_TryGetValue_WhenTypeMatches_ShouldReturnTrue()
	{
		ConstraintResult.Failure<int> sut = new(1, "foo", "bar");

		bool result = sut.TryGetValue(out int value);

		await That(result).IsTrue();
		await That(value).IsEqualTo(1);
	}

	[Fact]
	public async Task Failure_TryGetValue_WithNullValue_ShouldReturnFalse()
	{
		ConstraintResult.Failure<string?> sut = new(null, "foo", "bar");

		bool result = sut.TryGetValue(out string? value);

		await That(result).IsTrue();
		await That(value).IsNull();
	}

	[Fact]
	public async Task Success_AppendResult_ShouldDoNothing()
	{
		ConstraintResult.Success<int> sut = new(1, "foo");
		StringBuilder sb = new();

		sut.AppendResult(sb);

		await That(sb.ToString()).IsEmpty();
	}

	[Fact]
	public async Task Success_ShouldHaveSuccessOutcome()
	{
		ConstraintResult.Success<int> sut = new(1, "foo");

		await That(sut.Outcome).IsEqualTo(Outcome.Success);
	}

	[Fact]
	public async Task Success_T_TryGetValue_WhenTypeDoesNotMatch_ShouldReturnFalse()
	{
		ConstraintResult.Success<int> sut = new(1, "foo");

		bool result = sut.TryGetValue(out string? value);

		await That(result).IsFalse();
		await That(value).IsNull();
	}

	[Fact]
	public async Task Success_T_TryGetValue_WhenTypeMatches_ShouldReturnTrue()
	{
		ConstraintResult.Success<string> sut = new("bar", "foo");

		bool result = sut.TryGetValue(out string? value);

		await That(result).IsTrue();
		await That(value).IsEqualTo("bar");
	}

	[Fact]
	public async Task Success_TryGetValue_ShouldReturnFalse()
	{
		ConstraintResult.Success sut = new("foo");

		bool result = sut.TryGetValue(out string? value);

		await That(result).IsFalse();
		await That(value).IsNull();
	}
}
