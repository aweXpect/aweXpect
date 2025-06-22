namespace aweXpect.Core.Tests.Core;

public sealed class MemberAccessorTests
{
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
}
