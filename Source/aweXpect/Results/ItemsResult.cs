namespace aweXpect.Results;

/// <summary>
///     The result for counting items in a collection.
/// </summary>
public class ItemsResult<TReturn>(TReturn value)
{
	/// <summary>
	///     The number of items in a collection.
	/// </summary>
	public TReturn Items() => value;
}
