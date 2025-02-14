using System.Collections.Generic;
using System.Linq;
using aweXpect.Core.Constraints;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Constraints;

public sealed class ConstraintResultExtensionsTests
{
	public sealed class FailTests
	{
		[Fact]
		public async Task Failure_TryGetValue_WhenTypeDoesNotMatch_ShouldReturnFalse()
		{
			ConstraintResult sut = new ConstraintResult.Success<string>("value", "foo");
			sut = sut.Fail("bar", 1);

			bool result = sut.TryGetValue(out string? value);

			await That(sut.Outcome).IsEqualTo(Outcome.Failure);
			await That(result).IsFalse();
			await That(value).IsNull();
		}

		[Fact]
		public async Task Failure_TryGetValue_WhenTypeMatches_ShouldReturnTrue()
		{
			ConstraintResult sut = new ConstraintResult.Success<string>("value", "foo");
			sut = sut.Fail("bar", 1);

			bool result = sut.TryGetValue(out int value);

			await That(sut.Outcome).IsEqualTo(Outcome.Failure);
			await That(result).IsTrue();
			await That(value).IsEqualTo(1);
		}

		[Fact]
		public async Task Failure_TryGetValue_WithNullValue_ShouldReturnFalse()
		{
			ConstraintResult sut = new ConstraintResult.Success<string>("value", "foo");
			sut = sut.Fail<string?>("bar", null);

			bool result = sut.TryGetValue(out string? value);

			await That(sut.Outcome).IsEqualTo(Outcome.Failure);
			await That(result).IsTrue();
			await That(value).IsNull();
		}
	}

	public sealed class UseValueTests
	{
		[Fact]
		public async Task Failure_TryGetValue_WhenTypeDoesNotMatch_ShouldReturnFalse()
		{
			ConstraintResult sut = new ConstraintResult.Failure<string>("value", "foo", "bar");
			sut = sut.UseValue(1);

			bool result = sut.TryGetValue(out string? value);

			await That(result).IsFalse();
			await That(value).IsNull();
		}

		[Fact]
		public async Task Failure_TryGetValue_WhenTypeMatches_ShouldReturnTrue()
		{
			ConstraintResult sut = new ConstraintResult.Failure<string>("value", "foo", "bar");
			sut = sut.UseValue(1);

			bool result = sut.TryGetValue(out int value);

			await That(result).IsTrue();
			await That(value).IsEqualTo(1);
		}

		[Fact]
		public async Task Failure_TryGetValue_WithNullValue_ShouldReturnFalse()
		{
			ConstraintResult sut = new ConstraintResult.Failure<string>("value", "foo", "bar");
			sut = sut.UseValue<string?>(null);

			bool result = sut.TryGetValue(out string? value);

			await That(result).IsTrue();
			await That(value).IsNull();
		}
	}

	public sealed class WithContextTests
	{
		[Fact]
		public async Task NestedContexts_ShouldIncludeBoth()
		{
			ConstraintResult sut = new ConstraintResult.Failure<string>("value", "foo", "bar");
			sut = sut.WithContext("t1", "c1");
			sut = sut.WithContext("t2", "c2");

			List<ConstraintResult.Context> result = sut.GetContexts().ToList();

			await That(result).HasCount().EqualTo(2);
		}

		[Fact]
		public async Task TryGetValue_ShouldForwardToWrappedConstraintResult()
		{
			ConstraintResult sut = new ConstraintResult.Failure<string>("value1", "foo", "bar");
			sut = sut.WithContext("t1", "c1");

			bool result = sut.TryGetValue(out string? value);

			await That(result).IsTrue();
			await That(value).IsEqualTo("value1");
		}
	}

	public sealed class AppendExpectationTextTests
	{
		[Fact]
		public async Task ShouldAppendAfterExpectationText()
		{
			ConstraintResult.Success sut = new("foo");

			ConstraintResult result = sut.AppendExpectationText(s => s.Append("\nsuffix-foo"));

			await That(result.Outcome).IsEqualTo(Outcome.Success);
			await That(result.GetExpectationText()).IsEqualTo("foo\nsuffix-foo");
		}

		[Fact]
		public async Task ShouldKeepResultTextUnchanged()
		{
			ConstraintResult.Failure sut = new("foo", "bar");

			ConstraintResult result = sut.AppendExpectationText(s => s.Append("\nsuffix-foo"));

			await That(result.Outcome).IsEqualTo(Outcome.Failure);
			await That(result.GetExpectationText()).IsEqualTo("foo\nsuffix-foo");
			await That(result.GetResultText()).IsEqualTo("bar");
		}
	}

	public sealed class PrependExpectationTextTests
	{
		[Fact]
		public async Task ShouldAppendAfterExpectationText()
		{
			ConstraintResult.Success sut = new("foo");

			ConstraintResult result = sut.PrependExpectationText(s => s.Append("prefix-foo\n"));

			await That(result.Outcome).IsEqualTo(Outcome.Success);
			await That(result.GetExpectationText()).IsEqualTo("prefix-foo\nfoo");
		}

		[Fact]
		public async Task ShouldKeepResultTextUnchanged()
		{
			ConstraintResult.Failure sut = new("foo", "bar");

			ConstraintResult result = sut.PrependExpectationText(s => s.Append("prefix-foo\n"));

			await That(result.Outcome).IsEqualTo(Outcome.Failure);
			await That(result.GetExpectationText()).IsEqualTo("prefix-foo\nfoo");
			await That(result.GetResultText()).IsEqualTo("bar");
		}
	}
}
