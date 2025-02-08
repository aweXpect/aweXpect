using System.IO;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStream
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is write-only.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> IsWriteOnly(
		this IThat<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new ValueConstraint(
					"is write-only",
					actual => actual is { CanWrite: true, CanRead: false },
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not write-only.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> IsNotWriteOnly(
		this IThat<Stream?> source)
		=> new(source.ThatIs().ExpectationBuilder.AddConstraint((it, form) =>
				new ValueConstraint(
					"is not write-only",
					actual => actual != null && !(actual is { CanWrite: true, CanRead: false }),
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}
