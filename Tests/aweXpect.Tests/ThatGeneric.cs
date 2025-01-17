namespace aweXpect.Tests;

public sealed partial class ThatGeneric
{
	private class OtherBase
	{
		// ReSharper disable once UnusedAutoPropertyAccessor.Local
		public int Value { get; set; }
	}

	private sealed class Other : OtherBase
	{
	}
}
