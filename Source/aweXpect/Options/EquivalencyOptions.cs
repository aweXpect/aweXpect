using System.Collections.Generic;

namespace aweXpect.Options;

/// <summary>
///     Options for equivalency.
/// </summary>
public class EquivalencyOptions
{
	private readonly List<string> _membersToIgnore = new();
	internal IReadOnlyList<string> MembersToIgnore => _membersToIgnore;

	/// <summary>
	///     Ignores the <paramref name="memberToIgnore" /> when checking for equivalency.
	/// </summary>
	public EquivalencyOptions IgnoringMember(string memberToIgnore)
	{
		_membersToIgnore.Add(memberToIgnore);
		return this;
	}
}
