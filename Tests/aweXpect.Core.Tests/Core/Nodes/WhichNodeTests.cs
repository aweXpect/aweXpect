﻿namespace aweXpect.Core.Tests.Core.Nodes;

public sealed class WhichNodeTests
{
	[Fact]
	public async Task WhichCreatesGoodMessage()
	{
		Dummy subject = new() { Inner = new Dummy.Nested { Id = 1 }, Value = "foo" };

		async Task Act()
			=> await That(subject).Should().Be<Dummy>()
				.Which(p => p.Value, e => e.Should().Be("bar"));

		await That(Act).Should().Throw<XunitException>()
			.WithMessage("""
			             Expected subject to
			             be type Dummy which .Value should be equal to "bar",
			             but .Value was "foo" which differs at index 0:
			                ↓ (actual)
			               "foo"
			               "bar"
			                ↑ (expected)
			             """);
	}

	private sealed class Dummy
	{
		public Nested? Inner { get; set; }
		public string? Value { get; set; }

		public class Nested
		{
#pragma warning disable CS0649
			public int Field;
#pragma warning restore CS0649
			public int Id { get; set; }

			public int Method() => Id + 1;
		}
	}
}
