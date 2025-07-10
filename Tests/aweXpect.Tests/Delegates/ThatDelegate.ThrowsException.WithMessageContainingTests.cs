namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class ThrowsException
	{
		public sealed class WithMessageContainingTests
		{
			[Fact]
			public async Task CanCompareCaseInsensitive()
			{
				string message = "_FOO_BAR";
				Exception exception =
					new OuterException(message, new CustomException());
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).ThrowsException()
						.WithMessageContaining("foo").IgnoringCase();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task CanUseWildcardCheck()
			{
				string message = "_foo-BAR";
				Exception exception =
					new OuterException(message, new CustomException());
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).ThrowsException()
						.WithMessageContaining("f?o-");

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ShouldCompareCaseSensitive()
			{
				string message = "FOO";
				Exception exception =
					new OuterException(message, new CustomException());
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).ThrowsException()
						.WithMessageContaining("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that Delegate
					             throws an exception with Message containing matching "foo",
					             but it did not match:
					               ↓ (actual)
					               "FOO"
					               "foo"
					               ↑ (wildcard pattern)

					             Message:
					             FOO
					             """);
			}

			[Fact]
			public async Task ShouldIgnorePrecedingText()
			{
				string message = "some text before foo";
				Exception exception =
					new OuterException(message, new CustomException());
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).ThrowsException()
						.WithMessageContaining("foo");

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ShouldIgnoreSucceedingText()
			{
				string message = "foo and some other text";
				Exception exception =
					new OuterException(message, new CustomException());
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).ThrowsException()
						.WithMessageContaining("foo");

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task ShouldIncludeExceptionType()
			{
				string message = "FOO";
				Exception exception = new CustomException(message);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Throws<CustomException>()
						.WithMessageContaining("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that Delegate
					             throws a ThatDelegate.CustomException with Message containing matching "foo",
					             but it did not match:
					               ↓ (actual)
					               "FOO"
					               "foo"
					               ↑ (wildcard pattern)

					             Message:
					             FOO
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenAwaited_ShouldReturnThrownException(string message)
			{
				Exception exception =
					new OuterException(message, new CustomException());
				void Delegate() => throw exception;

				Exception result = await That(Delegate)
					.ThrowsException().WithMessageContaining(message);

				await That(result).IsSameAs(exception);
			}

			[Theory]
			[AutoData]
			public async Task WhenExpectedIsNull_ShouldSucceed(string message)
			{
				Exception exception = new(message);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).ThrowsException()
						.WithMessageContaining(null);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMessagesAreDifferent_ShouldFail()
			{
				string actual = "expected actual text";
				string expected = "expected other text";
				Action action = () => throw new CustomException(actual);

				async Task Act()
					=> await That(action).ThrowsException().WithMessageContaining(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that action
					             throws an exception with Message containing matching "expected other text",
					             but it did not match:
					               ↓ (actual)
					               "expected actual text"
					               "expected other text"
					               ↑ (wildcard pattern)

					             Message:
					             expected actual text
					             """);
			}
		}
	}
}
