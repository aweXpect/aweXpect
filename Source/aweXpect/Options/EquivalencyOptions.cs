using System.Collections.Generic;
using System.Linq;

namespace Testably.Expectations.Options;

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

	/// <inheritdoc />
	public override string ToString()
	{
		return _membersToIgnore.Count > 0
			? $" ignoring [{string.Join(", ", _membersToIgnore.Select<string, string>(m => Formatter.Format(m)))}]"
			: "";
	}
}
