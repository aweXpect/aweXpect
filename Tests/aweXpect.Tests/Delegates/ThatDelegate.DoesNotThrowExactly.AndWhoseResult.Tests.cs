﻿namespace aweXpect.Tests;

public sealed partial class ThatDelegate
{
	public sealed partial class DoesNotThrowExactly
	{
		public sealed class AndWhoseResult
		{
			public sealed class GenericTests
			{
				[Fact]
				public async Task WhenDelegateThrowsExpectedException_ShouldFail()
				{
					Func<int> @delegate = () => throw new CustomException();

					async Task Act()
						=> await That(@delegate).DoesNotThrowExactly<CustomException>().AndWhoseResult.IsEqualTo(5);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that @delegate
						             does not throw exactly a ThatDelegate.CustomException and whose result is equal to 5,
						             but it did throw a ThatDelegate.CustomException:
						               WhenDelegateThrowsExpectedException_ShouldFail
						             """);
				}

				[Fact]
				public async Task WhenDelegateThrowsOtherException_ShouldFail()
				{
					Func<int> @delegate = () => throw new CustomException();

					async Task Act()
						=> await That(@delegate).DoesNotThrowExactly<OtherException>().AndWhoseResult.IsEqualTo(5);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that @delegate
						             does not throw exactly a ThatDelegate.OtherException and whose result is equal to 5,
						             but it did throw a ThatDelegate.CustomException:
						               WhenDelegateThrowsOtherException_ShouldFail
						             """);
				}

				[Theory]
				[AutoData]
				public async Task WhenReturnValueDoesNotMatch_ShouldFail(int value)
				{
					Func<int> @delegate = () => value;

					async Task Act() => await That(@delegate).DoesNotThrowExactly<CustomException>().AndWhoseResult
						.IsEqualTo(value + 1);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that @delegate
						              does not throw exactly a ThatDelegate.CustomException and whose result is equal to {value + 1},
						              but the result was {value} which differs by -1
						              """);
				}

				[Theory]
				[AutoData]
				public async Task WhenReturnValueMatches_ShouldSucceed(int value)
				{
					Func<int> @delegate = () => value;

					await That(@delegate).DoesNotThrowExactly<CustomException>().AndWhoseResult.IsEqualTo(value);
				}
			}

			public sealed class TypeTests
			{
				[Fact]
				public async Task WhenDelegateThrowsExpectedException_ShouldFail()
				{
					Func<int> @delegate = () => throw new CustomException();

					async Task Act()
						=> await That(@delegate).DoesNotThrowExactly(typeof(CustomException)).AndWhoseResult.IsEqualTo(5);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that @delegate
						             does not throw exactly a ThatDelegate.CustomException and whose result is equal to 5,
						             but it did throw a ThatDelegate.CustomException:
						               WhenDelegateThrowsExpectedException_ShouldFail
						             """);
				}

				[Fact]
				public async Task WhenDelegateThrowsOtherException_ShouldFail()
				{
					Func<int> @delegate = () => throw new CustomException();

					async Task Act()
						=> await That(@delegate).DoesNotThrowExactly(typeof(OtherException)).AndWhoseResult.IsEqualTo(5);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that @delegate
						             does not throw exactly a ThatDelegate.OtherException and whose result is equal to 5,
						             but it did throw a ThatDelegate.CustomException:
						               WhenDelegateThrowsOtherException_ShouldFail
						             """);
				}

				[Theory]
				[AutoData]
				public async Task WhenReturnValueDoesNotMatch_ShouldFail(int value)
				{
					Func<int> @delegate = () => value;

					async Task Act() => await That(@delegate).DoesNotThrowExactly(typeof(CustomException)).AndWhoseResult
						.IsEqualTo(value + 1);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that @delegate
						              does not throw exactly a ThatDelegate.CustomException and whose result is equal to {value + 1},
						              but the result was {value} which differs by -1
						              """);
				}

				[Theory]
				[AutoData]
				public async Task WhenReturnValueMatches_ShouldSucceed(int value)
				{
					Func<int> @delegate = () => value;

					await That(@delegate).DoesNotThrowExactly(typeof(CustomException)).AndWhoseResult.IsEqualTo(value);
				}
			}
		}
	}
}
