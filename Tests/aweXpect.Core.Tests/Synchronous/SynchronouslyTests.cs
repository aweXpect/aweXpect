using System.Runtime.CompilerServices;
using aweXpect.Core.Tests.TestHelpers;
using aweXpect.Synchronous;
// ReSharper disable InvokeAsExtensionMethod

namespace aweXpect.Core.Tests.Synchronous;

public class SynchronouslyTests
{
	[Fact]
	public void WhenActionDoesNotThrow_ShouldSucceed()
	{
		Foo subject = new()
		{
			Bar = 3
		};

		int value = subject.Bar;
		Synchronously.Verify(That(() => ThrowIf(value != 3)).DoesNotThrow());
	}

	[Fact]
	public void WhenActionThrows_ShouldFail()
	{
		Foo subject = new()
		{
			Bar = 3
		};
		int value = subject.Bar;
		void Act() => Synchronously.Verify(That(() => ThrowIf(value == 3)).DoesNotThrow());

		Synchronously.Verify(That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected () => ThrowIf(value == 3) to
			             not throw any exception,
			             but it did throw a MyException:
			               WhenActionThrows_ShouldFail
			             """));
	}

	[Fact]
	public void WhenPropertyValuesMatch_ShouldFail()
	{
		Foo subject = new()
		{
			Bar = 3
		};
		int value = subject.Bar;
		void Act() => Synchronously.Verify(That(value).IsEqualTo(2));

		Synchronously.Verify(That(Act).Throws<XunitException>()
			.WithMessage("""
			             Expected value to
			             be equal to 2,
			             but it was 3
			             """));
	}

	[Fact]
	public void WhenPropertyValuesMatch_ShouldSucceed()
	{
		Foo subject = new()
		{
			Bar = 3
		};

		int value = Synchronously.Verify(That(subject.Bar).IsEqualTo(3));

		Synchronously.Verify(That(value).IsEqualTo(3));
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
