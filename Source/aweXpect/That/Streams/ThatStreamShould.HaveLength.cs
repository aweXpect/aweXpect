using System.IO;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStreamShould
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> has the <paramref name="expected" /> length.
	/// </summary>
	public static AndOrResult<Stream?, IThatShould<Stream?>> HaveLength(
		this IThatShould<Stream?> source,
		long expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					$"have length {expected}",
					actual => actual?.Length == expected,
					actual => actual == null
						? $"{it} was <null>"
						: $"{it} had length {actual.Length}")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> has the <paramref name="expected" /> length.
	/// </summary>
	public static AndOrResult<Stream?, IThatShould<Stream?>> NotHaveLength(
		this IThatShould<Stream?> source,
		long expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					$"not have length {expected}",
					actual => actual != null && actual.Length != expected,
					actual => actual == null ? $"{it} was <null>" : $"{it} had")),
			source);
}
