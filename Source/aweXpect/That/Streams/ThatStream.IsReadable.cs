using System.IO;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStream
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is readable.
	/// </summary>
	public static AndOrResult<Stream?, IExpectSubject<Stream?>> IsReadable(
		this IExpectSubject<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					"be readable",
					actual => actual?.CanRead == true,
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not readable.
	/// </summary>
	public static AndOrResult<Stream?, IExpectSubject<Stream?>> IsNotReadable(
		this IExpectSubject<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new ValueConstraint(
					"not be readable",
					actual => actual?.CanRead == false,
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}
