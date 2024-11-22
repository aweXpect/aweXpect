using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Testably.Expectations.Core.Helpers;

internal static class TypeExtensions
{
	private static readonly ConcurrentDictionary<Type, bool> TypeIsCompilerGeneratedCache = new();

	private static readonly ConcurrentDictionary<Type, bool> TypeIsRecordCache = new();

	private static readonly ConcurrentDictionary<(Type Type, MemberVisibilities Visibility),
			TypeMemberReflector>
		TypeMemberReflectorsCache = new();

	public static MemberInfo[] GetMembers(this Type typeToReflect, MemberVisibilities visibility)
	{
		return GetTypeReflectorFor(typeToReflect, visibility).Members;
	}

	/// <summary>
	///     Check if the type has a human-readable name.
	/// </summary>
	/// <returns>false for compiler generated type names, otherwise true.</returns>
	public static bool HasFriendlyName(this Type type)
	{
		return !type.IsAnonymous() && !type.IsTuple();
	}

	public static bool IsCompilerGenerated(this Type type)
	{
		return TypeIsCompilerGeneratedCache.GetOrAdd(type, static t =>
			t.IsRecord() ||
			t.IsAnonymous() ||
			t.IsTuple());
	}

	public static bool IsDecoratedWith<TAttribute>(this Type type)
		where TAttribute : Attribute
	{
		return type.IsDefined(typeof(TAttribute), inherit: false);
	}

	public static bool IsDecoratedWith<TAttribute>(this MemberInfo type)
		where TAttribute : Attribute
	{
		// Do not use MemberInfo.IsDefined
		// There is an issue with PropertyInfo and EventInfo preventing the inherit option to work.
		// https://github.com/dotnet/runtime/issues/30219
		return Attribute.IsDefined(type, typeof(TAttribute), inherit: false);
	}

	public static bool IsIndexer(this PropertyInfo member)
	{
		return member.GetIndexParameters().Length != 0;
	}

	public static bool IsRecord(this Type type)
	{
		return TypeIsRecordCache.GetOrAdd(type,
			static t => t.IsRecordClass() || t.IsRecordStruct());
	}

	private static TypeMemberReflector GetTypeReflectorFor(Type typeToReflect,
		MemberVisibilities visibility)
	{
		return TypeMemberReflectorsCache.GetOrAdd((typeToReflect, visibility),
			static key => new TypeMemberReflector(key.Type, key.Visibility));
	}

	private static bool IsAnonymous(this Type type)
	{
		bool nameContainsAnonymousType =
			type.FullName?.Contains("AnonymousType", StringComparison.Ordinal) == true;

		if (!nameContainsAnonymousType)
		{
			return false;
		}

		bool hasCompilerGeneratedAttribute =
			type.IsDecoratedWith<CompilerGeneratedAttribute>();

		return hasCompilerGeneratedAttribute;
	}

	private static bool IsRecordClass(this Type type)
	{
		return type.GetMethod("<Clone>$",
				       BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly) is
			       { } &&
		       type.GetProperty("EqualityContract",
				       BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)?
			       .GetMethod?.IsDecoratedWith<CompilerGeneratedAttribute>() == true;
	}

	private static bool IsRecordStruct(this Type type)
	{
		// As noted here: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/record-structs#open-questions
		// recognizing record structs from metadata is an open point. The following check is based on common sense
		// and heuristic testing, apparently giving good results but not supported by official documentation.
		return type.BaseType == typeof(ValueType) &&
		       type.GetMethod("PrintMembers",
			       BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null,
			       [typeof(StringBuilder)], null) is { } &&
		       type.GetMethod("op_Equality",
				       BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly, null,
				       [type, type], null)?
			       .IsDecoratedWith<CompilerGeneratedAttribute>() == true;
	}

	private static bool IsTuple(this Type type)
	{
		if (!type.IsGenericType)
		{
			return false;
		}

#if !(NET47 || NETSTANDARD2_0)
		return typeof(ITuple).IsAssignableFrom(type);
#else
		Type openType = type.GetGenericTypeDefinition();

		return openType == typeof(ValueTuple<>)
		       || openType == typeof(ValueTuple<,>)
		       || openType == typeof(ValueTuple<,,>)
		       || openType == typeof(ValueTuple<,,,>)
		       || openType == typeof(ValueTuple<,,,,>)
		       || openType == typeof(ValueTuple<,,,,,>)
		       || openType == typeof(ValueTuple<,,,,,,>)
		       || (openType == typeof(ValueTuple<,,,,,,,>) &&
		           IsTuple(type.GetGenericArguments()[7]))
		       || openType == typeof(Tuple<>)
		       || openType == typeof(Tuple<,>)
		       || openType == typeof(Tuple<,,>)
		       || openType == typeof(Tuple<,,,>)
		       || openType == typeof(Tuple<,,,,>)
		       || openType == typeof(Tuple<,,,,,>)
		       || openType == typeof(Tuple<,,,,,,>)
		       || (openType == typeof(Tuple<,,,,,,,>) && IsTuple(type.GetGenericArguments()[7]));
#endif
	}
}
