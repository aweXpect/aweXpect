using System.Collections.Generic;
using System.Linq;

namespace aweXpect.Tests;

/// <summary>
///     Locks in that callbacks taking <c>Action&lt;IThatSubject&lt;T&gt;&gt;</c> allow nested
///     <c>.Is&lt;TType&gt;()</c> calls. Regression tests for the change that switched the
///     drill-in callbacks (<c>Whose</c>, <c>Which</c>, <c>For</c>, <c>CompliesWith</c>,
///     <c>HasItemThat</c>, <c>HasInner*</c>, <c>Elements.ComplyWith</c>, and the per-item
///     callbacks in <c>IsEqualTo</c>) from <c>IThat&lt;T&gt;</c> to <c>IThatSubject&lt;T&gt;</c>.
/// </summary>
public sealed class IThatSubjectCallbacksTests
{
	[Fact]
	public async Task Whose_AllowsNestedIs()
	{
		Outer subject = new()
		{
			Item = new Derived
			{
				Name = "foo",
			},
		};

		async Task Act()
			=> await That(subject).Is<Outer>()
				.Whose(o => o.Item, it => it.Is<Derived>()
					.Whose(d => d.Name, it => it.IsEqualTo("foo")));

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task AndWhose_AllowsNestedIs()
	{
		Outer subject = new()
		{
			Item = new Derived
			{
				Name = "foo",
			},
			Other = new Derived
			{
				Name = "bar",
			},
		};

		async Task Act()
			=> await That(subject).Is<Outer>()
				.Whose(o => o.Item, it => it.Is<Derived>().Whose(d => d.Name, it => it.IsEqualTo("foo")))
				.AndWhose(o => o.Other, it => it.Is<Derived>().Whose(d => d.Name, it => it.IsEqualTo("bar")));

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task Whose_AllowsNestedIs_FailsWhenInnerTypeMismatches()
	{
		Outer subject = new()
		{
			Item = new OtherDerived(),
		};

		async Task Act()
			=> await That(subject).Is<Outer>()
				.Whose(o => o.Item, it => it.Is<Derived>());

		await That(Act).Throws<XunitException>();
	}

	[Fact]
	public async Task For_AllowsNestedIs()
	{
		Outer subject = new()
		{
			Item = new Derived
			{
				Name = "foo",
			},
		};

		async Task Act()
			=> await That(subject).For(o => o.Item, it => it.Is<Derived>()
				.Whose(d => d.Name, it => it.IsEqualTo("foo")));

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task CompliesWith_AllowsNestedIs()
	{
		Base subject = new Derived
		{
			Name = "foo",
		};

		async Task Act()
			=> await That(subject).CompliesWith(it => it.Is<Derived>()
				.Whose(d => d.Name, it => it.IsEqualTo("foo")));

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task HasItemThat_AllowsNestedIs()
	{
		List<Base> subject =
		[
			new Derived
			{
				Name = "foo",
			},
		];

		async Task Act()
			=> await That(subject).HasItemThat(it => it.Is<Derived>());

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task ElementsComplyWith_AllowsNestedIs()
	{
		List<Base> subject =
		[
			new Derived
			{
				Name = "foo",
			},
			new Derived
			{
				Name = "bar",
			},
		];

		async Task Act()
			=> await That(subject).All().ComplyWith(it => it.Is<Derived>());

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task IsEqualTo_PerItemCallback_AllowsNestedIs()
	{
		List<Base> subject =
		[
			new Derived
			{
				Name = "foo",
			},
			new Derived
			{
				Name = "bar",
			},
		];

		async Task Act()
			=> await That(subject).IsEqualTo(
			[
				it => it.Is<Derived>().Whose(d => d!.Name, it => it.IsEqualTo("foo")),
				it => it.Is<Derived>().Whose(d => d!.Name, it => it.IsEqualTo("bar")),
			]);

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task HasInnerException_AllowsNestedIs()
	{
		Exception subject = new InvalidOperationException(
			"outer",
			new InvalidCastException("inner"));

		async Task Act()
			=> await That(subject).HasInnerException(it => it.Is<InvalidCastException>()
				.Whose(e => e.Message, it => it.IsEqualTo("inner")));

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task ThatDelegateThrows_Whose_AllowsNestedIs()
	{
		void Throwing()
			=> throw new MyException(new Derived
			{
				Name = "foo",
			});

		async Task Act()
			=> await That(Throwing).Throws<MyException>()
				.Whose(e => e.Payload, it => it.Is<Derived>()
					.Whose(d => d.Name, it => it.IsEqualTo("foo")));

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task ThatDelegateThrows_WithInnerException_AllowsNestedIs()
	{
		void Throwing()
			=> throw new InvalidOperationException(
				"outer",
				new InvalidCastException("inner"));

		async Task Act()
			=> await That(Throwing).Throws<InvalidOperationException>()
				.WithInnerException(it => it.Is<InvalidCastException>()
					.Whose(e => e!.Message, it => it.IsEqualTo("inner")));

		await That(Act).DoesNotThrow();
	}

	private class Base;

	private sealed class Derived : Base
	{
		public string Name { get; init; } = "";
	}

	private sealed class OtherDerived : Base;

	private sealed class Outer
	{
		public Base Item { get; init; } = new Derived();
		public Base Other { get; init; } = new Derived();
	}

	private sealed class MyException(Base payload) : Exception
	{
		public Base Payload { get; } = payload;
	}
}
