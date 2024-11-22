using System.Collections.Generic;

namespace Testably.Expectations.Tests.ThatTests.Objects;

public sealed partial class ObjectShould
{
	public sealed partial class Be
	{
		public sealed class UsingTests
		{
			[Fact]
			public async Task WhenComparerConsidersDifferent_ShouldFail()
			{
				OuterClass subject = new() { Value = "Foo" };
				OuterClass expected = subject;

				async Task Act()
					=> await That(subject).Should().Be(expected).Using(new MyComparer(false));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be equal to expected using MyComparer,
					             but it was OuterClass {
					               Inner = <null>,
					               Value = "Foo"
					             }
					             """);
			}

			[Fact]
			public async Task WhenComparerConsidersEqual_ShouldSucceed()
			{
				OuterClass subject = new() { Value = "Foo" };
				OuterClass expected = new() { Value = "Bar" };

				async Task Act()
					=> await That(subject).Should().Be(expected).Using(new MyComparer(true));

				await That(Act).Should().NotThrow();
			}

			private sealed class MyComparer(bool considerEqual) : IEqualityComparer<object>
			{
				#region IEqualityComparer<object> Members

				bool IEqualityComparer<object>.Equals(object? x, object? y)
					=> considerEqual;

				public int GetHashCode(object obj)
					=> obj.GetHashCode();

				#endregion
			}
		}
	}
}
