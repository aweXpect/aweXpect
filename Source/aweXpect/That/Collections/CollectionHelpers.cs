using System;
using System.Collections.Generic;
using System.Linq;
using aweXpect.Core;
using aweXpect.Helpers;

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

	internal static ExpectationBuilder AddCollectionContext<TItem>(this ExpectationBuilder expectationBuilder,
		IEnumerable<TItem>? value, bool isIncomplete = false)
	{
		if (value is null)
		{
			return expectationBuilder;
		}

		return expectationBuilder.UpdateContexts(contexts
			=> contexts
				.Add(new ResultContext("Collection",
					() => Formatter.Format(value, typeof(TItem).GetFormattingOption(value switch
					{
						ICollection<TItem> coll => coll.Count,
						ICountable countable => countable.Count,
						_ => null,
					})).AppendIsIncomplete(isIncomplete),
					-1)));
	}

#if NET8_0_OR_GREATER
	internal static ExpectationBuilder AddCollectionContext<TItem>(this ExpectationBuilder expectationBuilder,
		IMaterializedEnumerable<TItem>? value, bool isIncomplete = false)
	{
		if (value is null)
		{
			return expectationBuilder;
		}

		return expectationBuilder.UpdateContexts(contexts
			=> contexts
				.Add(new ResultContext("Collection",
					() => Formatter.Format(value.MaterializedItems,
							typeof(TItem).GetFormattingOption(value.Count))
						.AppendIsIncomplete(isIncomplete),
					-1)));
	}
#endif

	internal static ExpectationBuilder AddCollectionContext<TKey, TValue>(this ExpectationBuilder expectationBuilder,
		IDictionary<TKey, TValue>? value, bool isIncomplete = false)
	{
		if (value is null)
		{
			return expectationBuilder;
		}

		return expectationBuilder.UpdateContexts(contexts
			=> contexts
				.Add(new ResultContext("Dictionary",
					() => Formatter.Format(value, typeof(TValue).GetFormattingOption(value.Count))
						.AppendIsIncomplete(isIncomplete),
					-2)));
	}

	internal static string AppendIsIncomplete(this string formattedItems, bool isIncomplete)
	{
		if (!isIncomplete || formattedItems.Length < 3)
		{
			return formattedItems;
		}

		if (formattedItems.EndsWith("…]"))
		{
			return $"{formattedItems[..^2]}(… and maybe others)]";
		}

		if (formattedItems.EndsWith($"…{Environment.NewLine}]"))
		{
			return $"""
			        {formattedItems[..^(Environment.NewLine.Length + 2)]}(… and maybe others)
			        ]
			        """;
		}

		if (formattedItems.EndsWith($"{Environment.NewLine}]"))
		{
			return $"""
			        {formattedItems[..^(Environment.NewLine.Length + 1)]},
			          (… and maybe others)
			        ]
			        """;
		}

		return $"""
		        {formattedItems[..^1]}, (… and maybe others)]
		        """;
	}

	internal static FormattingOptions GetFormattingOption(this Type type, int? count)
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
		if (count < 10 && singleLineTypes.Contains(type))
		{
			return FormattingOptions.SingleLine;
		}

		Type? underlyingType = Nullable.GetUnderlyingType(type);

		if (count < 10 && underlyingType != null &&
		    singleLineTypes.Contains(underlyingType))
		{
			return FormattingOptions.SingleLine;
		}

		return FormattingOptions.MultipleLines;
	}
}
