using System;

namespace aweXpect.Equivalency;

/// <summary>
///     Extension methods for <see cref="EquivalencyOptions" />.
/// </summary>
public static class EquivalencyOptionsExtensions
{
	/// <summary>
	///     Ignores the <paramref name="memberToIgnore" /> when checking for equivalency.
	/// </summary>
	public static EquivalencyOptions IgnoringMember(this EquivalencyOptions @this, string memberToIgnore)
		=> @this with
		{
			MembersToIgnore = [..@this.MembersToIgnore, memberToIgnore]
		};

	/// <summary>
	///     Ignores the order of collections when checking for equivalency
	///     when <paramref name="ignoreCollectionOrder" /> is <see langword="true" />.
	/// </summary>
	public static EquivalencyOptions IgnoringCollectionOrder(this EquivalencyOptions @this,
		bool ignoreCollectionOrder = true)
		=> @this with
		{
			IgnoreCollectionOrder = ignoreCollectionOrder
		};

	/// <summary>
	///     Creates a new <see cref="EquivalencyOptions" /> instance from the provided <paramref name="callback" />.
	/// </summary>
	/// <remarks>
	///     Uses the default instance, when no <paramref name="callback" /> is given.
	/// </remarks>
	internal static EquivalencyOptions FromCallback(Func<EquivalencyOptions, EquivalencyOptions>? callback)
		=> callback is null
			? new EquivalencyOptions()
			: callback(new EquivalencyOptions());
}
