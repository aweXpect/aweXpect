using System;
using System.Collections.Generic;
using System.Text;

namespace aweXpect.Equivalency;

/// <summary>
///     Options for equivalency.
/// </summary>
public record EquivalencyOptions : EquivalencyTypeOptions
{
	private readonly Func<Type, EquivalencyComparisonType>? _defaultComparisonTypeSelector;

	/// <summary>
	///     Specifies the selector how types should be compared, if not overwritten in the <see cref="CustomOptions" />.
	/// </summary>
	/// <remarks>
	///     Defaults to use the <see cref="EquivalencyDefaults.DefaultComparisonType" />.
	/// </remarks>
	public Func<Type, EquivalencyComparisonType> DefaultComparisonTypeSelector
	{
		get => _defaultComparisonTypeSelector ?? EquivalencyDefaults.DefaultComparisonType;
		init => _defaultComparisonTypeSelector = value;
	}

	/// <summary>
	///     Custom type-specific equivalency options.
	/// </summary>
	public Dictionary<Type, EquivalencyTypeOptions> CustomOptions { get; init; } = new();

	/// <summary>
	///     Specifies the <paramref name="options" /> for members of type <typeparamref name="TMember" />.
	/// </summary>
	public EquivalencyOptions For<TMember>(
		Func<EquivalencyTypeOptions, EquivalencyTypeOptions> options)
	{
		EquivalencyTypeOptions typeOptions = options(this);
		CustomOptions.Add(typeof(TMember), typeOptions);
		return this;
	}

	/// <inheritdoc />
	public override string ToString()
	{
		StringBuilder? sb = new();
		AppendOptions(sb);
		foreach (KeyValuePair<Type, EquivalencyTypeOptions> customOption in CustomOptions)
		{
			sb.Append(" - for ");
			Formatter.Format(sb, customOption.Key);
			sb.AppendLine(":");
			customOption.Value.AppendOptions(sb, "  ");
		}

		return sb.ToString().TrimEnd();
	}
}

/// <summary>
///     Options for equivalency for expected type <typeparamref name="TExpected" />.
/// </summary>
public record EquivalencyOptions<TExpected> : EquivalencyOptions
{
	/// <summary>
	///     Initializes the values with the <paramref name="inner" /> equivalency options.
	/// </summary>
	public EquivalencyOptions(EquivalencyOptions inner)
	{
		MembersToIgnore = inner.MembersToIgnore;
		IgnoreCollectionOrder = inner.IgnoreCollectionOrder;
		DefaultComparisonTypeSelector = inner.DefaultComparisonTypeSelector;
	}

	/// <summary>
	///     Specifies the <paramref name="options" /> for members of type <typeparamref name="TMember" />.
	/// </summary>
	public new EquivalencyOptions<TExpected> For<TMember>(
		Func<EquivalencyTypeOptions, EquivalencyTypeOptions> options)
	{
		base.For<TMember>(options);
		return this;
	}

	/// <inheritdoc />
	public override string ToString() => base.ToString();
}
