using aweXpect.Options;

namespace aweXpect.Core.Tests.Options;

public class CollectionIndexOptionsTests
{
	[Fact]
	public async Task DefaultMatch_FromEnd_ShouldThrowNotSupportedException()
	{
		CollectionIndexOptions sut = new();
		CollectionIndexOptions.IMatchFromBeginning match = (CollectionIndexOptions.IMatchFromBeginning)sut.Match;

		void Act()
			=> _ = match.FromEnd();

		await That(Act).Throws<NotSupportedException>()
			.WithMessage("You have to specify a dedicated index condition first.");
	}

	[Fact]
	public async Task DefaultMatch_ShouldHaveExpectedReturnValues()
	{
		CollectionIndexOptions sut = new();

		await That(sut.Match.GetDescription()).IsEmpty();
		await That(sut.Match.OnlySingleIndex()).IsFalse();
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(3)]
	[InlineData(-1)]
	public async Task DefaultMatch_ShouldMatchAllIndices(int index)
	{
		CollectionIndexOptions sut = new();
		CollectionIndexOptions.IMatchFromBeginning match = (CollectionIndexOptions.IMatchFromBeginning)sut.Match;

		await That(match.MatchesIndex(index)).IsTrue();
	}
}
