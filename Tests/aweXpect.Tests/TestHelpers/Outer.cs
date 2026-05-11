namespace aweXpect.Tests;

public sealed class Outer
{
	public Base Item { get; set; } = new Derived();
	public Base Other { get; set; } = new Derived();
}
