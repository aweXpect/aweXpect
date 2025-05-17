namespace aweXpect.Internal.Tests.ThatTests.Collections;

public partial class QuantifiedCollectionResult
{
#pragma warning disable CS9113 // Parameter is unread.
	public record MyClass(int Value);

	public record SubClass(int Value) : MyClass(Value);

	public record OtherClass(int Value);
#pragma warning restore CS9113 // Parameter is unread.
}
