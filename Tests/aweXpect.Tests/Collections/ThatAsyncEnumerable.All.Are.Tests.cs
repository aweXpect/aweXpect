﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class All
	{
		public sealed class Are
		{
			public sealed class GenericTests
			{
				[Fact]
				public async Task WhenTypeDoesNotMatch_ShouldFail()
				{
					IAsyncEnumerable<MyBaseClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyBaseClass());

					async Task Act()
						=> await That(subject).All().Are<MyClass>();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is of type ThatAsyncEnumerable.All.Are.MyClass for all items,
						             but not all were
						             """);
				}

				[Fact]
				public async Task WhenTypeMatchesBaseType_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyClass());

					async Task Act()
						=> await That(subject).All().Are<MyBaseClass>();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenTypeMatchesExactly_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyClass());

					async Task Act()
						=> await That(subject).All().Are<MyClass>();

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
						=> await That(subject).All().Are(typeof(MyClass));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is of type ThatAsyncEnumerable.All.Are.MyClass for all items,
						             but not all were
						             """);
				}

				[Fact]
				public async Task WhenTypeMatchesBaseType_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyClass());

					async Task Act()
						=> await That(subject).All().Are(typeof(MyBaseClass));

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenTypeMatchesExactly_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(
						Enumerable.Range(1, 10), _ => new MyClass());

					async Task Act()
						=> await That(subject).All().Are(typeof(MyClass));

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
