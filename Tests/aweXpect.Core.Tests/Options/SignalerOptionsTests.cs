using aweXpect.Chronology;
using aweXpect.Options;

namespace aweXpect.Core.Tests.Options;

public sealed class SignalerOptionsTests
{
	public sealed class Tests
	{
		[Fact]
		public async Task ToString_Empty_ShouldReturnEmptyString()
		{
			SignalerOptions sut = new();

			string result = sut.ToString();

			await That(result).IsEmpty();
		}

		[Fact]
		public async Task ToString_WithTimeout_ShouldIncludeTimeout()
		{
			SignalerOptions sut = new();
			sut.Timeout = 5.Seconds();

			string result = sut.ToString();

			await That(result).IsEqualTo(" within 0:05");
		}
	}

	public sealed class WithParameterTests
	{
		[Fact]
		public async Task ToString_Empty_ShouldReturnEmptyString()
		{
			SignalerOptions<int> sut = new();

			string result = sut.ToString();

			await That(result).IsEmpty();
		}

		[Fact]
		public async Task ToString_WithPredicate_ShouldIncludePredicateExpression()
		{
			SignalerOptions<int> sut = new();
			sut.WithPredicate(_ => true, "my predicate");

			string result = sut.ToString();

			await That(result).IsEqualTo(" with my predicate");
		}

		[Fact]
		public async Task ToString_WithTimeout_ShouldIncludeTimeout()
		{
			SignalerOptions<int> sut = new();
			sut.Timeout = 5.Seconds();

			string result = sut.ToString();

			await That(result).IsEqualTo(" within 0:05");
		}

		[Fact]
		public async Task ToString_WithTimeoutAndPredicate_ShouldIncludePredicateExpression()
		{
			SignalerOptions<int> sut = new();
			sut.Timeout = 75.Seconds();
			sut.WithPredicate(_ => true, "my predicate");

			string result = sut.ToString();

			await That(result).IsEqualTo(" with my predicate within 1:15");
		}
	}
}
