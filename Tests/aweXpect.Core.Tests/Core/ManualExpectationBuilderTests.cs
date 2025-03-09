using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using aweXpect.Core.Constraints;
using aweXpect.Core.Nodes;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core;

public class ManualExpectationBuilderTests
{
	[Fact]
	public async Task AddAsyncContextValueConstraint_ShouldAllowGettingExpectationBuilder()
	{
		ManualExpectationBuilder<int> sut = new();
		ExpectationBuilder? expectationBuilder = null;
		sut.AddConstraint((e, _, _) =>
		{
			expectationBuilder = e;
			return new DummyAsyncContextConstraint<int>(_
				=> Task.FromResult<ConstraintResult>(new DummyConstraint<int>(_ => true)));
		});

		await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(expectationBuilder).IsSameAs(sut);
	}

	[Fact]
	public async Task AddAsyncValueConstraint_ShouldAllowGettingExpectationBuilder()
	{
		ManualExpectationBuilder<int> sut = new();
		ExpectationBuilder? expectationBuilder = null;
		sut.AddConstraint((e, _, _) =>
		{
			expectationBuilder = e;
			return new DummyAsyncConstraint<int>(_
				=> Task.FromResult<ConstraintResult>(new DummyConstraint<int>(_ => true)));
		});

		await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(expectationBuilder).IsSameAs(sut);
	}

	[Fact]
	public async Task AddContextValueConstraint_ShouldAllowGettingExpectationBuilder()
	{
		ManualExpectationBuilder<int> sut = new();
		ExpectationBuilder? expectationBuilder = null;
		sut.AddConstraint((e, _, _) =>
		{
			expectationBuilder = e;
			return new DummyContextConstraint<int>(_ => new DummyConstraint<int>(_ => true));
		});

		await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(expectationBuilder).IsSameAs(sut);
	}

	[Fact]
	public async Task AddValueConstraint_ShouldAllowGettingExpectationBuilder()
	{
		ManualExpectationBuilder<int> sut = new();
		ExpectationBuilder? expectationBuilder = null;
		sut.AddConstraint((e, _, _) =>
		{
			expectationBuilder = e;
			return new DummyConstraint("");
		});

		await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(expectationBuilder).IsSameAs(sut);
	}


	[Fact]
	public async Task IsMet_ShouldThrowNotSupportedException()
	{
		ManualExpectationBuilder<int> sut = new();
		sut.AddConstraint((_, _) => new DummyConstraint<int>(_ => true));

		async Task Act() => await sut.IsMet(
			new ExpectationNode(), null!, new TimeSystemMock(), null, CancellationToken.None);

		await That(Act).Throws<NotSupportedException>()
			.WithMessage("Use IsMetBy for ManualExpectationBuilder!");
	}

	[Fact]
	public async Task IsMetBy_FailingConstraint_ShouldReturnFailure()
	{
		ManualExpectationBuilder<int> sut = new();
		sut.AddConstraint((_, _) => new DummyConstraint<int>(_ => false));

		ConstraintResult result = await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(result.Outcome).IsEqualTo(Outcome.Failure);
	}

	[Fact]
	public async Task IsMetBy_SucceedingConstraint_ShouldReturnSuccess()
	{
		ManualExpectationBuilder<int> sut = new();
		sut.AddConstraint((_, _) => new DummyConstraint<int>(_ => true));

		ConstraintResult result = await sut.IsMetBy(1, null!, CancellationToken.None);

		await That(result.Outcome).IsEqualTo(Outcome.Success);
	}

	[Fact]
	public async Task Subject_ShouldBeEmpty()
	{
		ManualExpectationBuilder<int> sut = new();

		await That(sut.Subject).IsEmpty();
	}

	private sealed class DummyConstraint<T>(
		Func<T, bool> predicate)
		: ConstraintResult(ExpectationGrammars.None), IValueConstraint<T>
	{
		private T? _actual;

		public ConstraintResult IsMetBy(T actual)
		{
			_actual = actual;
			Outcome = predicate(actual) ? Outcome.Success : Outcome.Failure;
			return this;
		}

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null) { }

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null) { }

		public override ConstraintResult Negate()
			=> this;

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value) where TValue : default
		{
			if (_actual is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			return base.TryGetValue(out value);
		}
	}
}
