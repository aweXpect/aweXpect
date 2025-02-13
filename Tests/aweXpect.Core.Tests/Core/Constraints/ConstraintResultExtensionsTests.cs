using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Core.Constraints;

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

#if DEBUG // TODO: replace after next awexpect update
			await That(result).HasCount().EqualTo(2);
#else
			await That(result).Has().Exactly(2).Items();
#endif
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

		[Fact]
		public async Task UpdateExpectationText_ShouldForwardToWrappedConstraintResult()
		{
			ConstraintResult sut = new ConstraintResult.Failure<string>("value1", "foo", "bar");
			sut = sut.WithContext("t1", "c1");
			StringBuilder sb = new();

			sut.UpdateExpectationText(s => s.Append("PRE-"), s => s.Append("-POST"));

			sut.AppendExpectation(sb);
			await That(sb.ToString()).IsEqualTo("PRE-foo-POST");
		}
	}
}
