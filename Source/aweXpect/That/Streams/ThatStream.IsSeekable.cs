using System.IO;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStream
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is seekable.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> IsSeekable(
		this IThat<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new ValueConstraint(
					"is seekable",
					actual => actual?.CanSeek == true,
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not seekable.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> IsNotSeekable(
		this IThat<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new ValueConstraint(
					"is not seekable",
					actual => actual?.CanSeek == false,
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}
