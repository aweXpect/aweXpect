using aweXpect.Core.Helpers;
using aweXpect.Core.Sources;

namespace aweXpect.Core.Tests.Core.Helpers;

public class ExpectHelpersTests
{
	[Fact]
	public async Task ThatIs_WhenClassDoesNotHaveAnExpectationBuilder_ShouldThrowNotSupportedException()
	{
		IThat<int> subject = new ThatWith();

		IThatIs<int> Act() => subject.Get();

		await That(Act).Throws<NotSupportedException>()
			.WithMessage("IThat<T> must also implement IThatIs<T>");
	}

	[Fact]
	public async Task ThatIs_WhenClassImplementsBoth_ShouldReturnSameObject()
	{
		IThat<int> subject = new ThatWithThatIs();

		IThatIs<int> result = subject.Get();

		await That(result).IsSameAs(subject);
	}

	[Fact]
	public async Task ThatIs_WhenClassImplementsThatVerb_ShouldReturnValueWithSameExpectationBuilder()
	{
		ThatWithThatVerb origin = new();
		IThat<int> subject = origin;

		IThatIs<int> result = subject.Get();

		await That(result).IsNotSameAs(subject);
		await That(result.ExpectationBuilder).IsSameAs(origin.ExpectationBuilder);
	}

	private sealed class ThatWith : IThat<int>;

	private sealed class ThatWithThatIs : IThat<int>, IThatIs<int>
	{
		public ExpectationBuilder ExpectationBuilder { get; }
			= new ExpectationBuilder<int>(new ValueSource<int>(0), "");
	}

	private sealed class ThatWithThatVerb : IThat<int>, IThatVerb<int>
	{
		public ExpectationBuilder ExpectationBuilder { get; }
			= new ExpectationBuilder<int>(new ValueSource<int>(0), "");
	}
}
