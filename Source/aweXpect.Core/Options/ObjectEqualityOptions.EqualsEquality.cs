namespace aweXpect.Options;

public partial class ObjectEqualityOptions
{
	/// <summary>
	///     Compares the objects via <see cref="object.Equals(object, object)" />.
	/// </summary>
	public ObjectEqualityOptions Equals()
	{
		_type = EqualsType;
		return this;
	}

	private sealed class EqualsEquality : IEquality
	{
		#region IEquality Members

		/// <inheritdoc />
		public bool AreConsideredEqual(object? actual, object? expected) => Equals(actual, expected);

		/// <inheritdoc />
		public Result AreConsideredEqual(object? actual, object? expected, string it)
			=> new(Equals(actual, expected),
				() => $"{it} was {Formatter.Format(actual, FormattingOptions.MultipleLines)}");

		/// <inheritdoc />
		public string GetExpectation(string expectedExpression)
			=> $"be equal to {expectedExpression}";

		#endregion
	}
}
