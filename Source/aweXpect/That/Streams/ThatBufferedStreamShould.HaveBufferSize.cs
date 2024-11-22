#if NET6_0_OR_GREATER
using System.IO;
using Testably.Expectations.Core;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatBufferedStreamShould
{
	/// <summary>
	///     Verifies that the subject <see cref="BufferedStream" /> has the <paramref name="expected" /> buffer size.
	/// </summary>
	public static AndOrResult<BufferedStream?, IThat<BufferedStream?>> HaveBufferSize(
		this IThat<BufferedStream?> source,
		int expected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					it,
					$"have buffer size {expected}",
					actual => actual?.BufferSize == expected,
					(a, i) => a == null
						? $"{i} was <null>"
						: $"{i} had buffer size {a.BufferSize}")),
			source);

	/// <summary>
	///     Verifies that the subject <see cref="BufferedStream" /> does not have the <paramref name="unexpected" />
	///     buffer size.
	/// </summary>
	public static AndOrResult<BufferedStream?, IThat<BufferedStream?>>
		NotHaveBufferSize(this IThat<BufferedStream?> source,
			int unexpected)
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new ValueConstraint(
					it,
					$"not have buffer size {unexpected}",
					actual => actual != null && actual.BufferSize != unexpected,
					(a, i) => a == null ? $"{i} was <null>" : $"{i} had")),
			source);
}
#endif
