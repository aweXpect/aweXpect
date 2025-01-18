using System.IO;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStream
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> has the <paramref name="expected" /> position.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> HasPosition(
		this IThat<Stream?> source,
		long expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					$"have position {expected}",
					actual => actual?.Position == expected,
					actual => actual == null
						? $"{it} was <null>"
						: $"{it} had position {actual.Position}")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> has the <paramref name="expected" /> position.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> DoesNotHavePosition(
		this IThat<Stream?> source,
		long expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					$"not have position {expected}",
					actual => actual != null && actual.Position != expected,
					actual => actual == null ? $"{it} was <null>" : $"{it} had")),
			source);
}
