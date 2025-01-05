#if NET8_0_OR_GREATER
using System.Text.Json;

namespace aweXpect.Tests.Json;

public sealed partial class JsonElementShould
{
	public sealed class BeObject
	{
		public sealed class Tests
		{
			[Fact]
			public async Task Dummy1()
			{
				JsonElement subject = FromString("[]");

				async Task Act()
					=> await That(subject).Should().BeObject(o => o
						.With("foo").Matching("foo").And
						.With("foo2").Matching(2.5).And
						.With("foo3").Matching(true).And
						.With("bar").AnObject(p => p
							.With("baz").AnArray(a => a.At(0).Matching(2))));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task Dummy2()
			{
				JsonElement subject = FromString("[]");

				async Task Act()
					=> await That(subject).Should().BeArray(a => a
						.With(5).Elements()
						.WithObjects(
							o => o
								.With(5).Properties()
								.With("foo").AnArray(a => a
									.With(5).Elements()
									.At(0).Matching(2).And
									.At(4).Matching("foo")),
							o => o.With(5).Properties(),
							o => o
								.With("foo").Matching("bar"),
							o => o
								.With("foo").Matching(2).And
								.With("bar").Matching(5))
						.InAnyOrder());

				await That(Act).Should().NotThrow();
			}
		}
	}
}
#endif
