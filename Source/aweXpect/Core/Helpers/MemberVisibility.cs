#nullable disable
using System;

namespace Testably.Expectations.Core.Helpers;

/// <summary>
///     Determines which members are included in the equivalency constraint
/// </summary>
[Flags]
internal enum MemberVisibilities
{
	None = 0,
	Internal = 1,
	Public = 2,
	ExplicitlyImplemented = 4
}
