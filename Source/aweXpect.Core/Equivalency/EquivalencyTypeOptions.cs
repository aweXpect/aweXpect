using System.Text;

namespace aweXpect.Equivalency;

/// <summary>
///     Equivalency options for a specific type.
/// </summary>
public record EquivalencyTypeOptions
{
	/// <summary>
	///     The comparison type to use.<br />
	///     If not set (<see langword="null" />), uses the
	///     <see cref="EquivalencyOptions.DefaultComparisonTypeSelector" /> to
	///     determine the <see cref="EquivalencyComparisonType" />.
	/// </summary>
	public EquivalencyComparisonType? ComparisonType { get; init; }

	/// <summary>
	///     The members that should be ignored when checking for equivalency.
	/// </summary>
	public string[] MembersToIgnore { get; init; } = [];

	/// <summary>
	///     Specifies which fields to include in the object comparison.
	/// </summary>
	public IncludeMembers Fields { get; init; } = IncludeMembers.Public;

	/// <summary>
	///     Specifies which properties to include in the object comparison.
	/// </summary>
	public IncludeMembers Properties { get; init; } = IncludeMembers.Public;

	/// <summary>
	///     Ignores the order of collections when checking for equivalency.
	/// </summary>
	public bool IgnoreCollectionOrder { get; init; }

	internal void AppendOptions(StringBuilder sb, string indentation = "")
	{
		sb.Append(indentation).Append(" - include").Append(Fields switch
		{
			IncludeMembers.Public => " public",
			IncludeMembers.Private => " private",
			IncludeMembers.Internal => " internal",
			_ => " no",
		}).Append(" fields and");

		if (Fields != Properties)
		{
			sb.Append(Properties switch
			{
				IncludeMembers.Public => " public",
				IncludeMembers.Private => " private",
				IncludeMembers.Internal => " internal",
				_ => " no",
			});
		}

		sb.AppendLine(" properties");
		if (ComparisonType is not null)
		{
			sb.Append(indentation).Append(" - compare types ");
			sb.AppendLine(ComparisonType switch
			{
				EquivalencyComparisonType.ByMembers => "by members",
				_ => "by value",
			});
		}

		if (IgnoreCollectionOrder)
		{
			sb.Append(indentation).AppendLine(" - ignore collection order");
		}

		if (MembersToIgnore.Length > 0)
		{
			sb.Append(indentation).Append(" - ignore members: ");
			Formatter.Format(sb, MembersToIgnore, FormattingOptions.SingleLine);
			sb.AppendLine();
		}
	}
}
