using System.IO;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStreamShould
{
	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is readable.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> BeReadable(
		this IThat<Stream?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					"be readable",
					actual => actual?.CanRead == true,
					actual => actual == null ? $"{it} was <null>" : $"{it} was not")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="Stream" /> is not readable.
	/// </summary>
	public static AndOrResult<Stream?, IThat<Stream?>> NotBeReadable(
		this IThat<Stream?> source)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					"not be readable",
					actual => actual?.CanRead == false,
					actual => actual == null ? $"{it} was <null>" : $"{it} was")),
			source);
}
