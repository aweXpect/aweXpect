using System;
using System.IO;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="Stream" /> values.
/// </summary>
public static partial class ThatStream
{
	private readonly struct ValueConstraint(
		string expectation,
		Func<Stream?, bool> successIf,
		Func<Stream?, string> onFailure)
		: IValueConstraint<Stream?>
	{
		public ConstraintResult IsMetBy(Stream? actual)
		{
			if (successIf(actual))
			{
				return new ConstraintResult.Success<Stream?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), onFailure(actual));
		}

		public override string ToString()
			=> expectation;
	}
}
