using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect.Internal.Tests.Helpers;

public class ExpectHelpersTests
{
	[Fact]
	public async Task
		ExpectSubject_ThatHas_WhenClassDoesNotImplementIThat_ShouldReturnSubjectWithSameExpectationBuilder()
	{
		ThatHas subject = new();

		IThat<int> result = subject.ExpectSubject();

		await That(result).IsNotSameAs(subject);
		await That(result).Is<IThatVerb<int>>()
			.Whose(x => x.ExpectationBuilder, e => e.IsSameAs(subject.ExpectationBuilder));
	}

	[Fact]
	public async Task ExpectSubject_ThatHas_WhenClassImplementsIThat_ShouldReturnSameObject()
	{
		ThatWithThatHas subject = new();

		IThat<int> result = subject.ExpectSubject();

		await That(result).IsSameAs(subject);
	}

	[Fact]
	public async Task
		ExpectSubject_ThatIs_WhenClassDoesNotImplementIThat_ShouldReturnSubjectWithSameExpectationBuilder()
	{
		ThatIs subject = new();

		IThat<int> result = subject.ExpectSubject();

		await That(result).IsNotSameAs(subject);
		await That(result).Is<IThatVerb<int>>()
			.Whose(x => x.ExpectationBuilder, e => e.IsSameAs(subject.ExpectationBuilder));
	}

	[Fact]
	public async Task ExpectSubject_ThatIs_WhenClassImplementsIThat_ShouldReturnSameObject()
	{
		ThatWithThatIs subject = new();

		IThat<int> result = subject.ExpectSubject();

		await That(result).IsSameAs(subject);
	}

	[Fact]
	public async Task ThatHas_WhenClassDoesNotHaveAnExpectationBuilder_ShouldThrowNotSupportedException()
	{
		IThat<int> subject = new ThatWith();

		IThatHas<int> Act() => subject.ThatHas();

		await That(Act).Throws<NotSupportedException>()
			.WithMessage("IThat<T> must also implement IThatHas<T>");
	}

	[Fact]
	public async Task ThatHas_WhenClassImplementsBoth_ShouldReturnSameObject()
	{
		IThat<int> subject = new ThatWithThatHas();

		IThatHas<int> result = subject.ThatHas();

		await That(result).IsSameAs(subject);
	}

	[Fact]
	public async Task ThatHas_WhenClassImplementsThatVerb_ShouldReturnValueWithSameExpectationBuilder()
	{
		ThatWithThatVerb origin = new();
		IThat<int> subject = origin;

		IThatHas<int> result = subject.ThatHas();

		await That(result).IsNotSameAs(subject);
		await That(result.ExpectationBuilder).IsSameAs(origin.ExpectationBuilder);
	}

	[Fact]
	public async Task ThatIs_WhenClassDoesNotHaveAnExpectationBuilder_ShouldThrowNotSupportedException()
	{
		IThat<int> subject = new ThatWith();

		IThatIs<int> Act() => subject.ThatIs();

		await That(Act).Throws<NotSupportedException>()
			.WithMessage("IThat<T> must also implement IThatIs<T>");
	}

	[Fact]
	public async Task ThatIs_WhenClassImplementsBoth_ShouldReturnSameObject()
	{
		IThat<int> subject = new ThatWithThatIs();

		IThatIs<int> result = subject.ThatIs();

		await That(result).IsSameAs(subject);
	}

	[Fact]
	public async Task ThatIs_WhenClassImplementsThatVerb_ShouldReturnValueWithSameExpectationBuilder()
	{
		ThatWithThatVerb origin = new();
		IThat<int> subject = origin;

		IThatIs<int> result = subject.ThatIs();

		await That(result).IsNotSameAs(subject);
		await That(result.ExpectationBuilder).IsSameAs(origin.ExpectationBuilder);
	}

	private sealed class ThatWith : IThat<int>;

	private sealed class ThatWithThatIs : IThat<int>, IThatIs<int>
	{
		public ExpectationBuilder ExpectationBuilder { get; }
			= new ManualExpectationBuilder<int>(null);
	}

	private sealed class ThatWithThatHas : IThat<int>, IThatHas<int>
	{
		public ExpectationBuilder ExpectationBuilder { get; }
			= new ManualExpectationBuilder<int>(null);
	}

	private sealed class ThatIs : IThatIs<int>
	{
		public ExpectationBuilder ExpectationBuilder { get; }
			= new ManualExpectationBuilder<int>(null);
	}

	private sealed class ThatHas : IThatHas<int>
	{
		public ExpectationBuilder ExpectationBuilder { get; }
			= new ManualExpectationBuilder<int>(null);
	}

	private sealed class ThatWithThatVerb : IThat<int>, IThatVerb<int>
	{
		public ExpectationBuilder ExpectationBuilder { get; }
			= new ManualExpectationBuilder<int>(null);
	}
}
