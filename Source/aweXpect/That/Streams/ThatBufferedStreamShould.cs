#if NET6_0_OR_GREATER
using System;
using System.IO;
using Testably.Expectations.Core.Constraints;

namespace Testably.Expectations;

/// <summary>
///     Expectations on <see cref="BufferedStream" /> values.
/// </summary>
public static partial class ThatBufferedStreamShould
{
	private readonly struct ValueConstraint(
		string it,
		string expectation,
		Func<BufferedStream?, bool> successIf,
		Func<BufferedStream?, string, string> onFailure)
		: IValueConstraint<BufferedStream?>
	{
		public ConstraintResult IsMetBy(BufferedStream? actual)
		{
			if (successIf(actual))
			{
				return new ConstraintResult.Success<BufferedStream?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), onFailure(actual, it));
		}

		public override string ToString()
			=> expectation;
	}
}
#endif
