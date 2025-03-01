using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aweXpect.Equivalency;

internal static class IncludeMembersExtensions
{
	public static BindingFlags GetBindingFlags(this IncludeMembers includeMembers)
	{
		if (includeMembers == IncludeMembers.Public)
		{
			return BindingFlags.Public | BindingFlags.Instance;
		}

		return BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
	}

	public static IEnumerable<FieldInfo> GetFields(this Type type, IncludeMembers includeMembers)
	{
		if (includeMembers == IncludeMembers.None)
		{
			yield break;
		}

		BindingFlags bindingFlags = GetBindingFlags(includeMembers);
		foreach (FieldInfo field in type.GetFields(bindingFlags))
		{
			if ((includeMembers.HasFlag(IncludeMembers.Internal) && !field.IsAssembly) ||
			    (includeMembers.HasFlag(IncludeMembers.Public) && !field.IsPublic) ||
			    (includeMembers.HasFlag(IncludeMembers.Private) && !field.IsPrivate))
			{
				continue;
			}

			yield return field;
		}
	}

	public static IEnumerable<PropertyInfo> GetProperties(this Type type, IncludeMembers includeMembers)
	{
		if (includeMembers == IncludeMembers.None)
		{
			yield break;
		}

		BindingFlags bindingFlags = GetBindingFlags(includeMembers);
		foreach (PropertyInfo property in type.GetProperties(bindingFlags).Where(x => x.CanRead))
		{
			MethodInfo getter = property.GetAccessors(true)[0];
			if (!getter.Name.StartsWith("get_", StringComparison.Ordinal) ||
			    (includeMembers.HasFlag(IncludeMembers.Internal) && !getter.IsAssembly) ||
			    (includeMembers.HasFlag(IncludeMembers.Public) && !getter.IsPublic) ||
			    (includeMembers.HasFlag(IncludeMembers.Private) && !getter.IsPrivate))
			{
				continue;
			}

			yield return property;
		}
	}
}
