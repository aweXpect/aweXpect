using System.IO;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStreamShould
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> has the <paramref name="expected" /> length.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> HaveLength(
		this IThat<Stream?> source,
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
	public static AndOrResult<Stream?, IThat<Stream?>> NotHaveLength(
		this IThat<Stream?> source,
		long expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					$"not have length {expected}",
					actual => actual != null && actual.Length != expected,
					actual => actual == null ? $"{it} was <null>" : $"{it} had")),
			source);
}
