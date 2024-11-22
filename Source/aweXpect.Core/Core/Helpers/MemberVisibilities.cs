#nullable disable
using System;

namespace aweXpect.Core.Helpers;

/// <summary>
///     Determines which members are included in the equivalency constraint
/// </summary>
[Flags]
public enum MemberVisibilities
{
	None = 0,
	Internal = 1,
	Public = 2,
	ExplicitlyImplemented = 4
}
