using System.IO;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStream
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is writable.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> IsWritable(
		this IThat<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					"be writable",
					actual => actual?.CanWrite == true,
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not writable.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> IsNotWritable(
		this IThat<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					"not be writable",
					actual => actual?.CanWrite == false,
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}
