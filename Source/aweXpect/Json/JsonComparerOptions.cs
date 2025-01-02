#if NET8_0_OR_GREATER
namespace aweXpect.Json;

/// <summary>
///     Options for strings as JSON.
/// </summary>
public class JsonComparerOptions
{
	/// <summary>
	///     Flag indicating, if subject may have additional members.
	/// </summary>
	public bool IgnoreAdditionalMembers { get; private set; }

	/// <summary>
	///     Ignores additional members in the subject.
	/// </summary>
	public JsonComparerOptions IgnoringAdditionalMembers(bool ignoreAdditionalMembers)
	{
		IgnoreAdditionalMembers = ignoreAdditionalMembers;
		return this;
	}
}
#endif
