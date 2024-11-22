namespace Testably.Expectations.Tests.ThatTests.Collections;

public partial class QuantifiedCollectionResult
{
	#pragma warning disable CS9113 // Parameter is unread.
	public class MyClass(int Value);

	public class SubClass(int value) : MyClass(value);

	public class OtherClass(int Value);
	#pragma warning restore CS9113 // Parameter is unread.
}
