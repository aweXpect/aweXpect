﻿namespace aweXpect.Tests.Delegates;

public sealed partial class DelegateShould
{
	public sealed partial class ThrowException
	{
		public sealed class WithMessageTests
		{
			[Fact]
			public async Task CanCompareCaseInsensitive()
			{
				string message = "FOO";
				Exception exception =
					new OuterException(message, new CustomException());
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Should().ThrowException()
						.WithMessage("foo").IgnoringCase();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task CanUseWildcardCheck()
			{
				string message = "foo-bar";
				Exception exception =
					new OuterException(message, new CustomException());
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Should().ThrowException()
						.WithMessage("foo*").AsWildcard();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task ShouldCompareCaseSensitive()
			{
				string message = "FOO";
				Exception exception =
					new OuterException(message, new CustomException());
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Should().ThrowException()
						.WithMessage("foo");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected Delegate to
					             throw an exception with Message equal to "foo",
					             but it was "FOO" which differs at index 0:
					                ↓ (actual)
					               "FOO"
					               "foo"
					                ↑ (expected)
					             """);
			}

			[Fact]
			public async Task ShouldIncludeExceptionType()
			{
				string message = "FOO";
				Exception exception = new CustomException(message);
				void Delegate() => throw exception;

				async Task Act()
					=> await That(Delegate).Should().Throw<CustomException>()
						.WithMessage("foo");

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected Delegate to
					             throw a CustomException with Message equal to "foo",
					             but it was "FOO" which differs at index 0:
					                ↓ (actual)
					               "FOO"
					               "foo"
					                ↑ (expected)
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
					.Should().ThrowException().WithMessage(message);

				await That(result).Should().BeSameAs(exception);
			}

			[Fact]
			public async Task WhenMessagesAreDifferent_ShouldFail()
			{
				string actual = "actual text";
				string expected = "expected other text";
				Action action = () => throw new CustomException(actual);

				async Task Act()
					=> await That(action).Should().ThrowException().WithMessage(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected action to
					             throw an exception with Message equal to "expected other text",
					             but it was "actual text" which differs at index 0:
					                ↓ (actual)
					               "actual text"
					               "expected other text"
					                ↑ (expected)
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenMessagesAreSame_ShouldSucceed(string message)
			{
				Exception subject = new(message);

				async Task Act()
					=> await That(subject).Should().HaveMessage(message);

				await That(Act).Should().NotThrow();
			}
		}
	}
}
