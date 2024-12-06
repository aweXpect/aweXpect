using aweXpect.Options;

namespace aweXpect.Tests.Options;

public class CollectionMatchOptionsTests
{
	public class EquivalenceRelationsTests
	{
		[Fact]
		public async Task ProperSubset_ShouldHaveSubsetFlag()
		{
			CollectionMatchOptions.EquivalenceRelations subject
				= CollectionMatchOptions.EquivalenceRelations.ProperSubset;

			await That(subject).Should().HaveFlag(CollectionMatchOptions.EquivalenceRelations.Subset);
		}

		[Fact]
		public async Task ProperSuperset_ShouldHaveSupersetFlag()
		{
			CollectionMatchOptions.EquivalenceRelations subject
				= CollectionMatchOptions.EquivalenceRelations.ProperSuperset;

			await That(subject).Should().HaveFlag(CollectionMatchOptions.EquivalenceRelations.Superset);
		}

		[Fact]
		public async Task Equivalent_ShouldNotHaveSubsetOrSupersetFlag()
		{
			CollectionMatchOptions.EquivalenceRelations subject
				= CollectionMatchOptions.EquivalenceRelations.Equivalent;

			await That(subject).Should().NotHaveFlag(CollectionMatchOptions.EquivalenceRelations.Subset);
			await That(subject).Should().NotHaveFlag(CollectionMatchOptions.EquivalenceRelations.Superset);
		}
	}
}
