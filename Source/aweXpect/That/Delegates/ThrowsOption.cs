// ReSharper disable once CheckNamespace

namespace Testably.Expectations;

internal class ThrowsOption
{
	public bool DoCheckThrow { get; private set; } = true;

	public void CheckThrow(bool doCheckThrow)
	{
		DoCheckThrow = doCheckThrow;
	}
}
