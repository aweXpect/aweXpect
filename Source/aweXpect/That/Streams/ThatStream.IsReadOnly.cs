using System.IO;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStream
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is read-only.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> IsReadOnly(
		this IThat<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ValueConstraint(
					"is read-only",
					actual => actual is { CanWrite: false, CanRead: true },
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not read-only.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> IsNotReadOnly(
		this IThat<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars) =>
				new ValueConstraint(
					"is not read-only",
					actual => actual != null && !(actual is { CanWrite: false, CanRead: true }),
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}
