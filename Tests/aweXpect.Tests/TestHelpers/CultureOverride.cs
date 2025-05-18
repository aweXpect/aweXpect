using System.Globalization;
using System.Threading;

namespace aweXpect.Tests;

internal sealed class CultureOverride : IDisposable
{
	private readonly CultureInfo _originalCulture;

	public CultureOverride(string cultureName)
	{
		_originalCulture = Thread.CurrentThread.CurrentCulture;
		CultureInfo culture = CultureInfo.GetCultureInfo(cultureName);
		Thread.CurrentThread.CurrentCulture = culture;
	}

	public void Dispose()
		=> Thread.CurrentThread.CurrentCulture = _originalCulture;
}
