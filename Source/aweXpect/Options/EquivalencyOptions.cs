using System.Collections.Generic;
using System.Linq;

namespace aweXpect.Options;

/// <summary>
///     Options for equivalency.
/// </summary>
public class EquivalencyOptions
{
	internal IReadOnlyList<string> MembersToIgnore => _membersToIgnore;
	private readonly List<string> _membersToIgnore = new();

	/// <summary>
	///     Ignores the <paramref name="memberToIgnore" /> when checking for equivalency.
	/// </summary>
	public EquivalencyOptions IgnoringMember(string memberToIgnore)
	{
		_membersToIgnore.Add(memberToIgnore);
		return this;
	}
}
