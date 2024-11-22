namespace Testably.Expectations.Tests.ThatTests.Objects;

public sealed partial class ObjectShould
{
	public sealed partial class Be
	{
		public sealed class EquivalentTests
		{
			[Fact]
			public async Task BasicObjects_ShouldBeEquivalent()
			{
				OuterClass subject = new() { Value = "Foo" };
				OuterClass expected = new() { Value = "Foo" };

				async Task Act()
					=> await That(subject).Should().Be(expected).Equivalent();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task MismatchedObjects_ShouldNotBeEquivalent()
			{
				OuterClass subject = new();
				OuterClass expected = new() { Value = "Foo" };

				async Task Act()
					=> await That(subject).Should().Be(expected).Equivalent();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be equivalent to expected,
					             but Property Value did not match:
					               Expected: "Foo"
					               Received: <null>
					             """);
			}

			[Fact]
			public async Task ObjectsWithNestedEnumerableMatches_ShouldBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3"]
						}
					}
				};
				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3"]
						}
					}
				};

				async Task Act()
					=> await That(subject).Should().Be(expected).Equivalent();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task ObjectsWithNestedEnumerableMismatch_ShouldNotBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3"]
						}
					}
				};

				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3", "4"]
						}
					}
				};

				async Task Act()
					=> await That(subject).Should().Be(expected).Equivalent();

				await That(Act).Should().ThrowException()
					.WithMessage("""
					             Expected subject to
					             be equivalent to expected,
					             but EnumerableItem Inner.Inner.Collection.[3] did not match:
					               Expected: "4"
					               Received: <null>
					             """);
			}

			[Fact]
			public async Task
				ObjectsWithNestedEnumerableMismatch_WithIgnoreRule_ShouldBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3"]
						}
					}
				};

				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3", "4"]
						}
					}
				};

				async Task Act()
					=> await That(subject).Should().Be(expected)
						.Equivalent(o => o.IgnoringMember("Inner.Inner.Collection.[3]"));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task
				ObjectsWithNestedEnumerableMismatch_WithIncorrectIgnoreRule_ShouldFail()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Baz",
							Collection = ["1", "2", "3"]
						}
					}
				};

				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass
						{
							Value = "Bart",
							Collection = ["1", "2", "3", "4"]
						}
					}
				};

				async Task Act()
					=> await That(subject).Should().Be(expected)
						.Equivalent(o => o.IgnoringMember("Inner.Inner.Collection.[3]"));

				await That(Act).Should().ThrowException()
					.WithMessage("""
					             Expected subject to
					             be equivalent to expected,
					             but it was z
					             """);
			}

			[Fact]
			public async Task ObjectsWithNestedMatches_ShouldBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass { Value = "Baz" }
					}
				};
				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass { Value = "Baz" }
					}
				};

				async Task Act()
					=> await That(subject).Should().Be(expected).Equivalent();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task ObjectsWithNestedMismatch_ShouldNotBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass()
					}
				};
				OuterClass expected = new()
				{
					Value = "Foo",
					Inner = new InnerClass
					{
						Value = "Bar",
						Inner = new InnerClass { Value = "Baz" }
					}
				};

				async Task Act()
					=> await That(subject).Should().Be(expected).Equivalent();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be equivalent to expected,
					             but Property Inner.Inner.Value did not match:
					               Expected: "Baz"
					               Received: <null>
					             """);
			}
		}
	}
}
