namespace aweXpect.Core.Initialization;

/// <summary>
///     aweXpect will call <see cref="IAweXpectInitializer.Initialize()" /> on all classes that implement this interface
///     before the first expectation is executed.
/// </summary>
public interface IAweXpectInitializer
{
	/// <summary>
	///     Can be used to initialize and customize aweXpect.
	/// </summary>
	void Initialize();
}
