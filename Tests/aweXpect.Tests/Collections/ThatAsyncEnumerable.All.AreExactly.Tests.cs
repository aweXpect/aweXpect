#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class All
	{
		public sealed class AreExactly
		{
			public sealed class GenericTests
			{
				[Fact]
				public async Task WhenTypeDoesNotMatch_ShouldFail()
				{
					IAsyncEnumerable<MyBaseClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyBaseClass());

					async Task Act()
						=> await That(subject).All().AreExactly<MyClass>();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is exactly of type MyClass for all items,
						             but not all were
						             """);
				}

				[Fact]
				public async Task WhenTypeMatchesBaseType_ShouldFail()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyClass());

					async Task Act()
						=> await That(subject).All().AreExactly<MyBaseClass>();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is exactly of type MyBaseClass for all items,
						             but not all were
						             """);
				}

				[Fact]
				public async Task WhenTypeMatchesExactly_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyClass());

					async Task Act()
						=> await That(subject).All().AreExactly<MyClass>();

					await That(Act).DoesNotThrow();
				}
			}

			public sealed class TypeTests
			{
				[Fact]
				public async Task WhenTypeDoesNotMatch_ShouldFail()
				{
					IAsyncEnumerable<MyBaseClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyBaseClass());

					async Task Act()
						=> await That(subject).All().AreExactly(typeof(MyClass));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is exactly of type MyClass for all items,
						             but not all were
						             """);
				}

				[Fact]
				public async Task WhenTypeMatchesBaseType_ShouldFail()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyClass());

					async Task Act()
						=> await That(subject).All().AreExactly(typeof(MyBaseClass));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is exactly of type MyBaseClass for all items,
						             but not all were
						             """);
				}

				[Fact]
				public async Task WhenTypeMatchesExactly_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyClass());

					async Task Act()
						=> await That(subject).All().AreExactly(typeof(MyClass));

					await That(Act).DoesNotThrow();
				}
			}

			public class MyClass : MyBaseClass
			{
				public int Bar { get; set; }
			}

			public class MyBaseClass
			{
				public int Foo { get; set; }
			}
		}
	}
}
#endif
