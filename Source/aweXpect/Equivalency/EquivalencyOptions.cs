using System;
using System.Collections.Generic;

namespace aweXpect.Equivalency;

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
	///     Ignores the order of collections when checking for equivalency.
	/// </summary>
	public bool IgnoreCollectionOrder { get; private set; }

	/// <summary>
	///     Ignores the <paramref name="memberToIgnore" /> when checking for equivalency.
	/// </summary>
	public EquivalencyOptions IgnoringMember(string memberToIgnore)
	{
		_membersToIgnore.Add(memberToIgnore);
		return this;
	}

	/// <summary>
	///     Ignores the order of collections when checking for equivalency
	///     when <paramref name="ignoreCollectionOrder" /> is <see langword="true" />.
	/// </summary>
	public EquivalencyOptions IgnoringCollectionOrder(bool ignoreCollectionOrder = true)
	{
		IgnoreCollectionOrder = ignoreCollectionOrder;
		return this;
	}

	/// <summary>
	///     Creates a new <see cref="EquivalencyOptions" /> instance from the provided <paramref name="callback" />.
	/// </summary>
	/// <remarks>
	///     Uses the default instance, when no <paramref name="callback" /> is given.
	/// </remarks>
	public static EquivalencyOptions FromCallback(Func<EquivalencyOptions, EquivalencyOptions>? callback)
		=> callback is null
			? new EquivalencyOptions()
			: callback(new EquivalencyOptions());
}
