﻿using System.Collections;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class All
	{
		public sealed partial class Are
		{
			public sealed class EnumerableGenericTests
			{
				[Fact]
				public async Task WhenTypeDoesNotMatch_ShouldFail()
				{
					IEnumerable subject = Enumerable.Range(1, 10).Select(v => new MyBaseClass
					{
						Foo = v,
					});

					async Task Act()
						=> await That(subject).All().Are<MyClass>();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is of type ThatEnumerable.All.Are.MyClass for all items,
						             but not all were

						             Not matching items:
						             [
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 1
						               },
						               (… and maybe others)
						             ]

						             Collection:
						             [
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 1
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 2
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 3
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 4
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 5
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 6
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 7
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 8
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 9
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 10
						               },
						               (… and maybe others)
						             ]
						             """);
				}

				[Fact]
				public async Task WhenTypeMatchesBaseType_ShouldSucceed()
				{
					IEnumerable subject = Enumerable.Range(1, 10).Select(v => new MyClass
					{
						Foo = v,
					});

					async Task Act()
						=> await That(subject).All().Are<MyBaseClass>();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenTypeMatchesExactly_ShouldSucceed()
				{
					IEnumerable subject = Enumerable.Range(1, 10).Select(v => new MyClass
					{
						Foo = v,
					});

					async Task Act()
						=> await That(subject).All().Are<MyClass>();

					await That(Act).DoesNotThrow();
				}
			}

			public sealed class EnumerableTypeTests
			{
				[Fact]
				public async Task WhenTypeDoesNotMatch_ShouldFail()
				{
					IEnumerable subject = Enumerable.Range(1, 10).Select(v => new MyBaseClass
					{
						Foo = v,
					});

					async Task Act()
						=> await That(subject).All().Are(typeof(MyClass));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is of type ThatEnumerable.All.Are.MyClass for all items,
						             but not all were

						             Not matching items:
						             [
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 1
						               },
						               (… and maybe others)
						             ]

						             Collection:
						             [
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 1
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 2
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 3
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 4
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 5
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 6
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 7
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 8
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 9
						               },
						               ThatEnumerable.All.Are.MyBaseClass {
						                 Foo = 10
						               },
						               (… and maybe others)
						             ]
						             """);
				}

				[Fact]
				public async Task WhenTypeIsNull_ShouldThrowArgumentNullException()
				{
					IEnumerable subject = Enumerable.Range(1, 10);

					async Task Act()
						=> await That(subject).All().Are(null!);

					await That(Act).Throws<ArgumentNullException>()
						.WithParamName("type").And
						.WithMessage("The type cannot be null.").AsPrefix();
				}

				[Fact]
				public async Task WhenTypeMatchesBaseType_ShouldSucceed()
				{
					IEnumerable subject = Enumerable.Range(1, 10).Select(v => new MyClass
					{
						Foo = v,
					});

					async Task Act()
						=> await That(subject).All().Are(typeof(MyBaseClass));

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenTypeMatchesExactly_ShouldSucceed()
				{
					IEnumerable subject = Enumerable.Range(1, 10).Select(v => new MyClass
					{
						Foo = v,
					});

					async Task Act()
						=> await That(subject).All().Are(typeof(MyClass));

					await That(Act).DoesNotThrow();
				}
			}
		}
	}
}
