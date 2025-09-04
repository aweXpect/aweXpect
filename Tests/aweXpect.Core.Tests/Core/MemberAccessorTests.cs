﻿namespace aweXpect.Core.Tests.Core;

public sealed class MemberAccessorTests
{
	[Theory]
	[InlineData(".Length ", true)]
	[InlineData(".SomethingElse ", false)]
	public async Task Equals_ShouldCompareStringRepresentation(string otherStringRepresentation, bool expectedResult)
	{
		MemberAccessor<string, int> sut = MemberAccessor<string, int>.FromExpression(x => x.Length);
		MemberAccessor<string, int> other =
			MemberAccessor<string, int>.FromFunc(x => x.Length, otherStringRepresentation);

		bool result = sut.Equals(other);

		await That(result).IsEqualTo(expectedResult);
	}

	[Fact]
	public async Task Equals_ToNull_ShouldBeFalse()
	{
		MemberAccessor<string, int> sut = MemberAccessor<string, int>.FromExpression(x => x.Length);

		bool result = sut.Equals(null);

		await That(result).IsFalse();
	}

	[Fact]
	public async Task Equals_ToOtherObject_ShouldBeFalse()
	{
		MemberAccessor<string, int> sut = MemberAccessor<string, int>.FromExpression(x => x.Length);
		object other = MemberAccessor<string, string>.FromExpression(x => x);

		bool result = sut.Equals(other);

		await That(result).IsFalse();
	}

	[Fact]
	public async Task FromExpression_ShouldCompileExpression()
	{
		MemberAccessor<string, int> subject = MemberAccessor<string, int>
			.FromExpression(x => x.Length);

		await That(subject.AccessMember("foo")).IsEqualTo(3);
	}

	[Fact]
	public async Task FromExpression_ShouldGetMemberPath()
	{
		MemberAccessor<string, int> subject = MemberAccessor<string, int>
			.FromExpression(x => x.Length);

		await That(subject.ToString()).IsEqualTo(".Length ");
	}

	[Theory]
	[InlineData("Foo")]
	[InlineData("  x => x.Foo")]
	[InlineData("x => x.Foo  ")]
	public async Task FromFunc_ShouldKeepNameUnchanged(string expression)
	{
		MemberAccessor<string, int> subject = MemberAccessor<string, int>
			.FromFunc(x => x.Length, expression);

		await That(subject.ToString()).IsEqualTo(expression);
	}

	[Theory]
	[InlineData("Foo", "Foo ")]
	[InlineData("x => x.Foo", ".Foo ")]
	[InlineData("  x => x.Foo", ".Foo ")]
	[InlineData("x => x.Foo  ", ".Foo ")]
	[InlineData("itIs => itIs.Foo  ", ".Foo ")]
	public async Task FromFuncAsMemberAccessor_ShouldTryToExtractMemberAccessor(string expression, string expected)
	{
		MemberAccessor<string, int> subject = MemberAccessor<string, int>
			.FromFuncAsMemberAccessor(x => x.Length, expression);

		await That(subject.ToString()).IsEqualTo(expected);
	}

	[Fact]
	public async Task GetHashCode_DifferentConstraint_ShouldNotBeEqual()
	{
		MemberAccessor<string, int> sut1 = MemberAccessor<string, int>.FromFunc(x => x.Length, "foo");
		MemberAccessor<string, int> sut2 = MemberAccessor<string, int>.FromFunc(x => x.Length, "bar");

		await That(sut1.GetHashCode()).IsNotEqualTo(sut2.GetHashCode());
	}

	[Fact]
	public async Task GetHashCode_SameConstraint_ShouldBeEqual()
	{
		MemberAccessor<string, int> sut1 = MemberAccessor<string, int>.FromFunc(x => x.Length, "foo");
		MemberAccessor<string, int> sut2 = MemberAccessor<string, int>.FromFunc(x => x.Length, "foo");

		await That(sut1.GetHashCode()).IsEqualTo(sut2.GetHashCode());
	}
}
