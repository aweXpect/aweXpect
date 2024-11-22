using System.IO;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStreamShould
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is seekable.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> BeSeekable(
		this IThat<Stream?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					"be seekable",
					actual => actual?.CanSeek == true,
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not seekable.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> NotBeSeekable(
		this IThat<Stream?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					"not be seekable",
					actual => actual?.CanSeek == false,
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}
