using System.IO;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStream
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> has the <paramref name="expected" /> length.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> HasLength(
		this IThat<Stream?> source,
		long expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					$"have length {expected}",
					actual => actual?.Length == expected,
					actual => actual == null
						? $"{it} was <null>"
						: $"{it} had length {actual.Length}")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> has the <paramref name="expected" /> length.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> DoesNotHaveLength(
		this IThat<Stream?> source,
		long expected)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					$"not have length {expected}",
					actual => actual != null && actual.Length != expected,
					actual => actual == null ? $"{it} was <null>" : $"{it} had")),
			source);
}
