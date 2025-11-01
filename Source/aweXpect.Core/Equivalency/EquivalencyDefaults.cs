using System;

namespace aweXpect.Equivalency;

/// <summary>
///     Default behaviour to use for equivalency.
/// </summary>
public static class EquivalencyDefaults
{
	/// <summary>
	///     The default selection of the <see cref="EquivalencyComparisonType" /> for
	///     the given <paramref name="type" />.
	/// </summary>
	public static EquivalencyComparisonType DefaultComparisonType(Type type)
	{
		if (type.IsPrimitive
		    || type.IsEnum
		    || type == typeof(string)
		    || type == typeof(decimal)
		    || type == typeof(DateTime)
		    || type == typeof(DateTimeOffset)
		    || type == typeof(TimeSpan)
		    || type == typeof(Guid))
		{
			return EquivalencyComparisonType.ByValue;
		}

		return EquivalencyComparisonType.ByMembers;
	}
}
