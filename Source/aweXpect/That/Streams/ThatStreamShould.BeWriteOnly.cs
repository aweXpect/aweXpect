using System.IO;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStreamShould
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is write-only.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> BeWriteOnly(
		this IThat<Stream?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					"be write-only",
					actual => actual is { CanWrite: true, CanRead: false },
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not write-only.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> NotBeWriteOnly(
		this IThat<Stream?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					"not be write-only",
					actual => actual != null && !(actual is { CanWrite: true, CanRead: false }),
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}
