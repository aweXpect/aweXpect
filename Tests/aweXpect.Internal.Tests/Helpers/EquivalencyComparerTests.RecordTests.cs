﻿using aweXpect.Core;
using aweXpect.Equivalency;

namespace aweXpect.Internal.Tests.Helpers;

public sealed partial class EquivalencyComparerTests
{
	public sealed class RecordTests
	{
		[Fact]
		public async Task RecordsWithValue_WhenValuesAreDifferent_ShouldNotBeConsideredEqual()
		{
			ARecordWithValue actual = new(1);
			ARecordWithValue expected = new(2);
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = await sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", ExpectationGrammars.None, actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not:
			                                Property Value differed:
			                                     Found: 1
			                                  Expected: 2
			                              """);
		}

		[Fact]
		public async Task RecordsWithValue_WhenValuesAreSame_ShouldBeConsideredEqual()
		{
			ARecordWithValue actual = new(1);
			ARecordWithValue expected = new(1);
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = await sut.AreConsideredEqual(actual, expected);

			await That(result).IsTrue();
		}

		[Fact]
		public async Task WithEmptyRecords_WhenTypesAreDifferent_ShouldNotBeConsideredEqual()
		{
			SomeRecord actual = new();
			SomeOtherRecord expected = new();
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = await sut.AreConsideredEqual(actual, expected);
			string failure = sut.GetExtendedFailure("it", ExpectationGrammars.None, actual, expected);

			await That(result).IsFalse();
			await That(failure).IsEqualTo("""
			                              it was not:
			                                It differed:
			                                     Found: SomeRecord { }
			                                  Expected: SomeOtherRecord { }
			                              """);
		}

		[Fact]
		public async Task WithEmptyRecords_WhenTypesAreSame_ShouldBeConsideredEqual()
		{
			SomeRecord actual = new();
			SomeRecord expected = new();
			EquivalencyComparer sut = new(new EquivalencyOptions());

			bool result = await sut.AreConsideredEqual(actual, expected);

			await That(result).IsTrue();
		}

		private record ARecordWithValue(int Value);

		private record SomeRecord;

		private record SomeOtherRecord;
	}
}
