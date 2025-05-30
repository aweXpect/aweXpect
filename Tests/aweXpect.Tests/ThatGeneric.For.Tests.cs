﻿namespace aweXpect.Tests;

public sealed partial class ThatGeneric
{
	public sealed class For
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(true, true, true)]
			[InlineData(true, false, false)]
			[InlineData(false, true, false)]
			[InlineData(false, false, false)]
			public async Task AndCombination_ShouldVerifyAllExpectations(bool a, bool b, bool expectSuccess)
			{
				MyCombinationClass subject = new()
				{
					A = a,
					B = b,
				};

				async Task Act()
					=> await That(subject)
						.For(o => o.A, v => v.IsTrue()).And
						.For(o => o.B, v => v.IsTrue());

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage($"""
					              Expected that subject
					              for .A is True and for .B is True,
					              but {(a ? "" : ".A was False")}{(!a && !b ? " and " : "")}{(b ? "" : ".B was False")}
					              """);
			}

			[Theory]
			[InlineData(true, true, true)]
			[InlineData(true, false, true)]
			[InlineData(false, true, true)]
			[InlineData(false, false, false)]
			public async Task OrCombination_ShouldVerifyAnyExpectations(bool a, bool b, bool expectSuccess)
			{
				MyCombinationClass subject = new()
				{
					A = a,
					B = b,
				};

				async Task Act()
					=> await That(subject)
						.For(o => o.A, v => v.IsTrue()).Or
						.For(o => o.B, v => v.IsTrue());

				await That(Act).Throws<XunitException>().OnlyIf(!expectSuccess)
					.WithMessage("""
					             Expected that subject
					             for .A is True or for .B is True,
					             but .A was False and .B was False
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenConditionIsNotSatisfied_ShouldFail(int value)
			{
				int expectedValue = value + 1;
				MyClass subject = new()
				{
					Value = value,
				};

				async Task Act()
					=> await That(subject)
						.For(o => o.Value, v => v.IsEqualTo(expectedValue));

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              for .Value is equal to {Formatter.Format(expectedValue)},
					              but .Value was {Formatter.Format(value)} which differs by -1
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenConditionIsSatisfied_ShouldSucceed(int value)
			{
				MyClass subject = new()
				{
					Value = value,
				};

				async Task Act()
					=> await That(subject).For(o => o.Value, v => v.IsEqualTo(value));

				await That(Act).DoesNotThrow();
			}

			private sealed class MyClass
			{
				public int Value { get; set; }
			}

			private sealed class MyCombinationClass
			{
				public bool A { get; set; }
				public bool B { get; set; }
			}
		}
	}
}
