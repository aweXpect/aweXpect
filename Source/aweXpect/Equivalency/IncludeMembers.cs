using System;

namespace aweXpect.Equivalency;

/// <summary>
///     Specifies which members to include in the object comparison.
/// </summary>
[Flags]
public enum IncludeMembers
{
	/// <summary>
	///     No members should be included in the object comparison.
	/// </summary>
	None = 0,

	/// <summary>
	///     Public members should be included in the object comparison.
	/// </summary>
	Public = 1 << 1,

	/// <summary>
	///     Internal members should be included in the object comparison.
	/// </summary>
	Internal = 1 << 2,

	/// <summary>
	///     Private members should be included in the object comparison.
	/// </summary>
	Private = 1 << 3,
}
