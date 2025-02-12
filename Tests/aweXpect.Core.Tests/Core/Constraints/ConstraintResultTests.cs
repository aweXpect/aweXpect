using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Core.Constraints;

namespace aweXpect.Core.Tests.Core.Constraints;

public class ConstraintResultTests
{
	[Fact]
	public async Task Context_Comparer_WhenItem1IsNull_ShouldReturnFalse()
	{
		ConstraintResult.Context context = new("foo", "baz");

		bool result = ConstraintResult.Context.Comparer.Equals(null!, context);

		await That(result).IsFalse();
	}

	[Fact]
	public async Task Context_Comparer_WhenItem2IsNull_ShouldReturnFalse()
	{
		ConstraintResult.Context context = new("foo", "baz");

		bool result = ConstraintResult.Context.Comparer.Equals(context, null!);

		await That(result).IsFalse();
	}

	[Fact]
	public async Task Context_Comparer_WhenTitleIsEqual_ShouldReturnTrue()
	{
		ConstraintResult.Context context1 = new("foo", "bar");
		ConstraintResult.Context context2 = new("foo", "baz");

		bool result = ConstraintResult.Context.Comparer.Equals(context1, context2);

		await That(result).IsTrue();
	}

	[Fact]
	public async Task Failure_GetContexts_ShouldBeEmpty()
	{
		ConstraintResult.Failure<int> sut = new(1, "foo", "bar");

		List<ConstraintResult.Context> contexts = sut.GetContexts().ToList();

		await That(contexts).IsEmpty();
	}

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
	public async Task Success_GetContexts_ShouldBeEmpty()
	{
		ConstraintResult.Success<int> sut = new(1, "foo");

		List<ConstraintResult.Context> contexts = sut.GetContexts().ToList();

		await That(contexts).IsEmpty();
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

	[Fact]
	public async Task UpdateExpectationText_ShouldUsePrefixAndSuffixForAppendExpectation()
	{
		ConstraintResult.Success sut = new("foo");
		StringBuilder sb = new();

		sut.UpdateExpectationText(s => s.Append("prefix-foo\n"), s => s.Append("\nsuffix-foo"));

		sut.AppendExpectation(sb);
		await That(sb.ToString()).IsEqualTo("prefix-foo\nfoo\nsuffix-foo");
	}
}
