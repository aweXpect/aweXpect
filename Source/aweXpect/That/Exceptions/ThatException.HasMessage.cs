using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatException
{
	/// <summary>
	///     Verifies that the actual exception has a message equal to <paramref name="expected" />
	/// </summary>
	public static StringEqualityTypeResult<Exception?, IThat<Exception?>> HasMessage(
		this IThat<Exception?> source,
		string expected)
	{
		StringEqualityOptions options = new();
		return new StringEqualityTypeResult<Exception?, IThat<Exception?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new HasMessageValueConstraint<Exception>(
					it, grammar, expected, options)),
			source,
			options);
	}
}
