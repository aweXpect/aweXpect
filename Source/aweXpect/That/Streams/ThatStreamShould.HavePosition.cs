using System.IO;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStreamShould
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> has the <paramref name="expected" /> position.
	/// </summary>
	public static AndOrResult<Stream?, IThatShould<Stream?>> HavePosition(
		this IThatShould<Stream?> source,
		long expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					$"have position {expected}",
					actual => actual?.Position == expected,
					actual => actual == null
						? $"{it} was <null>"
						: $"{it} had position {actual.Position}")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> has the <paramref name="expected" /> position.
	/// </summary>
	public static AndOrResult<Stream?, IThatShould<Stream?>> NotHavePosition(
		this IThatShould<Stream?> source,
		long expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					$"not have position {expected}",
					actual => actual != null && actual.Position != expected,
					actual => actual == null ? $"{it} was <null>" : $"{it} had")),
			source);
}
