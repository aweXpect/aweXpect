using System;
using System.Collections.Generic;
using System.Linq;

namespace aweXpect;

internal static class CollectionHelpers
{
	internal static string CreateDuplicateFailureMessage<TItem>(string it, List<TItem> duplicates)
	{
		StringBuilder sb = new();
		sb.Append(it).Append(" contained ");
		if (duplicates.Count == 1)
		{
			sb.Append("1 duplicate:");
		}
		else
		{
			sb.Append(duplicates.Count).Append(" duplicates:");
		}

		foreach (TItem duplicate in duplicates)
		{
			sb.AppendLine();
			sb.Append("  ");
			Formatter.Format(sb, duplicate);
			sb.Append(',');
		}

		sb.Length--;
		string failure = sb.ToString();
		return failure;
	}

	internal static string GetItemString(this EnumerableQuantifier quantifier)
		=> quantifier.IsSingle() ? "item" : "items";

	internal static FormattingOptions GetFormattingOption(this Type type)
	{
		Type[] singleLineTypes =
		[
			typeof(char),
			typeof(byte),
			typeof(sbyte),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(short),
			typeof(ushort),
#if NET8_0_OR_GREATER
			typeof(Int128),
			typeof(UInt128),
			typeof(Half),
#endif
		];
		if (singleLineTypes.Contains(type))
		{
			return FormattingOptions.SingleLine;
		}

		Type? underlyingType = Nullable.GetUnderlyingType(type);

		if (underlyingType != null &&
		    singleLineTypes.Contains(underlyingType))
		{
			return FormattingOptions.SingleLine;
		}

		return FormattingOptions.MultipleLines;
	}
}
