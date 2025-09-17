using aweXpect.Equivalency;

// ReSharper disable UnusedMember.Local

namespace aweXpect.Tests;

public sealed partial class ThatObject
{
	public sealed partial class IsEquivalentTo
	{
		public sealed class ItIs
		{
			public sealed class Tests
			{
				[Fact]
				public async Task WhenAllConditionsFail_ShouldFail()
				{
					DummyClass subject = new()
					{
						StringValue = "foo",
						IntValue = 42,
					};
					var expected = new
					{
						StringValue = It.Is<string>().That.IsEqualTo("folly"),
						IntValue = It.Is<int>().That.IsLessThan(2),
						BoolValue = true,
					};

					async Task Act()
						=> await That(subject).IsEquivalentTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equivalent to { StringValue = is string that is equal to "folly", IntValue = is int that is less than 2, BoolValue = True },
						             but it was not:
						               Property StringValue was "foo" which differs at index 2:
						                    ↓ (actual)
						                 "foo"
						                 "folly"
						                    ↑ (expected)
						             and
						               Property IntValue was 42
						             and
						               Property BoolValue differed:
						                    Found: False
						                 Expected: True

						             Equivalency options:
						              - include public fields and properties
						             """);
				}

				[Fact]
				public async Task WhenAllConditionsMeet_ShouldSucceed()
				{
					DummyClass subject = new()
					{
						StringValue = "foo",
						IntValue = 42,
					};
					var expected = new
					{
						StringValue = It.Is<string>().That.IsNotEmpty(),
						IntValue = It.Is<int>().That.IsGreaterThan(2),
					};

					async Task Act()
						=> await That(subject).IsEquivalentTo(expected);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenAnyConditionFails_ShouldFail()
				{
					DummyClass subject = new()
					{
						StringValue = "foo",
						IntValue = 42,
					};
					var expected = new
					{
						StringValue = It.Is<string>().That.IsNotEmpty(),
						IntValue = It.Is<int>().That.IsLessThan(2),
					};

					async Task Act()
						=> await That(subject).IsEquivalentTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equivalent to { StringValue = is string that is not empty, IntValue = is int that is less than 2 },
						             but it was not:
						               Property IntValue was 42

						             Equivalency options:
						              - include public fields and properties
						             """);
				}

				/// <summary>
				///     It is not possible to determine the type of <see langword="null" />!
				/// </summary>
				[Fact]
				public async Task WhenAnyNotNullCheckAndItIsNull_ShouldFail()
				{
					DummyClass subject = new()
					{
						StringValue = null,
						NullableIntValue = null,
						IntValue = 42,
					};
					var expected = new
					{
						StringValue = It.Is<string>().That.IsEmpty(),
						NullableIntValue = It.Is<int?>().That.IsEqualTo(0),
						IntValue = It.Is<int>().That.IsGreaterThan(2),
					};

					async Task Act()
						=> await That(subject).IsEquivalentTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equivalent to { StringValue = is string that is empty, NullableIntValue = is int? that is equal to 0, IntValue = is int that is greater than 2 },
						             but it was not:
						               Property StringValue was <null>
						             and
						               Property NullableIntValue was <null>

						             Equivalency options:
						              - include public fields and properties
						             """);
				}

				[Fact]
				public async Task WhenAnyTypeDoesNotMatch_ShouldFail()
				{
					DummyClass subject = new()
					{
						StringValue = "foo",
						IntValue = 1,
					};
					var expected = new
					{
						StringValue = It.Is<DateTime>(),
						IntValue = It.Is<int>().That.IsGreaterThan(2),
					};

					async Task Act()
						=> await That(subject).IsEquivalentTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equivalent to { StringValue = is DateTime, IntValue = is int that is greater than 2 },
						             but it was not:
						               Property StringValue was string
						             and
						               Property IntValue was 1

						             Equivalency options:
						              - include public fields and properties
						             """);
				}

				[Fact]
				public async Task WhenCheckForNullAndItIsNotNull_ShouldFail()
				{
					DummyClass subject = new()
					{
						StringValue = "",
						IntValue = 42,
					};
					var expected = new
					{
						StringValue = It.Is<string>().That.IsNull(),
						IntValue = It.Is<int>().That.IsGreaterThan(2),
					};

					async Task Act()
						=> await That(subject).IsEquivalentTo(expected);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equivalent to { StringValue = is string that is null, IntValue = is int that is greater than 2 },
						             but it was not:
						               Property StringValue was ""

						             Equivalency options:
						              - include public fields and properties
						             """);
				}

				/// <summary>
				///     It is not possible to determine the type of <see langword="null" />!
				/// </summary>
				[Fact]
				public async Task WhenCheckForNullAndItIsNull_ShouldSucceed()
				{
					DummyClass subject = new()
					{
						StringValue = null,
						IntValue = 42,
					};
					var expected = new
					{
						StringValue = It.Is<string>().That.IsNull(),
						IntValue = It.Is<int>().That.IsGreaterThan(2),
					};

					async Task Act()
						=> await That(subject).IsEquivalentTo(expected);

					await That(Act).DoesNotThrow();
				}

				// ReSharper disable UnusedAutoPropertyAccessor.Local
				private sealed class DummyClass
				{
					public string? StringValue { get; set; }
					public int? NullableIntValue { get; set; }
					public int IntValue { get; set; }
					public bool BoolValue { get; set; }
				}
				// ReSharper restore UnusedAutoPropertyAccessor.Local
			}
		}
	}
}
