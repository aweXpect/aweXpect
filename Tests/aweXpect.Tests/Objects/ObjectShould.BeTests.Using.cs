using System.Collections.Generic;

namespace aweXpect.Tests.Objects;

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
					=> await That(subject).Is().EqualTo(expected).Using(new MyComparer(false));

				await That(Act).Does().Throw<XunitException>()
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
					=> await That(subject).Is().EqualTo(expected).Using(new MyComparer(true));

				await That(Act).Does().NotThrow();
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
