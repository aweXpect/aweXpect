namespace aweXpect.Tests.TestHelpers.Models;

public class PocoWithoutDefaultConstructor(int value)
{
	public int Id { get; } = value;
}
