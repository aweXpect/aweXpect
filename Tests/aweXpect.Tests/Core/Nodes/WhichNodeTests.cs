namespace Testably.Expectations.Tests.Core.Nodes;

public sealed class WhichNodeTests
{
	[Fact]
	public async Task WhichCreatesGoodMessage()
	{
		Dummy subject = new()
		{
			Inner = new Dummy.Nested { Id = 1 },
			Value = "foo"
		};

		async Task Act()
			=> await That(subject).Should().Be<Dummy>()
				.Which(p => p.Value).Should(e => e.Be("bar"));

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
			public int Id { get; set; }
			#pragma warning disable CS0649
			public int Field;
			#pragma warning restore CS0649

			public int Method() => Id + 1;
		}
	}
}
