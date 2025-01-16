using System.IO;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStreamShould
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is seekable.
	/// </summary>
	public static AndOrResult<Stream?, IThatShould<Stream?>> BeSeekable(
		this IThatShould<Stream?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					"be seekable",
					actual => actual?.CanSeek == true,
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not seekable.
	/// </summary>
	public static AndOrResult<Stream?, IThatShould<Stream?>> NotBeSeekable(
		this IThatShould<Stream?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					"not be seekable",
					actual => actual?.CanSeek == false,
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}
