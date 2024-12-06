using aweXpect.Options;

namespace aweXpect.Tests.Options;

public class CollectionMatchOptionsTests
{
	public class EquivalenceRelationTests
	{
		[Fact]
		public async Task ProperSubset_ShouldHaveSubsetFlag()
		{
			CollectionMatchOptions.EquivalenceRelation subject
				= CollectionMatchOptions.EquivalenceRelation.ProperSubset;

			await That(subject).Should().HaveFlag(CollectionMatchOptions.EquivalenceRelation.Subset);
		}

		[Fact]
		public async Task ProperSuperset_ShouldHaveSupersetFlag()
		{
			CollectionMatchOptions.EquivalenceRelation subject
				= CollectionMatchOptions.EquivalenceRelation.ProperSuperset;

			await That(subject).Should().HaveFlag(CollectionMatchOptions.EquivalenceRelation.Superset);
		}

		[Fact]
		public async Task Equivalent_ShouldNotHaveSubsetOrSupersetFlag()
		{
			CollectionMatchOptions.EquivalenceRelation subject
				= CollectionMatchOptions.EquivalenceRelation.Equivalent;

			await That(subject).Should().NotHaveFlag(CollectionMatchOptions.EquivalenceRelation.Subset);
			await That(subject).Should().NotHaveFlag(CollectionMatchOptions.EquivalenceRelation.Superset);
		}
	}
}
