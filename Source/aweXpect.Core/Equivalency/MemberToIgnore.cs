using System;
using System.Reflection;

namespace aweXpect.Equivalency;

/// <summary>
///     Class to specify which members to ignore.
/// </summary>
public abstract class MemberToIgnore
{
	/// <summary>
	///     Checks if the member should be ignored.
	/// </summary>
	public abstract bool IgnoreMember(string memberPath, Type memberType, MemberInfo? memberInfo);

	/// <summary>
	///     Ignores all members that satisfy the <paramref name="predicate" />.
	/// </summary>
	public class ByPredicate(Func<string, Type, MemberInfo?, bool> predicate, string description) : MemberToIgnore
	{
		/// <inheritdoc cref="MemberToIgnore.IgnoreMember(string, Type, MemberInfo?)" />
		public override bool IgnoreMember(string memberPath, Type memberType, MemberInfo? memberInfo)
			=> predicate(memberPath, memberType, memberInfo);

		/// <inheritdoc cref="object.ToString()" />
		public override string ToString() => description;
	}

	/// <summary>
	///     Ignores all members that have the provided <paramref name="memberName" />.
	/// </summary>
	public class ByName(string memberName) : MemberToIgnore
	{
		/// <inheritdoc cref="MemberToIgnore.IgnoreMember(string, Type, MemberInfo?)" />
		public override bool IgnoreMember(string memberPath, Type memberType, MemberInfo? memberInfo)
			=> memberPath.EndsWith(memberName, StringComparison.OrdinalIgnoreCase);

		/// <inheritdoc cref="object.ToString()" />
		public override string ToString() => $"\"{memberName}\"";
	}
}
