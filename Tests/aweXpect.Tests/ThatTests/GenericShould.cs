namespace Testably.Expectations.Tests.ThatTests;

public sealed partial class GenericShould
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
