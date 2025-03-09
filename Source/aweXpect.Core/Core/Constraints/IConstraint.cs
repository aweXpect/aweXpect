using System.Text;

namespace aweXpect.Core.Constraints;

/// <summary>
///     Marker interface for a constraint.
/// </summary>
/// <remarks>This is a marker interface.</remarks>
public interface IConstraint
{
	/// <summary>
	///     Appends the expectation to the <paramref name="stringBuilder" />.
	/// </summary>
	void AppendExpectation(StringBuilder stringBuilder, string? indentation = null);
}
