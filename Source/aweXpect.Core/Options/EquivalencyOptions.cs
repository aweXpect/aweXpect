using System.Collections.Generic;

namespace aweXpect.Options;

/// <summary>
///     Options for equivalency.
/// </summary>
public class EquivalencyOptions
{
	private readonly List<string> _membersToIgnore = new();

	/// <summary>
	///     The members that should be ignored when checking for equivalency.
	/// </summary>
	public IReadOnlyList<string> MembersToIgnore => _membersToIgnore;

	/// <summary>
	///     Ignores the <paramref name="memberToIgnore" /> when checking for equivalency.
	/// </summary>
	public EquivalencyOptions IgnoringMember(string memberToIgnore)
	{
		_membersToIgnore.Add(memberToIgnore);
		return this;
	}
}
