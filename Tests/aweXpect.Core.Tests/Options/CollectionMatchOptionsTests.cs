﻿using aweXpect.Options;

namespace aweXpect.Core.Tests.Options;

public class CollectionMatchOptionsTests
{
	public class EquivalenceRelationsTests
	{
		[Fact]
		public async Task ProperSubset_ShouldHaveSubsetFlag()
		{
			CollectionMatchOptions.EquivalenceRelations subject
				= CollectionMatchOptions.EquivalenceRelations.IsContainedInProperly;

			await That(subject).Should().HaveFlag(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		}

		[Fact]
		public async Task ProperSuperset_ShouldHaveSupersetFlag()
		{
			CollectionMatchOptions.EquivalenceRelations subject
				= CollectionMatchOptions.EquivalenceRelations.ContainsProperly;

			await That(subject).Should().HaveFlag(CollectionMatchOptions.EquivalenceRelations.Contains);
		}

		[Fact]
		public async Task Equivalent_ShouldNotHaveSubsetOrSupersetFlag()
		{
			CollectionMatchOptions.EquivalenceRelations subject
				= CollectionMatchOptions.EquivalenceRelations.Equivalent;

			await That(subject).Should().NotHaveFlag(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
			await That(subject).Should().NotHaveFlag(CollectionMatchOptions.EquivalenceRelations.Contains);
		}
	}
}
