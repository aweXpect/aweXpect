namespace aweXpect.Core.Tests.Core;

public sealed class ResultContextsTests
{
	[Fact]
	public async Task Add_ShouldAddContext()
	{
		ResultContexts sut = new();

		sut.Add(new ResultContext("foo", "bar"));

		await That(sut).HasSingle().Which.IsEquivalentTo(new
		{
			Title = "foo",
		});
	}

	[Fact]
	public async Task AddMultiple_ShouldAddContext()
	{
		ResultContexts sut = new();

		sut.Add(new ResultContext("foo", "1"));
		sut.Add(new ResultContext("foo", "2"));
		sut.Add(new ResultContext("foo", "2"));
		sut.Add(new ResultContext("bar", "3"));
		sut.Add(new ResultContext("foo", "4"));

		await That(sut).HasCount().EqualTo(5);
	}

	[Fact]
	public async Task Clear_ShouldRemoveAllContexts()
	{
		ResultContexts sut =
		[
			new ResultContext("foo", "bar"),
			new ResultContext("foo", "bar"),
		];

		sut.Clear();

		await That(sut).IsEmpty();
	}

	[Fact]
	public async Task Remove_WithPredicate_ShouldRemoveAllMatchingContexts()
	{
		ResultContexts sut =
		[
			new ResultContext("foo", "bar"),
			new ResultContext("bar", "baz"),
			new ResultContext("foo", "bar"),
		];

		sut.Remove(c => c.Title == "foo");

		await That(sut).HasSingle().Which.IsEquivalentTo(new
		{
			Title = "bar",
		});
	}

	[Fact]
	public async Task Remove_WithTitle_ShouldRemoveAllMatchingContexts()
	{
		ResultContexts sut =
		[
			new ResultContext("foo", "bar"),
			new ResultContext("bar", "baz"),
			new ResultContext("foo", "bar"),
		];

		sut.Remove("foo");

		await That(sut).HasSingle().Which.IsEquivalentTo(new
		{
			Title = "bar",
		});
	}

	[Fact]
	public async Task ShouldInitializeEmpty()
	{
		ResultContexts sut = new();

		await That(sut).IsEmpty();
	}
}
