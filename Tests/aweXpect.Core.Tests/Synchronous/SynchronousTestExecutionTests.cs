using System.Runtime.CompilerServices;
using aweXpect.Core.Tests.TestHelpers;
using aweXpect.Synchronous;

namespace aweXpect.Core.Tests.Synchronous;

public class SynchronousTestExecutionTests
{
	[Fact]
	public void WhenActionDoesNotThrow_ShouldSucceed()
	{
		Foo subject = new()
		{
			Bar = 3
		};

		int value = subject.Bar;
		That(() => ThrowIf(value != 3)).DoesNotThrow().Verify();
	}

	[Fact]
	public void WhenActionThrows_ShouldFail()
	{
		Foo subject = new()
		{
			Bar = 3
		};
		int value = subject.Bar;
		void Act() => That(() => ThrowIf(value == 3)).DoesNotThrow().Verify();

		That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected () => ThrowIf(value == 3) to
			             not throw any exception,
			             but it did throw a MyException:
			               WhenActionThrows_ShouldFail
			             """).Verify();
	}

	[Fact]
	public void WhenPropertyValuesMatch_ShouldFail()
	{
		Foo subject = new()
		{
			Bar = 3
		};
		int value = subject.Bar;
		void Act() => That(value).IsEqualTo(2).Verify();

		That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected value to
			             be equal to 2,
			             but it was 3
			             """).Verify();
	}

	[Fact]
	public void WhenPropertyValuesMatch_ShouldSucceed()
	{
		Foo subject = new()
		{
			Bar = 3
		};

		int value = That(subject.Bar).IsEqualTo(3).Verify();

		That(value).IsEqualTo(3).Verify();
	}

	private static void ThrowIf(bool condition, [CallerMemberName] string message = "")
	{
		if (condition)
		{
			throw new MyException(message);
		}
	}

	private ref struct Foo
	{
		public int Bar;
	}
}
