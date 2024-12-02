using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class BeTests
	{
		[Fact]
		public async Task AnyOrder_WithCollectionInDifferentOrder_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 3, 2]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrder_WithSameCollection_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrder_WithDuplicatesInSubject_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 2, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it contained item 1 at index 1 that was not expected
				             """);
		}

		[Fact]
		public async Task AnyOrder_EmptyCollection_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it lacked 3 of 3 expected items: [1, 2, 3]
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithDuplicatesInExpected_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it lacked 1 of 4 expected items: [1]
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithDuplicatesAtEndOfSubject_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it contained item 3 at index 3 that was not expected
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithDuplicatesAtEndOfExpected_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 2, 3, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it lacked 1 of 4 expected items: [3]
				             """);
		}


		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithCollectionInDifferentOrder_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 3, 2]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithSameCollection_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithDuplicatesInSubject_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 2, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_EmptyCollection_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([]);
			int[] expected = [1, 1, 2, 3, 1];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
				             but it lacked 3 of 3 expected items: [1, 2, 3]
				             """);
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_EmptyCollectionWithDuplicatesInExpected_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([]);
			int[] expected = [1, 1, 2];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
				             but it lacked 2 of 2 expected items: [1, 2]
				             """);
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithDuplicatesInExpected_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtEndOfSubject_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtEndOfExpected_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 2, 3, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrder_WithCollectionInDifferentOrder_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 3, 2]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it contained item 3 at index 1 instead of 2
				             """);
		}

		[Fact]
		public async Task SameOrder_WithSameCollection_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrder_WithDuplicatesInSubject_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 2, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it contained item 1 at index 1 instead of 2
				             """);
		}

		[Fact]
		public async Task SameOrder_EmptyCollection_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it lacked 3 of 3 expected items: [1, 2, 3]
				             """);
		}

		[Fact]
		public async Task SameOrder_WithDuplicatesInExpected_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it contained item 2 at index 1 instead of 1
				             """);
		}

		[Fact]
		public async Task SameOrder_WithDuplicatesAtEndOfSubject_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it contained item 3 at index 3 that was not expected
				             """);
		}

		[Fact]
		public async Task SameOrder_WithDuplicatesAtEndOfExpected_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 2, 3, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it lacked 1 of 4 expected items: [3]
				             """);
		}


		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithCollectionInDifferentOrder_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 3, 2]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it contained item 3 at index 1 instead of 2
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithSameCollection_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithDuplicatesInSubject_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 2, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_EmptyCollection_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it lacked 3 of 3 expected items: [1, 2, 3]
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_EmptyCollectionWithDuplicatesInExpected_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([]);
			int[] expected = [1, 1, 2];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it lacked 2 of 2 expected items: [1, 2]
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithDuplicatesInExpected_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtEndOfSubject_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3, 3]);
			int[] expected = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtEndOfExpected_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);
			int[] expected = [1, 2, 3, 3];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}
	}
}
