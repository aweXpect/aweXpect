using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class Is
	{
		public sealed partial class EqualTo
		{
			public sealed class UsingTests
			{
				[Fact]
				public async Task WhenComparerConsidersDifferent_ShouldFail()
				{
					OuterClass subject = new()
					{
						Value = "Foo"
					};
					OuterClass expected = subject;

					async Task Act()
						=> await That(subject).Is(expected).Using(new MyComparer(false));

					await That(Act).Throws<XunitException>()
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
					OuterClass subject = new()
					{
						Value = "Foo"
					};
					OuterClass expected = new()
					{
						Value = "Bar"
					};

					async Task Act()
						=> await That(subject).Is(expected).Using(new MyComparer(true));

					await That(Act).DoesNotThrow();
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
}
