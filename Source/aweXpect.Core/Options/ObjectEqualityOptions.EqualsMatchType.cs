using aweXpect.Core;

namespace aweXpect.Options;

public partial class ObjectEqualityOptions
{
	/// <summary>
	///     Compares the objects via <see cref="object.Equals(object, object)" />.
	/// </summary>
	public ObjectEqualityOptions Equals()
	{
		_matchType = EqualsMatch;
		return this;
	}

	private sealed class EqualsMatchType : IObjectMatchType
	{
		/// <inheritdoc />
		public override string? ToString() => "";

		#region IEquality Members

		/// <inheritdoc />
		public bool AreConsideredEqual(object? actual, object? expected) => Equals(actual, expected);

		/// <inheritdoc />
		public string GetExpectation(string expected)
			=> $"be equal to {expected}";

		/// <inheritdoc />
		public string GetExtendedFailure(string it, object? actual, object? expected)
			=> $"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}";

		#endregion
	}
}
