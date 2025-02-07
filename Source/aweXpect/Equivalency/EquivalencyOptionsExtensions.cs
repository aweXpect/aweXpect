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
	public static TEquivalencyOptions IgnoringMember<TEquivalencyOptions>(
		this TEquivalencyOptions @this,
		string memberToIgnore)
		where TEquivalencyOptions : EquivalencyTypeOptions
		=> @this with
		{
			MembersToIgnore = [..@this.MembersToIgnore, memberToIgnore]
		};

	/// <summary>
	///     Includes fields according to the <paramref name="fieldsToInclude" /> parameter.
	/// </summary>
	/// <remarks>
	///     If <paramref name="fieldsToInclude" /> is set to <see cref="IncludeMembers.None" />, fields are excluded from the
	///     comparison.
	/// </remarks>
	public static TEquivalencyOptions IncludingFields<TEquivalencyOptions>(
		this TEquivalencyOptions @this,
		IncludeMembers fieldsToInclude)
		where TEquivalencyOptions : EquivalencyTypeOptions
		=> @this with
		{
			Fields = fieldsToInclude
		};

	/// <summary>
	///     Includes properties according to the <paramref name="propertiesToInclude" /> parameter.
	/// </summary>
	/// <remarks>
	///     If <paramref name="propertiesToInclude" /> is set to <see cref="IncludeMembers.None" />, properties are excluded
	///     from the
	///     comparison.
	/// </remarks>
	public static TEquivalencyOptions IncludingProperties<TEquivalencyOptions>(
		this TEquivalencyOptions @this,
		IncludeMembers propertiesToInclude)
		where TEquivalencyOptions : EquivalencyTypeOptions
		=> @this with
		{
			Properties = propertiesToInclude
		};

	/// <summary>
	///     Ignores the order of collections when checking for equivalency
	///     when <paramref name="ignoreCollectionOrder" /> is <see langword="true" />.
	/// </summary>
	public static TEquivalencyOptions IgnoringCollectionOrder<TEquivalencyOptions>(
		this TEquivalencyOptions @this,
		bool ignoreCollectionOrder = true)
		where TEquivalencyOptions : EquivalencyTypeOptions
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

	/// <summary>
	///     Returns type-specific <see cref="EquivalencyTypeOptions" />.
	/// </summary>
	internal static EquivalencyTypeOptions GetTypeOptions(this EquivalencyOptions @this, Type? type,
		EquivalencyTypeOptions defaultValue)
	{
		if (type != null && @this.CustomOptions.TryGetValue(type, out EquivalencyTypeOptions? customOptions))
		{
			return customOptions;
		}

		return defaultValue;
	}
}
