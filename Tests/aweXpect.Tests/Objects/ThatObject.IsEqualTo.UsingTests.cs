using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class IsEqualTo
	{
		public sealed class UsingTests
		{
			[Fact]
			public async Task WhenComparerConsidersDifferent_ShouldFail()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
				};
				OuterClass expected = subject;

				async Task Act()
					=> await That(subject).IsEqualTo(expected).Using(new MyComparer(false));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to expected using MyComparer,
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
					Value = "Foo",
				};
				OuterClass expected = new()
				{
					Value = "Bar",
				};

				async Task Act()
					=> await That(subject).IsEqualTo(expected).Using(new MyComparer(true));

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
