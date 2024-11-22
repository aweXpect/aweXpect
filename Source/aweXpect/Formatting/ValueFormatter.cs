using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Testably.Expectations.Core.Helpers;
using MemberVisibilities = Testably.Expectations.Core.Helpers.MemberVisibilities;

namespace Testably.Expectations.Formatting;

/// <summary>
///     Formatter for arbitrary objects in exception messages.
/// </summary>
public class ValueFormatter
{
	/// <summary>
	///     The default string representation of <see langword="null" />.
	/// </summary>
	public static readonly string NullString = "<null>";

	internal ValueFormatter() { }

	internal void FormatObject(StringBuilder stringBuilder, object value, FormattingOptions options)
	{
		if (value.GetType() == typeof(object))
		{
			stringBuilder.Append($"System.Object (HashCode={value.GetHashCode()})");
		}
		else if (HasCompilerGeneratedToStringImplementation(value))
		{
			WriteTypeAndMemberValues(value, stringBuilder, options);
		}
		else if (options.UseLineBreaks)
		{
			stringBuilder.AppendLine(value.ToString().Indent(indentFirstLine: false));
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
	private MemberInfo[] GetMembers(Type type)
	{
		return type.GetMembers(MemberVisibilities.Public);
	}

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
	private string TypeDisplayName(Type type) => type.Name;

	private static void WriteMemberValues(object obj, MemberInfo[] members,
		StringBuilder stringBuilder, int indentation, FormattingOptions options)
	{
		foreach (MemberInfo? member in members.OrderBy(mi => mi.Name, StringComparer.Ordinal))
		{
			WriteMemberValueTextFor(obj, member, stringBuilder, indentation, options);
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

	private static void WriteMemberValueTextFor(object value, MemberInfo member,
		StringBuilder stringBuilder, int indentation, FormattingOptions options)
	{
		string? formattedValue;

		try
		{
			if (member is FieldInfo fi)
			{
				formattedValue = Formatter.Format(fi.GetValue(value), options);
			}
			else if (member is PropertyInfo pi)
			{
				formattedValue = Formatter.Format(pi.GetValue(value), options);
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

	private void WriteTypeAndMemberValues(object obj, StringBuilder stringBuilder,
		FormattingOptions options)
	{
		Type type = obj.GetType();
		WriteTypeName(stringBuilder, type);
		WriteTypeValue(obj, stringBuilder, type, options);
	}

	private void WriteTypeName(StringBuilder stringBuilder, Type type)
	{
		string typeName = type.HasFriendlyName() ? TypeDisplayName(type) : string.Empty;
		stringBuilder.Append(typeName);
	}

	private void WriteTypeValue(object obj, StringBuilder stringBuilder, Type type,
		FormattingOptions options)
	{
		MemberInfo[] members = GetMembers(type);
		if (members.Length == 0)
		{
			stringBuilder.Append(" { }");
		}
		else
		{
			stringBuilder.Append(" {");
			stringBuilder.Append(options.UseLineBreaks ? Environment.NewLine : " ");
			WriteMemberValues(obj, members, stringBuilder, options.UseLineBreaks ? 2 : 0, options);
			stringBuilder.Append(options.UseLineBreaks ? Environment.NewLine : " ");
			stringBuilder.Append('}');
		}
	}
}
