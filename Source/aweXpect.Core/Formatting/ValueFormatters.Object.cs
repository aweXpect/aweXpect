using System;
using System.Linq;
using System.Reflection;
using System.Text;
using aweXpect.Core.Helpers;
using MemberVisibilities = aweXpect.Core.Helpers.MemberVisibilities;

namespace aweXpect.Formatting;

public static partial class ValueFormatters
{
	internal static void FormatObject(StringBuilder stringBuilder, object value, FormattingOptions options,
		FormattingContext? context)
	{
		if (value.GetType() == typeof(object))
		{
			stringBuilder.Append($"System.Object (HashCode={value.GetHashCode()})");
		}
		else if (HasCompilerGeneratedToStringImplementation(value))
		{
			context ??= new FormattingContext();
			WriteTypeAndMemberValues(value, stringBuilder, options, context);
		}
		else if (options.UseLineBreaks)
		{
			stringBuilder.Append(value.ToString().Indent(indentFirstLine: false));
		}
		else
		{
			stringBuilder.Append(value);
		}
	}

	/// <summary>
	///     Selects which members of <paramref name="type" /> to format.
	/// </summary>
	/// <param name="type">The <see cref="Type" /> of the object being formatted.</param>
	/// <returns>The members of <paramref name="type" /> that will be included when formatting this object.</returns>
	/// <remarks>The default is all non-private members.</remarks>
	private static MemberInfo[] GetMembers(Type type) => type.GetMembers(MemberVisibilities.Public);

	private static bool HasCompilerGeneratedToStringImplementation(object value)
	{
		Type type = value.GetType();

		return HasDefaultToStringImplementation(value) || type.IsCompilerGenerated();
	}

	private static bool HasDefaultToStringImplementation(object value)
	{
		string? str = value.ToString();

		return str is null || str == value.GetType().ToString();
	}

	/// <summary>
	///     Selects the name to display for <paramref name="type" />.
	/// </summary>
	/// <param name="type">The <see cref="Type" /> of the object being formatted.</param>
	/// <returns>The name to be displayed for <paramref name="type" />.</returns>
	/// <remarks>The default is <see cref="Type.FullName" />.</remarks>
	private static string TypeDisplayName(Type type) => type.Name;

	private static void WriteMemberValues(
		object obj,
		MemberInfo[] members,
		StringBuilder stringBuilder,
		int indentation,
		FormattingOptions options,
		FormattingContext context)
	{
		foreach (MemberInfo? member in members.OrderBy(mi => mi.Name, StringComparer.Ordinal))
		{
			WriteMemberValueTextFor(obj, member, stringBuilder, indentation, options, context);
			if (options.UseLineBreaks)
			{
				stringBuilder.AppendLine(",");
			}
			else
			{
				stringBuilder.Append(", ");
			}
		}

		stringBuilder.Length -= options.UseLineBreaks ? 1 + Environment.NewLine.Length : 2;
	}

	private static void WriteMemberValueTextFor(
		object value,
		MemberInfo member,
		StringBuilder stringBuilder,
		int indentation,
		FormattingOptions options,
		FormattingContext context)
	{
		string? formattedValue;

		try
		{
			if (member is FieldInfo fi)
			{
				formattedValue = Formatter.Format(fi.GetValue(value), options, context);
			}
			else if (member is PropertyInfo pi)
			{
				formattedValue = Formatter.Format(pi.GetValue(value), options, context);
			}
			else
			{
				return;
			}
		}
		catch (Exception ex)
		{
			ex = (ex as TargetInvocationException)?.InnerException ?? ex;
			formattedValue = $"[Member '{member.Name}' threw an exception: '{ex.Message}']";
		}

		stringBuilder.Append($"{new string(' ', indentation)}{member.Name} = ");
		if (options.UseLineBreaks)
		{
			formattedValue = formattedValue.Indent("  ", false);
		}

		stringBuilder.Append(formattedValue);
	}

	private static void WriteTypeAndMemberValues(
		object obj,
		StringBuilder stringBuilder,
		FormattingOptions options,
		FormattingContext context)
	{
		Type type = obj.GetType();
		WriteTypeName(stringBuilder, type);
		WriteTypeValues(obj, stringBuilder, type, options, context);
	}

	private static void WriteTypeName(StringBuilder stringBuilder, Type type)
	{
		string typeName = type.HasFriendlyName() ? TypeDisplayName(type) : string.Empty;
		stringBuilder.Append(typeName);
	}

	private static void WriteTypeValues(
		object obj,
		StringBuilder stringBuilder,
		Type type,
		FormattingOptions options,
		FormattingContext context)
	{
		if (!context.FormattedObjects.Add(obj))
		{
			stringBuilder.Append(" { *recursive* }");
			return;
		}

		MemberInfo[] members = GetMembers(type);
		if (members.Length == 0)
		{
			stringBuilder.Append(" { }");
		}
		else
		{
			stringBuilder.Append(" {");
			stringBuilder.Append(options.UseLineBreaks ? Environment.NewLine : " ");
			WriteMemberValues(obj, members, stringBuilder, options.UseLineBreaks ? 2 : 0, options, context);
			stringBuilder.Append(options.UseLineBreaks ? Environment.NewLine : " ");
			stringBuilder.Append('}');
		}
	}
}
