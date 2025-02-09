using aweXpect.Equivalency;

namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed class IsEquivalentTo
	{
		public sealed class Tests
		{
			[Fact]
			public async Task BasicObjects_ShouldBeEquivalent()
			{
				OuterClass subject = new()
				{
					Value = "Foo",
				};
				OuterClass expected = new()
				{
					Value = "Foo",
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task MismatchedObjects_ShouldNotBeEquivalent()
			{
				OuterClass subject = new();
				OuterClass expected = new()
				{
					Value = "Foo",
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Property Value was <null> instead of "Foo"
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
							Collection = ["1", "2", "3"],
						},
					},
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
							Collection = ["1", "2", "3"],
						},
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).DoesNotThrow();
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
							Collection = ["1", "2", "3"],
						},
					},
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
							Collection = ["1", "2", "3", "4"],
						},
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Element Inner.Inner.Collection[3] was missing "4"
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
							Collection = ["1", "2", "3"],
						},
					},
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
							Collection = ["1", "2", "3", "4"],
						},
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected, o => o.IgnoringMember("Inner.Inner.Collection[3]"));

				await That(Act).DoesNotThrow();
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
							Collection = ["1", "2", "3"],
						},
					},
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
							Collection = ["1", "2", "3", "4"],
						},
					},
				};

				async Task Act()
					=> await That(subject)
						.IsEquivalentTo(expected, o => o.IgnoringMember("Inner.Inner.Collection.[3]"));

				await That(Act).ThrowsException()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Element Inner.Inner.Collection[3] was missing "4"
					             and
					               Property Inner.Inner.Value differed:
					                    Found: "Baz"
					                 Expected: "Bart"
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
						Inner = new InnerClass
						{
							Value = "Baz",
						},
					},
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
						},
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).DoesNotThrow();
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
						Inner = new InnerClass(),
					},
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
						},
					},
				};

				async Task Act()
					=> await That(subject).IsEquivalentTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equivalent to expected,
					             but it was not:
					               Property Inner.Inner.Value was <null> instead of "Baz"
					             """);
			}
		}
	}
}
