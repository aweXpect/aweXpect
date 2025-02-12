using System;
using System.Linq;
using System.Reflection;
using System.Text;
using aweXpect.Core.Helpers;

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

	private static MemberInfo[] GetMembers(Type type) => [..type.GetFields(), ..type.GetProperties()];

	private static bool HasCompilerGeneratedToStringImplementation(object value)
	{
		Type type = value.GetType();

		return HasDefaultToStringImplementation(value);
	}

	private static bool HasDefaultToStringImplementation(object value)
	{
		string? str = value.ToString();

		return str is null || str == value.GetType().ToString();
	}

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
				stringBuilder.AppendLine(",").Append(options.Indentation);
			}
			else
			{
				stringBuilder.Append(", ");
			}
		}

		stringBuilder.Length -= options.UseLineBreaks ? 1 + Environment.NewLine.Length + options.Indentation.Length : 2;
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
		Formatter.Format(stringBuilder, type);
		WriteTypeValues(obj, stringBuilder, type, options, context);
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
			AppendLineWithIndentationOrBlank(stringBuilder, options);
			WriteMemberValues(obj, members, stringBuilder, options.UseLineBreaks ? 2 : 0, options, context);
			AppendLineWithIndentationOrBlank(stringBuilder, options);
			stringBuilder.Append('}');
		}
	}

	private static void AppendLineWithIndentationOrBlank(StringBuilder stringBuilder, FormattingOptions options)
	{
		if (options.UseLineBreaks)
		{
			stringBuilder.AppendLine().Append(options.Indentation);
		}
		else
		{
			stringBuilder.Append(' ');
		}
	}
}
