namespace aweXpect.Tests;

public class MyClass(int value = 0, string stringValue = "") : MyBaseClass(value)
{
	public string StringValue { get; } = stringValue;
}
