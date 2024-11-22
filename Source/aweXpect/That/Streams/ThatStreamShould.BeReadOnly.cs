using System.IO;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStreamShould
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is read-only.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> BeReadOnly(
		this IThat<Stream?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					"be read-only",
					actual => actual is { CanWrite: false, CanRead: true },
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not read-only.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> NotBeReadOnly(
		this IThat<Stream?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					"not be read-only",
					actual => actual != null && !(actual is { CanWrite: false, CanRead: true }),
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}
